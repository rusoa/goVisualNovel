#pragma once

#include "stdafx.h"
#include "ProcUtils.h"
#include "Racer.h"

const int WAIT_MILLISECOND = 50;
const int DEBUG_LOOP_MAX_WAIT = 500;
const int EXT_BYTES_MAX_SIZE = 233;
const int COLD_DOWN_TIME = 5000;
const int WM_EXT_TEXT_EXT = WM_USER + 233;
const int WM_EXT_TEXT_EXIT_PASSIVE = WM_USER + 234;
const int WM_EXT_TEXT_EXIT_ERROR = WM_USER + 235;

struct onSentenceEndArgsStruct
{
    int MainThreadId;
    
    unsigned char * ExtBuffer;
    int BytesLen;
    onSentenceEndArgsStruct(int MainThreadId, unsigned char * ExtBuffer, int BytesLen) :
        MainThreadId(MainThreadId), ExtBuffer(ExtBuffer), BytesLen(BytesLen)
    {};
};

void __stdcall ExtText(
    const wchar_t * ModuleName,
    void * HookAddr,
    int HookEspBias,
    bool HookValueAsAddr,
    int HookValueAsAddrBias,
    int BytesPerRead,
    int MainThreadId,
    unsigned char * ExtBuffer,
    bool * StopExtText);

inline bool IsCaredEvent(DEBUG_EVENT &dbe, void * HookAddr, unsigned long dMainThreadId);

void onSentenceEnd(void *);
