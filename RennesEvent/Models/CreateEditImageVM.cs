using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BO;

namespace RennesEvent.Models
{
    public class CreateEditImageVM
    {
        public Image Image { get; set; }

        public List<Evenement> Evenements { get; set; }

        public int? IdSelectedEvent { get; set; }

    }
}