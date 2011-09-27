using System.Text.RegularExpressions;
using System.Web;

namespace robokins.Utility.Search
{
    class AutoHotkey
    {
        static Regex search = new Regex("<span class=\"maintitle\">Search found (\\d+) match(?:es)?</span>");
        static Regex post = new Regex("</b>&nbsp; &nbsp;Posted: (.+?)&nbsp; &nbsp;Subject: <b><a href=\"post-(\\d+).html[^\"]*?\">([^<]*?)</a></b></span></td>");

        public static string[] UserStats(string user)
        {
            string forum = "http://www.autohotkey.com/forum/",
                data = "search_keywords=&search_terms=all&search_author=" +
                HttpUtility.UrlEncode(user) + "&search_forum=-1&search_time=0&search_fields=all&show_results=posts" +
                "&return_chars=0&sort_by=0&sort_dir=DESC";

            string html = HTTP.DownloadPage(forum + "search.php", data);

            GroupCollection hits = search.Match(html).Groups, stats = post.Match(html).Groups;
            if (hits.Count != 2 || stats.Count != 4)
                return null;

            return new string[] { hits[1].Value, 
                forum + "post-" + stats[2].Value + ".html#" + stats[2].Value,
                HttpUtility.HtmlDecode(stats[3].Value),
                stats[1].Value };
        }
    }
}
