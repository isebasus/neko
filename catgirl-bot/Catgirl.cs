using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using catgirl_bot.Util;
using Microsoft.Extensions.DependencyInjection;


namespace catgirl_bot
{
    class Catgirl
    {
        private DiscordSocketClient _client;
        private CommandService _commands;
        private IServiceProvider _services;

        public static void Main(string[] args)
            => new Catgirl().MainAsync().GetAwaiter().GetResult();

        public async Task MainAsync()
        {
            string token = "ODI4NDkxMjQyNjI3MjY4NjY4.YGqWmA.8YhxgWWiFrKRXjDq8PWJsaqzNj4";

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

        private async Task CheckOwo(SocketCommandContext context, SocketUserMessage message)
        {
            var author = (IGuildUser)context.Message.Author;
            var nickname = author.Nickname ?? author.Username;
            if (message != null && message.Author.IsBot)
            {
                return;
            }

            if (context.User is SocketGuildUser user)
            {
                if (user.Roles.Any(x => x.Name == "Owowified"))
                {
                    await Owowify(context, message, nickname);
                }
            }
        }

        private async Task Owowify(SocketCommandContext context, SocketUserMessage message, string nickname)
        {
            try
            {
                await context.Message.DeleteAsync();
            }
            catch
            {
                await context.Channel.SendMessageAsync("Tell your baka administrator to change the permissions in this channel (Manage Messages) so that way I can owowify! ;_;"); 
            }
            var newMessage = Owowification.Owowify(message.Content);
            await context.Channel.SendMessageAsync($"**{nickname}**: {newMessage} {Owowification.Express()}");
        }

        private async Task HandleCommandAsync(SocketMessage arg)
        {
            var message = arg as SocketUserMessage;
            var context = new SocketCommandContext(_client, message);

            if (!(context.Guild.Roles.Any(x => x.Name == "Owowified")))
            {
                await context.Guild.CreateRoleAsync("Owowified", GuildPermissions.None, Color.Default, false, false);
            }

            await CheckOwo(context, message);

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
