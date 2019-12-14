using System;
using System.Collections.Generic;
using System.Text;

namespace MWS.dbo
{
    class Wydarzenie_Sponsor : _JoiningTable
    {
        public int idwydarzenia { get; set; }
        public int idsponsora { get; set; }
    }
}
