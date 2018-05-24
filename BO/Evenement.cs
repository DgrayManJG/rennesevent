using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace BO
{
    public class Evenement : IIdentifiable
    {
        public int Id { get; set; }

        public string Nom { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime DateDebut { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime DateFin { get; set; }

        public string Descriptif { get; set; }

        public int Place { get; set; }

        public virtual Adresse Adresse { get; set; }

        public virtual Theme Theme { get; set; }

        public virtual List<Image> Images { get; set; }
        
    }
}
