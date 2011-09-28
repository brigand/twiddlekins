using System;
using System.Reflection;
using System.Text;
using robokins.IRC;
using robokins.Utility;

namespace robokins
{
    partial class Bot
    {
        bool Trigger(ReceivedMessage message)
        {
            #region Variables

            if (!Invoke(message))
                return false;

            bool notify = false;
            bool action = false;
            bool auth = Operators.IndexOf(string.Concat(Delimiter, message.User.Host, Delimiter)) != -1;
            bool search = false;

            string[] command = Utility.Texts.Commands(message.Text);
            string response = string.Empty;
            string def;
            string[] defs;
            String Nick;
            if (!string.IsNullOrEmpty(command[2])) {
                int SpaceIndex = -1;
                SpaceIndex = command[2].IndexOf(" ");
                if (SpaceIndex < 0)
                    Nick = command[2];
                else {
                    Nick = command[2].Substring(0, SpaceIndex);
                    Nick = Nick.Trim();
                }
                if (Nick != "")
                    Nick = ", " + Nick;
            }
            else
                Nick = "";

            #endregion

            switch (command[0].Trim().ToLowerInvariant())
            {
                #region Operator functions
                case "togglerobokins":
                    robokinsbot = robokinsbot ? false : true;
                    response = robokinsbot ? "Deactivating robokins' ! trigger" : "Activating robokins' ! trigger";
                    notify = true;
                    break;
				case "toggleahk4me":
					HTTP.useahk4me = HTTP.useahk4me ? false : true;
					response = HTTP.useahk4me 
							 ? "Future links will be in http://ahk4.me format" 
							 : "Future links will be in the http://bit.ly format";
					notify = true;
					break;
                case "quit":
                case "die":
                    if (auth)
                        quit = true;
                    else {
                        response = "You do not have the authority to make me quit.";
                        notify = true;
                    }
                    break;

                case "quiet":
                case "mute":
                case "m":
                    if (auth) {
                        if (command[1].Length == 0) {
                            response = "Please specify a mask to mute. Enter /msg ChanServ HELP QUIET for more information.";
                            notify = true;
                        }
                        else
                            Message("ChanServ", "QUIET " + Channel + " " + command[1]);
                    }
                    else
                    {
                        response = "You do not have the authority to mute users on the channel.";
                        notify = true;
                    }
                    break;

                case "unquiet":
                case "unmute":
                case "um":
                    if (auth)
                    {
                        if (command[1].Length == 0)
                        {
                            response = "Please specify a mask to unmute. Enter /msg ChanServ HELP UNQUIET for more information.";
                            notify = true;
                        }
                        else
                            Message("ChanServ", "UNQUIET " + Channel + " " + command[1]);
                    }
                    else
                    {
                        response = "You do not have the authority to unmute users on the channel.";
                        notify = true;
                    }
                    break;

                case "say":
                    if (auth)
                    {
                        if (command[2].Length == 0)
                        {
                            response = "You have not told me what to repeat.";
                            notify = true;
                        }
                        else
                            response = command[2];
                    }
                    else
                    {
                        response = "Sorry, I cannot repeat what you said.";
                        notify = true;
                    }
                    break;

                #endregion

                #region Messages

				case "version":
					response = string.Format("Hmmm... Version 0.5, am I.");
					break;
				
                case "status":
                case "stats":
                case "stat":
                case "s":
                    response = string.Format("Uptime: " + Utility.Font.Bold + "{0}" + Utility.Font.Bold, 
                        Utility.Time.ToDays(Math.Abs(Utility.Time.TimeSpanNow().Subtract(start).TotalSeconds)));
                    break;

                case "hello":
                case "who":
                case "hey":
                case "sup":
                case "hi":
                    response = string.Format("'Lo. I'm a helper bot for {0}.", Channel);
                    break;

                case "pastebin":
                case "paste":
                case "pb":
                case "p":
                    response = "Hmmm... At " + PasteURI + ", the official autohotkey pastebin, to share code" + Nick + ", please use.";
                    break;

                case "rules":
                case "rule":
                    response = "Hmm... Stay away from the dahk side, you must.  A PG rated channel, this is: swear or post links to material unsuitable for a younger audience, please do not. " +
                        "Topics, security related, for educational purposes, discussed, can only be.  i.e. no black hat, yes.";
                    break;

                case "help":
                    response = "Hello, help, how can we? If the tutorial at http://www.autohotkey.com/docs/Tutorial.htm, read you have not, please read, yes.";
                    break;
                case "t":
                case "tut":
                case "tutorial":
                    response = "Hmm... for strong the ahk to be with you" + Nick + ", read the tutorial you should.  May the ahk be with you at http://www.autohotkey.com/docs/Tutorial.htm";
                    break;

                case "about":
                    Assembly self = Assembly.GetExecutingAssembly();
                    StringBuilder about = new StringBuilder();
                    about.Append(((AssemblyTitleAttribute)Attribute.GetCustomAttribute(self, typeof(AssemblyTitleAttribute))).Title);
                    about.Append(" v");
                    about.Append(self.GetName().Version.ToString());
                    about.Append(" - ");
                    about.Append(((AssemblyDescriptionAttribute)Attribute.GetCustomAttribute(self, typeof(AssemblyDescriptionAttribute))).Description);
                    about.Append(" by ");
                    about.Append(((AssemblyCompanyAttribute)Attribute.GetCustomAttribute(self, typeof(AssemblyCompanyAttribute))).Company);
                    about.Append(". See ");
                    about.Append(((AssemblyProductAttribute)Attribute.GetCustomAttribute(self, typeof(AssemblyProductAttribute))).Product);
                    response = about.ToString();
                    break;

                #endregion

                #region Fun

                case "flip":
                case "coin":
                    response = string.Concat("flips a coin: " + Utility.Font.Colour, Utility.Texts.Random.Next() % 2 == 0 ? "3HEADS" : "4TAILS");
                    action = true;
                    break;

                case "magicball":
                case "eightball":
                case "8ball":
                case "ball":
                case "8":
                    response = Utility.EightBall.Reponse(command[2]);
                    break;

                case "troutslap":
                case "trout":
                case "slaps":
                case "slap":
                    response = Utility.Slap.Response(command[1].Length == 0 ? message.User.Nick : command[1]);
                    action = true;
                    break;

                #endregion

                #region Utilities

                case "random":
                case "rand":
                    response = string.Format("Random integer: {0}{1}{0} double: {0}{2}{0}",
                        Utility.Font.Bold, Utility.Texts.Random.Next(), Utility.Texts.Random.NextDouble());
                    break;

                case "clock":
                case "time":
                case "tiem":
                    response = Utility.Time.WorldTime();
                    break;

                #endregion

                #region Stubs

                case "c":
                case "calc":
                case "what":
                case "?":
                case "=":
                case "xdcc":
                case "tell":
                case "fmylife":
                case "fml":
                    response = "Sorry, this feature has been disabled.";
                    notify = true;
                    break;

                case "rr":
                case "top10":
                case "quote":
                case "qtop10":
                case "qlatest":
                    return true;

                #endregion

                #region Search

                case "define":
                case "def":
                case "d":
                    if (command[2].Length == 0) {
                        response = "A search term, please specify, hmm.";
                        notify = true;
                        break;
                    }
                    def = Utility.Search.Google.Define(command[2]);
                    if (string.IsNullOrEmpty(def))
                    {
                        response = "Could not find a definition for " + Utility.Font.Bold + command[2] + Utility.Font.Bold;
                        notify = true;
                    }
                    else
                        response = string.Format("{0}{1}{0}: {2}", new string[] { Utility.Font.Underlined, command[2], def });
                    break;

                case "google":
                case "g":
                    if (command[2].Length == 0)
                    {
                        response = "Please specify a search term.";
                        notify = true;
                        break;
                    }
                    defs = Utility.Search.Google.Search(command[2]);
                    if (defs == null)
                    {
                        response = "Could not find a definition for " + Utility.Font.Bold + command[2] + Utility.Font.Bold;
                        notify = true;
                    }
                    else
                        response = string.Format("{0} - {1}", defs[1], HTTP.ShortUrl(defs[0]));
                    break;

                case "user":
                case "u":
                    if (command[2].Length == 0) {
                        response = "Please specify a search term.";
                        notify = true;
                        break;
                    }
                    defs = Utility.Search.AutoHotkey.UserStats(command[2]);
                    if (defs == null) {
                        response = "Could not find user " + Utility.Font.Bold + command[2] + Utility.Font.Bold;
                        notify = true;
                    }
                    else {
                        response = string.Format("{0}{7}{0} made {1}6{2}{3}{2}{1} post{8}; {2}{4}{2} - {5} {1}14 on {6}",
                            new string[] { Utility.Font.Underlined, Utility.Font.Colour, Utility.Font.Bold,
                                defs[0], defs[2], HTTP.ShortUrl(defs[1]), defs[3], command[2], defs[0] == "1" ? string.Empty : "s" });
                    }
                    break;

                case "wikipedia":
                case "wp":
                    if (command[2].Length == 0) {
                        response = "Please specify a search term.";
                        notify = true;
                        break;
                    }
                    defs = Utility.Search.Wiki.Search(command[2]);
                    if (defs == null)
                    {
                        response = "Could not find a definition on the Wikipedia for " + Utility.Font.Bold + command[2] + Utility.Font.Bold;
                        notify = true;
                    }
                    else
                        response = string.Format("{0}{1}{0}: {2} - {3}",
                            new string[] { Utility.Font.Underlined, command[2], defs[0], HTTP.ShortUrl(defs[1]) });
                    break;

                case "urbandictionary":
                case "ud":
                    if (command[2].Length == 0)
                    {
                        response = "Please specify a search term.";
                        notify = true;
                        break;
                    }
                    def = Utility.Search.UrbanDictionary.Search(command[2]);
                    if (def == null)
                    {
                        response = "Could not find a definition on the Urban Dictionary for " + Utility.Font.Bold + command[2] + Utility.Font.Bold;
                        notify = true;
                    }
                    else
                    {
                        def = def.Replace('\r', ' ');
                        def = def.Replace('\n', ' ');
                        if (def.Length > 450)
                            def = string.Concat(def.Substring(0, 450 - 4), " ...");
                        response = string.Format("{0}{1}{0}: {2}", Utility.Font.Underlined, command[2], def);
                    }
                    break;

                case "winapi":
                case "msdn":
                    if (command[2].Length == 0)
                    {
                        response = "Please specify a search term.";
                        response = Utility.Font.Colour + "5An argument is required for this command.";
                        notify = true;
                        break;
                    }
                    defs = Utility.Search.MSDN.Search(command[2]);
                    if (defs == null)
                    {
                        response = "Could not find a definition on the MSDN for " + Utility.Font.Bold + command[2] + Utility.Font.Bold;
                        notify = true;
                    }
                    else
                        response = string.Format("{3}{0}{3}: {2} - {1}",
                            new string[] { defs[0], HTTP.ShortUrl(defs[1]), defs[2], Utility.Font.Bold });
                    break;

                case "y":
                case "yoda":
                case "yodish":
                    if (command[2].Length == 0)
                    {
                        response = "Hmm... translate a null statement, I cannot.";
                        notify = true;
                        break;
                    }
                    defs = Utility.Yodish.Yodize(command[2]);
                    if (defs == null)
                    {
                        response = "Hmm... translate that, I could not.";
                        notify = true;
                    }
                    else
                        response = defs[0];
                    break;

				
				case "wa":
				case "wolfram":
					if (command[2].Length == 0) {
						response = "This site you may find wolframalpha at.  " 
								 + WolframAlpha.SiteUrl 
								 + "  Herh herh herh.";
					}
					else {
						response = string.Format("If you learn about {0} want to, learn about {0}, you will.  Yeesssssss.  {1}" 
								 , command[2]
					             , WolframAlpha.Link(command[2]));
					}
					break;
				
                case "search":
                case "find":
                case "query":
                case "look":
                case "ahk":
                case "ab":
                    search = true;
                    goto default;
				
                default:
                    def = (search ? command[1] : message.Text).Trim().ToLowerInvariant();
                    if (def.Length < 2) 
                        break;

                    const int min = 2;
                    int letters = 0;
                    bool valid = false;
                    for (int i = 0; i < def.Length; i++)
                        if (char.IsLetter(def, i))
                        {
                            if (++letters >= min)
                            {
                                valid = true;
                                break;
                            }
                        }
                    if (!valid)
                        break;

                    defs = Utility.Manual.Lookup(def);
                    if (defs != null && defs.Length == 2)
                        response = string.Format("\x02{0}6{1}{0}\x02: {2}", Utility.Font.Colour, defs[0], HTTP.ShortUrl(Website + defs[1]));
                    else
                    {
                        defs = Utility.Search.Google.AutoHotkey(def);
                        if (defs == null)
                        {
                            response = "Could not find " + Utility.Font.Bold + def + Utility.Font.Bold;
                            notify = true;
                        }
                        else
                            response = string.Format("Hmm.... \"{0}{1}{0}\": {2} found, I have.", Utility.Font.Bold, defs[1], HTTP.ShortUrl(defs[0]));
                    }
                    break;
//                default: // No default trigger.
//                    break;
                #endregion
            }

            #region Message

            if (message.Target == Username)
                notify = true;

            if (notify)
                message.Target = message.User.Nick;

            if (message.Target[0] != '#')
                notify = true;

            if (action && !notify) // since /me doesn't work in notice
                response = Action(response);

            if (response.Length != 0)
                Message(message.Target, response, notify);

            return true;

            #endregion
        }
    }
}
