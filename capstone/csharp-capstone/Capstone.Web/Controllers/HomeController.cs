using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Capstone.Web.Models;
using Capstone.Web.DAL;
using Capstone.Web.Extensions;

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

        public IActionResult Detail(string parkCode, string unit)
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

            GetActiveUnit(model, unit);

            if (model.TempUnit == "c")
            {
                TemperatureConversion(model);
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

        private DetailViewModel GetActiveUnit(DetailViewModel model, string unit)
        {
            if (HttpContext.Session.Get<String>("Temp_Unit") == null)
            {
                SetUnit(unit);
            }
            else if (unit != null)
            {
                SetUnit(unit);
            }
            model.TempUnit = HttpContext.Session.Get<String>("Temp_Unit");

            return model;
        }

            private void SetUnit(string unit)
        {
            HttpContext.Session.Set("Temp_Unit", unit);
        }
    }
}
