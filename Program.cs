using System;
using System.IO;
using System.Security;

namespace robokins
{
    partial class Program
    {
        public static void Main()
        {
            const string conf = "robokins.conf";

            string usernm = "";
            var passwd = new SecureString();
            string chanls = "";

            usernm = GetKeyValue(conf, "username");
            passwd = GetKeySecureValue(conf, "password");
            chanls = GetKeyValue(conf, "channels");

            Tests(passwd);

            var bot = new Bot(usernm, passwd, chanls);
            bot.Server = GetKeyValue(conf, "server");
            bot.Port = GetKeyIntValue(conf, "port");
            bot.Website = GetKeyValue(conf, "website");

            bot.Start();
        }

        private static string GetKeyValue(string confFileName, string Key)
        {
            string Value = "";
            if (!File.Exists(confFileName))
                throw new FileNotFoundException("Configuration file not found.", confFileName);
            var table = ConfRead(new StreamReader(confFileName));
            if (table.ContainsKey(Key) && !string.IsNullOrEmpty(table[Key]))
            {
                Value = table[Key];
                table.Remove(Key);
            }
            if (Value.Length == 0)
                throw new ArgumentNullException("Key '" + Key + "' is blank.");
            return Value;
        }
        private static int GetKeyIntValue(string confFileName, string Key)
        {
            String Value = "";
            if (!File.Exists(confFileName))
                throw new FileNotFoundException("Configuration file not found.", confFileName);
            var table = ConfRead(new StreamReader(confFileName));
            if (table.ContainsKey(Key) && !string.IsNullOrEmpty(table[Key]))
            {
                Value = table[Key];
                table.Remove(Key);
            }
            if (Value.Length == 0)
                throw new ArgumentNullException("Key '" + Key + "' is blank.");

            return Convert.ToInt32(Value);
        }
        private static SecureString GetKeySecureValue(string confFileName, string Key)
        {
            var Value = new SecureString();
            if (!File.Exists(confFileName))
                throw new FileNotFoundException("Configuration file not found.", confFileName);
            var table = ConfRead(new StreamReader(confFileName));
            if (table.ContainsKey(Key) && !string.IsNullOrEmpty(table[Key]))
            {
                foreach (char letter in table[Key])
                    Value.AppendChar(letter);
                Value.MakeReadOnly();
                table.Remove(Key);
            }
            if (Value.Length == 0)
                throw new ArgumentNullException("Key '" + Key + "' is blank.");
            return Value;
        }
    }
}
