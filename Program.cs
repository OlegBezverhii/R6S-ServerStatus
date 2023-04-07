using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;

namespace UbiAPI
{
    public class Siege
    {
        public string AppID;
        public int MDM;
        public string Category;
        public string Name;
        public string Platform;
        public string Status;
        public bool Maintenance;
        public IList<string> ImpactedFeatures;
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Выполняем API запрос к серверам Ubisoft...");
            using (var client = new HttpClient(new HttpClientHandler { AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate }))
            {
                client.BaseAddress = new Uri("https://game-status-api.ubisoft.com/v1/");
                //HttpResponseMessage response = client.GetAsync("instances?appIds=e3d5ea9e-50bd-43b7-88bf-39794f4e3d40,fb4cc4c9-2063-461d-a1e8-84a7d36525fc,6e3c99c9-6c3f-43f4-b4f6-f1a3143f2764,4008612d-3baf-49e4-957a-33066726a7bc,76f580d5-7f50-47cc-bbc1-152d000bfe59,e0aa997a-1626-4e5b-8215-b5f586b59fa2").Result;
                HttpResponseMessage response = client.GetAsync("instances?appIds=e3d5ea9e-50bd-43b7-88bf-39794f4e3d40,fb4cc4c9-2063-461d-a1e8-84a7d36525fc,4008612d-3baf-49e4-957a-33066726a7bc").Result;
                response.EnsureSuccessStatusCode();
                string result = response.Content.ReadAsStringAsync().Result;
                //Console.WriteLine("Result: " + result);

                var mysiege = JsonConvert.DeserializeObject<List<Siege>>(result);
                //Console.WriteLine("Количество массивов " + mysiege.Count);
                foreach (var platform in mysiege)
                {
                    //Console.WriteLine("Платформа: " + platform.Platform + "\t\t Имя " + platform.Name + "\t Статус: " + platform.Status);
                    //Console.WriteLine("|{0,-10}|{1,10}|{2,-35} ", platform.Platform, platform.Status,platform.Name);
                    Console.WriteLine("Платформа: {0,-10}|Статус: {1,10} ", platform.Platform, platform.Status);
                }
            }
            Console.WriteLine("Нажмите любую клавишу для выхода...");
            Console.ReadLine();
        }
    }
}
