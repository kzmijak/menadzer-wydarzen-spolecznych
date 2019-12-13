using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Dapper;
using MWS.dbo;

namespace MWS.Procedures
{
    class JPSetWydarzenie_Pracownik : IJoiningTableProcedures
    {
        public void Delete(DatabaseObject object1, DatabaseObject object2)
        {
            Wydarzenie_Pracownik joiningObject = new Wydarzenie_Pracownik()
            {
                idwydarzenia = object1.id,
                idpracownika = object2.id
            };
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(DbHelper.CnnVal("cnMWS")))
            {
                connection.Execute("dbo.Wydarzenie_Pracownik_Delete @idwydarzenia, @idpracownika", joiningObject);
            }
        }

        public void Insert(DatabaseObject object1, DatabaseObject object2)
        {
            Wydarzenie_Pracownik joiningObject = new Wydarzenie_Pracownik()
            {
                idwydarzenia = object1.id,
                idpracownika = object2.id
            };
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(DbHelper.CnnVal("cnMWS")))
            {
                connection.Execute("dbo.Wydarzenie_Pracownik_Insert @idwydarzenia, @idpracownika", joiningObject);
            }
        }
    }
}
