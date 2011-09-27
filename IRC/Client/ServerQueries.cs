
namespace robokins.IRC
{
    partial class Client
    {
        public void Motd(string target)
        {
            send.Write("MOTD :");
            send.WriteLine(target);
            send.Flush();
        }

        public void Lusers(string mask, string target)
        {
            send.Write("LUSERS ");
            send.Write(mask);
            send.Write(' ');
            send.WriteLine(target);
            send.Flush();
        }

        public void Version(string target)
        {
            send.Write("VERSION ");
            send.WriteLine(target);
            send.Flush();
        }

        public void Stats(string query, string target)
        {
            send.Write("STATS ");
            send.Write(' ');
            send.Write(query);
            send.Write(' ');
            send.WriteLine(target);
            send.Flush();
        }

        public void Links(string server, string mask)
        {
            send.Write("LINKS ");
            send.Write(server);
            send.Write(' ');
            send.WriteLine(mask);
            send.Flush();
        }

        public void Time(string target)
        {
            send.Write("TIME ");
            send.WriteLine(target);
            send.Flush();
        }

        public void Connect(string server, string port, string remote)
        {
            send.Write("CONNECT ");
            send.Write(server);
            send.Write(' ');
            send.Write(port);
            send.Write(' ');
            send.WriteLine(remote);
            send.Flush();
        }

        public void Trace(string target)
        {
            send.Write("TRACE ");
            send.WriteLine(target);
            send.Flush();
        }

        public void Admin(string target)
        {
            send.Write("ADMIN ");
            send.WriteLine(target);
            send.Flush();
        }

        public void Info(string target)
        {
            send.Write("INFO ");
            send.WriteLine(target);
            send.Flush();
        }

        public void Kill(string nickname, string comment)
        {
            send.Write("KILL ");
            send.Write(nickname);
            send.Write(" :");
            send.WriteLine(comment);
            send.Flush();
        }
    }
}
