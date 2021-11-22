using System.Collections.Generic;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using catgirl_bot.Util;
using Discord.WebSocket;

namespace catgirl_bot.Modules.Config
{
    public class ServerConfig : ModuleBase<SocketCommandContext>
    {
        public static Dictionary<ulong, bool> filter = new Dictionary<ulong, bool>();

        [Command("toggleLewd")]
        public async Task RemoveCatgirl()
        {

            if (!((SocketGuildUser)Context.User).GuildPermissions.ManageGuild)
            {
                await ReplyAsync("Nu! u are not a administrator!!!", messageReference: new MessageReference(Context.Message.Id)).ConfigureAwait(false);
            }
            
            if (filter.ContainsKey(Context.Guild.Id))
            {
                filter[Context.Guild.Id] = !filter[Context.Guild.Id];
                await Context.Channel.SendMessageAsync("**Lewd had been toggled to " + filter[Context.Guild.Id] + " !**");
            }
            filter.Add(Context.Guild.Id, false);
            await Context.Channel.SendMessageAsync("**Lewd had been toggled to " + filter[Context.Guild.Id] + " !**");
        }
    }
}