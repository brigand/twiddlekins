
namespace robokins.Utility
{
    class Slap
    {
        public static string Response(string nickname)
        {
            const string trout = " trout";

            const string rainbow = " " + Font.Bold +
                Font.Colour + "4r" +
                Font.Colour + "7a" +
                Font.Colour + "8i" +
                Font.Colour + "9n" +
                Font.Colour + "3b" +
                Font.Colour + "2o" +
                Font.Colour + "6w" +
                Font.Colour + Font.Bold;

            return string.Concat("slaps ", nickname, " around a bit with a large", Texts.Random.Next(0, 3) == 1 ? rainbow + trout : trout);
        }
    }
}
