namespace TwitchObserver
{
    public class Streamer
    {
        private string _nickname;
        private Platform _platform;

        public Streamer(string nickname, Platform platform)
        {
            _nickname = nickname;
            _platform = platform;
        }
    }
}