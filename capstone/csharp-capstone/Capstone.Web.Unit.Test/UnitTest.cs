using Capstone.Web.Controllers;
using Capstone.Web.DAL;
using Capstone.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace Capstone.Web.Unit.Test
{
    [TestClass]
    public class UnitTest
    {

        [TestMethod]
        public void WeatherRecommendation()
        {
            Weather myObject = new Weather();

            string result = myObject.GetRecommendation("snow", 25, 32);
            Assert.AreEqual("Pack snowshoes. ", result);

            string result2 = myObject.GetRecommendation("rain", 60, 70);
            Assert.AreEqual("Pack rain gear and wear waterproof shoes. ", result2);

            string result3 = myObject.GetRecommendation("thunderstorms", 56, 70);
            Assert.AreEqual("Seek shelter and avoid hiking on exposed ridges. ", result3);

            string result4 = myObject.GetRecommendation("sunny", 30, 47);
            Assert.AreEqual("Pack sunblock. ", result4);

            string result5 = myObject.GetRecommendation("partly cloudy", 58, 70);
            Assert.AreEqual("Enjoy your day! ", result5);
        }
    }
}
