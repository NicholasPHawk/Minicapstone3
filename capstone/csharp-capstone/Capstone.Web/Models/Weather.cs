using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Web.Models
{
    public class Weather
    {
        public string ParkCode { get; set; }
        public int FiveDayForecastValue { get; set; }
        public int Low { get; set; }
        public int High { get; set; }
        public string Forecast { get; set; }
        public string Recommendation { get; set; }

        public string GetRecommendation(string forecast, int low, int high)
        {
            string recommendations = "";
            Dictionary<string, string> recommendationsDictionary = new Dictionary<string, string>()
            {
                {"snow", "Pack snowshoes. " },
                {"rain", "Pack rain gear and wear waterproof shoes. " },
                {"thunderstorms", "Seek shelter and avoid hiking on exposed ridges. " },
                {"sunny", "Pack sunblock. " },
                {"partly cloudy", "Enjoy your day! " }
            };            

            if (high - low > 20)
            {
                recommendations = recommendations + "Wear breathable layers! ";
            }

            if (high > 75)
            {
                recommendations = recommendations + "Bring an extra gallon of water! ";
            }
            else if (low < 20)
            {
                recommendations = recommendations + "Wear warm clothes or risk DEATH!!!!! ";
            }

            recommendations = recommendations + recommendationsDictionary[forecast];

            return recommendations;
        }
    }
}
