﻿using MWS.dbo;
using System;
using System.Collections.Generic;
using System.Text;

namespace MWS.dbo
{
    class KartaPlatnicza : DatabaseObject
    {
        public int id { get; set; }
        public int wlasciciel { get; set; }
        public string numer { get; set; }
        public string wygasniecie { get; set; }
        public string kbezpiecz { get; set; }
        public int kontakt { get; set; }
    }
}
