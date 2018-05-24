using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;

namespace DAL
{
    public static class GetDbContext
    {
        private static Context Context { get; set; }

        public static Context GetContext()
        {
            if (Context == null) Context = new Context();
            return Context;
        }
    }
}
