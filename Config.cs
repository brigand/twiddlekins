using System;
using System.Net.Sockets;
using System.Security;
using robokins.IRC;

namespace robokins
{
    partial class Bot
    {
        public string Server = "irc.freenode.net";
        public int Port = 6667;
        const string Delimiter = ";";
        public string Website = "http://www.autohotkey.net/";

        const string RealName = "IRC Bot";
        const string InitUsermode = "8";
        const string Usermode = "+iR";

        const string Operators = Delimiter + "unaffiliated/twdev-net" + Delimiter;

        const int ReceiveDelay = 100;
        const int SendDelay = ReceiveDelay * 2;
        const int SendMicroDelay = SendDelay / 5;

        const string PasteSync = "";
        const int PasteFreq = 2500;
        const string PasteURI = "http://www.autohotkey.net/paste/";

        TcpClient irc;
        Client client;
        public SecureString Password;
        public string Username;
        public string Channel;
        public static readonly char[] boundary = new char[] { ' ' };
        int sent = 0;
        bool quit = false;
        bool robokinsbot = true;
        TimeSpan start = Utility.Time.TimeSpanNow();
    }
}
