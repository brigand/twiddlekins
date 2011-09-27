using System.Text;

namespace robokins.IRC
{
    class User
    {
        string nick;
        string ident;
        string host;

        public User(string alias)
        {
            int x = alias.IndexOf('!');
            nick = x == -1 ? string.Empty : alias.Substring(0, x);

            x++;
            int y = alias.IndexOf('@', x);
            ident = y == -1 ? string.Empty : alias.Substring(x, y - x);

            y++;
            host = alias.Substring(y);
        }

        public string Nick
        {
            get { return nick; }
        }

        public string Ident
        {
            get { return ident; }
        }

        public string Host
        {
            get { return host; }
        }

        public override string ToString()
        {
            var buf = new StringBuilder(Nick.Length + ident.Length + host.Length + 2);
            if (Nick.Length != 0)
            {
                buf.Append(Nick);
                buf.Append('!');
            }
            if (ident.Length != 0)
            {
                buf.Append(ident);
                buf.Append('@');
            }
            buf.Append(host);
            return buf.ToString();
        }
    }
}
