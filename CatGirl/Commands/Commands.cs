using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using DiscordBot.Util;

namespace DiscordBot.Commands
{
    public class Commands : ModuleBase<SocketCommandContext>
    {
        [Command("goodnight")]
        public async Task Night()
        {
            var author = (IGuildUser)Context.Message.Author;
            var user = author.Nickname ?? author.Username;
            await Context.Channel.SendMessageAsync("uwu gn " + user);
        }
        
        [Command("simp")]
        public async Task Catgirl()
        {
            var admin = Context.Guild.GetUser(Context.Guild.Owner.Id);
            var nickname = admin.Nickname ?? admin.Username;
            var user = (IGuildUser) Context.Message.Author;
            if (user.GuildPermissions.Administrator)
            {
                await Context.Channel.SendMessageAsync("owo I love you " + nickname + " <3");
            }
            else
            {
                await Context.Channel.SendMessageAsync("ew you're not " + nickname);
            }
        }
        
        [Command("catgirl")]
        public async Task Neko()
        {
            string nekoImage = WebScraper.GetNeko();
            var role = CommandSource.GetRole(828491242627268668, Context);
            await CommandSource.SendImage(Context, role, nekoImage, "🌸 UwU");
        }
        
        [Command("cat")]
        public async Task Cat()
        {
            // Get bot's role
            var role = CommandSource.GetRole(828491242627268668, Context);
            
            string json = WebScraper.GetCat();
            string img = CommandSource.ParseJson(json, "url");
            await CommandSource.SendImage(Context, role, img, "🐱 Meoww");
        }
    }
}