using System;
using System.Data;
using System.Collections.Generic;
using System.Text;
using Dapper;

namespace MWS.dbo
{
    class Wydarzenie: DatabaseObject
    {
        public int id { get; set; }
        public string nazwa { get; set; }
        public string opis { get; set; }
        public string miejsce { get; set; }
        public DateTime dzien { get; set; }
        public TimeSpan godzina { get; set; }
        public decimal budzet { get; set; }
    }
}
