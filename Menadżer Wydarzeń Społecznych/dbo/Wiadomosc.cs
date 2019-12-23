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
        public string tytul { get; set; }
        public string tresc { get; set; }

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
        public Logowanie odbiorca
        {
            get
            {
                return DataAccess.Logowanie.GetRecordById(idodbiorcy) as Logowanie;
            }
            set
            {
                if (odbiorca is null)
                    DataAccess.Logowanie.Insert(value);
                else
                    DataAccess.Logowanie.Update(odbiorca, value);
            }
        }

        public Wniosek wniosek
        {
            get
            {
                foreach(Wniosek w in DataAccess.Wniosek.GetCollection())
                {
                    if (w.idwiadomosci == id)
                        return w;
                }
                return null;
            }
            set
            {
                if (wniosek is null)
                {
                    DataAccess.Wniosek.Insert(value);
                }
                else
                    DataAccess.Wniosek.Update(wniosek, value);
            }
        }
    }
}
