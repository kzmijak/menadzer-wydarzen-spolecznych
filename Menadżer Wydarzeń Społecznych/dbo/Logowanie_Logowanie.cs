using System;
using System.Collections.Generic;
using System.Text;

namespace MWS.dbo
{
    class Logowanie_Logowanie : _JoiningTable
    {
        public int idlogowania1 { get; set; }
        public int idlogowania2 { get; set; }

        public int id1
        {
            get
            {
                return idlogowania1;
            }
            set
            {
                idlogowania1 = value;
            }
        }
        public int id2
        {
            get
            {
                return idlogowania2;
            }
            set
            {
                idlogowania2 = value;
            }
        }


        public static void Add(_DatabaseObject obj1, _DatabaseObject obj2)
        {
            if (DataAccess.GetRecordById<Logowanie>(obj1.id) is null)
                obj1 = DataAccess.Insert(obj1) as Logowanie;

            if (DataAccess.GetRecordById<Logowanie>(obj2.id) is null)
                obj2 = DataAccess.Insert(obj2) as Logowanie;

            if(!DataAccess.IsInDb<Logowanie_Logowanie>(obj1, obj2) && !DataAccess.IsInDb<Logowanie_Logowanie>(obj2, obj1))
            {
                DataAccess.Insert(obj1, obj2);
                DataAccess.Insert(obj2, obj1);
            }
            else
            {
                throw new Exception("Jesteś już połączony z tym użytkownikiem");
            }
        }
    }
}
