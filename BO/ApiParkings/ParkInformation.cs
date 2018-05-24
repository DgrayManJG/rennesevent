using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO.ApiParkings
{
    public class ParkInformation
    {
        public string name { get; set; }
        public string status { get; set; }
        public int max { get; set; }
        public int free { get; set; }
    }
}
