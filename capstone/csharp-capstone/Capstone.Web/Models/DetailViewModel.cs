using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Web.Models
{
    public class DetailViewModel
    {
        public Park Park { get; set; }
        public Weather Weather { get; set; }
        public IList<Park> Parks { get;set; }
        public IList<Weather> Weathers { get; set; }
    }
}
