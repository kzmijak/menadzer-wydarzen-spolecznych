using System;
using System.Data;
using System.Collections.Generic;
using System.Text;
using Dapper;
using System.Linq;

namespace MWS.dbo
{
    class Wydarzenie: _DatabaseObject
    {
        public string nazwa { get; set; }
        public string opis { get; set; }
        public string miejsce { get; set; }
        public DateTime dzien { get; set; }
        public TimeSpan godzina { get; set; }
        public decimal budzet { get; set; }

        public List<Pracownik> pracownicy
        {
            get
            {
                var output = new List<Pracownik>(9999);
                IEnumerable<Wydarzenie_Pracownik> jt;
                jt = DataAccess.GetConnections<Wydarzenie_Pracownik>().Cast<Wydarzenie_Pracownik>();
                foreach(Wydarzenie_Pracownik ob in jt)            
                    if(ob.idwydarzenia == id)
                    {
                        Pracownik pracownik = DataAccess.GetRecordById<Pracownik>(ob.idpracownika);
                        if(pracownik.stanowisko.ToLower() != "organizator")
                            output.Add(pracownik);                 
                    }
                return output;
            }
        }       
        public List<Pracownik> organizatorzy
        {
            get
            {
                var output = new List<Pracownik>(9999);
                IEnumerable<Wydarzenie_Pracownik> jt;
                jt = DataAccess.GetConnections<Wydarzenie_Pracownik>().Cast<Wydarzenie_Pracownik>();
                foreach (Wydarzenie_Pracownik ob in jt)
                    if (ob.idwydarzenia == id)
                    {
                        Pracownik pracownik = DataAccess.GetRecordById<Pracownik>(ob.idpracownika);
                        if (pracownik.stanowisko.ToLower() == "organizator")
                            output.Add(pracownik);
                    }
                return output;
            }
        }
        public List<Sponsor> sponsorzy
        {
            get
            {
                var output = new List<Sponsor>(9999);
                IEnumerable<Wydarzenie_Sponsor> jt;
                jt = DataAccess.GetConnections<Wydarzenie_Sponsor>().Cast<Wydarzenie_Sponsor>();
                foreach (Wydarzenie_Sponsor ob in jt)
                    if (ob.idwydarzenia == id)
                        output.Add(DataAccess.GetRecordById<Sponsor>(ob.idsponsora));
                return output;
            }
        }
        public List<Uczestnik> uczestnicy
        {
            get
            {
                var output = new List<Uczestnik>(9999);
                IEnumerable<Wydarzenie_Uczestnik> jt;
                jt = DataAccess.GetConnections<Wydarzenie_Uczestnik>().Cast<Wydarzenie_Uczestnik>();
                foreach (Wydarzenie_Uczestnik ob in jt)
                    if (ob.idwydarzenia == id)
                        output.Add(DataAccess.GetRecordById<Uczestnik>(ob.iduczestnika));
                return output;
            }
        }
        public List<_CoreObject> czlonkowie
        {
            get
            {
                var output = new List<_CoreObject>(9999);
                IEnumerable<Wydarzenie_Pracownik> wp;
                wp = DataAccess.GetConnections<Wydarzenie_Pracownik>();
                foreach (Wydarzenie_Pracownik ob in wp)
                    if (ob.idwydarzenia == id)
                    {
                        Pracownik pracownik = DataAccess.GetRecordById<Pracownik>(ob.idpracownika);
                        output.Add(pracownik);
                    }
                IEnumerable<Wydarzenie_Sponsor> ws;
                ws = DataAccess.GetConnections<Wydarzenie_Sponsor>();
                foreach (var ob in ws)
                    if (ob.idwydarzenia == id)
                    {
                        Sponsor s = DataAccess.GetRecordById<Sponsor>(ob.idsponsora);
                        output.Add(s);
                    }
                IEnumerable<Wydarzenie_Uczestnik> wu;
                wu = DataAccess.GetConnections<Wydarzenie_Uczestnik>();
                foreach (var ob in wu)
                    if (ob.idwydarzenia == id)
                    {
                        Uczestnik u = DataAccess.GetRecordById<Uczestnik>(ob.iduczestnika);
                        output.Add(u);
                    }
                return output;
            }
        }
        public List<Dotacja> dotacje
        {
            get
            {
                var output = new List<Dotacja>(9999);
                var ddb = DataAccess.GetCollection<Dotacja>().Cast<Dotacja>();
                foreach (Dotacja d in ddb)
                    if (d.idwydarzenia == id)
                        output.Add(d);
                return output;
            }
        }
        public List<Bilet> bilety
        {
            get
            {
                var output = new List<Bilet>(9999);
                var ddb = DataAccess.GetCollection<Bilet>().Cast<Bilet>();
                foreach (Bilet d in ddb)
                    if (d.idwydarzenia == id)
                        output.Add(d);
                return output;
            }
        }
    }
}
