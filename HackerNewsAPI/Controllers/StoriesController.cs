using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HackerNewsAPI.Services;
using HackerNewsAPI.Responses;
using HackerNewsAPI.Services.Interfaces;

namespace HackerNewsAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StoriesController : ControllerBase
    {
        private readonly IHackerNewsService _hackerNewsService;

        public StoriesController(IHackerNewsService hackerNewsService)
        {
            _hackerNewsService = hackerNewsService;
        }

        [HttpGet("{n}")]
        public async Task<ActionResult<IEnumerable<StoryResponse>>> GetBestStories(int n)
        {
            // Get the sorted list of stories from the cache
            var stories = await _hackerNewsService.GetBestStoriesAsync();

            // Take the top n stories and materialize to a list
            var topStories = stories
                .Take(n)
                .Select(story => new StoryResponse
                {
                    Title = story.title,
                    Uri = story.url,
                    PostedBy = story.by,
                    Time = DateTimeOffset.FromUnixTimeSeconds(story.time).ToString("yyyy-MM-ddTHH:mm:ss+00:00"),
                    Score = story.score,
                    CommentCount = story.descendants
                })
                .ToList(); // <-- Add ToList() here

            return Ok(topStories);
        }
    }
}