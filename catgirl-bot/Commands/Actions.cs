using System.Linq;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using Discord.Commands;
using catgirl_bot.Util;
using Discord;

namespace catgirl_bot.Commands
{
    public class Actions : ModuleBase<SocketCommandContext>
    {
        private async Task Action(string key, string message, string action, string user)
        {
            if (user == "")
            {
                user = Context.User.Mention;
            } else
            {
                try
                {
                    user = Context.Message.MentionedUsers.First().Mention;
                }
                catch 
                {
                    await ReplyAsync("nu! that is not a real person xd", messageReference: new MessageReference(Context.Message.Id)).ConfigureAwait(false);
                    return;
                }
            }

            var index = Context.Guild.Users.Select(x => x.Mention).IndexOf(user);
            await CommandSource.SendAction(Context, Context.Guild.Users.ElementAt(index), key, message, action);
        }
        [Command("kiss")]
        public async Task Kiss([Remainder]string user = "")
        {
            await Action("kiss", "😽 mwuah", "kissing", user);
        }

        [Command("lick")]
        public async Task Lick([Remainder] string user = "")
        {
            await Action("lick", "👅 lick!", "licking", user);
        }

        [Command("hug")]
        public async Task Hug([Remainder] string user = "")
        {
            await Action("hug", "💞 hug!", "hugging", user);
        }

        [Command("slap")]
        public async Task Slap([Remainder] string user = "")
        {
            await Action("slap", "🔪 slap!", "slapping", user);
        }

        [Command("cuddle")]
        public async Task Cuddle([Remainder] string user = "")
        {
            await Action("cuddle", "💖 cuddle!", "cuddling", user);
        }

        [Command("cry")]
        public async Task Cry([Remainder] string user = "")
        {
            await Action("cry", "💔 crying", "crying becuase of", user);
        }

        [Command("feed")]
        public async Task feed([Remainder] string user = "")
        {
            await Action("feed", "🍜 food!", "feeding", user);
        }

        [Command("tickle")]
        public async Task tickle([Remainder] string user = "")
        {
            await Action("tickle", "✨ tickle!", "tickling", user);
        }
    }
}