using System.Text.RegularExpressions;
using System.Web;

namespace robokins.Utility.Search
{
    class MSDN
    {
        public static string[] Search(string query)
        {
            string xml = HTTP.DownloadPage("http://social.msdn.microsoft.com/Search/Feed.aspx?locale=en-GB&format=RSS&Refinement=86&Query=" +
                HttpUtility.UrlEncode(query));

            GroupCollection group = Texts.ItemDescrRSS.Match(xml).Groups;
            if (group.Count < 4 || group[2].Value == string.Empty)
                return null;

            return new string[] { group[2].Value, HttpUtility.HtmlDecode(group[1].Value), HttpUtility.HtmlDecode(group[3].Value) };
        }
    }
}
