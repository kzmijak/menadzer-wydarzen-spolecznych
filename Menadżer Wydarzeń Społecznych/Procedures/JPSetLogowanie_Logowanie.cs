using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Dapper;
using MWS.dbo;

namespace MWS.Procedures
{
    class JPSetLogowanie_Logowanie : _JoiningTableProcedures
    {
        public void Delete(_DatabaseObject object1, _DatabaseObject object2)
        {
            Logowanie_Logowanie joiningObject = new Logowanie_Logowanie()
            {
                idlogowania1 = object1.id,
                idlogowania2 = object2.id
            };
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(DbHelper.CnnVal("cnMWS")))
            {
                connection.Execute("dbo.Logowanie_Logowanie_Delete @idlogowania1, @idlogowania2", joiningObject);
            }
        }

        public void Insert(_DatabaseObject object1, _DatabaseObject object2)
        {
            Logowanie_Logowanie joiningObject = new Logowanie_Logowanie()
            {
                idlogowania1 = object1.id,
                idlogowania2 = object2.id
            };
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(DbHelper.CnnVal("cnMWS")))
            {
                connection.Execute("dbo.Logowanie_Logowanie_Insert @idlogowania1, @idlogowania2", joiningObject);
            }
        }

        public IEnumerable<_JoiningTable> GetCollection()
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(DbHelper.CnnVal("cnMWS")))
            {
                return connection.Query<Logowanie_Logowanie>("dbo.Logowanie_Logowanie_GetCollection");
            }
        }
    }
}
