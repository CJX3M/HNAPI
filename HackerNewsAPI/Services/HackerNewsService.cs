using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using HackerNewsAPI.Models;
using HackerNewsAPI.Services.Interfaces;

namespace HackerNewsAPI.Services
{
    public class HackerNewsService : IHackerNewsService
    {
        private readonly HttpClient _httpClient;

        public HackerNewsService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public Task<List<Story>> GetBestStoriesAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<int[]> GetBestStoryIdsAsync()
        {
            var response = await _httpClient.GetStringAsync("https://hacker-news.firebaseio.com/v0/beststories.json");
            return JsonSerializer.Deserialize(response, JsonContext.Default.Int32Array);
        }

        public async Task<Story> GetStoryDetailsAsync(int storyId)
        {
            var response = await _httpClient.GetStringAsync($"https://hacker-news.firebaseio.com/v0/item/{storyId}.json");
            return JsonSerializer.Deserialize(response, JsonContext.Default.Story);
        }
    }
}