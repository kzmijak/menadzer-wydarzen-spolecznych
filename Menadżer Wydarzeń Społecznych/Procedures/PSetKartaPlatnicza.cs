using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Dapper;
using MWS.dbo;

namespace MWS.Procedures
{
    class PSetKartaPlatnicza : DatabaseObjectProcedures
    {
        public void Delete(DatabaseObject dbobject)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(DbHelper.CnnVal("cnMWS")))
            {
                connection.Execute("dbo.KartaPlatnicza_Delete @id", dbobject);
            }
        }

        public List<DatabaseObject> GetCollection()
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(DbHelper.CnnVal("cnMWS")))
            {
                return connection.Query<KartaPlatnicza>("dbo.KartaPlatnicza_GetCollection").Cast<DatabaseObject>().ToList();
            }
        }

        public DatabaseObject GetRecord(DatabaseObject dbobject)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(DbHelper.CnnVal("cnMWS")))
            {
                return connection.QuerySingle<KartaPlatnicza>("dbo.KartaPlatnicza_GetRecord @wlasciciel, @numer, @wygasniecie, @kbezpiecz, @kontak", dbobject);
            }
        }

        public DatabaseObject GetRecordById(int id)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(DbHelper.CnnVal("cnMWS")))
            {
                return connection.QuerySingle<KartaPlatnicza>("dbo.KartaPlatnicza_GetRecordById @id", new { id });
            }
        }

        public DatabaseObject Insert(DatabaseObject dbobject)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(DbHelper.CnnVal("cnMWS")))
            {
                return connection.QuerySingle<KartaPlatnicza>("dbo.KartaPlatnicza_Insert @wlasciciel, @numer, @wygasniecie, @kbezpiecz, @kontakt", dbobject);
            }
        }

        public void Update(DatabaseObject dbobject_old, DatabaseObject dbobject_new)
        {
            dbobject_new.id = dbobject_old.id;
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(DbHelper.CnnVal("cnMWS")))
            {
                connection.Execute("dbo.KartaPlatnicza @id, @wlasciciel, @numer, @wygasniecie, @kbezpiecz, @kontakt", dbobject_new);
            }
        }
    }
}
