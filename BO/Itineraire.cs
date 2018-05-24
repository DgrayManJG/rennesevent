using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class Itineraire
    {
        public string Origine { get; set; }
        public string Destination { get; set; }
        public string DistanceKm { get; set; }
        public long DistanceMetres { get; set; }
        public string DureeHeuresMinutes { get; set; }
        public long DureeSecondes { get; set; }
    }
}
