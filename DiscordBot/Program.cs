using System;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;

namespace DiscordBot
{
    public class Program
    {
        private DiscordSocketClient _client;
	
        public static void Main(string[] args)
            => new Program().MainAsync().GetAwaiter().GetResult();

        public async Task MainAsync()
        {
            _client = new DiscordSocketClient();
            _client.Log += Log;
            await _client.LoginAsync(TokenType.Bot, 
                Environment.GetEnvironmentVariable("ODI4NDkxMjQyNjI3MjY4NjY4.YGqWmA.6t0MD_cBBBJCxTagcgVq6S2cZYQ"));
            await _client.StartAsync();
		
            // Block this task until the program is closed.
            await Task.Delay(-1);
        }
        private Task Log(LogMessage msg)
        {
            Console.WriteLine(msg.ToString());
            return Task.CompletedTask;
        }
    }
}