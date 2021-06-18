using System;
using RestSharp;
using NekosSharp;

namespace DiscordBot.Util
{
    public class WebScraper
    {
        public static NekoClient NekoClient = new NekoClient("neko");
        public static String GetCat()
        {
            var client = new RestClient();
            client.BaseUrl = new Uri("https://api.thecatapi.com/v1");

            var request = new RestRequest("https://api.thecatapi.com/v1/images/search", Method.GET)
                .AddHeader("x-api-key", "4ca58214-f2e4-4c02-96ae-4c6b862603b6");
            
            var response = client.Execute(request);
            var data = response.Content;
            
            return data;
        }

        public static String GetAction(string action)
        {
            var client = new RestClient();
            client.BaseUrl = new Uri("http://api.nekos.fun:8080/api/");

            var request = new RestRequest("http://api.nekos.fun:8080/api/" + action, Method.GET);

            var response = client.Execute(request);
            var data = response.Content;

            return data;
        }

        public static String GetNeko()
        {
            var request = NekoClient.Image.Neko().Result;
            return request.ImageUrl;
        }
    }
}