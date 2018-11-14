using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Capstone.Web.Models;
using Capstone.Web.DAL;

namespace Capstone.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IParkDAL _parkDAL;

        public HomeController(IParkDAL parkDAL)
        {
            _parkDAL = parkDAL;
        }

        public IActionResult Index(DetailViewModel model)
        {
            IList<Park> parks = _parkDAL.GetAllParks();

            model.Parks = parks;

            return View(model);
        }

        public IActionResult Detail(string parkCode)
        {
            DetailViewModel model = new DetailViewModel
            {
                Park = _parkDAL.GetPark(parkCode),
                Weathers = _parkDAL.GetAllWeather(parkCode)
            };

            foreach (Weather weather in model.Weathers)
            {
                weather.Recommendation = weather.GetRecommendation(weather.Forecast, weather.Low, weather.High);
            }

            return View(model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public void TemperatureConversion(DetailViewModel model)
        {
            foreach (Weather weather in model.Weathers)
            {
                weather.High = (int)((weather.High - 32) / 1.8);
                weather.Low = (int)((weather.Low - 32) / 1.8);
            }
        }
    }
}
