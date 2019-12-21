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
                try
                {
                    return connection.Query<Wniosek>("dbo.Wniosek_GetCollection").Cast<_DatabaseObject>().ToList();
                }
                catch (InvalidOperationException)
                {
                    return null;
                }
            }
        }

        public _DatabaseObject GetRecord(_DatabaseObject dbobject)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(DbHelper.CnnVal("cnMWS")))
            {
                try
                {
                    return connection.QuerySingle<Wniosek>("dbo.Wniosek_GetRecord @idadresata, @idodbiorcy, @kwota, @zatwierdzone", dbobject);
                }
                catch (InvalidOperationException)
                {
                    return null;
                }
            }
        }

        public _DatabaseObject GetRecordById(int id)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(DbHelper.CnnVal("cnMWS")))
            {
                try
                {
                    return connection.QuerySingle<Wniosek>("dbo.Wniosek_GetRecordById @id", new { id });
                }
                catch (InvalidOperationException)
                {
                    return null;
                }
            }
        }

        public _DatabaseObject Insert(_DatabaseObject dbobject)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(DbHelper.CnnVal("cnMWS")))
            {
                return connection.QuerySingle<Wniosek>("dbo.Wniosek_Insert @idadresata, @idodbiorcy, @kwota, @zatwierdzone", dbobject);
            }
        }

        public void Update(_DatabaseObject dbobject_old, _DatabaseObject dbobject_new)
        {
            dbobject_new.id = dbobject_old.id;
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(DbHelper.CnnVal("cnMWS")))
            {
                connection.Execute("dbo.Wniosek_Update @id, @idadresata, @idodbiorcy, @kwota, @zatwierdzone", dbobject_new);
            }
        }

        public void Send(Logowanie sender, Logowanie receiver, decimal amount, string message)
        {
            Wniosek wniosek = new Wniosek
            {
                idadresata = sender.id,
                idodbiorcy = receiver.id,
                kwota = amount,
                zatwierdzone = false
            };
            if (sender.id != receiver.id)
                DataAccess.Wniosek.Insert(wniosek);
        }
    }
}
