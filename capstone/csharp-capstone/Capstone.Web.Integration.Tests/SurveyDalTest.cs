using Capstone.Web.DAL;
using Capstone.Web.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace Capstone.Test.Dal
{
    [TestClass]
    public class SurveyDalTest : DatabaseTest
    {
        private ISurveyDAL _surveyDal;

        [TestInitialize]
        public void Setup()
        {
            _surveyDal = new SurveyDAL(NpGeekDbConnectionString);

        }

        [TestMethod]
        public void SavePostTest()
        {
            using (SqlConnection conn = new SqlConnection(NpGeekDbConnectionString))
            {
                SqlCommand cmd = conn.CreateCommand(); ;

                conn.Open();
                const string sql =
                     @"INSERT park VALUES ('CVNP', 'Cuyahoga Valley National Park', 'Ohio', 32832, 696, 125, 0, 'Woodland', 2000, 2189849, 'Of all the paths you take in life, make sure a few of them are dirt.', 'John Muir', 'Though a short distance from the urban areas of Cleveland and Akron, Cuyahoga Valley National Park seems worlds away. The park is a refuge for native plants and wildlife, and provides routes of discovery for visitors. The winding Cuyahoga River gives way to deep forests, rolling hills, and open farmlands. Walk or ride the Towpath Trail to follow the historic route of the Ohio & Erie Canal', 0, 390);
                          INSERT park VALUES ('ENP', 'Everglades National Park', 'Florida', 1508538, 0, 35, 0, 'Tropical', 1934, 1110901, 'There are no other Everglades in the world. They are, they have always been, one of the unique regions of the earth; remote, never wholly known. Nothing anywhere else is like them.', 'Marjory Stoneman Douglas', 'The Florida Everglades, located in southern Florida, is one of the largest wetlands in the world. Several hundred years ago, this wetlands was a major part of a 5,184,000 acre watershed that covered almost a third of the entire state of Florida. The Everglades consist of a shallow sheet of fresh water that rolls slowly over the lowlands and through billions of blades of sawgrass. As water moves through the Everglades, it causes the sawgrass to ripple like green waves; this is why the Everglades received the nickname ""River of Grass.""', 8, 760);
                          INSERT park VALUES ('GCNP', 'Grand Canyon National Park', 'Arizona', 1217262, 8000, 115, 120, 'Desert', 1919, 4756771, 'It is the one great wonders. . . every American should see.', 'Theodore Roosevelt', 'If there is any place on Earth that puts into perspective the grandiosity of Mother Nature, it is the Grand Canyon. The natural wonder, located in northern Arizona, is a window into the regio''s geological and Native American past. As one of the country''s first national parks, the Grand Canyon has long been considered a U.S. treasure, and continues to inspire scientific study and puzzlement.', 8, 450);";

                cmd.CommandText = sql;

                //cmd = new SqlCommand("INSERT INTO park(parkCode,parkName,state,acreage,elevationInFeet,milesOfTrail,numberOfCampsites,climate, yearFounded, annualVisitorCount, inspirationalQuote,inspirationalQuoteSource, parkDescription,entryFee,numberOfAnimalSpecies) VALUES ('TNP','Test National Park','ofMind','123456','9001','13000','500','Humid','2018','3','GitGud','GiantDad','It''s something like a forest in Dark Souls','0','2');SELECT parkCode = scope_identity();", conn);
                cmd.ExecuteNonQuery();
            }
            // Arrange 
            ISurveyDAL surveyDal = new SurveyDAL(NpGeekDbConnectionString);

            Survey model = new Survey
            {
                ParkCode = "ENP",
                EmailAddress = "ernicholson22@gmail.com",
                State = "OH",
                ActivityLevel = "active"
            };

            bool didWork = surveyDal.SavePost(model);

            Assert.AreEqual(true, didWork);
        }

        [TestMethod]
        public void GetParksTest()
        {
            using (var connection = new SqlConnection(NpGeekDbConnectionString))
            {
                const string sql =
                    @"INSERT park VALUES ('CVNP', 'Cuyahoga Valley National Park', 'Ohio', 32832, 696, 125, 0, 'Woodland', 2000, 2189849, 'Of all the paths you take in life, make sure a few of them are dirt.', 'John Muir', 'Though a short distance from the urban areas of Cleveland and Akron, Cuyahoga Valley National Park seems worlds away. The park is a refuge for native plants and wildlife, and provides routes of discovery for visitors. The winding Cuyahoga River gives way to deep forests, rolling hills, and open farmlands. Walk or ride the Towpath Trail to follow the historic route of the Ohio & Erie Canal', 0, 390);
                          INSERT park VALUES ('ENP', 'Everglades National Park', 'Florida', 1508538, 0, 35, 0, 'Tropical', 1934, 1110901, 'There are no other Everglades in the world. They are, they have always been, one of the unique regions of the earth; remote, never wholly known. Nothing anywhere else is like them.', 'Marjory Stoneman Douglas', 'The Florida Everglades, located in southern Florida, is one of the largest wetlands in the world. Several hundred years ago, this wetlands was a major part of a 5,184,000 acre watershed that covered almost a third of the entire state of Florida. The Everglades consist of a shallow sheet of fresh water that rolls slowly over the lowlands and through billions of blades of sawgrass. As water moves through the Everglades, it causes the sawgrass to ripple like green waves; this is why the Everglades received the nickname ""River of Grass.""', 8, 760);
                          INSERT park VALUES ('GCNP', 'Grand Canyon National Park', 'Arizona', 1217262, 8000, 115, 120, 'Desert', 1919, 4756771, 'It is the one great wonders. . . every American should see.', 'Theodore Roosevelt', 'If there is any place on Earth that puts into perspective the grandiosity of Mother Nature, it is the Grand Canyon. The natural wonder, located in northern Arizona, is a window into the regio''s geological and Native American past. As one of the country''s first national parks, the Grand Canyon has long been considered a U.S. treasure, and continues to inspire scientific study and puzzlement.', 8, 450);
                          INSERT survey_result VALUES ('CVNP','ernicholson22@gmail.com','OH','active');
                          INSERT survey_result VALUES ('CVNP','sampnicholson@gmail.com','OH','active');
                          INSERT survey_result VALUES ('GCNP','nickhawk@yahoo.com','OH','sedentary');";

                var command = connection.CreateCommand();
                command.CommandText = sql;

                connection.Open();
                command.ExecuteNonQuery();
            }

            var parks = _surveyDal.GetParks();
            Assert.AreEqual(3,parks.Count);
        }

        [TestMethod]
        public void GetFavoriteParksTest()
        {
            using (var connection = new SqlConnection(NpGeekDbConnectionString))
            {
                const string sql =
                    @"INSERT park VALUES ('CVNP', 'Cuyahoga Valley National Park', 'Ohio', 32832, 696, 125, 0, 'Woodland', 2000, 2189849, 'Of all the paths you take in life, make sure a few of them are dirt.', 'John Muir', 'Though a short distance from the urban areas of Cleveland and Akron, Cuyahoga Valley National Park seems worlds away. The park is a refuge for native plants and wildlife, and provides routes of discovery for visitors. The winding Cuyahoga River gives way to deep forests, rolling hills, and open farmlands. Walk or ride the Towpath Trail to follow the historic route of the Ohio & Erie Canal', 0, 390);
                          INSERT park VALUES ('ENP', 'Everglades National Park', 'Florida', 1508538, 0, 35, 0, 'Tropical', 1934, 1110901, 'There are no other Everglades in the world. They are, they have always been, one of the unique regions of the earth; remote, never wholly known. Nothing anywhere else is like them.', 'Marjory Stoneman Douglas', 'The Florida Everglades, located in southern Florida, is one of the largest wetlands in the world. Several hundred years ago, this wetlands was a major part of a 5,184,000 acre watershed that covered almost a third of the entire state of Florida. The Everglades consist of a shallow sheet of fresh water that rolls slowly over the lowlands and through billions of blades of sawgrass. As water moves through the Everglades, it causes the sawgrass to ripple like green waves; this is why the Everglades received the nickname ""River of Grass.""', 8, 760);
                          INSERT park VALUES ('GCNP', 'Grand Canyon National Park', 'Arizona', 1217262, 8000, 115, 120, 'Desert', 1919, 4756771, 'It is the one great wonders. . . every American should see.', 'Theodore Roosevelt', 'If there is any place on Earth that puts into perspective the grandiosity of Mother Nature, it is the Grand Canyon. The natural wonder, located in northern Arizona, is a window into the regio''s geological and Native American past. As one of the country''s first national parks, the Grand Canyon has long been considered a U.S. treasure, and continues to inspire scientific study and puzzlement.', 8, 450);";

                var command = connection.CreateCommand();
                command.CommandText = sql;

                connection.Open();
                command.ExecuteNonQuery();
            }

            var parks = _surveyDal.GetParks();
            Assert.AreEqual(3, parks.Count);
        }
    }
}