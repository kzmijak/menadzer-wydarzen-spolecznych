using System;
using System.Collections.Generic;
using System.Text;

namespace MWS.dbo
{
    class Uczestnik_Bilet : IJoiningTable
    {
        public int iduczestnika { get; set; }
        public int idbiletu { get; set; }
    }
}
