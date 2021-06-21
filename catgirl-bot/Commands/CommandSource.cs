using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Newtonsoft.Json.Linq;
using catgirl_bot.Util;

namespace catgirl_bot.Commands
{
    public class CommandSource
    {
        // Set role color to custom color if default
        public static Color CheckColor(Color color)
        {
            if (color == Color.Default || color == null)
            {
                return new Color(88, 101, 242);
            }

            return color;
        }

        // Gets main role of a user
        public static SocketRole GetMainRole(ulong id, SocketCommandContext context)
        {
            var bot = (IGuildUser)context.Guild.GetUser(id);

            if (bot != null)
            {
                IDictionary<int, SocketRole> socketRoles = new Dictionary<int, SocketRole>();
                foreach (ulong roleId in bot.RoleIds)
                {
                    // Set each role to SocketRole array
                    SocketRole role = context.Guild.GetRole(roleId);
                    socketRoles.Add(role.Position, role);
                }
                // Get main role
                var mainRole = socketRoles[socketRoles.Keys.Max()].Id;
                return context.Guild.GetRole(mainRole);
            }
            return null;
        }

        // Sends an images with EmbedBuilders
        public static async Task SendImage(SocketCommandContext context, SocketRole role, string image, string message)
        {
            Color color = CheckColor(role.Color);
            EmbedBuilder builder = new EmbedBuilder();
            builder.WithTitle(message);
            builder.WithImageUrl(image);
            builder.WithFooter($"Requested by: {context.Message.Author.Username}#{context.Message.Author.Discriminator}", context.User.GetAvatarUrl());
            builder.WithColor(color);

            await context.Channel.SendMessageAsync("", false, builder.Build());
        }

        // Sends Actions with EmbedBuilders
        public static async Task FormatAction(SocketCommandContext context, SocketRole role, SocketUser user,
            string image, string message, string action)
        {
            Color color = CheckColor(role.Color);
            EmbedBuilder builder = new EmbedBuilder();
            builder.WithTitle(message);
            builder.WithDescription("**" + context.Message.Author.Username + "** is " + action + " **" + user.Username + "**!");
            builder.WithFooter($"Requested by: {context.Message.Author.Username}#{context.Message.Author.Discriminator}", context.User.GetAvatarUrl());
            builder.WithImageUrl(image);
            builder.WithColor(color);

            await context.Channel.SendMessageAsync("", false, builder.Build());
        }

        public static async Task SendAction(SocketCommandContext context, SocketUser user, string key, string message, string action)
        {
            // Get bots role
            var info = await context.Client.GetApplicationInfoAsync();
            var role = GetMainRole(info.Id, context);
            string json = WebScraper.GetAction(key);
            var objects = JObject.Parse(json);

            string image = (String)objects["image"];

            await FormatAction(context, role, user, image, message, action);
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
    }
}