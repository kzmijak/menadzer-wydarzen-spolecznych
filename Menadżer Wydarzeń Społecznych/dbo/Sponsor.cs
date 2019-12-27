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
        
        public List<Dotacja> dotacje
        {
            get
            {
                var output = new List<Dotacja>(9999);
                var ddb = DataAccess.GetCollection<Dotacja>().Cast<Dotacja>();
                foreach(var d in ddb)
                    if (d.idsponsora == id)
                        output.Add(d);
                return output;
            }
        }
    }
}
