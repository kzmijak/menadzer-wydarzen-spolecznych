using Dapper;
using MWS.dbo;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace MWS.Procedures
{
    class PSetWniosek: _DatabaseObjectProcedures
    {
        public void Delete(_DatabaseObject dbobject)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(DbHelper.CnnVal("cnMWS")))
            {
                connection.Execute("dbo.Wniosek_Delete @id", dbobject);
            }
        }

        public List<_DatabaseObject> GetCollection()
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(DbHelper.CnnVal("cnMWS")))
            {
                return connection.Query<Wniosek>("dbo.Wniosek_GetCollection").Cast<_DatabaseObject>().ToList();
            }
        }

        public _DatabaseObject GetRecord(_DatabaseObject dbobject)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(DbHelper.CnnVal("cnMWS")))
            {
                return connection.QuerySingle<Wniosek>("dbo.Wniosek_GetRecord @idkarty, @idadresata, @kwota, @dzien, @godzina", dbobject);
            }
        }

        public _DatabaseObject GetRecordById(int id)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(DbHelper.CnnVal("cnMWS")))
            {
                return connection.QuerySingle<Wniosek>("dbo.Wniosek_GetRecordById @id", new { id });
            }
        }

        public _DatabaseObject Insert(_DatabaseObject dbobject)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(DbHelper.CnnVal("cnMWS")))
            {
                return connection.QuerySingle<Wniosek>("dbo.Wniosek_Insert @idkarty, @idadresata, @kwota, @dzien, @godzina", dbobject);
            }
        }

        public void Update(_DatabaseObject dbobject_old, _DatabaseObject dbobject_new)
        {
            dbobject_new.id = dbobject_old.id;
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(DbHelper.CnnVal("cnMWS")))
            {
                connection.Execute("dbo.Wniosek_Update @id, @idpracownika, @idwydarzenia, @kwota, @zatwierdzone", dbobject_new);
            }
        }
    }
}
