using System;
using System.Reflection;
using System.Runtime.Remoting.Contexts;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using DiscordBot.Modules;
using DiscordBot.Util;
using Microsoft.Extensions.DependencyInjection;

namespace DiscordBot
{
    public class Program
    {
        private DiscordSocketClient _client;
        private CommandService _commands;
        private IServiceProvider _services;
        
        public IMessageChannel TextingChannel { get; private set; } = null;
	
        public static void Main(string[] args)
            => new Program().MainAsync().GetAwaiter().GetResult();

        public async Task MainAsync()
        {
            string token = "ODI4NDkxMjQyNjI3MjY4NjY4.YGqWmA.Z4oKUGNe51sSaHPrCyk_wgJDxvA";
            
            _client = new DiscordSocketClient();
            _commands = new CommandService();

            _services = new ServiceCollection()
                .AddSingleton(_client)
                .AddSingleton(_commands)
                .BuildServiceProvider();
            
            _client.Log += Log;
            
            await RegisterCommandsAsync();

            await _client.LoginAsync(TokenType.Bot, 
                token);
            await _client.StartAsync();
		
            // Block this task until the program is closed.
            await Task.Delay(-1);
        }
        
        public async Task RegisterCommandsAsync()
        {
            _client.MessageReceived += HandleCommandAsync;
            await _commands.AddModulesAsync(Assembly.GetEntryAssembly(), _services);
            
        }

        private async Task HandleCommandAsync(SocketMessage arg)
        {
            bool isOwo = Owowifier.IsOwowify;
            var message = arg as SocketUserMessage;
            var context = new SocketCommandContext(_client, message);
            var author = (IGuildUser)context.Message.Author;
            var nickname = author.Nickname ?? author.Username;
            if (message.Author.IsBot)
            {
                return;
            }

            if (isOwo)
            {
                var newMessage = Owowification.Owowify(message.Content);
                await context.Message.DeleteAsync();
                await context.Channel.SendMessageAsync("**" + nickname + "**: " + newMessage + " " + Owowification.Express());
            }

            int argPos = 0;
            if (message.HasStringPrefix("~", ref argPos))
            {
                var result = await _commands.ExecuteAsync(context, argPos, _services);
                if (!result.IsSuccess) Console.WriteLine(result.ErrorReason);
            }
        }

        private Task Log(LogMessage msg)
        {
            Console.WriteLine(msg.ToString());
            return Task.CompletedTask;
        }
        
        
    }
}