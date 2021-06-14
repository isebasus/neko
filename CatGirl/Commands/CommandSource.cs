using System;
using System.Linq;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Newtonsoft.Json.Linq;
using DiscordBot.Util;

namespace DiscordBot.Commands
{
    public class CommandSource
    {
        public static SocketRole GetRole(ulong id, SocketCommandContext context)
        {
            var user = (IGuildUser) context.Guild.GetUser(828491242627268668);
            var roles = user.RoleIds;
            var mainRole = roles.ElementAt(1);
            return context.Guild.GetRole(mainRole);
        }

        public static async Task SendImage(SocketCommandContext context, SocketRole role, string image, string message)
        {
            EmbedBuilder builder = new EmbedBuilder();
            builder.WithTitle(message);
            builder.WithImageUrl(image);
            builder.WithColor(role.Color);
            await context.Channel.SendMessageAsync("", false, builder.Build());
        }

        private static async Task FormatAction(SocketCommandContext context, SocketRole role, SocketUser user,
            string image, string message, string action)
        {
            EmbedBuilder builder = new EmbedBuilder();
            builder.WithTitle(message);
            builder.WithDescription("**" + context.Message.Author.Username + "** is " + action + " **" + user.Username + "**!");
            builder.WithFooter($"Requested by: {context.Message.Author.Username}#{context.Message.Author.Discriminator}", context.User.GetAvatarUrl());
            builder.WithImageUrl(image);
            builder.WithColor(role.Color);
            await context.Channel.SendMessageAsync("", false, builder.Build());
        }

        public static string ParseJson(string json, string target)
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
        
        public static async Task SendAction(SocketCommandContext context, SocketUser user, string key, string message, string action)
        {
            // Get bots role
            var role = GetRole(828491242627268668, context);
            string json = WebScraper.GetAction(key);
            var objects = JObject.Parse(json);
            
            string image = (String) objects["image"];
            await FormatAction(context, role, user, image, message, action);
        }
    }
}