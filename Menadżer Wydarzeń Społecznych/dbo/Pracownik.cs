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
                var jt = DataAccess.Wydarzenie_Pracownik.GetCollection();
                foreach(Wydarzenie_Pracownik ob in jt)
                    if (ob.idpracownika == id)
                        output.Add(DataAccess.Wydarzenie.GetRecordById(ob.idwydarzenia) as Wydarzenie);
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
                    var jt = DataAccess.Pracownik_Pracownik.GetCollection();
                    foreach (Pracownik_Pracownik ob in jt)
                        if (ob.idorganizatora == id)
                            output.Add(DataAccess.Pracownik.GetRecordById(ob.idpracownika) as Pracownik);
                    return output;
                }
                else
                {
                    var output = new List<Pracownik>(9999);
                    var jt = DataAccess.Pracownik_Pracownik.GetCollection();
                    foreach (Pracownik_Pracownik ob in jt)
                        if (ob.idpracownika == id)
                            output.Add(DataAccess.Pracownik.GetRecordById(ob.idorganizatora) as Pracownik);
                    return output;
                }
            }
        }

    }
}
