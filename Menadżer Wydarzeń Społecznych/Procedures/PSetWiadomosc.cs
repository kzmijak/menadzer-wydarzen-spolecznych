using Dapper;
using MWS.dbo;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace MWS.Procedures
{
    class PSetWiadomosc: _DatabaseObjectProcedures
    {
        public void Delete(_DatabaseObject dbobject)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(DbHelper.CnnVal("cnMWS")))
            {
                connection.Execute("dbo.Wiadomosc_Delete @id", dbobject);
            }
        }

        public List<_DatabaseObject> GetCollection()
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(DbHelper.CnnVal("cnMWS")))
            {
                return connection.Query<Wiadomosc>("dbo.Wiadomosc_GetCollection").Cast<_DatabaseObject>().ToList();
            }
        }

        public _DatabaseObject GetRecord(_DatabaseObject dbobject)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(DbHelper.CnnVal("cnMWS")))
            {
                return connection.QuerySingle<Wiadomosc>("dbo.Wiadomosc_GetRecord @idpracownika, @idpracownika2, @idsponsora, @idsponsora2, @iduczestnika, @uczestnika2, @dzien, @godzina, @tresc", dbobject);
            }
        }

        public _DatabaseObject GetRecordById(int id)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(DbHelper.CnnVal("cnMWS")))
            {
                return connection.QuerySingle<Wiadomosc>("dbo.Wiadomosc_GetRecordById @id", new { id });
            }
        }

        public _DatabaseObject Insert(_DatabaseObject dbobject)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(DbHelper.CnnVal("cnMWS")))
            {
                return connection.QuerySingle<Wiadomosc>("dbo.Wiadomosc_Insert @idpracownika, @idpracownika2, @idsponsora, @idsponsora2, @iduczestnika, @uczestnika2, @dzien, @godzina, @tresc", dbobject);
            }
        }

        public void Update(_DatabaseObject dbobject_old, _DatabaseObject dbobject_new)
        {
            dbobject_new.id = dbobject_old.id;
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(DbHelper.CnnVal("cnMWS")))
            {
                connection.Execute("dbo.Wiadomosc_Update @id, @idpracownika, @idpracownika2, @idsponsora, @idsponsora2, @iduczestnika, @uczestnika2, @dzien, @godzina, @tresc", dbobject_new);
            }
        }
    }
}
