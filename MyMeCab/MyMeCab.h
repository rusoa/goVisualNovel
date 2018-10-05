#pragma once

#include "stdafx.h"
#include <mecab.h>

void __stdcall MeCabSegment(wchar_t * text, unsigned char * pSegBuffer, int SegBufferSize);
