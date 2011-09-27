using System;
using System.Text.RegularExpressions;

namespace robokins.Utility
{
    class Texts
    {
        internal static Random Random = new Random();

        public static readonly string[] YesResponses = new string[] { "Okay", "Sure thing", "Affirmative", "Roger that", "Yes", "Fine" };

        public static Regex StripTags = new Regex("<[^>]*?>");
        public static Regex ItemRSS = new Regex(@"<item>\s*<title>([^<]+)</title>\s*<link>([^<]+)</link>");
        public static Regex ItemDescrRSS = new Regex(@"<item>\s*<link>([^<]+)</link>\s*<title>([^<]+)</title>\s*<description>([^<]+)</description>");

        /// <summary>
        /// Splits command into an array as { first word, second word, everything after first word }.
        /// </summary>
        /// <param name="Text">The line to split.</param>
        /// <returns>A string array with three elements.</returns>
        public static string[] Commands(string Text)
        {
            string[] result = new string[] { string.Empty, string.Empty, string.Empty };

            string[] parts = Text.Trim().Split(Bot.boundary, 2);
            if (parts.Length > 0)
            {
                result[0] = parts[0];
                if (parts.Length > 1)
                {
                    result[2] = parts[1];
                    int z = result[2].IndexOfAny(Bot.boundary);
                    result[1] = z == -1 ? result[2] : result[2].Substring(0, z);
                }
            }

            return result;
        }

        public static string StringBetween(string search, string start, string end, int offset = 0)
        {
            int x = search.IndexOf(start, offset);
            if (x == -1)
                return null;
            x += start.Length;

            int y = search.IndexOf(end, x) - x;
            if (y < 1)
                return null;

            return search.Substring(x, y);
        }
    }
}
