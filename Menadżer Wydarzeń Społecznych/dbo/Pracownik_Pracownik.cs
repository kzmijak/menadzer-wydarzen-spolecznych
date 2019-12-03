using System;
using System.Collections.Generic;
using System.Text;

namespace MWS.dbo
{
    class Pracownik_Pracownik : IJoiningTable
    {
        public int idorganizatora { get; set; }
        public int idpracownika { get; set; }
    }
}
