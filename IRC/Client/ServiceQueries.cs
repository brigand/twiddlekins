
namespace robokins.IRC
{
    partial class Client
    {
        public void Servlist(string mask, string type)
        {
            send.Write("SERVLIST ");
            send.Write(mask);
            send.Write(' ');
            send.WriteLine(type);
            send.Flush();
        }

        public void Squery(string service, string text)
        {
            send.Write("SQUERY ");
            send.Write(service);
            send.Write(" :");
            send.WriteLine(text);
            send.Flush();
        }
    }
}
