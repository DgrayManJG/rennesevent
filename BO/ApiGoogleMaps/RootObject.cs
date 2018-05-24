﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO.ApiGoogleMaps
{
    public class RootObject
    {
        public List<string> destination_addresses { get; set; }
        public List<string> origin_addresses { get; set; }
        public List<Row> rows { get; set; }
        public string status { get; set; }
    }
}
