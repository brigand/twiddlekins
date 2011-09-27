using System;
using System.Net.Sockets;
using System.Threading;
using robokins.IRC;

namespace robokins
{
    partial class Bot
    {
        void Connect()
        {
            irc = new TcpClient(Server, Port);
            irc.SendBufferSize = Client.BufferSize;
            irc.ReceiveBufferSize = Client.BufferSize;

            client = new Client(irc.GetStream());
            client.Pass(Password);
            client.User(Username, InitUsermode, RealName);
            client.Nick(Username);
            client.Mode(Username, Usermode);
            
            Thread.Sleep(SendDelay * 3);

            client.Join(Channel, string.Empty);

            PasteSetup();
            FunBotsSetup();
        }
    }
}
