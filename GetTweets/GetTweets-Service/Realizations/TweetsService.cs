using GetTweets_Service.Interfaces;
using GetTweets_Service.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace GetTweets_Service.Realizations
{
    public class TweetsService : ITweetsService
    {
        public async Task<List<Tweet>> GetTweets(string url, DateTime startDate, DateTime endDate)
        {
            var client = new HttpClient();
            var tweets = new List<Tweet>();

            await GetTweetsFromBadApi(url, tweets, client, startDate, endDate);

            return tweets
                .OrderBy(t => t.Stamp)          // sort by date-time stamp
                .GroupBy(t => t.Id)             // remove doubles
                .Select(grp => grp.First())     // unfortunately Distinct don't want work, perhaps need to override equals (too many work for small task)
                .ToList();
        }

        private async Task GetTweetsFromBadApi(string url, List<Tweet> tweets, HttpClient client, DateTime startDate, DateTime endDate)
        {
            var requestUrl = $"{url}?startDate={startDate:yyyy-MM-ddThh:mm:ssZ}&endDate={endDate:yyyy-MM-ddThh:mm:ssZ}";
            var response = await client.GetStringAsync(requestUrl);
            var tweetsFromBadApi =  JsonConvert.DeserializeObject<List<Tweet>>(response);

            // bad api returns maximum 100 records, so if records count equal 100
            // then we divide time period on half and recursively try to get period 
            // of time where records less then 100
            if (tweetsFromBadApi.Count == 100)
            {
                var firstHalfStartDate = startDate;
                var firstHalfEndDate = GetHalfDate(startDate, endDate);

                await GetTweetsFromBadApi(url, tweets, client, firstHalfStartDate, firstHalfEndDate);

                var secondHalfStartDate = firstHalfEndDate;
                var secondHalfEndDate = endDate;

                await GetTweetsFromBadApi(url, tweets, client, secondHalfStartDate, secondHalfEndDate);
            }

            // if records a less then 100 we add them to final list
            tweets.AddRange(tweetsFromBadApi);
        }

        private static DateTime GetHalfDate(DateTime start, DateTime end)
        {
            var ticks = (end.Ticks - start.Ticks) / 2;

            return new DateTime(start.Ticks + (ticks > 0 ? ticks : 1));
        }
    }
}
