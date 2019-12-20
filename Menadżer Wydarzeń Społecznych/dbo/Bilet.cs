using System;
using System.Collections.Generic;
using System.Text;

namespace MWS.dbo
{
    class Bilet : _DatabaseObject
    {
        public int idwydarzenia { get; set; }
        public string nazwa { get; set; }
        public decimal cena { get; set; }
        public string opis { get; set; }

        public List<Uczestnik> uczestnicy
        {
            get
            {
                var output = new List<Uczestnik>(9999);
                var jt = DataAccess.Uczestnik_Bilet.GetCollection();
                foreach (Uczestnik_Bilet ob in jt)
                    if (ob.idbiletu == id)
                        output.Add(DataAccess.Uczestnik.GetRecordById(ob.iduczestnika) as Uczestnik);
                return output;
            }
        }

        public Wydarzenie wydarzenie
        {
            get
            {
                return DataAccess.Wydarzenie.GetRecordById(idwydarzenia) as Wydarzenie;
            }
            set
            {
                if (wydarzenie is null)
                    DataAccess.Wydarzenie.Insert(value);
                else
                    DataAccess.Wydarzenie.Update(wydarzenie, value);
            }
        }
    }
}
