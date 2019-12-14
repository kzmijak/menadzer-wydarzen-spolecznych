using System;
using System.Collections.Generic;
using System.Text;

namespace MWS.dbo
{
    class Platnosc : _DatabaseObject
    {
        public int idkarty { get; set; }
        public int idadresata { get; set; }
        public decimal kwota { get; set; }
        public DateTime dzien { get; set; }
        public TimeSpan godzina { get; set; }

        public KartaPlatnicza kartaPlatnicza
        {
            get
            {
                return DataAccess.KartaPlatnicza.GetRecordById(idkarty) as KartaPlatnicza;
            }
            set
            {
                if (kartaPlatnicza is null)
                    DataAccess.KartaPlatnicza.Insert(value);
                else
                    DataAccess.KartaPlatnicza.Update(kartaPlatnicza, value);
            }
        }
        public Kontakt odbiorca
        {
            get
            {
                return DataAccess.Kontakt.GetRecordById(idadresata) as Kontakt;
            }
            set
            {
                if (odbiorca is null)
                    DataAccess.Kontakt.Insert(value);
                else
                    DataAccess.Kontakt.Update(odbiorca, value);
            }
        }

    }
}
