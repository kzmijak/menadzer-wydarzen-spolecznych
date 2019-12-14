﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Dapper;
using MWS.dbo;

namespace MWS.Procedures
{
    class JPSetWydarzenie_Sponsor : _JoiningTableProcedures
    {
        public void Delete(_DatabaseObject object1, _DatabaseObject object2)
        {
            Wydarzenie_Sponsor joiningObject = new Wydarzenie_Sponsor()
            {
                idwydarzenia = object1.id,
                idsponsora = object2.id
            };
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(DbHelper.CnnVal("cnMWS")))
            {
                connection.Execute("dbo.Wydarzenie_Sponsor_Delete @idwydarzenia, @idsponsora", joiningObject);
            }
        }

        public void Insert(_DatabaseObject object1, _DatabaseObject object2)
        {
            Wydarzenie_Sponsor joiningObject = new Wydarzenie_Sponsor()
            {
                idwydarzenia = object1.id,
                idsponsora = object2.id
            };
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(DbHelper.CnnVal("cnMWS")))
            {
                connection.Execute("dbo.Wydarzenie_Sponsor_Insert @idwydarzenia, @idsponsora", joiningObject);
            }
        }
    }
}
