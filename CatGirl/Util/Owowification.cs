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
            Regex emoteRegex = new Regex(@":[^:\s]*(?:::[^:\s]*)*:"); // Regex to find emoji 
            Regex alphaRegex = new Regex(@"[a-zA-Z]+(?![^<]*\>)"); // Regex prevent owowify of emoji
            Match match = emoteRegex.Match(text);

            if (match.Success)
            {
                foreach (string emote in match.Groups)
                {
                    string emoteName = emote.Substring(1, emote.Length - 2);
                    var value = emotes.First(x => x.Name == emoteName);
                    text = text.Replace(emote, $"<:{value.Name}:{value.Id}>");
                }
            }
            
            // Owowify text that is not an emote
            Match owo = alphaRegex.Match(text);
            if (owo.Success)
            {
                foreach (string alpha in owo.Groups)
                {
                    var owowified = alpha.Replace("l", "w").Replace("L", "W")
                        .Replace("r", "w").Replace("R", "W")
                        .Replace("o", "u").Replace("O", "U");

                    text = text.Replace(alpha, owowified);
                }
            }

            return text;
        }
    }
}