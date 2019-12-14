using System;
using System.Collections.Generic;
using System.Text;

namespace MWS.dbo
{
    class Wydarzenie_Uczestnik : _JoiningTable
    {
        public int idwydarzenia { get; set; }
        public int iduczestnika { get; set; }

        public static void Add(_DatabaseObject obj1, _DatabaseObject obj2)
        {
            if (DataAccess.Wydarzenie.GetRecord(obj1) is null)
                obj1 = DataAccess.Wydarzenie.Insert(obj1) as Wydarzenie;

            if (DataAccess.Uczestnik.GetRecord(obj2) is null)
                obj2 = DataAccess.Uczestnik.Insert(obj2) as Uczestnik;

            DataAccess.Wydarzenie_Uczestnik.Insert(obj1, obj2);
        }
    }
}
