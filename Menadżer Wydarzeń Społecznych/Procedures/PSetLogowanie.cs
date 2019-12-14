using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Dapper;
using MWS.dbo;

namespace MWS.Procedures
{
    class PSetLogowanie : _DatabaseObjectProcedures
    {
        public void Delete(_DatabaseObject dbobject)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(DbHelper.CnnVal("cnMWS")))
            {
                connection.Execute("dbo.Logowanie_Delete @id", dbobject);
            }
        }

        public List<_DatabaseObject> GetCollection()
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(DbHelper.CnnVal("cnMWS")))
            {
                return connection.Query<Logowanie>("dbo.Logowanie_GetCollection").Cast<_DatabaseObject>().ToList();
            }
        }

        public _DatabaseObject GetRecord(_DatabaseObject dbobject)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(DbHelper.CnnVal("cnMWS")))
            {
                return connection.QuerySingle<Logowanie>("dbo.Logowanie_GetRecord @login, @haslo, @idpracownika, @idsponsora, @iduczestnika", dbobject);
            }
        }

        public _DatabaseObject GetRecordById(int id)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(DbHelper.CnnVal("cnMWS")))
            {
                return connection.QuerySingle<Logowanie>("dbo.Logowanie_GetRecordById @id", new { id });
            }
        }

        public _DatabaseObject Insert(_DatabaseObject dbobject)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(DbHelper.CnnVal("cnMWS")))
            {
                return connection.QuerySingle<Logowanie>("dbo.Logowanie_Insert @login, @haslo, @idpracownika, @idsponsora, @iduczestnika", dbobject);
            }
        }

        public void Update(_DatabaseObject dbobject_old, _DatabaseObject dbobject_new)
        {
            dbobject_new.id = dbobject_old.id;
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(DbHelper.CnnVal("cnMWS")))
            {
                connection.Execute("dbo.Logowanie_Update @id, @login, @haslo, @idpracownika, @idsponsora, @iduczestnika", dbobject_new);
            }
        }

        public _DatabaseObject CheckCredentials(_DatabaseObject databaseObject)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(DbHelper.CnnVal("cnMWS")))
            {
                try
                {
                    return connection.QuerySingle<Logowanie>("dbo.Logowanie_CheckCredentials @login, @haslo", databaseObject);
                }
                catch(InvalidOperationException)
                {
                    return null;
                }
            }
        }
    }
}
