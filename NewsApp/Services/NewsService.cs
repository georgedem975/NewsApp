using NewsApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace NewsApp.Services
{
    class NewsService
    {
        private readonly HttpClient _client;

        private const string _apiKey = "4730331687dd4b4ca28b0dbdf0dc5b0c";

        private const string _baseUrl = "https://newsapi.org/v2/top-headlines";

        public NewsService()
        {
            _client = new HttpClient();
        }

        public async Task<List<NewsItem>?> GetNewsAsync()
        {
            try
            {
                var url = $"{_baseUrl}?country=us&apiKey={_apiKey}";
                var response = await _client.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    var body = await response.Content.ReadAsStringAsync();

                    var answer = JsonNode.Parse(body)!;

                    int count = (int)answer["totalResults"];

                    return new List<NewsItem>();
                }
            }
            catch (Exception ex) {
                Console.WriteLine(ex.Message);
            }

            return null;
        }
    }
}

