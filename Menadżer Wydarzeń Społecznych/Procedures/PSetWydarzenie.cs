using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Dapper;
using MWS.dbo;

namespace MWS.Procedures
{
    class PSetWydarzenie : DatabaseObjectProcedures
    {
        public void Delete(DatabaseObject dbobject)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(DbHelper.CnnVal("cnMWS")))
            {
                connection.Execute("dbo.Wydarzenie_Delete @id", dbobject);
            }
        }

        public List<DatabaseObject> GetCollection()
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(DbHelper.CnnVal("cnMWS")))
            {
                return connection.Query<Wydarzenie>("dbo.Wydarzenie_GetCollection").Cast<DatabaseObject>().ToList();
            }
        }

        public DatabaseObject GetRecord(DatabaseObject dbobject)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(DbHelper.CnnVal("cnMWS")))
            {
                return connection.QuerySingle<Wydarzenie>("dbo.Wydarzenie_GetRecord @nazwa, @opis, @miejsce, @dzien, @godzina, @budzet", dbobject);
            }
        }

        public DatabaseObject GetRecordById(int id)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(DbHelper.CnnVal("cnMWS")))
            {
                return connection.QuerySingle<Wydarzenie>("dbo.Wydarzenie_GetRecordById @id", new { id });
            }
        }

        public DatabaseObject Insert(DatabaseObject dbobject)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(DbHelper.CnnVal("cnMWS")))
            {
                return connection.QuerySingle<Wydarzenie>("dbo.Wydarzenie_Insert @nazwa, @opis, @miejsce, @dzien, @godzina, @budzet", dbobject);
            }
        }

        public void Update(DatabaseObject dbobject_old, DatabaseObject dbobject_new)
        {
            dbobject_new.id = dbobject_old.id;
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(DbHelper.CnnVal("cnMWS")))
            {
                connection.Execute("dbo.Wydarzenie_Update @id, @nazwa, @opis, @miejsce, @dzien, @godzina, @budzet", dbobject_new);
            }
        }
    }
}
