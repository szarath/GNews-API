using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using GTest.Models;
using Nancy.Json;
using System.Net.Http.Json;
using System.Text.Json;
using DevTrends.MvcDonutCaching;
using Microsoft.Extensions.Caching.Memory;

namespace GTest.Controllers
{
    
    [Route("api/[Controller]")]
    [ApiController]
    public class GNewsController : ControllerBase
    {
        string API_Key= "token=0a5d48542c9808f76cbbb519a29a203a";
        string base_url = "https://gnews.io/api/v4/";
        static HttpClient client = new HttpClient();
        static Dictionary<string,string> cache = new Dictionary<string,string>();
        private readonly ILogger<GNewsController> _logger;
        public GNewsController(ILogger<GNewsController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Returns the latest top news articles 
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        [HttpGet("GetHeadLines")]
        
        public async Task<GNews> GetHeadLines()//fetch n articles
        {



            GNews Gnewsdata = new GNews();
            string urlreqeust = base_url + "top-headlines?" + API_Key;
            string data;

            if (cache.Values.Count() > 100)
            { 
                cache.Remove(urlreqeust);
            }

            if (cache.ContainsKey(urlreqeust))
            {
                cache.TryGetValue(urlreqeust, out data);
                Gnewsdata = new JavaScriptSerializer().Deserialize<GNews>(data);
            }
            else
            {
                HttpResponseMessage response = client.GetAsync(urlreqeust).Result;

                if (response.IsSuccessStatusCode)
                {
                    data = response.Content.ReadAsStringAsync().Result;
                    Gnewsdata = new JavaScriptSerializer().Deserialize<GNews>(data);
                    cache.Add(urlreqeust, data);
                }

            }
               

            return Gnewsdata;
        }


        /// <summary>
        /// Returns the news articles that are searched using the keyword and a specific author or title 
        /// </summary>
        /// <param name="Keyword"></param>
        /// <param name="SearchSpecificTitleOrAuthor"></param>
        /// <returns></returns>
        [HttpGet("SearchSpecificTitleOrAuthor")]
        public async Task<GNews> SearchSpecificTitleOrAuthor(string Keyword,string SearchSpecificTitleOrAuthor)
        {
            GNews Gnewsdata = new GNews();
            string urlreqeust = base_url + "search?q=" + Keyword + "&in=" + SearchSpecificTitleOrAuthor + "&" + API_Key;
      
            string data;

            if (cache.Values.Count() > 100)
            {
                cache.Remove(urlreqeust);
            }

            if (cache.ContainsKey(urlreqeust))
            {
                cache.TryGetValue(urlreqeust, out data);
                Gnewsdata = new JavaScriptSerializer().Deserialize<GNews>(data);
            }
            else
            {
                HttpResponseMessage response = client.GetAsync(urlreqeust).Result;

                if (response.IsSuccessStatusCode)
                {
                    data = response.Content.ReadAsStringAsync().Result;
                    Gnewsdata = new JavaScriptSerializer().Deserialize<GNews>(data);
                    cache.Add(urlreqeust, data);
                }

            }


            return Gnewsdata;
        }

       /// <summary>
       /// Returns the news articles that are found under the given keyword 
       /// </summary>
       /// <param name="SearchByKeyword"></param>
       /// <returns></returns>
        [HttpGet("SearchKeyword")]
        public async Task<GNews> SearchKeyword(string SearchByKeyword)
        {

            GNews Gnewsdata = new GNews();
            string urlreqeust = base_url + "search?q=" + SearchByKeyword + "&" + API_Key;
            string data;

            if (cache.Values.Count() > 100)
            {
                cache.Remove(urlreqeust);
            }

            if (cache.ContainsKey(urlreqeust))
            {
                cache.TryGetValue(urlreqeust, out data);
                Gnewsdata = new JavaScriptSerializer().Deserialize<GNews>(data);
            }
            else
            {
                HttpResponseMessage response = client.GetAsync(urlreqeust).Result;

                if (response.IsSuccessStatusCode)
                {
                    data = response.Content.ReadAsStringAsync().Result;
                    Gnewsdata = new JavaScriptSerializer().Deserialize<GNews>(data);
                    cache.Add(urlreqeust, data);
                }

            }


            return Gnewsdata;
        }


    }
}
