using MWS.dbo;
using System;
using System.Collections.Generic;
using System.Text;

namespace MWS.dbo
{
    class KartaPlatnicza : _DatabaseObject
    {
        public string wlasciciel { get; set; }
        public string numer { get; set; }
        public string wygasniecie { get; set; }
        public string kbezpiecz { get; set; }
        public int kontakt { get; set; }

        public Kontakt owner
        {
            get
            {
                return DataAccess.GetRecordById<Kontakt>(kontakt);
            }
        }
        public List<Platnosc> platnosci
        {
            get
            {
                var output = new List<Platnosc>(9999);
                var platnosci = DataAccess.GetCollection<Platnosc>();
                foreach (Platnosc ob in platnosci)
                    if (ob.idkarty == id)
                        output.Add(DataAccess.GetRecordById<Platnosc>(ob.idwydarzenia) as Platnosc);
                return output;
            }
        }
    }
}
