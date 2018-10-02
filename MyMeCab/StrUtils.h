#pragma once

#include "stdafx.h"
#include <Windows.h>

#define SHIFT_JIS 932
#define UTF_8 CP_UTF8
#define ANSI CP_ACP

void A2W(const char *, wchar_t *, int);
void W2A(const wchar_t *, char *, int);
