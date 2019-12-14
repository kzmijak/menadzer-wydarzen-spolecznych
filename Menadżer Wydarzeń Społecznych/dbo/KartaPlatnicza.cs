using MWS.dbo;
using System;
using System.Collections.Generic;
using System.Text;

namespace MWS.dbo
{
    class KartaPlatnicza : _DatabaseObject
    {
        public int wlasciciel { get; set; }
        public string numer { get; set; }
        public string wygasniecie { get; set; }
        public string kbezpiecz { get; set; }
        public int kontakt { get; set; }
    }
}
