using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Text.RegularExpressions;

namespace goVisualNovel
{
    class Youdao
    {
        private const string url = "http://openapi.youdao.com/api";
        private string appKey;
        private string appSecret;

        //{ my-language-type, api-language-type }
        private static Dictionary<string, string> LANG_MAP = new Dictionary<string, string>()
        {
            { "zhs", "zh-CHS" },
            { "en", "EN" },
            { "ja", "ja" },
            { "fr", "fr" },
            { "ko", "ko" },
            { "ru", "ru" },
            { "es", "es" },
            { "auto", "auto" }
        };

        public Youdao(string appKey, string appSecret)
        {
            this.appKey = appKey;
            this.appSecret = appSecret;
        }

        public string Translate(string src, string from, string to)
        {
            string salt = "0";
            string md5 = Md5(Encoding.GetEncoding("utf-8").GetBytes(appKey + src + salt + appSecret));

            Dictionary<string, string> data = new Dictionary<string, string>()
            {
                { "q", src },
                { "from", CvtLang(from) },
                { "to", CvtLang(to) },
                { "appKey", appKey },
                { "salt", salt },
                { "sign", md5 }
            };

            string raw = Requests.Post(url, data, "application/x-www-form-urlencoded");
            return Regex.Match(raw, "(?<=\\\"translation\\\":\\u005b\\\").*?(?=\\\"(]|,))").Value;
        }

        private static string Md5(byte[] bytes)
        {
            byte[] md5Bytes = new MD5CryptoServiceProvider().ComputeHash(bytes);
            return BitConverter.ToString(md5Bytes).Replace("-", "");
        }

        private static string CvtLang(string lang)
        {
            return LANG_MAP.ContainsKey(lang) ? LANG_MAP[lang] : LANG_MAP["auto"];
        }
    }
}
