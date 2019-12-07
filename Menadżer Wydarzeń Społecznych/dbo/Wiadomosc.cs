using System;
using System.Collections.Generic;
using System.Text;

namespace MWS.dbo
{
    class Wiadomosc: DatabaseObject
    {
        public int idpracownika { get; set; } = 0;
        public int idpracownika2 { get; set; } = 0;
        public int idsponsora { get; set; } = 0;
        public int idsponsora2 { get; set; } = 0;
        public int iduczestnika { get; set; } = 0;
        public int iduczestnika2 { get; set; } = 0;
        public DateTime dzien { get; set; }
        public TimeSpan godzina { get; set; }
        public string tresc { get; set; }

    }
}
