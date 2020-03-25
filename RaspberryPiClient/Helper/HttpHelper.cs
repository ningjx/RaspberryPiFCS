using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace RaspberryPiClient.Helper
{
    public class HttpHelper
    {/// <summary>
     /// Http (GET/POST)
     /// </summary>
     /// <param name="url">请求URL</param>
     /// <param name="parameters">请求参数</param>
     /// <param name="method">请求方法</param>
     /// <returns>响应内容</returns>
        private static T GetRequest<T>(string url, IDictionary<string, string> parameters, string method)
        {
            string retString = string.Empty;
            HttpWebRequest req = null;
            HttpWebResponse rsp = null;
            Stream reqStream = null;
            try
            {
                req = (HttpWebRequest)WebRequest.Create(url);
                req.Method = method;
                req.KeepAlive = false;
                req.ProtocolVersion = HttpVersion.Version10;
                req.Timeout = 5000;
                req.ContentType = "application/x-www-form-urlencoded;charset=utf-8";
                byte[] postData = Encoding.UTF8.GetBytes(BuildQuery(parameters, "utf8"));
                reqStream = req.GetRequestStream();
                if (parameters.Count != 0)
                    reqStream.Write(postData, 0, postData.Length);
                rsp = (HttpWebResponse)req.GetResponse();
                Encoding encoding = Encoding.GetEncoding(rsp.CharacterSet);
                string resStr = string.Empty;
                using (Stream stream = rsp.GetResponseStream())
                {
                    var reader = new StreamReader(stream, encoding);
                    resStr = reader.ReadToEnd();
                }
                if (!string.IsNullOrEmpty(resStr))
                    return JsonConvert.DeserializeObject<T>(resStr);
                return default;
            }
            catch (Exception ex)
            {
                return default;
            }
        }

        /// <summary>
        /// 组装普通文本请求参数。
        /// </summary>
        /// <param name="parameters">Key-Value形式请求参数字典</param>
        /// <returns>URL编码后的请求数据</returns>
        private static string BuildQuery(IDictionary<string, string> parameters, string encode)
        {
            StringBuilder postData = new StringBuilder();
            bool hasParam = false;
            IEnumerator<KeyValuePair<string, string>> dem = parameters.GetEnumerator();
            while (dem.MoveNext())
            {
                string name = dem.Current.Key;
                string value = dem.Current.Value;
                // 忽略参数名或参数值为空的参数
                if (!string.IsNullOrEmpty(name))//&& !string.IsNullOrEmpty(value)
                {
                    if (hasParam)
                    {
                        postData.Append("&");
                    }
                    postData.Append(name);
                    postData.Append("=");
                    if (encode == "gb2312")
                    {
                        postData.Append(HttpUtility.UrlEncode(value, Encoding.GetEncoding("gb2312")));
                    }
                    else if (encode == "utf8")
                    {
                        postData.Append(HttpUtility.UrlEncode(value, Encoding.UTF8));
                    }
                    else
                    {
                        postData.Append(value);
                    }
                    hasParam = true;
                }
            }
            return postData.ToString();
        }

    }
}
