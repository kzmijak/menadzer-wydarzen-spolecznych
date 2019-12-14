using System;
using System.Collections.Generic;
using System.Text;

namespace MWS.dbo
{
    class Pracownik_Pracownik : _JoiningTable
    {
        public int idorganizatora { get; set; }
        public int idpracownika { get; set; }
    }
}
