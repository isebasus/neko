using System.Linq;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Newtonsoft.Json.Linq;
using DiscordBot.Util;

namespace DiscordBot.Modules
{
    public class Commands : ModuleBase<SocketCommandContext>
    {
        private SocketRole GetRole(ulong id, SocketCommandContext context)
        {
            var user = (IGuildUser)Context.Guild.GetUser(828491242627268668);
            var roles = user.RoleIds;
            var mainRole = roles.ElementAt(1);
            return Context.Guild.GetRole(mainRole);
        }
        
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
            var user = (IGuildUser) Context.Message.Author;
            var nickname = user.Nickname ?? user.Username;
            if (user.GuildPermissions.Administrator)
            {
                await Context.Channel.SendMessageAsync("owo I love you " + nickname + " <3");
            }
            else
            {
                await Context.Channel.SendMessageAsync("ew your not " + nickname);
            }
        }

        [Command("catgirl")]
        public async Task Neko()
        {
            string nekoImage = WebScrape.GetNeko();
            var role = GetRole(828491242627268668, Context);
            
            EmbedBuilder builder = new EmbedBuilder();
            builder.WithTitle("🌸 UwU");
            builder.WithImageUrl(nekoImage);
            builder.WithColor(role.Color);
            await Context.Channel.SendMessageAsync("", false, builder.Build());

        }
        
        [Command("cat")]
        public async Task Cat()
        {
            
            string json = WebScrape.GetCat();
            string img = "";
            
            // Get bot role color
            var role = GetRole(828491242627268668, Context);
            JArray array = JArray.Parse(json);
            foreach (JObject obj in array.Children<JObject>())
            {
                foreach (JProperty singleProp in obj.Properties())
                {
                    string name = singleProp.Name;
                    string value = singleProp.Value.ToString(); 

                    if (name == "url")
                    {
                        img = value;
                    }
                }
            }
            
            EmbedBuilder builder = new EmbedBuilder();
            builder.WithTitle("🐱 Meoww");
            builder.WithImageUrl(img);
            builder.WithColor(role.Color);
            await Context.Channel.SendMessageAsync("", false, builder.Build());
            
        }
    }
}