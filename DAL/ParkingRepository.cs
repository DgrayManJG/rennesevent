using System;
using BO;

namespace DAL
{
    public class ParkingRepository
    {
        private static ParkingRepository instance;
        private readonly Context db;

        public ParkingRepository()
        {
            db = GetDbContext.GetContext();
        }

        public static ParkingRepository GetInstance()
        {
            return instance ?? (instance = new ParkingRepository());
        }

        public void Update(Parking parking)
        {
            Parking p = DalGenerique<Parking>.GetInstance().GetById(parking.Id);
            p.Identifiant = parking.Identifiant;
            p.Nom = parking.Nom;
            p.Statut = parking.Statut;
            p.PlacesMax = parking.PlacesMax;
            p.PlacesLibres = parking.PlacesLibres;
            p.Coordonnees = parking.Coordonnees;
            p.Adresse = parking.Adresse;
            p.TexteHoraires = parking.TexteHoraires;
            p.TexteTarifs = parking.TexteTarifs;
            p.LastUpdate = parking.LastUpdate;

            p.ListeHoraires = parking.ListeHoraires;

            db.SaveChanges();
        }
    }
}
