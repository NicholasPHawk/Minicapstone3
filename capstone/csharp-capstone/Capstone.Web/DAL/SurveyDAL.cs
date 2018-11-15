using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Capstone.Web.Models;

namespace Capstone.Web.DAL
{
    public class SurveyDAL : ISurveyDAL
    {
        private readonly string _connectionString;

        public SurveyDAL(string connectionString)
        {
            _connectionString = connectionString;
        }

        public bool SavePost(Survey model)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("INSERT INTO survey_result (parkCode, emailAddress, state, activityLevel) " +
                                                    "VALUES (@ParkCode, @EmailAddress, @State, @ActivityLevel);", conn);
                    cmd.Parameters.AddWithValue("@ParkCode", model.ParkCode);
                    cmd.Parameters.AddWithValue("@EmailAddress", model.EmailAddress);
                    cmd.Parameters.AddWithValue("@State", model.State);
                    cmd.Parameters.AddWithValue("@ActivityLevel", model.ActivityLevel);

                    int rowsAffected = cmd.ExecuteNonQuery();

                    return (rowsAffected > 0);
                }
   
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public Dictionary<string, string> GetParks()
        {
            Dictionary<string, string> parks = new Dictionary<string, string>();

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
                            ParkName = Convert.ToString(reader["parkName"])                            
                        };

                        parks.Add(park.ParkCode, park.ParkName);
                    }
                }
            }
            catch (SqlException ex)
            {
                throw;
            }

            return parks;
        }

        public IList<FavoriteParksVM> GetFavoriteParks()
        {
            IList<FavoriteParksVM> parks = new List<FavoriteParksVM>();

            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("SELECT p.parkCode, p.parkName, Count(s.surveyId) AS surveyCount FROM park p " +
                                                    "JOIN survey_result s ON p.parkCode = s.parkCode " +
                                                    "GROUP BY p.parkCode, p.parkName " +
                                                    "ORDER BY surveyCount DESC, p.parkName", conn);

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        FavoriteParksVM park = new FavoriteParksVM
                        {
                            ParkCode = Convert.ToString(reader["parkCode"]),
                            ParkName = Convert.ToString(reader["parkName"]),
                            SurveyCount = Convert.ToInt32(reader["surveyCount"]),
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
    }
}
