using System;
using System.Collections.Generic;
using System.Text;

namespace MWS.dbo
{
    class Pracownik: _CoreObject
    {
        public string stanowisko { get; set; }
        public decimal wynagrodzenie { get; set; }
    }
}
