using System;
using System.Collections.Generic;
using System.Text;

namespace MWS.dbo
{
    class Wydarzenie_Pracownik : IJoiningTable
    {
        public int idwydarzenia { get; set; }
        public int idpracownika { get; set; }
    }
}
