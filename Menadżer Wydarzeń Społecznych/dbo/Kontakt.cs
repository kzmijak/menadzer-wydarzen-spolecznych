using System;
using System.Collections.Generic;
using System.Text;

namespace MWS.dbo
{
    class Kontakt : DatabaseObject
    {
        public int id { get; set; }
        public string imie { get; set; }
        public string nazwisko { get; set; }
        public string telefon { get; set; }
        public string email { get; set; }
        public string miejscowosc { get; set; }
        public string nrdomu { get; set; }
        public string miasto { get; set; }
        public string poczta { get; set; }
        public string ulica { get; set; }
        public int idpracownika { get; set; }
        public int iduczestnika { get; set; }
    }
}
