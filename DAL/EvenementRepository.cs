using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;
using System.Diagnostics;

namespace DAL
{
    public class EvenementRepository
    {

        private static EvenementRepository instance;
        private readonly Context db;

        public EvenementRepository()
        {
            db = GetDbContext.GetContext();
        }

        public static EvenementRepository GetInstance()
        {
            return instance ?? (instance = new EvenementRepository());
        }

        public void Update(Evenement evenement)
        {
            Evenement e = DalGenerique<Evenement>.GetInstance().GetById(evenement.Id);
            e.Id = evenement.Id;
            e.DateDebut = evenement.DateDebut;
            e.DateFin = evenement.DateFin;
            e.Descriptif = evenement.Descriptif;
            e.Nom = evenement.Nom;
            e.Place = evenement.Place;
            e.Theme = evenement.Theme;
            e.Images = evenement.Images;

            db.SaveChanges();
        }

        public void Delete(int id)
        {
            try
            {
                var evenement = DalGenerique<Evenement>.GetInstance().GetById(id);
                evenement.Theme = null;
                db.Evenements.Remove(evenement);
                db.SaveChanges();
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }

        }

    }
}
