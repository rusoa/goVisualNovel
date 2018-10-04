#include "ExtText.h"

using namespace std;

void __stdcall ExtText(
    const wchar_t * ModuleName,
    void * HookAddr,
    int HookEspBias,
    bool HookValueAsAddr,
    int HookValueAsAddrBias,
    int BytesPerRead,
    int MainThreadId,
    unsigned char * ExtBuffer,
    bool * StopExtText)
{
    unsigned long pid; //pid of debugged process
    HANDLE hProc = NULL; //process handler of debugged process
    unsigned long dMainThreadId; //main thread id of debugged process
    void * BaseAddr; //module base addr that always be changed by alsr
    void * pValue; //pointer to the register to read
    CONTEXT ctx;
    unsigned char OriginalCode; //backup the original code for recovering after break point error
    const unsigned char int3 = 0xCC; //the int3 break point code
    bool CodeChanged = false;
    DEBUG_EVENT dbe;
    struct onSentenceEndArgsStruct seas(MainThreadId, ExtBuffer, 0);
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
        HookAddr = (void *)((unsigned long)BaseAddr + (unsigned long)HookAddr);

        if(!ReadProcessMemory(hProc, HookAddr, &OriginalCode, sizeof(BYTE), NULL))
            throw runtime_error("failed to read origin code");

        if(!WriteProcessMemory(hProc, HookAddr, &int3, sizeof(BYTE), NULL))
            throw runtime_error("failed to set int3 break point");
        CodeChanged = true;

        if(!DebugActiveProcess(pid)) throw runtime_error("failed to enter debug mode");

        for(;;)
        {
            //process control
            if(*StopExtText)
            {
                DebugActiveProcessStop(pid);
                if(CodeChanged) WriteProcessMemory(hProc, HookAddr, &OriginalCode, sizeof(char), NULL);
                break;
            }

            if(!WaitForDebugEvent(&dbe, DEBUG_LOOP_MAX_WAIT)) continue;

            //if the debugged program exit, then we exit too
            if(dbe.dwDebugEventCode == EXIT_PROCESS_DEBUG_EVENT)
            {
                DebugActiveProcessStop(pid);
                PostThreadMessage(MainThreadId, WM_EXT_TEXT_EXIT_PASSIVE, NULL, NULL);
                break;
            }

            //event that we do not care
            if(!IsCaredEvent(dbe, HookAddr, dMainThreadId))
            {
                ContinueDebugEvent(dbe.dwProcessId, dbe.dwThreadId, DBG_EXCEPTION_NOT_HANDLED);
                continue;
            }

            //first break
            if(dbe.u.Exception.ExceptionRecord.ExceptionCode == EXCEPTION_BREAKPOINT)
            {
                //timeout means a sentence is complete
                if(!racer.status) racer.start();

                HANDLE hThread = OpenThread(THREAD_ALL_ACCESS, false, dMainThreadId);
                if(!hThread) throw runtime_error("failed to get thread handle");

                ctx.ContextFlags = CONTEXT_FULL;
                if(!GetThreadContext(hThread, &ctx)) throw runtime_error("failed to get thread context");

                if(HookAddr == (void *)0x100) pValue = (char *)HookAddr + 1;
                else if(HookEspBias > 0) pValue = (void *)(ctx.Esp + HookEspBias);
                else pValue = (char *)&ctx.Esp + HookEspBias;

                //read the bytes
                for(int i = 0; i < BytesPerRead; i++)
                {
                    if(HookValueAsAddr)
                    {
                        if(!ReadProcessMemory(hProc, (void *)((unsigned long)pValue + HookValueAsAddrBias + i), ExtBuffer + seas.BytesLen++, sizeof(char), NULL))
                            throw runtime_error("failed to read bytes");
                    }
                    else
                    {
                        //read 1 byte per loop from high to low, bias = BytesPerRead - i - 1
                        *(ExtBuffer + seas.BytesLen++) = *((unsigned char *)((unsigned long)pValue + BytesPerRead - i - 1));
                    }
                    racer.reset();
                }

                //recover the original code
                if(!WriteProcessMemory(hProc, HookAddr, &OriginalCode, sizeof(char), NULL))
                    throw runtime_error("failed to recover original code");
                CodeChanged = false;

                //reset the process pointer
                ctx.ContextFlags = CONTEXT_CONTROL;
                if(!GetThreadContext(hThread, &ctx))
                    throw runtime_error("failed to get thread content");
                ctx.Eip = (unsigned long)HookAddr;
                ctx.ContextFlags = CONTEXT_CONTROL;
                if(!SetThreadContext(hThread, &ctx))
                    throw runtime_error("failed to set thread content");

                //impact resistant
                if(seas.BytesLen >= EXT_BYTES_MAX_SIZE - 3) seas.BytesLen = 0;

                //set to sigle-step-mode to arouse second break
                ctx.ContextFlags = CONTEXT_CONTROL;
                if(!GetThreadContext(hThread, &ctx)) throw runtime_error("failed to get thread content");
                ctx.EFlags |= 0x100; //set the TF position as 1 in EFlags register to start sigle-step-mode
                ctx.ContextFlags = CONTEXT_CONTROL;
                if(!SetThreadContext(hThread, &ctx)) throw runtime_error("failed to set thread content");
            }

            //second break, reset the breakpoint
            else if(dbe.u.Exception.ExceptionRecord.ExceptionCode == EXCEPTION_SINGLE_STEP)
            {
                if(!WriteProcessMemory(hProc, HookAddr, &int3, sizeof(BYTE), NULL))
                    throw runtime_error("failed to reset the int3 breakpoint");
                CodeChanged = true;
            }

            ContinueDebugEvent(dbe.dwProcessId, dbe.dwThreadId, DBG_CONTINUE);
        }
    }
    catch(runtime_error)
    {
        DebugActiveProcessStop(pid);
        racer.stop();
        if(CodeChanged) WriteProcessMemory(hProc, HookAddr, &OriginalCode, sizeof(char), NULL);
        PostThreadMessage(MainThreadId, WM_EXT_TEXT_EXIT_ERROR, NULL, NULL);
        return;
    }
}

inline bool IsCaredEvent(DEBUG_EVENT &dbe, void * HookAddr, unsigned long dMainThreadId)
{
    if(dbe.dwThreadId == dMainThreadId && dbe.dwDebugEventCode == EXCEPTION_DEBUG_EVENT)
    {
        if(dbe.u.Exception.ExceptionRecord.ExceptionCode == EXCEPTION_BREAKPOINT && dbe.u.Exception.ExceptionRecord.ExceptionAddress == HookAddr)
            return true;
        if(dbe.u.Exception.ExceptionRecord.ExceptionCode == EXCEPTION_SINGLE_STEP)
            return true;
    }
    return false;
}

void onSentenceEnd(void * p)
{
    struct onSentenceEndArgsStruct * pArgs = (struct onSentenceEndArgsStruct *)p;
    if(!pArgs->BytesLen) return;
    for(int i = pArgs->BytesLen; i < EXT_BYTES_MAX_SIZE; i++) pArgs->ExtBuffer[i] = 0;
    pArgs->BytesLen = 0;
    PostThreadMessage(pArgs-> MainThreadId, WM_EXT_TEXT_EXT, NULL, NULL);
}
