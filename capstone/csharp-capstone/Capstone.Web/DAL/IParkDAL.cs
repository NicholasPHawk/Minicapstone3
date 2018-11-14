using Capstone.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Web.DAL
{
    public interface IParkDAL
    {
        Park GetPark(string parkCode);

        //Weather GetWeather(string parkCode);

        IList<Weather> GetAllWeather(string parkCode);

        IList<Park> GetAllParks();
    }
}
