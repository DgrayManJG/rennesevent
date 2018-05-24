using System;

namespace BO.ApiParkings
{
    public class TarifParking
    {
        public EnumJoursSemaine JourDebut { get; set; }
        public EnumJoursSemaine JourFin { get; set; }
        public TimeSpan HeureDebut { get; set; }
        public TimeSpan HeureFin { get; set; }
        public double TarifMinute { get; set; }


    }
}
