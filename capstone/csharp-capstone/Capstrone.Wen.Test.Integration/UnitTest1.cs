using Capstone.Web.DAL;
using Capstone.Web.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data.SqlClient;
using System.Transactions;

namespace Capstone.Wen.Test.Integration
{
    [TestClass]
    public class CapstoneTests
    {
        private TransactionScope tran;

        private string connectionString = @"Data Source=.\sqlexpress;Initial Catalog=NPGeek;Integrated Security=True";

        [TestInitialize]
        public void Initialize()
        {
            tran = new TransactionScope();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd;

                conn.Open();

                cmd = new SqlCommand("INSERT INTO park(parkCode,parkName,state,acreage,elevationInFeet,milesOfTrail,numberOfCampsites,climate, yearFounded, annualVisitorCount, inspirationalQuote,inspirationalQuoteSource, parkDescription,entryFee,numberOfAnimalSpecies) VALUES ('TNP','Test National Park','ofMind','123456','9001','13000','500','Humid','2018','3','GitGud','GiantDad','It''s something like a forest in Dark Souls','0','2');", conn);
                cmd.ExecuteNonQuery();

                //cmd = new SqlCommand("INSERT INTO survey_result(parkCode,emailAddress,state,activityLevel) VALUES ('GTNP','ernicholson22@gmail.com','OH','active'); SELECT CAST(SCOPE_IDENTITY() as int);", conn);
                //cmd.ExecuteNonQuery();
            }
        }

        [TestCleanup]
        public void Cleanup()
        {
            tran.Dispose();
        }

        [TestMethod]
        public void GetParkByParkCodeTest()
        {
            // Arrange 
            ParkDAL parkDal = new ParkDAL(connectionString);

            //Act
            Park park = parkDal.GetPark("TNP"); 

            //Assert
            //Assert.IsNotNull(park);
            Assert.AreEqual(park.ParkCode, "TNP");
        }

        [TestMethod]
        public void NewSurveyTest()
        {
            // Arrange 
            SurveyDAL surveyDal = new SurveyDAL(connectionString);
            Survey newSurvey = new Survey
            {
                ParkCode = "GTNP",
                EmailAddress = "ernicholson22@gmail.com",
                State = "OH",
                ActivityLevel = "active"

            };

            bool didWork = surveyDal.SavePost(newSurvey);

            //Assert
            Assert.AreEqual(true, didWork);
        }
    }
}
