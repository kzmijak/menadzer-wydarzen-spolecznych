using MWS.dbo;
using System;
using System.Collections.Generic;
using System.Text;
using Dapper;
using System.Data;
using System.Linq;
using MWS.Pages;

namespace MWS.Procedures
{
    class PSetWydarzenie : DatabaseObjectProcedures
    {
        public void Insert(DatabaseObject dbobject)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(DbHelper.CnnVal("cnMWS")))
            {
                connection.Execute("dbo.Wydarzenie_Insert @nazwa, @opis, @miejsce, @dzien, @godzina, @budzet",
                                   dbobject);
            }
        }

        public void Update(DatabaseObject dbobject_old, DatabaseObject dbobject_new)
        {
            (dbobject_new as Wydarzenie).foreign_id = (dbobject_old as Wydarzenie).id;
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(DbHelper.CnnVal("cnMWS")))
            {
                connection.Execute("dbo.Wydarzenie_Update @nazwa, @opis, @miejsce, @dzien, @godzina, @budzet, @foreign_id", dbobject_new);
            }
        }

        public void Delete(DatabaseObject dbobject)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(DbHelper.CnnVal("cnMWS")))
            {
                connection.Execute("dbo.Wydarzenie_Delete @id", dbobject);
            }
        }

        public DatabaseObject GetRecord(DatabaseObject dbobject)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(DbHelper.CnnVal("cnMWS")))
            {
                foreach (Wydarzenie w in connection.Query<Wydarzenie>("dbo.Wydarzenie_Select"))
                {
                    if (w.id == (dbobject as Wydarzenie).id)
                        return w;
                }
            }
            return null;
        }

        public DatabaseObject GetRecordById(int id)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(DbHelper.CnnVal("cnMWS")))
            {
                var output = connection.Query<Wydarzenie>("dbo.Wydarzenie_GetRecord @id", new { id }).ToList()[0];
                return output;
            }
        }

        public List<DatabaseObject> GetCollection()
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(DbHelper.CnnVal("cnMWS")))
            {
                return connection.Query<Wydarzenie>("dbo.Wydarzenie_Select").Cast<DatabaseObject>().ToList();
            }
        }
    }
}
