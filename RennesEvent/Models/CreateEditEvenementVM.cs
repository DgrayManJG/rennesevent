using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BO;

namespace RennesEvent.Models
{
    public class CreateEditEvenementVM
    {
        public Evenement Evenement { get; set; }

        public List<Image> Images { get; set; }
        
        public Image defaultImage { get; set; }

        public List<Theme> Themes { get; set; }

        public int? IdSelectedTheme { get; set; }


    }
}