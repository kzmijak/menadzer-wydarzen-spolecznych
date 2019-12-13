using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Dapper;
using MWS.dbo;

namespace MWS.Procedures
{
    class PSetPlatnosc : DatabaseObjectProcedures
    {
        public void Delete(DatabaseObject dbobject)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(DbHelper.CnnVal("cnMWS")))
            {
                connection.Execute("dbo.Platnosc_Delete @id", dbobject);
            }
        }

        public List<DatabaseObject> GetCollection()
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(DbHelper.CnnVal("cnMWS")))
            {
                return connection.Query<Platnosc>("dbo.Platnosc_GetCollection").Cast<DatabaseObject>().ToList();
            }
        }

        public DatabaseObject GetRecord(DatabaseObject dbobject)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(DbHelper.CnnVal("cnMWS")))
            {
                return connection.QuerySingle<Platnosc>("dbo.Platnosc_GetRecord @idkarty, @idadresata, @kwota, @dzien, @godzina", dbobject);
            }
        }

        public DatabaseObject GetRecordById(int id)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(DbHelper.CnnVal("cnMWS")))
            {
                return connection.QuerySingle<Platnosc>("dbo.Platnosc_GetRecordById @id", new { id });
            }
        }

        public DatabaseObject Insert(DatabaseObject dbobject)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(DbHelper.CnnVal("cnMWS")))
            {
                return connection.QuerySingle<Platnosc>("dbo.Platnosc_Insert @idkarty, @idadresata, @kwota, @dzien, @godzina", dbobject);
            }
        }

        public void Update(DatabaseObject dbobject_old, DatabaseObject dbobject_new)
        {
            dbobject_new.id = dbobject_old.id;
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(DbHelper.CnnVal("cnMWS")))
            {
                connection.Execute("dbo.Platnosc_Update @id, @idkarty, @idadresata, @kwota, @dzien, @godzina", dbobject_new);
            }
        }
    }
}