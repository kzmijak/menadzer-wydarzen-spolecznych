using System;
using System.Collections.Generic;
using System.Text;

namespace MWS.dbo
{
    class Wydarzenie_Uczestnik : IJoiningTable
    {
        public int idwydarzenia { get; set; }
        public int iduczestnika { get; set; }
    }
}
