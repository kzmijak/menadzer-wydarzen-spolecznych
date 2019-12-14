using System;
using System.Collections.Generic;
using System.Text;

namespace MWS.dbo
{
    class Uczestnik : _CoreObject
    {
        public int fid { get; set; }

        public List<Wydarzenie> wydarzenia
        {
            get
            {
                var output = new List<Wydarzenie>(9999);
                var jt = DataAccess.Wydarzenie_Uczestnik.GetCollection();
                foreach (Wydarzenie_Uczestnik ob in jt)
                    if (ob.iduczestnika == id)
                        output.Add(DataAccess.Wydarzenie.GetRecordById(ob.idwydarzenia) as Wydarzenie);
                return output;
            }
        }

        public List<Bilet> bilety
        {
            get
            {
                var output = new List<Bilet>(9999);
                var jt = DataAccess.Uczestnik_Bilet.GetCollection();
                foreach (Uczestnik_Bilet ob in jt)
                    if (ob.iduczestnika == id)
                        output.Add(DataAccess.Bilet.GetRecordById(ob.idbiletu) as Bilet);
                return output;
            }
        }
    }
}
