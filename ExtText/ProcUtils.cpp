#include "procutils.h"

bool EnableDebugPriv()
{
    HANDLE hToken;
    LUID sedebugnameValue;
    TOKEN_PRIVILEGES tkp;

    if(!OpenProcessToken(GetCurrentProcess(), TOKEN_ADJUST_PRIVILEGES | TOKEN_QUERY, &hToken))
        return false;

    if(!LookupPrivilegeValue(NULL, SE_DEBUG_NAME, &sedebugnameValue))
    {
        CloseHandle(hToken);
        return false;
    }

    tkp.PrivilegeCount = 1;
    tkp.Privileges[0].Luid = sedebugnameValue;
    tkp.Privileges[0].Attributes = SE_PRIVILEGE_ENABLED;

    if(!AdjustTokenPrivileges(hToken, FALSE, &tkp, sizeof tkp, NULL, NULL))
    {
        CloseHandle(hToken);
        return false;
    }

    return true;
}

unsigned long GetPidByName(const wchar_t * name)
{
    HANDLE hProcSnap = CreateToolhelp32Snapshot(TH32CS_SNAPPROCESS, 0);
    if(hProcSnap == INVALID_HANDLE_VALUE) throw "failed to get process snapshot";

    PROCESSENTRY32W pe32;
    pe32.dwSize = sizeof(PROCESSENTRY32W);
    if(!Process32FirstW(hProcSnap, &pe32))
    {
        CloseHandle(hProcSnap);
        throw "failed to read process snapshot";
    }

    unsigned long pid = 0;
    do
    {
        if(wcscmp(pe32.szExeFile, name) == 0)
        {
            pid = pe32.th32ProcessID;
            break;
        }
    } while(Process32NextW(hProcSnap, &pe32));

    CloseHandle(hProcSnap);
    return pid;
}

unsigned long GetMtid(unsigned long pid)
{
    HANDLE hThreadSnap = CreateToolhelp32Snapshot(TH32CS_SNAPTHREAD, 0);
    if(hThreadSnap == INVALID_HANDLE_VALUE) throw "failed to get thread snapshot";

    THREADENTRY32 te32;
    te32.dwSize = sizeof(THREADENTRY32);
    if(!Thread32First(hThreadSnap, &te32))
    {
        CloseHandle(hThreadSnap);
        throw "failed to read thread snapshot";
    }

    do
    {
        if(te32.th32OwnerProcessID == pid)
            return te32.th32ThreadID;
    } while(Thread32Next(hThreadSnap, &te32));

    CloseHandle(hThreadSnap);
    throw "failed to find main thread id";
}

void * GetBaseAddr(unsigned long pid)
{
    HANDLE hModSnap = CreateToolhelp32Snapshot(TH32CS_SNAPMODULE, pid);
    if(hModSnap == INVALID_HANDLE_VALUE) throw "failed to get module snapshot";

    MODULEENTRY32 me32;
    me32.dwSize = sizeof(MODULEENTRY32);
    if(!Module32First(hModSnap, &me32))
    {
        CloseHandle(hModSnap);
        throw "failed to read module snapshot";
    }

    do
    {
        if(me32.th32ProcessID == pid)
            return me32.modBaseAddr;
    } while(Module32Next(hModSnap, &me32));

    CloseHandle(hModSnap);
    throw "failed to find base addr";
}
