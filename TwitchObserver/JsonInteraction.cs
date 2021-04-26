using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace TwitchObserver
{
    public class JsonInteraction
    {
        public string Path { get; set; }

        private static string UserListFile = @"\userlist.json";

        public JsonInteraction()
        {
            this.Path = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
        }

        public List<Streamer> GetCurrentStreamersList()
        {

            var temp = new List<Streamer>();
            Console.WriteLine(Path + UserListFile);
            try
            {
                using (StreamReader sr = new StreamReader(Path + UserListFile))
                {
                    temp = JsonConvert.DeserializeObject<List<Streamer>>(sr.ReadToEnd());
                }
            }
            catch (Exception e)
            {
                File.Create(Path + UserListFile);
            }

            if (temp == null)
            {
                return new List<Streamer>();
            }

            return temp;
        }

        public async void UpdateUserList(List<Streamer> streamers)
        {
            try
            {
                await using (StreamWriter sw = new StreamWriter(Path + UserListFile, false, System.Text.Encoding.UTF8))
                {
                    await sw.WriteLineAsync(JsonConvert.SerializeObject(streamers, Formatting.Indented));
                }
                //Console.WriteLine($"Пользоатель добавлен");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}