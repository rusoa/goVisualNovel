#include "MyMeCab.h"

void __stdcall MeCabSegment(wchar_t * text, unsigned char * pSegBuffer, int SegBufferSize)
{
    char * textUtf8 = new char[SegBufferSize];
    memset(textUtf8, 0, SegBufferSize);
    W2A(text, textUtf8, UTF_8);

    MeCab::Tagger * tagger = MeCab::createTagger("");

    std::string str = tagger->parse(textUtf8);
    int pos = str.rfind("EOS");

    memset(pSegBuffer, 0, SegBufferSize);
    memcpy(pSegBuffer, str.c_str(), pos);
    
    delete textUtf8;
    delete[]tagger;
}
