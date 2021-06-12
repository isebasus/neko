using System;
using System.CodeDom;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Newtonsoft.Json.Linq;
using DiscordBot.Util;
using RestSharp;

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

        private async Task SendImage(SocketCommandContext context, SocketRole role, string image, string message)
        {
            EmbedBuilder builder = new EmbedBuilder();
            builder.WithTitle(message);
            builder.WithImageUrl(image);
            builder.WithColor(role.Color);
            await context.Channel.SendMessageAsync("", false, builder.Build());
        }

        private string ParseJson(string json, string target)
        {
            JArray array = JArray.Parse(json);
            foreach (JObject obj in array.Children<JObject>())
            {
                foreach (JProperty singleProp in obj.Properties())
                {
                    string name = singleProp.Name;
                    string value = singleProp.Value.ToString(); 

                    if (name == target)
                    {
                        return value;
                    }
                }
            }
            return "";
        }

        private async Task SendImage(string action, string message)
        {
            // Get bots role
            var role = GetRole(828491242627268668, Context);
            
            string kissJson = WebScrape.GetAction(action);
            var objects = JObject.Parse(kissJson);
            
            string kissImage = (String) objects["image"];
            await SendImage(Context, role, kissImage, message);
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

        [Command("kiss")]
        public async Task Kiss()
        {
            await SendImage("kiss", "😽 mwuah");
        }
        
        [Command("lick")]
        public async Task Lick()
        {
            await SendImage("lick", "😽 mwuah");
        }
        
        [Command("hug")]
        public async Task Hug()
        {
            await SendImage("hug", "😽 mwuah");
        }
        
        [Command("slap")]
        public async Task Slap()
        {
            await SendImage("slap", "😽 mwuah");
        }
        
        [Command("cuddle")]
        public async Task Cuddle()
        {
            await SendImage("cuddle", "😽 mwuah");
        }
        
        [Command("catgirl")]
        public async Task Neko()
        {
            string nekoImage = WebScrape.GetNeko();
            var role = GetRole(828491242627268668, Context);
            await SendImage(Context, role, nekoImage, "🌸 UwU");
        }
        
        [Command("cat")]
        public async Task Cat()
        {
            // Get bot's role
            var role = GetRole(828491242627268668, Context);
            
            string json = WebScrape.GetCat();
            string img = ParseJson(json, "url");
            await SendImage(Context, role, img, "🐱 Meoww");
        }
    }
}