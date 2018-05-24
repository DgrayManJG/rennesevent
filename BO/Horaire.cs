using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO.ApiParkings;

namespace BO
{
    public class Horaire : IIdentifiable
    {
        public int Id { get; set; }

        public virtual List<Tarif> ListeTarifs { get; set; }

        public  EnumJoursSemaine JourDebut { get; set; }

        public  EnumJoursSemaine JourFin { get; set; }

        public  double HeureDebut { get; set; }

        public  double HeureFin { get; set; }


        public Parking Parking { get; set; }


        public override bool Equals(object obj)
        {
            var item = obj as Horaire;

            return this.HeureDebut == item.HeureDebut && this.HeureFin == item.HeureFin && this.JourDebut == item.JourDebut
                && this.JourFin == item.JourFin && this.ListeTarifs == item.ListeTarifs && this.Parking.Id == item.Parking.Id;
        }


        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (ListeTarifs != null ? ListeTarifs.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (int)JourDebut;
                hashCode = (hashCode * 397) ^ (int)JourFin;
                hashCode = (hashCode * 397) ^ HeureDebut.GetHashCode();
                hashCode = (hashCode * 397) ^ HeureFin.GetHashCode();
                return hashCode;
            }
        }
    }
}
