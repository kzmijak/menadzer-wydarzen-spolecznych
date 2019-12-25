using System;
using System.Collections.Generic;
using System.Text;

namespace MWS.dbo
{
    class Logowanie_Logowanie : _JoiningTable
    {
        public int idlogowania1 { get; set; }
        public int idlogowania2 { get; set; }

    public static void Add(_DatabaseObject obj1, _DatabaseObject obj2)
    {
        if (DataAccess.GetRecord(obj1) is null)
            obj1 = DataAccess.Insert(obj1) as Logowanie;

        if (DataAccess.GetRecord(obj2) is null)
            obj2 = DataAccess.Insert(obj2) as Logowanie;

        DataAccess.Insert(obj1, obj2);
    }
}
}
