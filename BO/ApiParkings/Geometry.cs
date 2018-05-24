using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO.ApiParkings
{
    public class Geometry
    {
        public string type { get; set; }
        public List<double> coordinates { get; set; }
    }
}
