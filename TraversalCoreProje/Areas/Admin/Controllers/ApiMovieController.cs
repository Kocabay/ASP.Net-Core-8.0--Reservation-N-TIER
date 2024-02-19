using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using TraversalCoreProje.Areas.Admin.Models;


namespace TraversalCoreProje.Areas.Admin.Controllers
{
    [Area("Admin")]
    [AllowAnonymous]
    public class ApiMovieController : Controller
    {
        public async Task< IActionResult> Index()
        {
            List<ApiViewModel> apimoview = new List<ApiViewModel>();
             
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("https://imdb-top-100-movies.p.rapidapi.com/"),
                Headers =
    {
        { "X-RapidAPI-Key", "826d75534bmshec1118a646372c2p116bccjsne022bea76a48" },
        { "X-RapidAPI-Host", "imdb-top-100-movies.p.rapidapi.com" },
    },
            };
            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync(); 
                apimoview = JsonConvert.DeserializeObject<List<ApiViewModel>>(body);

            return View(apimoview);
            }
        }
    }
}

