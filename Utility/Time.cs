using System;
using System.Text;

namespace robokins.Utility
{
    class Time
    {
        public static TimeSpan TimeSpanNow()
        {
            DateTime now = DateTime.Now;
            return new TimeSpan(now.Day, now.Hour, now.Minute, now.Second, now.Millisecond);
        }

        public static string ToDays(int s)
        {
            return ToDays((double)s);
        }

        public static string ToDays(double s)
        {
            int[] t = new int[] { 60 * 60 * 24, 60 * 60, 60, 1 };
            string[] w = new string[] { "day", "hour", "min", "sec", "msec" };

            char p = ' ', l = 's';
            StringBuilder x = new StringBuilder();

            int c = -1; // short circuit if less than a minute

            if (s < t[3]) // less than a second
            {
                s *= 1000;
                c = 4;
            }
            else if (s < t[2])
                c = 3;

            if (c > 0)
            {
                int d = (int)s;
                x.Append(d.ToString());
                x.Append(p);
                x.Append(w[c]);
                if (d != 1)
                    x.Append(l);
                return x.ToString();
            }

            for (int i = 0; i < t.Length - 1; i++)
            {
                int d = (int)(s / t[i]);

                if (d != 0)
                {
                    if (x.Length > 0)
                        x.Append(p);

                    x.Append(d.ToString());

                    x.Append(p);
                    x.Append(w[i]);

                    if (d != 1)
                        x.Append(l);
                }

                s -= d * t[i];
                if (s < 1)
                    break;
            }

            return x.ToString();
        }

        static readonly string[] Locations = new string[] { "UK", "Europe", "Africa", "US Central", "US Eastern", "Brazil", "Japan", "Australia" };
        static readonly int[] LocationOffsets = new int[] { 0, 1, 2, -6, -5, -3, 9, 10 };

        public static string WorldTime()
        {
            StringBuilder txt = new StringBuilder();
            txt.Append("Times: ");

            int count = Math.Min(Locations.Length, LocationOffsets.Length);

            for (int i = 0; i < count; i++)
            {
                txt.Append(Font.Bold);
                txt.Append(Locations[i]);
                txt.Append(Font.Bold);
                txt.Append(' ');

                DateTime now = DateTime.Now.ToUniversalTime();
                if (DateTime.Now.IsDaylightSavingTime())
                    now = now.AddHours(1);
                now = now.AddHours(LocationOffsets[i]);

                int hour = now.Hour % 12;
                hour = hour == 0 ? 12 : hour;

                txt.Append(hour.ToString());
                txt.Append(':');
                txt.Append(now.Minute.ToString().PadLeft(2, '0'));
                txt.Append(now.Hour > 12 ? 'p' : 'a');
                txt.Append('m');

                if (i + 1 < count)
                    txt.Append(" - ");
            }

            return txt.ToString();
        }
    }
}
