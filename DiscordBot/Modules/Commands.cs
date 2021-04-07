using System;
using System.Collections.Generic;
using System.Web;
using System.Reflection;
using System.Security.Principal;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
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
        
        [Command("cat")]
        public async Task Cat()
        {
            string json = getImage();
            string img = "";
            
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
                    //Do something with name and value
                    //System.Windows.MessageBox.Show("name is "+name+" and value is "+value);
                }
            }
            
            EmbedBuilder builder = new EmbedBuilder();

            builder.WithTitle("UWWWUU CATT");
            builder.WithImageUrl(img);

            builder.WithColor(new Color(255,  159, 159));
            await Context.Channel.SendMessageAsync("", false, builder.Build());       
            
        }
    }
}