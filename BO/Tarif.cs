using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class Tarif : IIdentifiable
    {
        public int Id { get; set; }

        public double Prix { get; set; }

        public double HeureDebut { get; set; }

        public double HeureFin { get; set; }

        public double Tarification { get; set; }

        public bool IsFirstDay { get; set; }

        public Horaire Horaire { get; set; }
    }
}
