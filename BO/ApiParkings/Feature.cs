using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO.ApiParkings
{
    public class Feature
    {
        public string type { get; set; }
        public Crs crs { get; set; }
        public Geometry geometry { get; set; }
        public string id { get; set; }
    }
}
