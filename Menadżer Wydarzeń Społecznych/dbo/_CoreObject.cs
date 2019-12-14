using System;
using System.Collections.Generic;
using System.Text;

namespace MWS.dbo
{
    abstract class _CoreObject: _DatabaseObject
    {
        public Logowanie logowanie
        {
            get
            {
                Logowanie output = new Logowanie();
                IEnumerable<_DatabaseObject> database = DataAccess.Logowanie.GetCollection();
                foreach (_DatabaseObject @do in database)
                {
                    if ((@do as Logowanie).owner.id == id && (@do as Logowanie).owner.GetType() == this.GetType())
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
                IEnumerable<_DatabaseObject> database = DataAccess.Kontakt.GetCollection();
                foreach (_DatabaseObject @do in database)
                {
                    if ((@do as Kontakt).owner.id == id && (@do as Kontakt).owner.GetType() == this.GetType())
                    {
                        output = @do as Kontakt;
                    }
                }
                return output;
            }
        }

        public List<Wydarzenie> wydarzenia { get; set; } = new List<Wydarzenie>(255);
    }
}
