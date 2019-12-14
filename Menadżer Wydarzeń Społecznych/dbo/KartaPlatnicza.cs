using MWS.dbo;
using System;
using System.Collections.Generic;
using System.Text;

namespace MWS.dbo
{
    class KartaPlatnicza : _DatabaseObject
    {
        public int wlasciciel { get; set; }
        public string numer { get; set; }
        public string wygasniecie { get; set; }
        public string kbezpiecz { get; set; }
        public int kontakt { get; set; }

        public Kontakt owner
        {
            get
            {
                return DataAccess.Kontakt.GetRecordById(kontakt) as Kontakt;
            }
        }
        public List<Platnosc> platnosci
        {
            get
            {
                var output = new List<Platnosc>(9999);
                var platnosci = DataAccess.Platnosc.GetCollection();
                foreach (Platnosc ob in platnosci)
                    if (ob.idkarty == id)
                        output.Add(DataAccess.Platnosc.GetRecordById(ob.idadresata) as Platnosc);
                return output;
            }
        }
    }
}
