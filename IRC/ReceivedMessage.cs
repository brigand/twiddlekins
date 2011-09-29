using System;
using System.Text;
using robokins;

namespace robokins.IRC
{
    class ReceivedMessage
    {
        bool notice;
        User user;
        string target;
        string text;
		bool auth;

        const string privmsgTxt = "PRIVMSG";
        const string noticeTxt = "NOTICE";

        public ReceivedMessage(string query)
        {
            if (query.Length > Client.BufferSize || query.Length < (":x!y " + noticeTxt + " z :").Length || query[0] != ':')
                throw new ArgumentOutOfRangeException();

            int z;

            // RFC example:
            //   :Angel!wings@irc.org PRIVMSG Wiz :Are you receiving this message ?

            z = query.IndexOf(':', 1);
            if (z == -1)
                throw new ArgumentOutOfRangeException();
            text = z + 1 >= query.Length ? string.Empty : query.Substring(z + 1);

            string[] values = query.Substring(1, z - 2).Split(Bot.boundary);

            if (values.Length != 3)
                throw new ArgumentOutOfRangeException();

            user = new User(values[0]);

            switch (values[1].ToUpperInvariant())
            {
                case privmsgTxt:
                    notice = false;
                    break;
                case noticeTxt:
                    notice = true;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
			
            target = values[2];
        }
		
		public bool HasAuth
		{
			get { return auth; }
			
			set { auth = value; }
		}
		
        public bool Notice
        {
            get { return notice; }
        }

        public User User
        {
            get { return user; }
        }

        public string Target
        {
            get { return target; }
            set { target = value; }
        }

        public string Text
        {
            get { return text; }
            set { text = value; }
        }

        public override string ToString()
        {
            var buf = new StringBuilder(Client.BufferSize);
            char bound = Bot.boundary[0];
            buf.Append(':');
            buf.Append(user.ToString());
            buf.Append(bound);
            buf.Append(notice ? noticeTxt : privmsgTxt);
            buf.Append(bound);
            buf.Append(target);
            buf.Append(bound);
            buf.Append(':');
            buf.Append(text);
            if (buf.Length > Client.BufferSize)
                throw new ArgumentOutOfRangeException();
            return buf.ToString();
        }
    }
}
