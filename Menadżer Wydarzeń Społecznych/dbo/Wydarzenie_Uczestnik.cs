using System;
using System.Collections.Generic;
using System.Text;

namespace MWS.dbo
{
    class Wydarzenie_Uczestnik : _JoiningTable
    {
        public int idwydarzenia { get; set; }
        public int iduczestnika { get; set; }
    }
}
