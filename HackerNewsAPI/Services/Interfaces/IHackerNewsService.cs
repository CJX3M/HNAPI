using HackerNewsAPI.Models;
using System.Threading.Tasks;

namespace HackerNewsAPI.Services.Interfaces
{
    public interface IHackerNewsService
    {
        Task<int[]> GetBestStoryIdsAsync();
        Task<Story> GetStoryDetailsAsync(int storyId);
        Task<List<Story>> GetBestStoriesAsync();
    }
}
