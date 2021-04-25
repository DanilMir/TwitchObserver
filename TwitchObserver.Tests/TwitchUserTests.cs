using System.Threading;
using System.Threading.Tasks;
using NUnit.Framework;
using TwitchObserver;

namespace TwitchObserver.Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void AddUser()
        {
            var length = Users.Length;
            Users.Add("Twitch");
            Users.Add("Twitch");
            Users.Add("Twitch");
            Users.Add("Twitch");
            Users.Add("9A916DE0997A5B2DE262D6C1E9415CCD1E1072BB0CCCFD886252559369E7D97C");
            Users.Add("F5464995E3FAF55BEC8E066EDF77A7FCC65A32DFA3C645EAED909B71F4ACF3EA");
            Assert.AreEqual(length + 3, Users.Length);
        }

        [Test]
        public void GetUsers()
        {
            var temp = Users.GetUserInfo();
            Assert.AreEqual(1, temp.Count);
        }

        [Test]
        public void GetOnline()
        {
            Users.Online.Clear();
            Users.Data.Clear();
            Users.Data.Add("NYC_Timescape");
            var task = Task.Run(async () => { await Users.GetOnlineUsers(); });
            task.Wait();
            //Assert.AreEqual(1, Users.Data.Count);
            Assert.AreEqual(1, Users.Online.Count);
        }
    }
}