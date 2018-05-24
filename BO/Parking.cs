using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class Parking : IIdentifiable
    {
        public int Id { get; set; }

        public string Identifiant { get; set; }

        public string Nom { get; set; }

        public string Statut { get; set; }

        public int PlacesMax { get; set; }

        public int PlacesLibres { get; set; }

        public string Coordonnees { get; set; }

        public string Adresse { get; set; }

        public string TexteHoraires { get; set; }

        public string TexteTarifs { get; set; }

        /*public DateTime HeureArrivee { get; set; }

        public DateTime HeureDepart { get; set; }*/

        public virtual List<Horaire> ListeHoraires { get; set; }

        public DateTime LastUpdate { get; set; }

        public Parking() { }


    }
}
