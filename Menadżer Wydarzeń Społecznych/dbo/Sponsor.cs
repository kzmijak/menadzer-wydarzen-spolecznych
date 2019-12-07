using System;
using System.Collections.Generic;
using System.Text;

namespace MWS.dbo
{
    class Sponsor: DatabaseObject
    {
        public string nazwa { get; set; }

        public Logowanie logowanie
        {
            get
            {
                Logowanie output = new Logowanie();
                IEnumerable<DatabaseObject> database = DataAccess.Logowanie.GetCollection();
                foreach (DatabaseObject @do in database)
                {
                    if ((@do as Logowanie).idsponsora == id)
                    {
                        output = @do as Logowanie;
                    }
                }
                return output;
            }
        }


    }
}
