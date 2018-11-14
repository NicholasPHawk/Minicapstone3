using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Capstone.Web.DAL;
using Capstone.Web.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Capstone.Web.Controllers
{
    public class SurveyController : Controller
    {
        private readonly ISurveyDAL _surveyDAL;

        public SurveyController(ISurveyDAL surveyDAL)
        {
            _surveyDAL = surveyDAL;
        }

        // GET: Survey
        public ActionResult Index(Survey model)
        {
            Dictionary<string, string> parks = _surveyDAL.GetParks();

            model.ParksDictionary = parks;

            return View(model);
        }

        // POST: Survey/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveSurvey(Survey model)
        {
            try
            {
                // TODO: Add insert logic here
                _surveyDAL.SavePost(model);
                return RedirectToAction(nameof(FavoriteParks));
            }
            catch(Exception ex)
            {
                return View(nameof(Index));
            }
        }

        public ActionResult FavoriteParks()
        {
            return View();
        }
    }
}