using System;

namespace DiscordBot.Util
{
    public class Owowification
    {
        private static string[] expressions = {
            ">_<", ":3", "ʕʘ‿ʘʔ", ":D", "._.",
            ";3", "xD", "ㅇㅅㅇ", "(人◕ω◕)",
            ">_>", "ÙωÙ", "UwU", "OwO", ":P",
            "(◠‿◠✿)", "^_^", ";_;", "XDDD",
            "x3", "(• o •)", "<_<"
        };
        public static string Express()
        {
            Random rand = new Random();
            int random = rand.Next(0, 20);
            return expressions[random];
        }
        public static string Owowify(string text)
        {
            return text.Replace("l", "w").Replace("L", "W")
                .Replace("r", "w").Replace("R", "W")
                .Replace("o", "u").Replace("O", "U");
        }
    }
}