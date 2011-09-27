
namespace robokins.IRC
{
    partial class Client
    {
        public void Who(string mask, bool op)
        {
            send.Write("WHO ");
            send.Write(mask);
            send.WriteLine(op ? " o" : string.Empty);
            send.Flush();
        }

        public void Whois(string target, string mask)
        {
            send.Write("WHOIS ");
            send.Write(target);
            send.Write(' ');
            send.WriteLine(mask);
            send.Flush();
        }

        public void Whowas(string[] nickname, string count, string target)
        {
            Whowas(string.Join(",", nickname), count, target);
        }

        public void Whowas(string nickname, string count, string target)
        {
            send.Write("WHOWAS ");
            send.Write(nickname);
            send.Write(' ');
            send.Write(count);
            send.Write(' ');
            send.WriteLine(target);
            send.Flush();
        }
    }
}
