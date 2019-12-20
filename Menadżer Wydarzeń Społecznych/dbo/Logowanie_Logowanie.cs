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
        if (DataAccess.Logowanie.GetRecord(obj1) is null)
            obj1 = DataAccess.Logowanie.Insert(obj1) as Logowanie;

        if (DataAccess.Logowanie.GetRecord(obj2) is null)
            obj2 = DataAccess.Logowanie.Insert(obj2) as Logowanie;

        DataAccess.Logowanie_Logowanie.Insert(obj1, obj2);
    }
}
}
