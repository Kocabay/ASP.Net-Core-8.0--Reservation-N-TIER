using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TraversalCoreProje.Areas.Admin.Models;

namespace TraversalCoreProje.Areas.Admin.Controllers
{
    public class ApiExchangeController : Controller
    {
        [Area("Admin")]
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            List<BookingExchangeViewModel2>bookingExchangeViewModels = new List<BookingExchangeViewModel2>();
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("https://booking-com15.p.rapidapi.com/api/v1/meta/getExchangeRates?base_currency=TRY"),
                Headers =
    {
        { "X-RapidAPI-Key", "826d75534bmshec1118a646372c2p116bccjsne022bea76a48" },
        { "X-RapidAPI-Host", "booking-com15.p.rapidapi.com" },
    },
            };
            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();
                var values   = JsonConvert.DeserializeObject<BookingExchangeViewModel2>(body);
                return View(values.exchange_rates);
            }
        }
    }
}
