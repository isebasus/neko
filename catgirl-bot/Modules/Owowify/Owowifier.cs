using System.Linq;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;

namespace catgirl_bot.Commands
{
    public class Owowifier : ModuleBase<SocketCommandContext>
    {
        /**
         * Uses a highly complex algorithm to programmatically
         * "owofiy" a message and output "owowified" string.
         */

        [Command("owowify")]
        public async Task SetBool(string arg = null)
        {
            var user = (IGuildUser)Context.Message.Author;
            var role = Context.Guild.Roles.FirstOrDefault(x => x.Name == "Owowified");

            if (arg != null)
            {
                switch (arg.ToLower())
                {
                    case "on":
                        await user.AddRoleAsync(role);
                        await ReplyAsync("**OWOWIFY is on for " + user.Mention + " UwU**", messageReference: new MessageReference(Context.Message.Id)).ConfigureAwait(false);
                        break;
                    case "off":
                        await user.RemoveRoleAsync(role);
                        await ReplyAsync("**OWOWIFY is off for " + user.Mention + " D:**");
                        break;
                    default:
                        await ReplyAsync("can u pls tell me if owowify is on or off xd", messageReference: new MessageReference(Context.Message.Id)).ConfigureAwait(false);
                        break;
                }
            }
            else
            {
                await ReplyAsync("can u pls tell me if owowify is on or off xd", messageReference: new MessageReference(Context.Message.Id)).ConfigureAwait(false);
            }


        }
    }
}