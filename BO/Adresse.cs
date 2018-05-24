using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class Adresse : IIdentifiable
    {
        public int Id { get; set; }
        

        public string Rue { get; set; }

        public string Ville { get; set; }

        public int Cp { get; set; }
    }
}
