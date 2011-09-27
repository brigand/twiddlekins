
namespace robokins.IRC
{
    partial class Client
    {
        public void Away(string text)
        {
            send.Write("AWAY :");
            send.WriteLine(text);
            send.Flush();
        }

        public void Rehash()
        {
            send.WriteLine("REHASH");
            send.Flush();
        }

        public void Die()
        {
            send.WriteLine("DIE");
            send.Flush();
        }

        public void Restart()
        {
            send.WriteLine("RESTART");
            send.Flush();
        }

        public void Summon(string user, string target, string channel)
        {
            send.Write("SUMMON ");
            send.Write(user);
            send.Write(' ');
            send.Write(target);
            send.Write(' ');
            send.WriteLine(channel);
            send.Flush();
        }

        public void Users(string target)
        {
            send.Write("USERS ");
            send.WriteLine(target);
            send.Flush();
        }

        public void Wallops(string text)
        {
            Operwall(text);
        }

        public void Operwall(string text)
        {
            send.Write("WALLOPS :");
            send.WriteLine(text);
            send.Flush();
        }

        public void Userhost(params string[] nickname)
        {
            send.Write("USERHOST ");
            send.WriteLine(string.Join(" ", nickname));
            send.Flush();
        }

        public void Ison(params string[] nickname)
        {
            send.Write("ISON ");
            send.WriteLine(string.Join(" ", nickname));
            send.Flush();
        }
    }
}
