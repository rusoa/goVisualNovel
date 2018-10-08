#pragma once

#include "stdafx.h"
#include "ProcUtils.h"
#include "Racer.h"

const int WAIT_MILLISECOND = 50;
const int DEBUG_LOOP_MAX_WAIT = 500;
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
