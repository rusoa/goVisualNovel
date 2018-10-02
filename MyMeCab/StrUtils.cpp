#include "StrUtils.h"

void A2W(const char * src, wchar_t * dst, int SrcEnc)
{
    int len = MultiByteToWideChar(SrcEnc, 0, src, -1, NULL, 0);
    MultiByteToWideChar(SrcEnc, 0, src, -1, dst, len);
}

void W2A(const wchar_t * src, char * dst, int DstEnc)
{
    int len = WideCharToMultiByte(DstEnc, 0, src, -1, NULL, 0, NULL, NULL);
    WideCharToMultiByte(DstEnc, 0, src, -1, dst, len, NULL, NULL);
}
