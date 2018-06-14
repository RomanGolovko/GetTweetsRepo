# GetTweetsRepo
This is a repository with test solution for CustomerTimes

A Bad REST API
A Client needs us to pull 2 years of ultra-cool curated tweets they have collected and stored
themselves. We need to make sure that we get all tweets tweeted in 2016 and 2017.
The only way to get this data is to use the client's REST API (https://badapi.iqvia.io/swagger/). But it's not a well-designed API...

Complications 
- The API only lets you search for tweets with timestamps falling in a window specified by
startDate and endDate.
- The API can only return 100 results in a single response, even if there are more than 100
tweets in the specified window.
- There is no indication that there are more results and there are no pagination
parameters returned.
- There can be no duplicate entries in the results you return.

Requirements
- Your application should be written in C# or JavaScript
- Submit your app via a public GitHub repository (make sure we can access it!)
- We must be able to build and run your app
- The final results should contain all records with no duplicates
- The results should be output in a way that allows us to validate your records
- The operation should complete in a reasonable amount of time
- Your application should make actual calls to our live REST API to get the tweets.
