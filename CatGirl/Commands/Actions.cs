using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using DiscordBot.Util;

namespace DiscordBot.Commands
{
    public class Actions : ModuleBase<SocketCommandContext>
    {
        [Command("kiss")]
        public async Task Kiss()
        {
            await CommandSource.SendAction("kiss", "😽 mwuah", Context);
        }
        
        [Command("lick")]
        public async Task Lick()
        {
            await CommandSource.SendAction("lick", "😽 mwuah", Context);
        }
        
        [Command("hug")]
        public async Task Hug()
        {
            await CommandSource.SendAction("hug", "😽 mwuah", Context);
        }
        
        [Command("slap")]
        public async Task Slap()
        {
            await CommandSource.SendAction("slap", "😽 mwuah", Context);
        }
        
        [Command("cuddle")]
        public async Task Cuddle()
        {
            await CommandSource.SendAction("cuddle", "😽 mwuah", Context);
        }
    }
}