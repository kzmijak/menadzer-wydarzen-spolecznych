using System;
using System.Collections.Generic;
using System.Text;

namespace MWS.dbo
{
    class Pracownik_Pracownik : _JoiningTable
    {
        public int idorganizatora { get; set; }
        public int idpracownika { get; set; }

        public static void Add(_DatabaseObject obj1, _DatabaseObject obj2)
        {
            if (DataAccess.GetRecord(obj1) is null)
                obj1 = DataAccess.Insert(obj1) as Pracownik;

            if (DataAccess.GetRecord(obj2) is null)
                obj2 = DataAccess.Insert(obj2) as Pracownik;

            DataAccess.Insert(obj1, obj2);
        }
    }
}
