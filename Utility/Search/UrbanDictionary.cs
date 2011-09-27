using System.Web;

namespace robokins.Utility.Search
{
    class UrbanDictionary
    {
        public static string Search(string term)
        {
            var html = HTTP.DownloadPage("http://www.urbandictionary.com/define.php?term=" + HttpUtility.UrlEncode(term));
            var z = html.IndexOf("<table id='entries'>");
            html = Texts.StringBetween(html, "<div class=\"definition\">", "</div>", z);
            html = Texts.StripTags.Replace(html, string.Empty);
            return HttpUtility.HtmlDecode(html);
        }
    }
}