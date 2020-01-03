using System;
using System.Collections.Generic;
using System.Text;

namespace MWS.dbo
{
    class Wydarzenie_Uczestnik : _JoiningTable
    {
        public int idwydarzenia { get; set; }
        public int iduczestnika { get; set; }

        public int id1
        {
            get
            {
                return idwydarzenia;
            }
            set
            {
                idwydarzenia = value;
            }
        }
        public int id2
        {
            get
            {
                return iduczestnika;
            }
            set
            {
                iduczestnika = value;
            }
        }

        public static void Add(_DatabaseObject obj1, _DatabaseObject obj2)
        {
            if (DataAccess.GetRecordById<Wydarzenie>(obj1.id) is null)
                obj1 = DataAccess.Insert(obj1) as Wydarzenie;

            if (DataAccess.GetRecordById<Uczestnik>(obj2.id) is null)
                obj2 = DataAccess.Insert(obj2) as Uczestnik;

            if (!DataAccess.IsInDb<Wydarzenie_Uczestnik>(obj1, obj2))
            {
                DataAccess.Insert(obj1, obj2);
            }
            else
            {
                throw new Exception("Jesteś już połączony z tym wydarzeniem");
            }
        }
    }
}
