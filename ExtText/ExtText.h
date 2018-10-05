#pragma once

#include "stdafx.h"
#include "ProcUtils.h"
#include "Racer.h"

const int WAIT_MILLISECOND = 50;
const int DEBUG_LOOP_MAX_WAIT = 500;
const int WM_EXT_TEXT_EXT = WM_USER + 233;
const int WM_EXT_TEXT_EXIT_PASSIVE = WM_USER + 234;
const int WM_EXT_TEXT_EXIT_ERROR = WM_USER + 235;

const unsigned char int3 = 0xCC;

struct Hooker
{
    void * Addr;
    int EspBias;
    bool ValueAsAddr;
    int ValueAsAddrBias;
    int BytesPerRead;
};

struct onSentenceEndArgsStruct
{
    int MainThreadId;
    unsigned char * ExtBuffer;
    int ExtBufferSize;
    int BytesLen;
};

void __stdcall ExtText(
    const wchar_t * ModuleName,
    struct Hooker * Hookers,
    int HookersNum,
    int MainThreadId,
    unsigned char * ExtBuffer,
    int ExtBufferSize,
    bool * StopExtText
);

void onSentenceEnd(void *);
