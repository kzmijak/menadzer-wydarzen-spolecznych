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
        public Sponsor sponsor
        {
            get
            {
                return DataAccess.Sponsor.GetRecordById(idsponsora) as Sponsor;
            }
            set
            {
                if (sponsor is null)
                    DataAccess.Sponsor.Insert(value);
                else
                    DataAccess.Sponsor.Update(sponsor, value);
            }
        }
    }
}
