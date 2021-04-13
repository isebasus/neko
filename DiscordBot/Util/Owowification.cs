namespace DiscordBot.Util
{
    public class Owowify
    {
        public string ReplacMessage(string text)
        {
            return text.Replace("l", "w").Replace("L", "W")
                .Replace("r", "w").Replace("R", "W")
                .Replace("o", "u").Replace("O", "U");
        }
    }
}