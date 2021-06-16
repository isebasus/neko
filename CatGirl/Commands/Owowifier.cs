using System.Threading.Tasks;
using Discord;
using Discord.Commands;

namespace DiscordBot.Commands
{
    public class Owowifier : ModuleBase<SocketCommandContext>
    {
        /**
         * Uses a highly complex algorithm to programmatically
         * "owofiy" a message and output "owowified" string.
         */
        public static bool _IsOwowify = new bool();
        public static bool IsOwowify 
        {  get { return _IsOwowify; } set { _IsOwowify = value; } }
        
        [Command("owowify")]
        public async Task SetBool(string arg)
        {
            var admin = Context.Guild.GetUser(Context.Guild.Owner.Id);
            var nickname = admin.Nickname ?? admin.Username;
            var user = (IGuildUser) Context.Message.Author;
            if (user.GuildPermissions.Administrator)
            {
                switch (arg.ToLower())
                {
                    case "on":
                        _IsOwowify = true;
                        await ReplyAsync("**OWOWIFY IS ON UwU**");
                        break;
                    case "off":
                        _IsOwowify = false;
                        await ReplyAsync("**OWOWIFY IS OFF D:**");
                        break;
                    default:
                        await ReplyAsync("can u pls tell me if owowify is on or off xd");
                        break;
                }
            }
            else
            {
                await Context.Channel.SendMessageAsync("your not daddy " + nickname);
            }
           

        }
    }
}