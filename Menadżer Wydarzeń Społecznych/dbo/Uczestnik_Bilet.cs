using System;
using System.Collections.Generic;
using System.Text;

namespace MWS.dbo
{
    class Uczestnik_Bilet : _JoiningTable
    {
        public int iduczestnika { get; set; }
        public int idbiletu { get; set; }

        public static void Add(_DatabaseObject obj1, _DatabaseObject obj2)
        {
            if (DataAccess.Uczestnik.GetRecord(obj1) is null)
                obj1 = DataAccess.Uczestnik.Insert(obj1) as Uczestnik;

            if (DataAccess.Bilet.GetRecord(obj2) is null)
                obj2 = DataAccess.Bilet.Insert(obj2) as Bilet;

            DataAccess.Uczestnik_Bilet.Insert(obj1, obj2);
        }
    }
}
