using System;
using System.Diagnostics;
using System.Security;
using System.Web.Services;
using robokins.Utility;
using robokins.Utility.Search;

namespace robokins
{
    partial class Program
    {
        [Conditional("DEBUG")]
        static void Tests(SecureString password)
        {
            string[] defs;
            string def;

            defs = Wiki.Search("test");
            Console.WriteLine("Wiki: {0}", defs == null ? "null" : string.Join(" ", defs));
            Console.WriteLine();

            defs = Google.AutoHotkey("foobar");
            Console.WriteLine("Google AutoHotkey: {0}", defs == null ? "null" : string.Join(" ", defs));
            Console.WriteLine();

            defs = Google.Search("meaning of life");
            Console.WriteLine("Google Search: {0}", defs == null ? "null" : string.Join(" ", defs));
            Console.WriteLine();

            Console.WriteLine("Google Define: {0}", Google.Define("test") ?? "null");
            Console.WriteLine();

            defs = MSDN.Search("createwindow");
            Console.WriteLine("MSDN: {0}", defs == null ? "null" : string.Join(" ", defs));
            Console.WriteLine();

            def = UrbanDictionary.Search("windows");
            Console.WriteLine("UrbanDictionary: {0}", def);
            Console.WriteLine();

            def = HTTP.ShortUrl("http://maps.google.co.uk/maps?f=q&source=s_q&hl=en&geocode=&q=louth&sll=53.800651,-4.064941&sspn=33.219383,38.803711&ie=UTF8&hq=&hnear=Louth,+United+Kingdom&ll=53.370272,-0.004034&spn=0.064883,0.075788&z=14");
            Console.WriteLine("Short URL: {0}", def);
            Console.WriteLine();

            def = "dllcall()";
            defs = Manual.Lookup(def);
            Console.WriteLine("Manual \"{0}\": {1}: {2}", def, defs[0], defs[1]);
            def = "nothing";
            Console.WriteLine("Manual \"{0}\": {1}", def, Manual.Lookup(def) == null ? "null" : "not null");
            Console.WriteLine();

            Console.WriteLine();
            Console.Write("Connect to IRC [y/n]? ");

            char mode = (char)Console.Read();
            if (!(mode == 'y' || mode == 'Y'))
                Environment.Exit(0);
        }
    }
}
