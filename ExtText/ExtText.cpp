#include "ExtText.h"

using namespace std;

void __stdcall ExtText(
    const wchar_t * ModuleName,
    struct Hooker * Hookers,
    int HookersNum,
    int MainThreadId,
    unsigned char * ExtBuffer,
    int ExtBufferSize,
    bool * StopExtText
)
{
    unsigned long pid; //pid of debugged process
    HANDLE hProc = 0; //process handler of debugged process
    unsigned long dMainThreadId; //main thread id of debugged process
    void * BaseAddr; //module base addr that always be changed by alsr
    
    unsigned char * OriginalCodes = new unsigned char[HookersNum]; //backup the original code for recovering after break point error
    
    struct onSentenceEndArgsStruct seas;
    Racer racer(WAIT_MILLISECOND, onSentenceEnd, (void *)&seas);

    try
    {
        for(pid = 0; pid == 0; pid = GetPidByName(ModuleName))
        {
            if(*StopExtText) return;
            Sleep(500);
        }

        hProc = OpenProcess(PROCESS_ALL_ACCESS, false, pid);
        if(!hProc) throw runtime_error("failed to get process handle");

        dMainThreadId = GetMtid(pid);

        BaseAddr = GetBaseAddr(pid);

        for(int i = 0; i < HookersNum; i++)
        {
            Hookers[i].Addr = (void *)((unsigned long)BaseAddr + (unsigned long)Hookers[i].Addr);
            if(!ReadProcessMemory(hProc, Hookers[i].Addr, &OriginalCodes[i], sizeof(char), NULL))
                throw runtime_error("failed to read origin code");
        }

        for(int i = 0; i < HookersNum; i++)
        {
            if(!WriteProcessMemory(hProc, Hookers[i].Addr, &int3, sizeof(char), NULL))
                throw runtime_error("failed to set int3 break point");
        }

        if(!DebugActiveProcess(pid)) throw runtime_error("failed to enter debug mode");

        CONTEXT ctx;
        DEBUG_EVENT dbe;
        void * pValue = 0;
        void * SingleStepAddrBefore = 0;
        seas.MainThreadId = MainThreadId;
        seas.ExtBuffer = ExtBuffer;
        seas.ExtBufferSize = ExtBufferSize;
        seas.BytesLen = 0;
        for(;;)
        {
            //process control
            if(*StopExtText)
            {
                DebugActiveProcessStop(pid);
                for(int i = 0; i < HookersNum; i++)
                    WriteProcessMemory(hProc, Hookers[i].Addr, &OriginalCodes[i], sizeof(char), NULL);
                delete OriginalCodes;
                break;
            }
            
            //wait for debug event
            if(!WaitForDebugEvent(&dbe, DEBUG_LOOP_MAX_WAIT)) continue;

            //if the debugged program exit, then we exit too
            if(dbe.dwDebugEventCode == EXIT_PROCESS_DEBUG_EVENT)
            {
                DebugActiveProcessStop(pid);
                delete OriginalCodes;
                PostThreadMessage(MainThreadId, WM_EXT_TEXT_EXIT_PASSIVE, NULL, NULL);
                break;
            }

            //first filter
            if(dbe.dwThreadId != dMainThreadId || dbe.dwDebugEventCode != EXCEPTION_DEBUG_EVENT)
            {
                ContinueDebugEvent(dbe.dwProcessId, dbe.dwThreadId, DBG_EXCEPTION_NOT_HANDLED);
            }
            //int3 break
            else if(dbe.u.Exception.ExceptionRecord.ExceptionCode == EXCEPTION_BREAKPOINT)
            {
                HANDLE hThread = OpenThread(THREAD_ALL_ACCESS, false, dMainThreadId);
                if(!hThread) throw runtime_error("failed to get thread handle");
                ctx.ContextFlags = CONTEXT_FULL;
                if(!GetThreadContext(hThread, &ctx)) throw runtime_error("failed to get thread context");

                if(seas.BytesLen > ExtBufferSize - HookersNum * 2) seas.BytesLen = 0; //impact resistant

                int LastIdx = -1;
                for(int i = 0; i < HookersNum; i++)
                {
                    if(Hookers[i].Addr != dbe.u.Exception.ExceptionRecord.ExceptionAddress) continue;

                    LastIdx = i;

                    if(!racer.status) racer.start();

                    if(Hookers[i].EspBias < 0 && !Hookers[i].ValueAsAddr)
                    {
                        pValue = (char *)&ctx.Esp + Hookers[i].EspBias;
                        for(int c = 0; c < Hookers[i].BytesPerRead; c++)
                        {
                            *(ExtBuffer + seas.BytesLen++) = *((char *)((unsigned long)pValue + Hookers[i].BytesPerRead - c - 1));
                        }
                    }
                    else
                    {
                        if(Hookers[i].EspBias < 0)
                        {
                            pValue = *(void **)((char *)&ctx.Esp + Hookers[i].EspBias);
                        }
                        else if(!Hookers[i].ValueAsAddr)
                        {
                            pValue = (void *)(ctx.Esp + Hookers[i].EspBias);
                        }
                        else
                        {
                            if(!ReadProcessMemory(hProc, (void *)(ctx.Esp + Hookers[i].EspBias), pValue, sizeof(void *), NULL))
                                throw runtime_error("failed to read bytes");
                        }
                        for(int c = 0; c < Hookers[i].BytesPerRead; c++)
                        {
                            if(!ReadProcessMemory(hProc, (void *)((char *)pValue + Hookers[i].ValueAsAddrBias + c), ExtBuffer + seas.BytesLen++, sizeof(char), NULL))
                                throw runtime_error("failed to read bytes");
                        }
                    }

                    racer.reset();
                }
                if(LastIdx == -1)
                {
                    ContinueDebugEvent(dbe.dwProcessId, dbe.dwThreadId, DBG_EXCEPTION_NOT_HANDLED);
                }
                else
                {
                    //recover the original code
                    if(!WriteProcessMemory(hProc, Hookers[LastIdx].Addr, &OriginalCodes[LastIdx], sizeof(char), NULL))
                        throw runtime_error("failed to recover original code");

                    //reset the process pointer and set to sigle-step-mode to arouse second break
                    ctx.ContextFlags = CONTEXT_CONTROL;
                    if(!GetThreadContext(hThread, &ctx))
                        throw runtime_error("failed to get thread content");
                    ctx.Eip = (unsigned long)Hookers[LastIdx].Addr;
                    ctx.EFlags |= 0x100; //set the TF position as 1 in EFlags register to start sigle-step-mode
                    ctx.ContextFlags = CONTEXT_CONTROL;
                    if(!SetThreadContext(hThread, &ctx))
                        throw runtime_error("failed to set thread content");

                    SingleStepAddrBefore = Hookers[LastIdx].Addr;

                    ContinueDebugEvent(dbe.dwProcessId, dbe.dwThreadId, DBG_CONTINUE);
                }
            }
            //second break, reset the breakpoint
            else if(dbe.u.Exception.ExceptionRecord.ExceptionCode == EXCEPTION_SINGLE_STEP)
            {
                if(SingleStepAddrBefore == 0)
                {
                    ContinueDebugEvent(dbe.dwProcessId, dbe.dwThreadId, DBG_EXCEPTION_NOT_HANDLED);
                }
                else
                {
                    if(!WriteProcessMemory(hProc, SingleStepAddrBefore, &int3, sizeof(BYTE), NULL))
                        throw runtime_error("failed to reset the int3 breakpoint");
                    ContinueDebugEvent(dbe.dwProcessId, dbe.dwThreadId, DBG_CONTINUE);
                    SingleStepAddrBefore = 0;
                }
            }
            else
            {
                ContinueDebugEvent(dbe.dwProcessId, dbe.dwThreadId, DBG_EXCEPTION_NOT_HANDLED);
            }
        }
    }
    catch(runtime_error e)
    {
        DebugActiveProcessStop(pid);
        delete OriginalCodes;
        racer.stop();
        for(int i = 0; i < HookersNum; i++)
            WriteProcessMemory(hProc, Hookers[i].Addr, &OriginalCodes, sizeof(char), NULL);
        PostThreadMessage(MainThreadId, WM_EXT_TEXT_EXIT_ERROR, NULL, NULL);
        return;
    }
}

void onSentenceEnd(void * p)
{
    struct onSentenceEndArgsStruct * pArgs = (struct onSentenceEndArgsStruct *)p;
    if(!pArgs->BytesLen) return;
    for(int i = pArgs->BytesLen; i < pArgs->ExtBufferSize; i++) pArgs->ExtBuffer[i] = 0;
    pArgs->BytesLen = 0;
    PostThreadMessage(pArgs-> MainThreadId, WM_EXT_TEXT_EXT, NULL, NULL);
}
