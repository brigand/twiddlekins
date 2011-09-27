
namespace robokins.IRC
{
    partial class Client
    {
        public void Join(string[] channel, params string[] key)
        {
            Join(string.Join(",", channel), string.Join(",", key));
        }

        public void Join(string channel, string key)
        {
            RawMessage("JOIN", channel, key, false);
        }

        public void Part(string[] channel, string message)
        {
            Part(string.Join(",", channel), message);
        }

        public void Part(string channel, string message)
        {
            RawMessage("PART", channel, message, true);
        }

        public void Mode(string target, string flags)
        {
            RawMessage("MODE", target, flags, false);
        }

        public void Topic(string channel, string topic)
        {
            RawMessage("TOPIC", channel, topic, true);
        }

        public void Names(string[] channel, string target)
        {
            Names(string.Join(",", channel), target);
        }

        public void Names(string channel, string target)
        {
            RawMessage("NAMES", channel, target, false);
        }

        public void List(string[] channel, string target)
        {
            List(string.Join(",", channel), target);
        }

        public void List(string channel, string target)
        {
            RawMessage("LIST", channel, target, false);
        }

        public void Invite(string nickname, string channel)
        {
            send.Write("INVITE ");
            send.Write(nickname);
            send.Write(' ');
            send.WriteLine(channel);
            send.Flush();
        }

        public void Kick(string[] channel, string[] user, string comment)
        {
            Kick(string.Join(",", channel), string.Join(",", user), comment);
        }

        public void Kick(string channel, string user, string comment)
        {
            send.Write("KICK ");
            send.Write(channel);
            send.Write(' ');
            send.Write(user);
            send.Write(" :");
            send.WriteLine(comment);
            send.Flush();
        }


        void RawMessage(string command, string channel, string text, bool delimit)
        {
            send.Write(command);
            send.Write(' ');
            send.Write(channel);
            send.Write(' ');
            if (delimit)
                send.Write(':');
            send.WriteLine(text);
            send.Flush();
        }
        public void Names(string channel)
        {
            send.Write("NAMES ");
            send.Write(channel);
            send.WriteLine();
            send.Flush();
        }
    }
}
