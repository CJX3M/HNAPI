using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HackerNewsAPI.Models;
using HackerNewsAPI.Services.Interfaces;
using Microsoft.Extensions.Caching.Memory;

namespace HackerNewsAPI.Services
{
    public class CachedHackerNewsService : IHackerNewsService
    {
        private readonly IHackerNewsService _hackerNewsService;
        private readonly IMemoryCache _cache;

        public CachedHackerNewsService(IHackerNewsService hackerNewsService, IMemoryCache cache)
        {
            _hackerNewsService = hackerNewsService;
            _cache = cache;
        }

        public async Task<int[]> GetBestStoryIdsAsync()
        {
            return await _cache.GetOrCreateAsync("best_story_ids", async entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5);
                return await _hackerNewsService.GetBestStoryIdsAsync();
            });
        }

        public async Task<List<Story>> GetBestStoriesAsync()
        {
            return await _cache.GetOrCreateAsync("best_stories", async entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5);
                var storyIds = await GetBestStoryIdsAsync();
                var stories = new List<Story>();
                foreach (var storyId in storyIds)
                {
                    var story = await _hackerNewsService.GetStoryDetailsAsync(storyId);
                    stories.Add(story);
                }
                // Sort stories by score in descending order
                return stories.OrderByDescending(s => s.score).ToList();
            });
        }

        public async Task<Story> GetStoryDetailsAsync(int storyId)
        {
            return await _hackerNewsService.GetStoryDetailsAsync(storyId);
        }
    }
}