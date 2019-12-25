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
                var database = DataAccess.GetCollection<Logowanie>();
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
                        DataAccess.Insert(value);
                    }
                    else
                    {
                        DataAccess.Update(logowanie, value);
                    }
                }
            }
        }
        public Kontakt kontakt
        {
            get
            {
                Kontakt output = null;
                IEnumerable<_DatabaseObject> database = DataAccess.GetCollection<Kontakt>();
                foreach (_DatabaseObject @do in database)
                {
                    if ((@do as Kontakt).owner.id == id && (@do as Kontakt).owner.GetType() == GetType())
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
                        DataAccess.Insert(value);
                    }
                    else
                    {
                        DataAccess.Update(kontakt, value);
                    }
                }
            }
        }
    }
}
