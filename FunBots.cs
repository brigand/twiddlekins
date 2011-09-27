using System;
using System.Diagnostics;
using System.Timers;

namespace robokins
{
    partial class Bot
    {
        Timer bots = null;

        [Conditional("FUNBOTS")]
        void FunBotsSetup()
        {
            var random = new Random();
            bots = new Timer(60 * 60 * 1000 / 2);
            bots.Elapsed += new ElapsedEventHandler(delegate(object sender, ElapsedEventArgs e)
            {
                Message("lolikins", "!stuff");
            });
            bots.Start();
        }
    }
}
