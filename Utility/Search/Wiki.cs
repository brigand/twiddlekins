using System;
using System.Web;

namespace robokins.Utility.Search
{
    class Wiki
    {
        public static string[] Search(string term)
        {
            string html = HTTP.DownloadPage("http://en.wikipedia.org/w/api.php?action=opensearch&format=xml&limit=1&search=" +
                HttpUtility.UrlEncode(term));

            const string boundary = "<Description xml:space=\"preserve\">";
            int pos = html.IndexOf(boundary);
            if (pos == -1)
                return null;

            pos += boundary.Length;
            html = html.Substring(pos, html.IndexOf("</Url>") - pos);
            return HttpUtility.HtmlDecode(html).Split(new string[] { "</Description><Url xml:space=\"preserve\">" }, StringSplitOptions.None);
        }
    }
}
