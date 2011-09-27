using System;
using System.IO;
using System.Net;
using System.Text;

namespace robokins.Utility
{
    class HTTP
    {
        const string UserAgent = "Mozilla/5.0 (X11; U; Linux x86_64; en-US; rv:1.9.2.8) Gecko/20100722 Ubuntu/10.04 (lucid) Firefox/3.6.8";

        public static string DownloadPage(string uri)
        {
            try
            {
                HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create(uri);
                req.UserAgent = UserAgent;

                HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
                if (resp.StatusCode != HttpStatusCode.OK)
                    return string.Empty;

                string res = (new StreamReader(resp.GetResponseStream())).ReadToEnd();
                resp.Close();
                return res;
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

        public static string DownloadPage(string uri, string data, NetworkCredential auth = null, string contentType = null)
        {
            try
            {
                ServicePointManager.Expect100Continue = false;
                byte[] buffer = Encoding.ASCII.GetBytes(data);
                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(uri);

                if (auth != null)
                    req.Credentials = auth;

                req.Method = "POST";
                req.ContentType = contentType ?? "application/x-www-form-urlencoded";
                req.ContentLength = buffer.Length;

                Stream post = req.GetRequestStream();
                post.Write(buffer, 0, buffer.Length);
                post.Close();

                HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
                return (new StreamReader(resp.GetResponseStream())).ReadToEnd();
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

        public static string ShortUrl(string url)
        {
            const string service = "https://www.googleapis.com/urlshortener/v1/url";
            var request = string.Concat("{\"longUrl\":\"", Uri.EscapeUriString(url), "\"}");

            var result = DownloadPage(service, request, null, "application/json");

            if (string.IsNullOrEmpty(result))
                return url;

            const string domain = "http://goo.gl/";
            var id = Texts.StringBetween(result, domain, "\"");
            return domain + id;
        }
    }
}
