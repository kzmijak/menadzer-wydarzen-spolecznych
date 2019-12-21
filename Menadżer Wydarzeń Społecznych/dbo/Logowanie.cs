using System;
using System.Collections.Generic;
using System.Linq;
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
            set
            {
                if (owner is null)
                {
                    if (value is Pracownik)
                        value = DataAccess.Pracownik.Insert(value) as Pracownik;
                    if (value is Sponsor)
                        value = DataAccess.Sponsor.Insert(value) as Sponsor;
                    if (value is Uczestnik)
                        value = DataAccess.Uczestnik.Insert(value) as Uczestnik;
                    value.logowanie = this;
                }
                else
                {
                    if (value is Pracownik)
                        DataAccess.Pracownik.Update(pracownik, value);
                    if (value is Sponsor)
                        DataAccess.Sponsor.Update(sponsor, value);
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
                if (pracownik is null)
                {
                    value = DataAccess.Pracownik.Insert(value) as Pracownik;
                    value.logowanie = this;
                }
                else
                    DataAccess.Pracownik.Update(pracownik, value);
            }
        }
        public Sponsor sponsor
        {
            get 
            {
                return DataAccess.Sponsor.GetRecordById(idsponsora) as Sponsor;
            }
            set
            {
                if (sponsor is null)
                {
                    value = DataAccess.Sponsor.Insert(value) as Sponsor;
                    value.logowanie = this;
                }
                else
                    DataAccess.Sponsor.Update(sponsor, value);
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
                if (uczestnik is null)
                {
                    value = DataAccess.Uczestnik.Insert(value) as Uczestnik;
                    value.logowanie = this;
                }
                else
                    DataAccess.Uczestnik.Update(uczestnik, value);
            }
        }
        public List<Logowanie> kontakty
        {
            get
            {
                var output = new List<Logowanie>(9999);
                var jt = DataAccess.Logowanie_Logowanie.GetCollection();
                foreach (Logowanie_Logowanie ob in jt)
                    if (ob.idlogowania1 == id)
                        output.Add(DataAccess.Logowanie.GetRecordById(ob.idlogowania2) as Logowanie);
                return output;
            }
        }
    }
}
