using System.IO;

namespace robokins.IRC
{
    partial class Client
    {
        public StreamWriter send;
        public StreamReader receive;

        public Client(Stream Client)
        {
            send = new StreamWriter(Client);
            send.NewLine = Linefeed;
            receive = new StreamReader(Client);
        }
    }
}
