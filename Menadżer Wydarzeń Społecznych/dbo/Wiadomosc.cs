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
                return DataAccess.GetRecordById<Logowanie>(idadresata) as Logowanie;
            }
            set
            {
                if (adresat is null)
                    DataAccess.Insert(value);
                else
                    DataAccess.Update(adresat, value);
            }
        }
        public Logowanie odbiorca
        {
            get
            {
                return DataAccess.GetRecordById<Logowanie>(idodbiorcy) as Logowanie;
            }
            set
            {
                if (odbiorca is null)
                    DataAccess.Insert(value);
                else
                    DataAccess.Update(odbiorca, value);
            }
        }

        public Wniosek wniosek
        {
            get
            {
                foreach(Wniosek w in DataAccess.GetCollection<Wniosek>())
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
                    DataAccess.Insert(value);
                }
                else
                    DataAccess.Update(wniosek, value);
            }
        }

        public static void Send(string title, string message, Logowanie sender, Logowanie receiver, Wniosek addition = null)
        {
            Wiadomosc wiadomosc = new Wiadomosc
            {
                idadresata = sender.id,
                idodbiorcy = receiver.id,
                dzien = DateTime.Now,
                godzina = DateTime.Now.TimeOfDay,
                tytul = title,
                tresc = message
            };

            if (sender.id != receiver.id)
            {
                wiadomosc = DataAccess.Insert(wiadomosc) as Wiadomosc;
                if (addition != null)
                {
                    addition.idwiadomosci = wiadomosc.id;
                    addition.zatwierdzone = false;
                    DataAccess.Insert(addition);
                }
            }
        }
    }
}
