using System;
using System.IO;
using System.Net;
using System.Text;
using System.Web;

namespace robokins.Utility
{
    class HTTP
    {
        const string UserAgent = "Mozilla/5.0 (X11; U; Linux x86_64; en-US; rv:1.9.2.8) Gecko/20100722 Ubuntu/10.04 (lucid) Firefox/3.6.8";
		public static bool useahk4me = true;	
		
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
            const string service = "http://api.bitly.com/v3/shorten?login={0}&apiKey={1}&longUrl={2}&format=txt";		
			
			// This key is Frankie's personal key.  They may stop working at any time.
			// Consider replacing them after testing.
			string api_login = "o_26thqndlmc";
			string api_key = "R_954a40faa0cdcdcb5c0b48af321f6830";
			
			if (useahk4me) {
				api_login = "ahk4me";
				api_key = "R_4b3df1f5417d94ff356ed511fd50a153";
			}
            var request = string.Format(service, api_login, api_key, HttpUtility.UrlEncode(url));
            var result = DownloadPage(request);

            if (string.IsNullOrEmpty(result))
                return url;
			
            return result;
        }
    }
}
