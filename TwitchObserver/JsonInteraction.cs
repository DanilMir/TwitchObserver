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

        public Dictionary<string, User> GetCurrentUserList()
        {

            Console.WriteLine(Path + UserListFile);
            try
            {
                using (StreamReader sr = new StreamReader(Path + UserListFile))
                {
                    return JsonConvert.DeserializeObject<Dictionary<string, User>>(sr.ReadToEnd());
                }
            }
            catch (Exception e)
            {
                File.Create(Path + UserListFile);
            }
            return default;
        }

        public async void UpdateUserList(Dictionary<string, User> user)
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(Path + UserListFile, false, System.Text.Encoding.UTF8))
                {
                    await sw.WriteLineAsync(JsonConvert.SerializeObject(user, Formatting.Indented));
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