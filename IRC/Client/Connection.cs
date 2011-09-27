using System;
using System.Runtime.InteropServices;
using System.Security;

namespace robokins.IRC
{
    partial class Client
    {
        public void Pass(SecureString password)
        {
            send.Write("PASS ");
            IntPtr bstr = Marshal.SecureStringToBSTR(password);
            send.WriteLine(Marshal.PtrToStringUni(bstr));
            try { Marshal.ZeroFreeBSTR(bstr); }
            catch (MissingMethodException) { }
            send.Flush();
        }

        public void Nick(string nickname)
        {
            send.Write("NICK ");
            send.WriteLine(nickname);
            send.Flush();
        }

        public void User(string user, string mode, string realname)
        {
            send.Write("USER ");
            send.Write(user);
            send.Write(' ');
            send.Write(mode);
            send.Write(" * :");
            send.WriteLine(realname);
            send.Flush();
        }

        public void Oper(string name, SecureString password)
        {
            send.Write("OPER ");
            send.Write(name);
            send.Write(' ');
            IntPtr bstr = Marshal.SecureStringToBSTR(password);
            send.WriteLine(Marshal.PtrToStringUni(bstr));
            Marshal.ZeroFreeBSTR(bstr);
            send.Flush();
        }

        public void Service(string nickname, string distribution, string info)
        {
            send.Write("SERVICE ");
            send.Write(nickname);
            send.Write(" * ");
            send.Write(distribution);
            send.Write(" 0 0 :");
            send.WriteLine(info);
            send.Flush();
        }

        public void Quit(string message)
        {
            send.Write("QUIT :");
            send.WriteLine(message);
            send.Flush();
        }

        public void Squit(string server, string comment)
        {
            send.Write("SQUIT ");
            send.Write(server);
            send.Write(" :");
            send.WriteLine(comment);
            send.Flush();
        }
    }
}
