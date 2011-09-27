using System;
using System.Diagnostics;
using System.Web.Services;
using System.Security;
using robokins.IRC;

namespace robokins
{
    partial class Bot
    {
        public Bot(string UID, SecureString PWD, string CHN)
        {
            Username = UID;
            Password = PWD;
            Channel = CHN;
            AppDomain.CurrentDomain.ProcessExit += new EventHandler(delegate(object sender, EventArgs e) { quit = true; });
        }

        [Conditional("DEBUG")]
        void Echo(string text)
        {
            Console.Write(DateTime.Now.ToUniversalTime().ToString("[HH:mm:ss] "));
            Console.WriteLine(text);
        }

        public void Start()
        {
            Connect();
            string line;

            while ((line = client.receive.ReadLine()) != null)
            {
                Echo(line);
                string[] msg = line.Split(boundary, 3);

                if (msg[0] == "PING")
                    client.Pong(msg[1]);
                else if (msg[1] == "PRIVMSG")
                {
                    ReceivedMessage message;
                    try { message = new ReceivedMessage(line); }
                    catch (ArgumentOutOfRangeException) { continue; }

                    Trigger(message);

                    if (quit)
                    {
                        if (paste != null && paste.Enabled)
                            paste.Stop();

                        if (bots != null && bots.Enabled)
                            bots.Stop();

                        if (irc.Connected)
                        {
                            client.Quit("Hmm... Back to the swamp, to meditate, I go.  Yesss!");
                            irc.Client.Close(SendDelay);
                        }
                        break;
                    }
                }
            }
        }
    }
}
