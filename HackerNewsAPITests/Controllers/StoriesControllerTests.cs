using System.Collections.Generic;
using System.Threading.Tasks;
using HackerNewsAPI.Controllers;
using HackerNewsAPI.Models;
using HackerNewsAPI.Responses;
using HackerNewsAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace HackerNewsAPI.Tests
{
    public class StoriesControllerTests
    {
        [Fact]
        public async Task GetBestStories_ReturnsTopNStories()
        {
            // Arrange
            var mockHackerNewsService = new Mock<IHackerNewsService>();
            mockHackerNewsService
                .Setup(service => service.GetBestStoriesAsync())
                .ReturnsAsync(new List<Story>
                {
                    new Story { title = "Story 1", score = 100 },
                    new Story { title = "Story 2", score = 200 },
                    new Story { title = "Story 3", score = 300 }
                });

            var controller = new StoriesController(mockHackerNewsService.Object);

            // Act
            var result = await controller.GetBestStories(2);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var stories = Assert.IsType<List<StoryResponse>>(okResult.Value);
            Assert.Equal(2, stories.Count);
            Assert.Equal("Story 1", stories[0].Title);
            Assert.Equal("Story 2", stories[1].Title);

        }
    }
}