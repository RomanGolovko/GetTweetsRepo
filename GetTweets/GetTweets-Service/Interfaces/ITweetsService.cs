using GetTweets_Service.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GetTweets_Service.Interfaces
{
    public interface ITweetsService
    {
        Task<List<Tweet>> GetTweets(string url, DateTime startDate, DateTime endDate);
    }
}
