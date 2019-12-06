using System;
using System.Collections.Generic;
using System.Text;

namespace MWS.dbo
{
    class Dotacja : DatabaseObject
    {
        public int id { get; set; }
        public int idwydarzenia { get; set; } = 0;
        public int idsponsora { get; set; } = 0;
        public string oczekiwania { get; set; }
        public decimal kwota { get; set; }
        public bool zatwierdzone { get; set; }
    }
}
