using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using ModelLibrary;
using Newtonsoft.Json;

namespace ClassDemoCatchesClient
{
    internal class RestClient
    {

        private const String uri = "http://localhost:4162/Service1.svc";

        public RestClient()
        {
        }

        public void Start()
        {
            //var catchesList = GetCatchesAsync().Result;
            //Console.WriteLine("Alle fangster\n" +
            //    String.Join("\n", catchesList));


            //var oneCatch = GetOneCatchAsync(1).Result;
            //Console.WriteLine("Fangst nr="+ 1 +"\n" +
            //                  oneCatch);


            //var deleteCatch = DeleteCatchAsync(1).Result;
            //Console.WriteLine("Fangst nr=" + 1 + " er slettet \n" +
            //                  deleteCatch);


            AddCatchAsync(new Catch(64, "Lars", "Laks", 1.2, "Norge", 51));
            var catchesList2 = GetCatchesAsync().Result;
            Console.WriteLine("Alle fangster\n" +
                              String.Join("\n", catchesList2));
        }


        private async Task<IList<Catch>> GetCatchesAsync()
        {
            using (HttpClient client = new HttpClient())
            {
                string content = await client.GetStringAsync(uri + "/catches");
                IList<Catch> cList = JsonConvert.DeserializeObject<IList<Catch>>(content);
                return cList;
            }
        }

        private async Task<Catch> GetOneCatchAsync(int id)
        {
            using (HttpClient client = new HttpClient())
            {
                string content = await client.GetStringAsync(uri + "/catch/" + id);
                Catch c = JsonConvert.DeserializeObject<Catch>(content);
                return c;
            }
        }

        private async Task<Catch> DeleteCatchAsync(int id)
        {
            using (HttpClient client = new HttpClient())
            {
                var content = await client.DeleteAsync(uri + "/catches?id=" + id);
                Catch c = JsonConvert.DeserializeObject<Catch>(content.Content.ReadAsStringAsync().Result);
                return c;
            }
        }

        private async void AddCatchAsync(Catch newCatch)
        {
            using (HttpClient client = new HttpClient())
            {
                HttpContent content = new StringContent(JsonConvert.SerializeObject(newCatch));
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                var result = await client.PostAsync(uri + "/catches", content);
                
            }
        }

    }
}