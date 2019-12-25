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
                var jt = DataAccess.GetConnections<Uczestnik_Bilet>();
                foreach (Uczestnik_Bilet ob in jt)
                    if (ob.idbiletu == id)
                        output.Add(DataAccess.GetRecordById<Uczestnik>(ob.iduczestnika));
                return output;
            }
        }

        public Wydarzenie wydarzenie
        {
            get
            {
                return DataAccess.GetRecordById<Wydarzenie>(idwydarzenia);
            }
            set
            {
                if (wydarzenie is null)
                    DataAccess.Insert(value);
                else
                    DataAccess.Update(wydarzenie, value);
            }
        }
    }
}
