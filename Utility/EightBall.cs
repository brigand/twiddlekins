
namespace robokins.Utility
{
    class EightBall
    {
        static readonly string[] replies = new string[]
        {
            "As I see it, yes.",
            "Ask again later.",
            "Better not tell you now.",
            "Cannot predict now.",
            "Concentrate and ask again.",
            "Don't count on it.",
            "It is certain.",
            "It is decidedly so.",
            "Most likely.",
            "My reply is no.",
            "My sources say no.",
            "Outlook good.",
            "Outlook not so good.",
            "Reply hazy, try again.",
            "Signs point to yes.",
            "Very doubtful.",
            "Without a doubt.",
            "Yes.",
            "Yes - definitely.",
            "You may rely on it.",
        };

        const string error = "Is that a question?";

        public static string Reponse(string query)
        {
            if (query.Length == 0)
                return error;

            bool mark = false;

            for (int i = query.Length - 1; i > -1; i--)
            {
                if (char.IsWhiteSpace(query, i))
                    continue;
                else
                {
                    mark = query[i] == '?';
                    break;
                }
            }

            return mark ? replies[Texts.Random.Next(0, replies.Length)] : error;
        }
    }
}
