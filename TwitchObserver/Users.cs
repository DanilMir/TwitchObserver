using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using TwitchLib.Api;
using TwitchLib.Api.Core;
using TwitchLib.Api.Core.Interfaces;

namespace TwitchObserver
{
    public static class Users
    {
        private static HashSet<string> _data = new HashSet<string>();
        public static List<string> Online = new List<string>() {};
        
        private static readonly IApiSettings _settings = new ApiSettings()
        {
            ClientId = "gp762nuuoqcoxypju8c569th9wz7q5",
            AccessToken = "6h8p4zccs3o7iuqozsgxitfdz7pykx"
        };
        private static TwitchAPI _twitchApi = new TwitchAPI(settings: _settings);

        
        public static int Length
        {
            get { return _data.Count; }
        }

        public static void Add(string nickname)
        {
            _data.Add(nickname);
        }

        public static void SetHashSet(HashSet<string> users)
        {
            _data = users;
        }

        public static async void GetOnlineUsers()
        {
            var temp = _twitchApi.Helix.Users.GetUsersAsync(logins: _data.ToList());
            await Task.Run(temp.Wait);
            foreach (var user in temp.Result.Users)
            {
                if (await _twitchApi.V5.Streams.BroadcasterOnlineAsync(user.Id))
                {
                    Online.Add(user.DisplayName);
                }
            }
        }
    }
}