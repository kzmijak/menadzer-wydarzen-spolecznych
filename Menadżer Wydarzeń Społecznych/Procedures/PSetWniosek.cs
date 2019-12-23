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
                    return connection.QuerySingle<Wniosek>("dbo.Wniosek_GetRecord @idwiadomosci, @kwota, @akcja, @zatwierdzone", dbobject);
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
                return connection.QuerySingle<Wniosek>("dbo.Wniosek_Insert @idwniadomosci, @kwota, @akcja, @zatwierdzone", dbobject);
            }
        }

        public void Update(_DatabaseObject dbobject_old, _DatabaseObject dbobject_new)
        {
            dbobject_new.id = dbobject_old.id;
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(DbHelper.CnnVal("cnMWS")))
            {
                connection.Execute("dbo.Wniosek_Update @id, @idwiadomosci, @kwota, @akcja, @zatwierdzone", dbobject_new);
            }
        }

        public void Send(Wiadomosc source, decimal amount, string action)
        {
            Wniosek wniosek = new Wniosek
            {
                idwiadomosci = source.id,
                kwota = amount,
                akcja = action,
                zatwierdzone = false
            };
            DataAccess.Wniosek.Insert(wniosek);
        }
    }
}
