using System;
using System.Threading.Tasks;
using GetTweets_Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace GetTweets.Controllers
{
    [Produces("application/json")]
    [Route("api/tweets")]
    public class TweetsController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly ITweetsService _tweetsService;

        public TweetsController(IConfiguration configuration, ITweetsService tweetsService)
        {
            _configuration = configuration;
            _tweetsService = tweetsService;
        }

        [HttpGet]
        [Route("period")]
        public async Task<IActionResult> GetTweets([FromQuery] DateTime startDate, DateTime endDate)
        {
            if (startDate == default(DateTime) || endDate == default(DateTime))
            {
                return BadRequest("Please specify date, for example /api/tweets/period?startdate=2016-01-01&enddate=2018-01-01");
            }

            var badApiUrl = _configuration["ApiUrl"];
            var tweets = await _tweetsService.GetTweets(badApiUrl, startDate, endDate);

            return Ok(tweets);
        }
    }
}
