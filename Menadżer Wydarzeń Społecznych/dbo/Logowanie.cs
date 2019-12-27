using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace MWS.dbo
{
    class Logowanie : _DatabaseObject
    { 
        public string login { get; set; }
        public string haslo { get; set; }
        public int idpracownika { get; set; } = 0;
        public int idsponsora { get; set; } = 0;
        public int iduczestnika { get; set; } = 0;

        public _CoreObject owner
        {
            get
            {
                if (idpracownika != 0)
                    return pracownik;
                if (idsponsora != 0)
                    return sponsor;
                if (iduczestnika != 0)
                    return uczestnik;
                else return null;
            }
            set
            {
                if (owner is null)
                {
                    if (value is Pracownik)
                        value = DataAccess.Insert(value) as Pracownik;
                    if (value is Sponsor)
                        value = DataAccess.Insert(value) as Sponsor;
                    if (value is Uczestnik)
                        value = DataAccess.Insert(value) as Uczestnik;
                    value.logowanie = this;
                }
                else
                {
                    if (value is Pracownik)
                        DataAccess.Update(pracownik, value);
                    if (value is Sponsor)
                        DataAccess.Update(sponsor, value);
                    if (value is Uczestnik)
                        DataAccess.Update(uczestnik, value);
                }
            }
        }
        public Pracownik pracownik
        {
            get
            {
                return DataAccess.GetRecordById<Pracownik>(idpracownika);
            }
            set
            {
                if (pracownik is null)
                {
                    value = DataAccess.Insert(value) as Pracownik;
                    value.logowanie = this;
                }
                else
                    DataAccess.Update(pracownik, value);
            }
        }
        public Sponsor sponsor
        {
            get 
            {
                return DataAccess.GetRecordById<Sponsor>(idsponsora);
            }
            set
            {
                if (sponsor is null)
                {
                    value = DataAccess.Insert(value) as Sponsor;
                    value.logowanie = this;
                }
                else
                    DataAccess.Update(sponsor, value);
            }
        }
        public Uczestnik uczestnik
        {
            get
            {
                return DataAccess.GetRecordById<Uczestnik>(iduczestnika);
            }
            set
            {
                if (uczestnik is null)
                {
                    value = DataAccess.Insert(value) as Uczestnik;
                    value.logowanie = this;
                }
                else
                    DataAccess.Update(uczestnik, value);
            }
        }
        public List<Logowanie> kontakty
        {
            get
            {
                var output = new List<Logowanie>(9999);
                var jt = DataAccess.GetConnections<Logowanie_Logowanie>();
                foreach (Logowanie_Logowanie ob in jt)
                    if (ob.idlogowania1 == id)
                        output.Add(DataAccess.GetRecordById<Logowanie>(ob.idlogowania2));
                return output;
            }
        }

        public static Logowanie CheckCredentials(_DatabaseObject databaseObject)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(DbHelper.CnnVal("cnMWS")))
            {
                try
                {
                    return connection.QuerySingle<Logowanie>("dbo.Logowanie_CheckCredentials @login, @haslo", databaseObject);
                }
                catch (InvalidOperationException)
                {
                    return null;
                }
            }
        }
    }
}
