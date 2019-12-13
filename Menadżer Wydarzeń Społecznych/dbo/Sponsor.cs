using System;
using System.Collections.Generic;
using System.Text;

namespace MWS.dbo
{
    class Sponsor: CoreObject
    {
        public string nazwa { get; set; }

        public new Kontakt kontakt = null;
    }
}
