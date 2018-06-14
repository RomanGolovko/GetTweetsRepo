using GetTweets.Controllers;
using GetTweets_Service.Interfaces;
using GetTweets_Service.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace GetTweets_Tests.Controllers
{

    public class TweetsControllerTest
    {
        [Fact]
        public void GetTweetsOkObjectResult()
        {
            // Arrange
            var startDate = DateTime.UtcNow.AddDays(-1);
            var endDate = DateTime.UtcNow;

            var configurationMoq = new Mock<IConfiguration>();
            var tweetServiceMoq = new Mock<ITweetsService>();
            tweetServiceMoq.Setup(x => x.GetTweets("https://test.com", startDate, endDate))
                .Returns(It.IsAny<Task<List<Tweet>>>());
            var controller = new TweetsController(configurationMoq.Object, tweetServiceMoq.Object);

            // Act
            var result = controller.GetTweets(startDate, endDate);

            // Assert
            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public void GetTweetsBadRequestObjectResult()
        {
            // Arrange
            var configurationMoq = new Mock<IConfiguration>();
            var tweetServiceMoq = new Mock<ITweetsService>();
            tweetServiceMoq.Setup(x => x.GetTweets("https://test.com", default(DateTime), default(DateTime)))
                .Returns(It.IsAny<Task<List<Tweet>>>());
            var controller = new TweetsController(configurationMoq.Object, tweetServiceMoq.Object);

            // Act
            var result = controller.GetTweets(default(DateTime), default(DateTime));

            // Assert
            Assert.IsType<BadRequestObjectResult>(result.Result);
        }
    }
}
