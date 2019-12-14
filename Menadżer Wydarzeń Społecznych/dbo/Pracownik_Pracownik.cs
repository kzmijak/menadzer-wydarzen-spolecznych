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
            if (DataAccess.Pracownik.GetRecord(obj1) is null)
                obj1 = DataAccess.Pracownik.Insert(obj1) as Pracownik;

            if (DataAccess.Pracownik.GetRecord(obj2) is null)
                obj2 = DataAccess.Pracownik.Insert(obj2) as Pracownik;

            DataAccess.Pracownik_Pracownik.Insert(obj1, obj2);
        }
    }
}
