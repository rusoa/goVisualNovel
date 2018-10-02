using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;

namespace goVisualNovel.Dictionary
{
    static class HJJP
    {
        public static string[] Search(string word)
        {
            string url = "https://dict.hjenglish.com/jp/jc/" + HttpUtility.UrlEncode(word);
            string raw = Requests.Get(url, new Dictionary<string, string>() { { "Cookie", "HJ_SID=0"} });
            string temp1 = Regex.Match(raw, "(?<=<div class=\"simple\">(.|\n)*<ul>)(.|\n)*?(?=</ul>(.|\n)*</div>)").Value;
            List<string> temp2 = new List<string>();
            foreach (Match match in Regex.Matches(temp1, "(?<=</span>).*?(?=</li>)"))
                temp2.Add(match.Value);
            return temp2.ToArray();
        }
    }
}
