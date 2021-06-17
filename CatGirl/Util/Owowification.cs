using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web.Caching;
using Discord;

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
        public static string Owowify(string text, List<GuildEmote> emotes)
        {
            // Owowify text that is not an emote
            MatchCollection alphaMatches = Regex.Matches(text, @"[a-zA-Z]+(?![^:]*\:)"); // Regex prevent owowify of emoji
            var alphaList = alphaMatches.Cast<Match>().Select(match => match.Value).ToList();

            foreach (string alpha in alphaList)
            { 
                var owowified = alpha.Replace("l", "w").Replace("L", "W")
                    .Replace("r", "w").Replace("R", "W")
                    .Replace("o", "u").Replace("O", "U");

                text = text.Replace(alpha, owowified);
            }

            return text;
        }
    }
}