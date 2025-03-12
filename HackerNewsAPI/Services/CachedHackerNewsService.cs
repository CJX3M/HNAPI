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
                var storyIds = await GetBestStoryIdsAsync();
                var stories = new List<Story>();
                // Process story IDs in batches of 10
                foreach (var batch in storyIds.Chunk(10))
                {
                    var tasks = batch.Select(async storyId =>
                    {
                        try
                        {
                            return await _hackerNewsService.GetStoryDetailsAsync(storyId);
                        }
                        catch
                        {
                            // Log error and ignore failed requests
                            return null;
                        }
                    });

                    var batchStories = await Task.WhenAll(tasks);
                    stories.AddRange(batchStories.Where(s => s != null));
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