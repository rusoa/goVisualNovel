#include "Racer.h"

Racer::Racer(unsigned int timeout, void (*cb) (void *), void * cbParams)
{
    this->timeout = timeout;
    this->cb = cb;
    this->cbParams = cbParams;
    this->loopTid = 0;
    this->status = false;
}

void Racer::start()
{
    HANDLE hThread = CreateThread(NULL, 0, (LPTHREAD_START_ROUTINE)this->loop, (void *)this, 0, &this->loopTid);
    this->status = true;
}

void Racer::reset()
{
    if(!this->status) return;
    PostThreadMessage(this->loopTid, WM_RESET, NULL, NULL);
}

void Racer::stop()
{
    if(!this->status) return;
    PostThreadMessage(this->loopTid, WM_STOP, NULL, NULL);
    this->status = false;
}

void __stdcall Racer::loop(void * p)
{
    Racer * pR = (Racer *)p;
    MSG msg;
    for(unsigned int i = 0;;i++)
    {
        if(PeekMessage(&msg, NULL, 0, 0, PM_REMOVE))
        {
            if(msg.message == WM_RESET) i = 0;
            else if(msg.message == WM_STOP) return;
        }
        else if(i >= pR->timeout)
        {
            pR->cb(pR->cbParams);
            break;
        }
        Sleep(1);
    }
    pR->status = false;
}
