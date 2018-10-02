#pragma once

#include "stdafx.h"
#include <mecab.h>

#define MAX_BUFFER_SIZE 233
#define MAX_SEG_BUFFER_SIZE (MAX_BUFFER_SIZE * 20)

void __stdcall MeCabSegment(wchar_t *, unsigned char *);
