using Capstone.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Web.DAL
{
    public interface ISurveyDAL
    {
        bool SavePost(Survey model);
        Dictionary<string, string> GetParks();
        IList<FavoriteParksVM> GetFavoriteParks();
    }
}
