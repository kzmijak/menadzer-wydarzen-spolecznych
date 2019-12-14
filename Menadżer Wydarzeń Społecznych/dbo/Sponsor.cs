using System;
using System.Collections.Generic;
using System.Text;

namespace MWS.dbo
{
    class Sponsor: _CoreObject
    {
        public string nazwa { get; set; }

        public new Kontakt kontakt = null;
    }
}
