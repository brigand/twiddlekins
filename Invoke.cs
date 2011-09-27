using System;
using robokins.IRC;

namespace robokins
{
    partial class Bot
    {
        bool Invoke(ReceivedMessage message) {
            message.Text = message.Text.Trim();

            if (message.User.Host == "services." || message.User.Ident == "freenode" || message.User.Nick.Length == 0 || message.Text.Length == 0)
                return false;

            bool query = message.Target[0] != '#';
            char first = message.Text[0];
            string word = message.Text.Split(Bot.boundary, 2, StringSplitOptions.RemoveEmptyEntries)[0].ToLowerInvariant();
            string nickLow = Username.ToLowerInvariant();

            if ((first == '!') && (!robokinsbot))
                first = '~';
            if (first == '~') {
                int remove = 1;
                word = word.Substring(1);
                if (word.Equals(Channel, StringComparison.OrdinalIgnoreCase) || word == nickLow)
                    remove += word.Length;
                message.Text = message.Text.Substring(remove).Trim();
                return message.Text.Length != 0;
            }
            else if (word.IndexOf(nickLow) == 0) {
                bool range = Username.Length < word.Length;
                bool bound = range ? !char.IsLetterOrDigit(word, Username.Length) : false;
                if (!range || (range && bound)) {
                    message.Text = message.Text.Substring(Username.Length + (bound ? 1 : 0)).Trim();
                    return message.Text.Length != 0;
                }
                else
                    return query;
            }
            else
                return query;
        }
    }
}
