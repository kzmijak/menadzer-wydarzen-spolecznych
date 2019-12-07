using System;
using System.Collections.Generic;
using System.Text;

namespace MWS.dbo
{
    class Platnosc : DatabaseObject
    {
        public int idkarty { get; set; }
        public int idadresata { get; set; }
        public decimal kwota { get; set; }
        public DateTime dzien { get; set; }
        public TimeSpan godzina { get; set; }
    }
}
