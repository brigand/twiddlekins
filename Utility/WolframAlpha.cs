using System;

namespace robokins.Utility
{
    class WolframAlpha
    {
		public static readonly string SiteUrl = "http://www.wolframalpha.com";
		
        public static string Link(string query)
        {
			const string url = "http://www.wolframalpha.com/input/?i=";
			
			string queryEncoded = System.Web.HttpUtility.UrlEncode(query);
			
			Console.WriteLine("WolframAlpha Long Url: {0}", url + queryEncoded);
			return HTTP.ShortUrl(url + queryEncoded);
			
        }
    }
}
