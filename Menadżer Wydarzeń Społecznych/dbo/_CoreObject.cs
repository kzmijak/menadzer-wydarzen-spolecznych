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
                if(value != null && value is Logowanie)
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
                var database = DataAccess.GetCollection<Kontakt>();
                foreach (Kontakt found in database)
                {
                    if (found.owner.id == id && found.owner.GetType() == this.GetType())
                    {
                        output = found;
                    }
                }
                return output;
            }
            set
            {
                if(value != null && value is Kontakt)
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
        public List<Wydarzenie> wydarzenia
        {
            get
            {
                if (this is Pracownik)
                {
                    var output = new List<Wydarzenie>(9999);
                    var jt = DataAccess.GetConnections<Wydarzenie_Pracownik>();
                    foreach (Wydarzenie_Pracownik ob in jt)
                        if (ob.idpracownika == id)
                            output.Add(DataAccess.GetRecordById<Wydarzenie>(ob.idwydarzenia));
                    return output;
                }
                else if (this is Sponsor)
                {
                    var output = new List<Wydarzenie>(9999);
                    var jt = DataAccess.GetConnections<Wydarzenie_Sponsor>();
                    foreach (Wydarzenie_Sponsor ob in jt)
                        if (ob.idsponsora == id)
                            output.Add(DataAccess.GetRecordById<Wydarzenie>(ob.idwydarzenia));
                    return output;
                }
                else if (this is Uczestnik)
                {
                    var output = new List<Wydarzenie>(9999);
                    var jt = DataAccess.GetConnections<Wydarzenie_Uczestnik>();
                    foreach (Wydarzenie_Uczestnik ob in jt)
                        if (ob.iduczestnika == id)
                            output.Add(DataAccess.GetRecordById<Wydarzenie>(ob.idwydarzenia));
                    return output;
                }
                else return null;
            }
        }
    }
}
