using System;
using System.Collections.Generic;
using System.Text;

namespace MWS.dbo
{
    class Bilet : _DatabaseObject
    {
        public string nazwa { get; set; }
        public decimal cena { get; set; }
        public string opis { get; set; }
        
    }
}
