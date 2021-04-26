using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using TwitchLib.Api;
using TwitchLib.Api.Core;
using TwitchLib.Api.Core.Interfaces;
using TwitchLib.Api.Helix.Models.Users.GetUsers;

namespace TwitchObserver
{
    public static class Users
    {
        public static List<Streamer> Data = new List<Streamer>();
        public static List<string> OldOnline = new List<string>() { };
        public static List<string> NowOnline = new List<string>() { };

        private static readonly IApiSettings _settings = new ApiSettings()
        {
            ClientId = "gp762nuuoqcoxypju8c569th9wz7q5",
            AccessToken = "6h8p4zccs3o7iuqozsgxitfdz7pykx"
        };

        private static readonly TwitchAPI _twitchApi = new TwitchAPI(settings: _settings);

        public static JsonInteraction JSON = new JsonInteraction();


        public static int Length => Data.Count;

        public static void Add(string nickname)
        {
            Data.Add(new Streamer(nickname, Platform.Twitch));
            JSON.UpdateUserList(Data);
        }

        public static void SetHashSet(HashSet<string> users)
        {
            Data.Clear();
            foreach (var user in users)
            {
                Data.Add(new Streamer(user, Platform.Twitch));
            }
            JSON.UpdateUserList(Data);
        }


        public static void UpdateStreamerList()
        {
            Data = JSON.GetCurrentStreamersList();
        }

        public static List<string> ToStringList(this List<Streamer> streamers)
        {
            var temp = new List<string>();
            foreach (var streamer in streamers)
            {
                temp.Add(streamer.Nickname);
            }
            
            return temp;
        }
        
        public static List<User> GetUserInfo()
        {
            var temp = _twitchApi.Helix.Users.GetUsersAsync(logins: Data.ToStringList());
            return temp.Result.Users.ToList();
        }

        public static async Task GetOnlineUsers()
        {
            NowOnline.Clear();
            var online = new List<string>();
            var temp = Task.Run(GetUserInfo);
            foreach (var user in temp.Result)
            {
                if (await _twitchApi.V5.Streams.BroadcasterOnlineAsync(user.Id))
                {
                    if (!OldOnline.Contains(user.DisplayName))
                    {
                        await Task.Run(() => { NowOnline.Add(user.DisplayName); });
                    }
                    await Task.Run(() => { online.Add(user.DisplayName); });
                }
            }

            OldOnline = new List<string>(online);
        }
    }
}