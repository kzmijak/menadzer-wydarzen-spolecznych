﻿using System;
using System.Collections.Generic;
using System.Text;

namespace MWS.dbo
{
    class Wniosek : _DatabaseObject
    {
        public int idwiadomosci { get; set; }
        public decimal kwota { get; set; }
        public string akcja { get; set; }
        public bool zatwierdzone { get; set; }

        public Wiadomosc wiadomosc
        {
            get
            {
                return DataAccess.GetRecordById<Wiadomosc>(idwiadomosci) as Wiadomosc;
            }
            set
            {
                if (wiadomosc is null)
                    DataAccess.Insert(value);
                else
                    DataAccess.Update(wiadomosc, value);
            }
        }
    }
}
