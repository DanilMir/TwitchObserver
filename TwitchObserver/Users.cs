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
        public static HashSet<string> Data = new HashSet<string>();
        public static List<string> Online = new List<string>() {};
        
        private static readonly IApiSettings _settings = new ApiSettings()
        {
            ClientId = "gp762nuuoqcoxypju8c569th9wz7q5",
            AccessToken = "6h8p4zccs3o7iuqozsgxitfdz7pykx"
        };
        private static readonly TwitchAPI _twitchApi = new TwitchAPI(settings: _settings);

        
        public static int Length => Data.Count;

        public static void Add(string nickname)
        {
            Data.Add(nickname);
        }

        public static void SetHashSet(HashSet<string> users)
        {
            Data = users;
        }

        public static List<User> GetUserInfo()
        {
            var temp = _twitchApi.Helix.Users.GetUsersAsync(logins: Data.ToList());
            return temp.Result.Users.ToList();
        }
        
        public static async Task GetOnlineUsers()
        {
            Online.Clear();
            var temp = Task.Run(GetUserInfo);
            foreach (var user in temp.Result)
            {
                if (await _twitchApi.V5.Streams.BroadcasterOnlineAsync(user.Id))
                {
                    await Task.Run(() =>
                    {
                        Online.Add(user.DisplayName);
                    });
                }
            }
        }
    }
}