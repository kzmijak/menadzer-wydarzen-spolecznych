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
                return DataAccess.GetRecordById<KartaPlatnicza>(idkarty) as KartaPlatnicza;
            }
            set
            {
                if (kartaPlatnicza is null)
                    DataAccess.Insert(value);
                else
                    DataAccess.Update(kartaPlatnicza, value);
            }
        }
        public Kontakt odbiorca
        {
            get
            {
                return DataAccess.GetRecordById<Kontakt>(idadresata) as Kontakt;
            }
            set
            {
                if (odbiorca is null)
                    DataAccess.Insert(value);
                else
                    DataAccess.Update(odbiorca, value);
            }
        }

    }
}
