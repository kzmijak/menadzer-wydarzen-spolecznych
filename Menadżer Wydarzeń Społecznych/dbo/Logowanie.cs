using System;
using System.Collections.Generic;
using System.Text;

namespace MWS.dbo
{
    class Logowanie : _DatabaseObject
    { 
        public string login { get; set; }
        public string haslo { get; set; }
        public int idpracownika { get; set; } = 0;
        public int idsponsora { get; set; } = 0;
        public int iduczestnika { get; set; } = 0;

        public _CoreObject owner
        {
            get
            {
                if (idpracownika != 0)
                    return pracownik;
                if (idsponsora != 0)
                    return sponsor;
                if (iduczestnika != 0)
                    return uczestnik;
                else return null;
            }
        }

        public Pracownik pracownik
        {
            get
            {
                return DataAccess.Pracownik.GetRecordById(idpracownika) as Pracownik;
            }
        }

        public Sponsor sponsor
        {
            get 
            {
                return DataAccess.Sponsor.GetRecordById(idsponsora) as Sponsor;
            }
        }

        public Uczestnik uczestnik
        {
            get
            {
                return DataAccess.Uczestnik.GetRecordById(iduczestnika) as Uczestnik;
            }
        }
    }
}
