using System;
using System.Collections.Generic;
using System.Text;

namespace MWS.dbo
{
    class Wydarzenie_Sponsor : _JoiningTable
    {
        public int idwydarzenia { get; set; }
        public int idsponsora { get; set; }

        public static void Add(_DatabaseObject obj1, _DatabaseObject obj2)
        {
            if (DataAccess.GetRecord(obj1) is null)
                obj1 = DataAccess.Insert(obj1) as Wydarzenie;

            if (DataAccess.GetRecord(obj2) is null)
                obj2 = DataAccess.Insert(obj2) as Sponsor;

            DataAccess.Insert(obj1, obj2);
        }
    }
}
