using System;
using System.Collections.Generic;
using System.Text;

namespace MWS.dbo
{
    class Wniosek : _DatabaseObject
    {
        public int idadresata { get; set; }
        public int idodbiorcy { get; set; }
        public decimal kwota { get; set; }
        public bool zatwierdzone { get; set; } = false;

        public Logowanie adresat
        {
            get
            {
                return DataAccess.Logowanie.GetRecordById(idadresata) as Logowanie;
            }
            set
            {
                if (adresat is null)
                    DataAccess.Logowanie.Insert(value);
                else
                    DataAccess.Logowanie.Update(adresat, value);
            }
        }
        public Wydarzenie wydarzenie
        {
            get
            {
                return DataAccess.Wydarzenie.GetRecordById(idodbiorcy) as Wydarzenie;
            }
            set
            {
                if (wydarzenie is null)
                    DataAccess.Wydarzenie.Insert(value);
                else
                    DataAccess.Wydarzenie.Update(wydarzenie, value);
            }
        }
    }
}
