using System;
using System.Collections.Generic;
using System.Text;

namespace MWS.dbo
{
    class Pracownik: DatabaseObject
    {
        public int id { get; set; }
        public string stanowisko { get; set; }
        public decimal wynagrodzenie { get; set; }
    }
}
