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
            if (Context.Guild.Id != 881050555156406272)
            {
                string nekoImage = WebScraper.GetNeko();
                var role = CommandSource.GetMainRole(828491242627268668, Context);
                await CommandSource.SendImage(Context, role, nekoImage, "🌸 uwu");
            }
        }
        
        [Command("cat")]
        public async Task Cat()
        {
            var role = CommandSource.GetMainRole(828491242627268668, Context);
            
            string json = WebScraper.GetCat();
            string img = CommandSource.ParseJson(json, "url");
            await CommandSource.SendImage(Context, role, img, "🐱 Meoww-");
        }
        
        [Command("help")]
        public async Task Help()
        {
            var role = CommandSource.GetMainRole(828491242627268668, Context);
            var bot = Context.Guild.GetUser(828491242627268668);
            Color color = CommandSource.CheckColor(role.Color);
            EmbedBuilder builder = new EmbedBuilder();
            builder.WithAuthor($"{bot.Username}#{bot.DiscriminatorValue}", bot.GetAvatarUrl());
            builder.WithTitle("🐈 Help - Commands");
            builder.AddField("Owowify", "`~owowify [on] [off]`", true);
            builder.AddField("Cat", "`~cat`", true);
            builder.AddField("Catgirl", "`~catgirl`", true);
            builder.AddField("Hug", "`~hug [@user]`", true);
            builder.AddField("Kiss", "`~kiss [@user]`", true);
            builder.AddField("Cuddle", "`~cuddle [@user]`", true);
            builder.AddField("Slap", "`~slap [@user]`", true);
            builder.AddField("Lick", "`~lick [@user]`", true);
            builder.WithColor(color);
            
            await Context.Channel.SendMessageAsync("", false, builder.Build());
        }
    }
}
