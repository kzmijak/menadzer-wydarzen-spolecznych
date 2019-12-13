using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Dapper;
using MWS.dbo;

namespace MWS.Procedures
{
    class PSetKontakt : DatabaseObjectProcedures
    {
        public void Delete(DatabaseObject dbobject)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(DbHelper.CnnVal("cnMWS")))
            {
                connection.Execute("dbo.Kontakt_Delete @id", dbobject);
            }
        }

        public List<DatabaseObject> GetCollection()
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(DbHelper.CnnVal("cnMWS")))
            {
                return connection.Query<Kontakt>("dbo.Kontakt_GetCollection").Cast<DatabaseObject>().ToList();
            }
        }

        public DatabaseObject GetRecord(DatabaseObject dbobject)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(DbHelper.CnnVal("cnMWS")))
            {
                return connection.Query<DatabaseObject>("dbo.Kontakt_GetRecord @imie, @nazwisko, @telefon, @email, @miejscowosc, @nrdomu, @miasto, @poczta, @ulica, @idpracownika, @iduczestnika", dbobject).ToList()[0];
            }
        }

        public DatabaseObject GetRecordById(int id)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(DbHelper.CnnVal("cnMWS")))
            {
                return connection.QuerySingle<Kontakt>("dbo.Kontakt_GetRecordById @id", new { id });
            }
        }

        public DatabaseObject Insert(DatabaseObject dbobject)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(DbHelper.CnnVal("cnMWS")))
            {
                return connection.QuerySingle<Kontakt>("dbo.Kontakt_Insert @imie, @nazwisko, @telefon, @email, @miejscowosc, @nrdomu, @miasto, @poczta, @ulica, @idpracownika, @iduczestnika", dbobject);
            }
        }

        public void Update(DatabaseObject dbobject_old, DatabaseObject dbobject_new)
        {
            dbobject_new.id = dbobject_old.id;
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(DbHelper.CnnVal("cnMWS")))
            {
                connection.Execute("dbo.Kontakt_Update @id, @imie, @nazwisko, @telefon, @email, @miejscowosc, @nrdomu, @miasto, @poczta, @ulica, @idpracownika, @iduczestnika", dbobject_new);
            }
        }
    }
}
