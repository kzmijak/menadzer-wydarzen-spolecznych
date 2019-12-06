using System;
using System.Collections.Generic;
using System.Text;

namespace MWS.dbo
{
    class Logowanie : DatabaseObject
    {
        public int id { get; set; }
        public string login { get; set; }
        public string haslo { get; set; }
        public int idpracownika { get; set; } = 0;
        public int idsponsora { get; set; } = 0;
        public int iduczestnika { get; set; } = 0;
    }
}
