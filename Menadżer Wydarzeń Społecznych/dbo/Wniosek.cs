﻿using System;
using System.Collections.Generic;
using System.Text;

namespace MWS.dbo
{
    class Wniosek : DatabaseObject
    {
        public int id { get; set; }
        public int idpracownika { get; set; }
        public int indwydarzenia { get; set; }
        public decimal kwota { get; set; }
        public bool zatwierdzone { get; set; }
    }
}
