using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using System.Web.Script.Serialization;
using System.Web;

namespace goVisualNovel
{
    static class Requests
    {
        public static string Get(string url)
        {
            return Get(url, new Dictionary<string, string>());
        }

        public static string Get(string url, Dictionary<string, string> header)
        {
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);

            foreach (string key in header.Keys)
                req.Headers.Add(key, header[key]);
            req.Method = "GET";

            HttpWebResponse res = (HttpWebResponse)req.GetResponse();
            StreamReader resStream = new StreamReader(res.GetResponseStream());
            return HttpUtility.UrlDecode(resStream.ReadToEnd());
        }

        public static string Post(string url, Dictionary<string, string> data, string contentType)
        {
            return Post(url, new Dictionary<string, string>(), data, contentType);
        }

        public static string Post(string url, Dictionary<string, string> header, Dictionary<string, string> data, string contentType)
        {
            string content;
            if (contentType == "application/json") content = DicToJson(data);
            else content = DicToForm(data);
            return Post(url, header, content, contentType);
        }

        public static string Post(string url, Dictionary<string, string> header, string content, string contentType)
        {
            byte[] contentBytes = Encoding.ASCII.GetBytes(content);

            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
            req.Method = "POST";
            req.ContentType = contentType;
            req.KeepAlive = true;
            req.ContentLength = contentBytes.Length;
            
            using (Stream reqStream = req.GetRequestStream())
            {
                reqStream.Write(contentBytes, 0, contentBytes.Length);
                reqStream.Close();
            }

            HttpWebResponse res = (HttpWebResponse)req.GetResponse();
            StreamReader resStream = new StreamReader(res.GetResponseStream());
            return HttpUtility.UrlDecode(resStream.ReadToEnd());
        }

        public static string DicToJson<TKey, TValue>(Dictionary<TKey, TValue> dic)
        {
            return new JavaScriptSerializer().Serialize(dic);
        }

        public static string DicToForm<TKey, TValue>(Dictionary<TKey, TValue> dic)
        {
            List<string> list = new List<string>();
            foreach(TKey key in dic.Keys)
                list.Add(HttpUtility.UrlEncode(key.ToString()) + "=" + HttpUtility.UrlEncode(dic[key].ToString()));
            return string.Join("&", list);
        }
    }
}
