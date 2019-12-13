using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Dapper;
using MWS.dbo;

namespace MWS.Procedures
{
    class JPSetPracownik_Pracownik : IJoiningTableProcedures
    {
        public void Delete(DatabaseObject object1, DatabaseObject object2)
        {
            Pracownik_Pracownik joiningObject = new Pracownik_Pracownik()
            {
                idorganizatora = object1.id,
                idpracownika = object2.id
            };
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(DbHelper.CnnVal("cnMWS")))
            {
                connection.Execute("dbo.Pracownik_Pracownik_Delete @idorganizatora, @idpracownika", joiningObject);
            }
        }

        public void Insert(DatabaseObject object1, DatabaseObject object2)
        {
            Pracownik_Pracownik joiningObject = new Pracownik_Pracownik()
            {
                idorganizatora = object1.id,
                idpracownika = object2.id
            };
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(DbHelper.CnnVal("cnMWS")))
            {
                connection.Execute("dbo.Pracownik_Pracownik_Insert @idorganizatora, @idpracownika", joiningObject);
            }
        }
    }
}
