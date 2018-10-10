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
    unsigned long pid = 0; //pid of debugged process
    HANDLE hProc = 0; //process handler of debugged process
    unsigned long dMainThreadId = 0; //main thread id of debugged process
    void * BaseAddr = 0; //module base addr that always be changed by alsr
    unsigned char * OriginalCodes = new unsigned char[HookersNum]; //backup the original code for recovering after break point error
    struct onSentenceEndArgsStruct seas;
    Racer racer(WAIT_MILLISECOND, onSentenceEnd, (void *)&seas);

    try
    {
        for(; pid == 0; pid = GetPidByName(ModuleName))
        {
            if(*StopExtText) return;
            Sleep(500);
        }

        PostThreadMessage(MainThreadId, WM_EXT_TEXT_GET_PID_SUCCESS, NULL, NULL);

        hProc = OpenProcess(PROCESS_ALL_ACCESS, false, pid);
        if(!hProc) throw WM_EXT_TEXT_GET_HPROC_FAILED;

        try { BaseAddr = GetBaseAddr(pid); }
        catch(exception) { throw WM_EXT_TEXT_GET_BASE_FAILED; }

        for(int i = 0; i < HookersNum; i++)
        {
            Hookers[i].Addr = (void *)((unsigned long)BaseAddr + (unsigned long)Hookers[i].Addr);
            if(!ReadProcessMemory(hProc, Hookers[i].Addr, &OriginalCodes[i], sizeof(char), NULL))
                throw WM_EXT_TEXT_GET_CODE_FAILED;
        }

        for(int i = 0; i < HookersNum; i++)
        {
            if(!WriteProcessMemory(hProc, Hookers[i].Addr, &int3, sizeof(char), NULL))
                throw WM_EXT_TEXT_SET_CODE_FAILED;
        }

        CONTEXT ctx;
        DEBUG_EVENT dbe;
        void * pValue = 0;
        void * SingleStepAddrBefore = 0;
        seas.MainThreadId = MainThreadId;
        seas.ExtBuffer = ExtBuffer;
        seas.ExtBufferSize = ExtBufferSize;
        seas.BytesLen = 0;

        if(!DebugActiveProcess(pid)) throw WM_EXT_TEXT_ENTER_DEBUG_FAILED;

        for(;;)
        {
            //process control
            if(*StopExtText)
                throw WM_EXT_TEXT_EXIT_ACTIVE;
            
            //wait for debug event
            if(!WaitForDebugEvent(&dbe, DEBUG_LOOP_MAX_WAIT))
                continue;

            //if the debugged program exit, then we exit too
            if(dbe.dwDebugEventCode == EXIT_PROCESS_DEBUG_EVENT)
                throw WM_EXT_TEXT_EXIT_PASSIVE;

            //first filter
            if(dbe.dwDebugEventCode != EXCEPTION_DEBUG_EVENT)
            {
                ContinueDebugEvent(dbe.dwProcessId, dbe.dwThreadId, DBG_EXCEPTION_NOT_HANDLED);
            }

            //int3 break
            else if(dbe.u.Exception.ExceptionRecord.ExceptionCode == EXCEPTION_BREAKPOINT)
            {
                HANDLE hThread = OpenThread(THREAD_ALL_ACCESS, false, dbe.dwThreadId);
                if(!hThread) throw WM_EXT_TEXT_GET_HTHREAD_FAILED;
                ctx.ContextFlags = CONTEXT_FULL;
                if(!GetThreadContext(hThread, &ctx)) throw WM_EXT_TEXT_GET_CTX_FAILED;

                int LastIdx = -1;
                for(int i = 0; i < HookersNum; i++)
                {
                    if(Hookers[i].Addr != dbe.u.Exception.ExceptionRecord.ExceptionAddress) continue;

                    if(!racer.status) racer.start();
                    LastIdx = i;
                    bool IsInnerAddr = true; //whether use * or ReadProcessMemory()

                    //set the pValue
                    if(Hookers[i].EspBias < 0 && !Hookers[i].ValueAsAddr)
                    {
                        pValue = (char *)&ctx.Esp + Hookers[i].EspBias;
                        IsInnerAddr = false;
                    }
                    else if(Hookers[i].EspBias < 0)
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
                            throw WM_EXT_TEXT_GET_MEM_FAILED;
                    }

                    //read the bytes
                    for(int c = 0; ; c++)
                    {
                        racer.reset();
                        if(IsInnerAddr)
                        {
                            if(!ReadProcessMemory(hProc, (void *)((char *)pValue + Hookers[i].ValueAsAddrBias + c), ExtBuffer + seas.BytesLen++, sizeof(char), NULL))
                                throw WM_EXT_TEXT_GET_MEM_FAILED;
                        }
                        else
                        {
                            *(ExtBuffer + seas.BytesLen++) = *((char *)((unsigned long)pValue + Hookers[i].BytesPerRead - c - 1));
                        }

                        if(seas.BytesLen >= ExtBufferSize) seas.BytesLen = 0; //impact resistant

                        if(Hookers[i].BytesPerRead != 0) //fixed bytes
                        {
                            if(c >= Hookers[i].BytesPerRead - 1) break;
                        }
                        else                            //whole sentence
                        {
                            if(*(ExtBuffer + seas.BytesLen - 1) == 0) break;
                        }
                    }
                }

                if(LastIdx == -1) //not the goal addr
                {
                    ContinueDebugEvent(dbe.dwProcessId, dbe.dwThreadId, DBG_EXCEPTION_NOT_HANDLED);
                }
                else
                {
                    //recover the original code
                    if(!WriteProcessMemory(hProc, Hookers[LastIdx].Addr, &OriginalCodes[LastIdx], sizeof(char), NULL))
                        throw WM_EXT_TEXT_SET_CODE_FAILED;

                    //reset the process pointer and set to sigle-step-mode to arouse second break
                    ctx.ContextFlags = CONTEXT_CONTROL;
                    if(!GetThreadContext(hThread, &ctx))
                        throw WM_EXT_TEXT_GET_CTX_FAILED;
                    ctx.Eip = (unsigned long)Hookers[LastIdx].Addr;
                    ctx.EFlags |= 0x100; //set the TF position as 1 in EFlags register to start sigle-step-mode
                    ctx.ContextFlags = CONTEXT_CONTROL;
                    if(!SetThreadContext(hThread, &ctx))
                        throw WM_EXT_TEXT_SET_CTX_FAILED;

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
                        throw WM_EXT_TEXT_SET_CODE_FAILED;
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
    catch(int e)
    {
        switch(e)
        {
        case WM_EXT_TEXT_EXIT_ACTIVE:
        case WM_EXT_TEXT_EXIT_PASSIVE:
        case WM_EXT_TEXT_GET_HTHREAD_FAILED:
        case WM_EXT_TEXT_GET_CTX_FAILED:
        case WM_EXT_TEXT_SET_CTX_FAILED:
        case WM_EXT_TEXT_GET_MEM_FAILED:
        case WM_EXT_TEXT_SET_CODE_FAILED: //exceptions in debug loop
            DebugActiveProcessStop(pid);
            racer.stop();
        case WM_EXT_TEXT_ENTER_DEBUG_FAILED:
            for(int i = 0; i < HookersNum; i++)
                WriteProcessMemory(hProc, Hookers[i].Addr, &OriginalCodes[i], sizeof(char), NULL);
        default:
            if(OriginalCodes) delete OriginalCodes;
            PostThreadMessage(MainThreadId, e, NULL, NULL);
            break;
        }
    }
}

void onSentenceEnd(void * p)
{
    struct onSentenceEndArgsStruct * pArgs = (struct onSentenceEndArgsStruct *)p;
    if(!pArgs->BytesLen) return;
    for(int i = pArgs->BytesLen; i < pArgs->ExtBufferSize; i++) pArgs->ExtBuffer[i] = 0;
    pArgs->BytesLen = 0;
    PostThreadMessage(pArgs-> MainThreadId, WM_EXT_TEXT_EXT_SUCCESS, NULL, NULL);
}
