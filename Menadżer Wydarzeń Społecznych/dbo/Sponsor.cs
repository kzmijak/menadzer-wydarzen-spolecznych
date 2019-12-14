using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MWS.dbo
{
    class Sponsor: _CoreObject
    {
        public string nazwa { get; set; }
        public new Kontakt kontakt = null;

        public List<Wydarzenie> wydarzenia
        {
            get
            {
                var output = new List<Wydarzenie>(9999);
                var jt = DataAccess.Wydarzenie_Sponsor.GetCollection();
                foreach (Wydarzenie_Sponsor ob in jt)
                    if (ob.idsponsora == id)
                        output.Add(DataAccess.Wydarzenie.GetRecordById(ob.idwydarzenia) as Wydarzenie);
                return output;
            }
        }
        public List<Dotacja> dotacje
        {
            get
            {
                var output = new List<Dotacja>(9999);
                var ddb = DataAccess.Dotacja.GetCollection().Cast<Dotacja>();
                foreach(var d in ddb)
                    if (d.idsponsora == id)
                        output.Add(d);
                return output;
            }
        }
    }
}
