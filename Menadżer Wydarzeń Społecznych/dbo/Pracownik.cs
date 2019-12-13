using System;
using System.Collections.Generic;
using System.Text;

namespace MWS.dbo
{
    class Pracownik: CoreObject
    {
        public string stanowisko { get; set; }
        public decimal wynagrodzenie { get; set; }
    }
}
