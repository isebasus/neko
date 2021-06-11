using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Web.UI;
using System.Web.UI.WebControls;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Newtonsoft.Json.Linq;
using RestSharp;


namespace DiscordBot.Modules
{
    public class Commands : ModuleBase<SocketCommandContext>
    {
        public static String getImage()
        {
            var client = new RestClient();
            client.BaseUrl = new Uri("https://api.thecatapi.com/v1");

            var request = new RestRequest("https://api.thecatapi.com/v1/images/search", Method.GET)
                .AddHeader("x-api-key", "4ca58214-f2e4-4c02-96ae-4c6b862603b6");
            
            var response = client.Execute(request);
            var data = response.Content;
            
            return data;
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
        
        [Command("cat")]
        public async Task Cat()
        {
            string json = getImage();
            string img = "";
            var user = (IGuildUser)Context.Guild.GetUser(828491242627268668);
            var roles = user.RoleIds;
            var mainRole = roles.ElementAt(1);
            var role = Context.Guild.GetRole(mainRole);
            
            Color color = role.Color;
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
            builder.WithColor(color);
            await Context.Channel.SendMessageAsync("", false, builder.Build());
            
        }
    }
}