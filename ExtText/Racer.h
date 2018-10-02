#pragma once

#include<Windows.h>

#define WM_RESET (WM_USER + 100)
#define WM_STOP (WM_USER + 200)

class Racer
{
private:
    unsigned int timeout;
    void (*cb) (void *);
    void * cbParams;
    unsigned long loopTid;
    static void __stdcall loop(void *);
public:
    bool status;
    Racer(unsigned int, void (*cb) (void *), void * = NULL);
    void start();
    void reset();
    void stop();
};
