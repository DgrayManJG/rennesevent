using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO.ApiParkings
{
    public class RootObject
    {
        public List<Park> parks { get; set; }
        public Features features { get; set; }
    }
}
