using System;
using System.Collections.Generic;
using System.Text;

namespace MWS.dbo
{
    class Uczestnik : _CoreObject
    {
        public int fid { get; set; }
        

        public List<Bilet> bilety
        {
            get
            {
                var output = new List<Bilet>(9999);
                var jt = DataAccess.GetConnections<Uczestnik_Bilet>();
                foreach (Uczestnik_Bilet ob in jt)
                    if (ob.iduczestnika == id)
                        output.Add(DataAccess.GetRecordById<Bilet>(ob.idbiletu) as Bilet);
                return output;
            }
        }
    }
}
