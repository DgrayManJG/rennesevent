using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;

namespace DAL
{
    public class Context : DbContext
    {
        public DbSet<Evenement> Evenements { get; set; }
        public DbSet<Adresse> Adresses { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Theme> Themes { get; set; }
        public DbSet<Tarif> Tarifs { get; set; }
        public DbSet<Horaire> Horaires { get; set; }
        public DbSet<Parking> Parkings { get; set; }
    }
}