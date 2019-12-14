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
                Logowanie output = null;
                var database = DataAccess.Logowanie.GetCollection();
                foreach (var @do in database)
                {
                    if ((@do as Logowanie).owner.id == id && (@do as Logowanie).owner.GetType() == this.GetType())
                    {
                        output = @do as Logowanie;
                    }
                }
                return output;
            }
            set
            {
                if(value != null)
                {
                    if(logowanie is null)
                    {
                        DataAccess.Logowanie.Insert(value);
                    }
                    else
                    {
                        DataAccess.Logowanie.Update(logowanie, value);
                    }
                }
            }
        }
        public Kontakt kontakt
        {
            get
            {
                Kontakt output = null;
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
            set
            {
                if(value != null)
                {
                    if (kontakt is null)
                    {
                        DataAccess.Kontakt.Insert(value);
                    }
                    else
                    {
                        DataAccess.Kontakt.Update(kontakt, value);
                    }
                }
            }
        }
    }
}
