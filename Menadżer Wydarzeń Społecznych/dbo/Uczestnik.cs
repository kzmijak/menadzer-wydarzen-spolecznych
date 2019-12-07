using System;
using System.Collections.Generic;
using System.Text;

namespace MWS.dbo
{
    class Uczestnik : DatabaseObject
    {
        public int fid { get; set; }

        public Logowanie logowanie
        {
            get
            {
                Logowanie output = new Logowanie();
                IEnumerable<DatabaseObject> database = DataAccess.Logowanie.GetCollection();
                foreach (DatabaseObject @do in database)
                {
                    if ((@do as Logowanie).iduczestnika == id)
                    {
                        output = @do as Logowanie;
                    }
                }
                return output;
            }
        }
        public Kontakt kontakt
        {
            get
            {
                Kontakt output = new Kontakt();
                IEnumerable<DatabaseObject> database = DataAccess.Kontakt.GetCollection();
                foreach (DatabaseObject @do in database)
                {
                    if ((@do as Kontakt).iduczestnika == id)
                    {
                        output = @do as Kontakt;
                    }
                }
                return output;
            }
        }


    }
}
