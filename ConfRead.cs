using System.Collections.Generic;
using System.IO;

namespace robokins
{
    partial class Program
    {
        static Dictionary<string, string> ConfRead(TextReader source)
        {
            const char comment = '#';
            const char assign = '=';

            var table = new Dictionary<string, string>();
            string line;

            while ((line = source.ReadLine()) != null)
            {
                line = line.Trim();

                if (line.Length == 0 || line[0] == comment)
                    continue;

/*
                int z = 0;
                while ((z = line.IndexOf(comment)) != -1)
                {
                    if (char.IsWhiteSpace(line, z - 1))
                        break;
                }

                if (z != -1)
                    line = line.Substring(0, z - 1);
*/
                string key, value = null;

                if (line.IndexOf(assign) == -1)
                    key = line.Trim();
                else
                {
                    string[] parts = line.Split(new char[] { assign }, 2);
                    key = parts[0].Trim();
                    value = parts[1].Trim();
                }

                if (table.ContainsKey(key))
                    table[key] = value;
                else
                    table.Add(key, value);
            }

            return table;
        }
    }
}
