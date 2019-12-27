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
            if (DataAccess.GetRecordById<Uczestnik>(obj1.id) is null)
                obj1 = DataAccess.Insert(obj1) as Uczestnik;

            if (DataAccess.GetRecordById<Bilet>(obj2.id) is null)
                obj2 = DataAccess.Insert(obj2) as Bilet;

            DataAccess.Insert(obj1, obj2);
        }
    }
}
