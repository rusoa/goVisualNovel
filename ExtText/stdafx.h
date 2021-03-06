#pragma once

#include <Windows.h>
#include <tlhelp32.h>
#include <iostream>

const int WM_EXT_TEXT = WM_USER + 100;
const int WM_EXT_TEXT_GET_PID_SUCCESS = WM_EXT_TEXT + 10;
const int WM_EXT_TEXT_GET_HPROC_FAILED = WM_EXT_TEXT + 11;
const int WM_EXT_TEXT_GET_HTHREAD_FAILED = WM_EXT_TEXT + 12;
const int WM_EXT_TEXT_GET_BASE_FAILED = WM_EXT_TEXT + 13;
const int WM_EXT_TEXT_GET_CTX_FAILED = WM_EXT_TEXT + 20;
const int WM_EXT_TEXT_SET_CTX_FAILED = WM_EXT_TEXT + 21;
const int WM_EXT_TEXT_ENTER_DEBUG_FAILED = WM_EXT_TEXT + 30;
const int WM_EXT_TEXT_GET_CODE_FAILED = WM_EXT_TEXT + 40;
const int WM_EXT_TEXT_SET_CODE_FAILED = WM_EXT_TEXT + 41;
const int WM_EXT_TEXT_GET_MEM_FAILED = WM_EXT_TEXT + 42;
const int WM_EXT_TEXT_EXT_SUCCESS = WM_EXT_TEXT + 50;
const int WM_EXT_TEXT_EXIT_PASSIVE = WM_EXT_TEXT + 60;
const int WM_EXT_TEXT_EXIT_ACTIVE = WM_EXT_TEXT + 61;
