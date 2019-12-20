using System;
using System.Collections.Generic;
using System.Text;

namespace MWS.dbo
{
    class Wniosek : _DatabaseObject
    {
        public int idpracownika { get; set; }
        public int idwydarzenia { get; set; }
        public decimal kwota { get; set; }
        public bool zatwierdzone { get; set; } = false;

        public Pracownik pracownik
        {
            get
            {
                return DataAccess.Pracownik.GetRecordById(idpracownika) as Pracownik;
            }
            set
            {
                if (pracownik is null)
                    DataAccess.Pracownik.Insert(value);
                else
                    DataAccess.Pracownik.Update(pracownik, value);
            }
        }
        public Wydarzenie wydarzenie
        {
            get
            {
                return DataAccess.Wydarzenie.GetRecordById(idwydarzenia) as Wydarzenie;
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
