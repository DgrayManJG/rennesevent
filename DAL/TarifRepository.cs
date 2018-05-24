using System;
using BO;

namespace DAL
{
    public class TarifRepository
    {
        private static TarifRepository instance;
        private readonly Context db;

        public TarifRepository()
        {
            db = GetDbContext.GetContext();
        }

        public static TarifRepository GetInstance()
        {
            return instance ?? (instance = new TarifRepository());
        }

        public void Update(Tarif tarif)
        {
            Tarif t = DalGenerique<Tarif>.GetInstance().GetById(tarif.Id);
            t.Horaire = tarif.Horaire;
            t.HeureDebut = tarif.HeureDebut;
            t.HeureFin = tarif.HeureFin;
            t.IsFirstDay = tarif.IsFirstDay;
            t.Prix = tarif.Prix;
            t.Tarification = tarif.Tarification;
            db.SaveChanges();
        }
    }
}