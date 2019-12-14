using System;
using System.Collections.Generic;
using System.Text;

namespace MWS.dbo
{
    class Kontakt : _DatabaseObject
    {
        public string imie { get; set; }
        public string nazwisko { get; set; }
        public string telefon { get; set; }
        public string email { get; set; }
        public string miejscowosc { get; set; }
        public string nrdomu { get; set; }
        public string miasto { get; set; }
        public string poczta { get; set; }
        public string ulica { get; set; }
        public int idpracownika { get; set; } = 0;
        public int iduczestnika { get; set; } = 0;

        public _CoreObject owner
        {
            get
            {
                if (iduczestnika != 0)
                    return uczestnik;
                if (idpracownika != 0)
                    return pracownik;
                else return null;
            }
            set
            {
                if(value != null && owner != null)
                {            
                    if (value is Pracownik)
                        DataAccess.Pracownik.Update(pracownik, value);
                    if (value is Uczestnik)
                        DataAccess.Uczestnik.Update(uczestnik, value);            
                }
            }
        }
        public Pracownik pracownik
        {
            get
            {
                return DataAccess.Pracownik.GetRecordById(idpracownika) as Pracownik;
            }
            set
            {
                if(pracownik!=null)
                    DataAccess.Pracownik.Update(pracownik, value);
            }
        }
        public Uczestnik uczestnik
        {
            get
            {
                return DataAccess.Uczestnik.GetRecordById(iduczestnika) as Uczestnik;
            }
            set
            {
                if(uczestnik != null)
                    DataAccess.Uczestnik.Update(uczestnik, value);
            }
        }

        public KartaPlatnicza kartaPlatnicza
        {
            get
            {
                var output = new KartaPlatnicza();
                var karty = DataAccess.KartaPlatnicza.GetCollection();
                foreach (KartaPlatnicza karta in karty)
                    if (karta.kontakt == id)
                        output = karta;
                return output;
            }
            set
            {
                if(kartaPlatnicza == null)
                    DataAccess.KartaPlatnicza.Insert(value);
                else
                    DataAccess.KartaPlatnicza.Update(kartaPlatnicza, value);
            }
        }
    }
}
