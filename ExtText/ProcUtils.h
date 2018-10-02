#pragma once

#include "stdafx.h"

bool EnableDebugPriv(); //upgrade the debug privilege
unsigned long GetPidByName(const wchar_t *); //get process id by process name
unsigned long GetMtid(unsigned long);  //get the id of the main thread by appointing pid
void * GetBaseAddr(unsigned long);     //get base addr of a module to calculate the absolute addr
