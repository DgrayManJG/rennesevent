using BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RennesEvent.Models
{
    public class ParkingEvenementVM
    {
        public List<Parking> parkings { get; set; }

        public Evenement evenement { get; set; }
        public string coordonneesEvenement { get; set; }
    }
}