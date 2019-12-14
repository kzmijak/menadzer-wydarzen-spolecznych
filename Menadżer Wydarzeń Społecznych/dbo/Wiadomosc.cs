using System;
using System.Collections.Generic;
using System.Text;

namespace MWS.dbo
{
    class Wiadomosc: _DatabaseObject
    {
        public int idadresata { get; set; } = 0;
        public int idodbiorcy { get; set; } = 0;
        public DateTime dzien { get; set; }
        public TimeSpan godzina { get; set; }
        public string tresc { get; set; }

        public Kontakt adresat
        {
            get
            {
                return DataAccess.Kontakt.GetRecordById(idadresata) as Kontakt;
            }
            set
            {
                if (adresat is null)
                    DataAccess.Kontakt.Insert(value);
                else
                    DataAccess.Kontakt.Update(adresat, value);
            }
        }
        public Kontakt odbiorca
        {
            get
            {
                return DataAccess.Kontakt.GetRecordById(idodbiorcy) as Kontakt;
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
