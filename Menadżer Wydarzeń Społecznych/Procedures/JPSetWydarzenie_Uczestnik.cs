using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Dapper;
using MWS.dbo;

namespace MWS.Procedures
{
    class JPSetWydarzenie_Uczestnik : _JoiningTableProcedures
    {
        public void Delete(_DatabaseObject object1, _DatabaseObject object2)
        {
            Wydarzenie_Uczestnik joiningObject = new Wydarzenie_Uczestnik()
            {
                idwydarzenia = object1.id,
                iduczestnika = object2.id
            };
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(DbHelper.CnnVal("cnMWS")))
            {
                connection.Execute("dbo.Wydarzenie_Uczestnik_Delete @idwydarzenia, @iduczestnika", joiningObject);
            }
        }

        public void Insert(_DatabaseObject object1, _DatabaseObject object2)
        {
            Wydarzenie_Uczestnik joiningObject = new Wydarzenie_Uczestnik()
            {
                idwydarzenia = object1.id,
                iduczestnika = object2.id
            };
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(DbHelper.CnnVal("cnMWS")))
            {
                connection.Execute("dbo.Wydarzenie_Uczestnik_Insert @idwydarzenia, @iduczestnika", joiningObject);
            }
        }
    }
}
