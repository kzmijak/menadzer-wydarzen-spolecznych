using System;
using System.Collections.Generic;
using System.Text;

namespace MWS.dbo
{
    class Pracownik: _CoreObject
    {
        public string stanowisko { get; set; }
        public decimal wynagrodzenie { get; set; }

        public List<Wydarzenie> wydarzenia
        {
            get
            {
                var output = new List<Wydarzenie>(9999);
                var jt = DataAccess.GetConnections<Wydarzenie_Pracownik>();
                foreach(Wydarzenie_Pracownik ob in jt)
                    if (ob.idpracownika == id)
                        output.Add(DataAccess.GetRecordById<Wydarzenie>(ob.idwydarzenia) as Wydarzenie);
                return output;
            }
        }
        public List<Pracownik> kadra
        {
            get
            {
                if (stanowisko.ToLower() == "organizator")
                {
                    var output = new List<Pracownik>(9999);
                    var jt = DataAccess.GetConnections<Pracownik_Pracownik>();
                    foreach (Pracownik_Pracownik ob in jt)
                        if (ob.idorganizatora == id)
                            output.Add(DataAccess.GetRecordById<Pracownik>(ob.idpracownika) as Pracownik);
                    return output;
                }
                else
                {
                    var output = new List<Pracownik>(9999);
                    var jt = DataAccess.GetConnections<Pracownik_Pracownik>();
                    foreach (Pracownik_Pracownik ob in jt)
                        if (ob.idpracownika == id)
                            output.Add(DataAccess.GetRecordById<Pracownik>(ob.idorganizatora) as Pracownik);
                    return output;
                }
            }
        }

    }
}
