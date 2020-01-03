using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Dapper;
using MWS.dbo;

namespace MWS.Procedures
{
    class PSetKartaPlatnicza : _DatabaseObjectProcedures
    {
        public void Delete(_DatabaseObject dbobject)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(DbHelper.CnnVal("cnMWS")))
            {
                connection.Execute("dbo.KartaPlatnicza_Delete @id", dbobject);
            }
        }

        public List<_DatabaseObject> GetCollection()
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(DbHelper.CnnVal("cnMWS")))
            {
                try
                {
                    return connection.Query<KartaPlatnicza>("dbo.KartaPlatnicza_GetCollection").Cast<_DatabaseObject>().ToList();
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
                    return connection.QuerySingle<KartaPlatnicza>("dbo.KartaPlatnicza_GetRecord @wlasciciel, @numer, @wygasniecie, @kbezpiecz, @kontak", dbobject);
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
                    return connection.QuerySingle<KartaPlatnicza>("dbo.KartaPlatnicza_GetRecordById @id", new { id });
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
                return connection.QuerySingle<KartaPlatnicza>("dbo.KartaPlatnicza_Insert @wlasciciel, @numer, @wygasniecie, @kbezpiecz, @kontakt", dbobject);
            }
        }

        public void Update(_DatabaseObject dbobject_old, _DatabaseObject dbobject_new)
        {
            dbobject_new.id = dbobject_old.id;
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(DbHelper.CnnVal("cnMWS")))
            {
                connection.Execute("dbo.KartaPlatnicza_Update @id, @wlasciciel, @numer, @wygasniecie, @kbezpiecz, @kontakt", dbobject_new);
            }
        }
    }
}
