using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Capstone.Web.Models;

namespace Capstone.Web.DAL
{
    public class ParkDAL : IParkDAL
    {
        private readonly string _connectionString;

        public ParkDAL(string connectionString)
        {
            _connectionString = connectionString;
        }

        public Park GetPark(string parkCode)
        {
            return GetAllParks().FirstOrDefault(p => p.ParkCode == parkCode);
        }

        public IList<Park> GetAllParks()
        {
            IList<Park> parks = new List<Park>();

            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("SELECT * FROM park", conn);

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Park park = new Park
                        {
                            ParkCode = Convert.ToString(reader["parkCode"]),
                            ParkName = Convert.ToString(reader["parkName"]),
                            State = Convert.ToString(reader["state"]),
                            Acreage = Convert.ToInt32(reader["acreage"]),
                            ElevationInFeet = Convert.ToInt32(reader["elevationInFeet"]),
                            MilesOfTrail = Convert.ToDouble(reader["milesOfTrail"]),
                            NumberOfCampsites = Convert.ToInt32(reader["numberOfCampsites"]),
                            Climate = Convert.ToString(reader["Climate"]),
                            YearFounded = Convert.ToInt32(reader["yearFounded"]),
                            AnnualVistorCount = Convert.ToInt32(reader["annualVisitorCount"]),
                            InspirationalQuote = Convert.ToString(reader["inspirationalQuote"]),
                            InspirationalQuoteSource = Convert.ToString(reader["inspirationalQuoteSource"]),
                            ParkDescription = Convert.ToString(reader["parkDescription"]),
                            EntryFee = Convert.ToInt32(reader["EntryFee"]),
                            NumberOfAnimalSpecies = Convert.ToInt32(reader["numberOfAnimalSpecies"])
                        };

                        parks.Add(park);
                    }
                }
            }
            catch (SqlException ex)
            {
                throw;
            }

            return parks;
        }
        public IList<Weather> GetAllWeather(string parkCode)
        {
            IList<Weather> weathers = new List<Weather>();

            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("SELECT * FROM weather WHERE parkCode = @parkCode", conn);
                    cmd.Parameters.AddWithValue("@parkCode", parkCode);

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Weather weather = new Weather
                        {
                           ParkCode = Convert.ToString(reader["parkCode"]),
                           FiveDayForecastValue = Convert.ToInt32(reader["fiveDayForecastValue"]),
                            Low = Convert.ToInt32(reader["low"]),
                            High = Convert.ToInt32(reader["high"]),
                            Forecast = Convert.ToString(reader["forecast"]),
                        };

                        weathers.Add(weather);
                    }
                }
            }
            catch (SqlException ex)
            {
                throw;
            }

            return weathers;
        }
    }
}
