namespace TwitchObserver
{
    public class Streamer
    {
        public string Nickname;
        public Platform Platform;

        public Streamer(string nickname, Platform platform)
        {
            Nickname = nickname;
            Platform = platform;
        }
    }
}