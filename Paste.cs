using System.Diagnostics;
using System.IO;
using System.Timers;

namespace robokins
{
    partial class Bot
    {
        Timer paste = null;

        [Conditional("PASTE")]
        void PasteSetup()
        {
            if (!Directory.Exists(PasteSync))
                return;

            var dir = new DirectoryInfo(PasteSync);

            var check = new ElapsedEventHandler(delegate(object s, ElapsedEventArgs e)
            {
                foreach (FileInfo file in dir.GetFiles())
                {
                    string id = file.Name, nick = File.ReadAllText(file.FullName), info = string.Empty;

                    int z = nick.IndexOf(' ');
                    if (z != -1)
                    {
                        info = string.Concat(" - ", nick.Substring(z + 1));
                        nick = nick.Substring(0, z);
                    }

                    Message(Channel, string.Format("{0} pasted {1}{2}{3}", new string[] { nick, PasteURI, id, info }));
                    file.Delete();
                }
            });

            paste = new Timer(PasteFreq);

            foreach (FileInfo file in dir.GetFiles())
                file.Delete();

            paste.Elapsed += check;
            paste.Start();
        }
    }
}
