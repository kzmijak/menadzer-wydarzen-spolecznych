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
            if (DataAccess.GetRecordById<Wydarzenie>(obj1.id) is null)
                obj1 = DataAccess.Insert(obj1) as Wydarzenie;

            if (DataAccess.GetRecordById<Uczestnik>(obj2.id) is null)
                obj2 = DataAccess.Insert(obj2) as Uczestnik;

            DataAccess.Insert(obj1, obj2);
        }
    }
}
