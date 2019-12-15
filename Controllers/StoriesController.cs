using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;


namespace SantanderBestStoriesAPI.Controllers
{
    [Route("api/[controller]")]
    public class StoriesController : Controller
    {
        private static IMemoryCache _cachedStories;
        private static readonly HttpClient httpClient = new HttpClient();

        [HttpGet]
        public ActionResult<object> GetTwentyTopStories()
        {
            if (_cachedStories == null) InsertTopTwentyStoriesInCache();
            return _cachedStories.Get(Stories.TopTwenty);
        }

        #region Stories CacheOperations
        private void InsertTopTwentyStoriesInCache()
        {
            string StoriesList = RetrieveStories();
            var StoriesList2Model = JsonConvert.DeserializeObject<List<Story>>(StoriesList.ToString());
            var TopTwentyStories = StoriesList2Model.Take(20).OrderByDescending(o => o.score).ToList();
            CreateCachedStoriesList(TopTwentyStories);
        }
        private static void CreateCachedStoriesList(List<Story> StoriesList)
        {
            var cacheEntryOptions = new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromDays(1));
            _cachedStories = new MemoryCache(new MemoryCacheOptions());
            _cachedStories.Set(Stories.TopTwenty, StoriesList, cacheEntryOptions);
        }

        #endregion


        #region HackerNewsAPI Readings
        private string RetrieveStories()
        {            
            string url = "https://hacker-news.firebaseio.com/v0/beststories.json";
            var storyIds = httpClient.GetAsync(url).Result.Content.ReadAsStringAsync().Result;
            var jsonStoriesById = JsonConvert.DeserializeObject<List<int>>(storyIds);
            string strStoriesDetailedList = "[";
            foreach (var story in jsonStoriesById)
            {
                string storyDetails = StoryDetailsById(story);
                strStoriesDetailedList += storyDetails + ",";
            }
            strStoriesDetailedList = strStoriesDetailedList.TrimEnd(',');
            strStoriesDetailedList += "]";
            return strStoriesDetailedList;
        }

        private string StoryDetailsById(int StoryId)
        {
            string url = "https://hacker-news.firebaseio.com/v0/item/" + StoryId + ".json";
            return httpClient.GetAsync(url).Result.Content.ReadAsStringAsync().Result;
        }

        #endregion
    }
}
