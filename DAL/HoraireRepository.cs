using System;
using BO;

namespace DAL
{
    public class HoraireRepository
    {
        private static HoraireRepository instance;
        private readonly Context db;

        private HoraireRepository()
        {
            db = GetDbContext.GetContext();
        }

        public static HoraireRepository GetInstance()
        {
            return instance ?? (instance = new HoraireRepository());
        }

        public void Update(Horaire horaire)
        {
            Horaire h = DalGenerique<Horaire>.GetInstance().GetById(horaire.Id);
            h.HeureDebut = horaire.HeureDebut;
            h.HeureFin = horaire.HeureFin;
            h.JourDebut = horaire.JourDebut;
            h.JourFin = horaire.JourFin;
            h.ListeTarifs = horaire.ListeTarifs;

            db.SaveChanges();
        }
    }
}