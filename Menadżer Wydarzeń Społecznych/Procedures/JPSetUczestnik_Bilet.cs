﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Dapper;
using MWS.dbo;

namespace MWS.Procedures
{
    class JPSetUczestnik_Bilet : _JoiningTableProcedures
    {
        public void Delete(_DatabaseObject object1, _DatabaseObject object2)
        {
            Uczestnik_Bilet joiningObject = new Uczestnik_Bilet()
            {
                iduczestnika = object1.id,
                idbiletu = object2.id
            };
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(DbHelper.CnnVal("cnMWS")))
            {
                connection.Execute("dbo.Uczestnik_Bilet_Delete @iduczestnika, @idbiletu", joiningObject);
            }
        }

        public void Insert(_DatabaseObject object1, _DatabaseObject object2)
        {
            Uczestnik_Bilet joiningObject = new Uczestnik_Bilet()
            {
                iduczestnika = object1.id,
                idbiletu = object2.id
            };
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(DbHelper.CnnVal("cnMWS")))
            {
                connection.Execute("dbo.Uczestnik_Bilet_Insert @iduczestnika, @idbiletu", joiningObject);
            }
        }
        public IEnumerable<_JoiningTable> GetCollection()
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(DbHelper.CnnVal("cnMWS")))
            {
                return connection.Query<Uczestnik_Bilet>("dbo.Uczestnik_Bilet_GetCollection");
            }
        }
    }
}
