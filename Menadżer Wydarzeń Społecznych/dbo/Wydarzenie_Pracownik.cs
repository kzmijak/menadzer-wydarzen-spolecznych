using System;
using System.Collections.Generic;
using System.Text;

namespace MWS.dbo
{
    class Wydarzenie_Pracownik : _JoiningTable
    {
        public int idwydarzenia { get; set; }
        public int idpracownika { get; set; }

        public static void Add(_DatabaseObject obj1, _DatabaseObject obj2)
        {
            if(DataAccess.Wydarzenie.GetRecord(obj1) is null)
                obj1 = DataAccess.Wydarzenie.Insert(obj1) as Wydarzenie;

            if (DataAccess.Pracownik.GetRecord(obj2) is null)
                obj2 = DataAccess.Pracownik.Insert(obj2) as Pracownik;
            
            DataAccess.Wydarzenie_Pracownik.Insert(obj1, obj2);
        }
    }
}
