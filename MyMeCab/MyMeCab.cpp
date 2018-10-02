#include "MyMeCab.h"

void __stdcall MeCabSegment(wchar_t * text, unsigned char * res)
{
    char * textUtf8 = new char[MAX_BUFFER_SIZE];
    memset(textUtf8, 0, MAX_BUFFER_SIZE);
    W2A(text, textUtf8, UTF_8);

    MeCab::Tagger * tagger = MeCab::createTagger("");

    std::string str = tagger->parse(textUtf8);
    int pos = str.rfind("\nEOS");

    memset(res, 0, MAX_SEG_BUFFER_SIZE);
    memcpy(res, str.c_str(), pos - 1);
    
    delete textUtf8;
    delete[]tagger;
}
