using System;
using System.Collections.Generic;
using System.Text;

namespace MWS.dbo
{
    class Dotacja : _DatabaseObject
    {
        public int idwydarzenia { get; set; }
        public int idsponsora { get; set; }
        public string oczekiwania { get; set; }
        public decimal kwota { get; set; }
        public bool zatwierdzone { get; set; }

        public Wydarzenie wydarzenie
        {
            get
            {
                return DataAccess.GetRecordById<Wydarzenie>(idwydarzenia) as Wydarzenie;
            }
            set
            {
                if (wydarzenie is null)
                    DataAccess.Insert(value);
                else
                    DataAccess.Update(wydarzenie, value);
            }
        }
        public Sponsor sponsor
        {
            get
            {
                return DataAccess.GetRecordById<Sponsor>(idsponsora) as Sponsor;
            }
            set
            {
                if (sponsor is null)
                    DataAccess.Insert(value);
                else
                    DataAccess.Update(sponsor, value);
            }
        }
    }
}
