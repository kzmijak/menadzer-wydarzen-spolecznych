﻿using Dapper;
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
                try
                {
                    return connection.Query<Wiadomosc>("dbo.Wiadomosc_GetCollection").Cast<_DatabaseObject>().ToList();
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
                    return connection.QuerySingle<Wiadomosc>("dbo.Wiadomosc_GetRecord @idadresata, @idodbiorcy, @dzien, @godzina, @tytul, @tresc", dbobject);
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
                    return connection.QuerySingle<Wiadomosc>("dbo.Wiadomosc_GetRecordById @id", new { id });
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
                return connection.QuerySingle<Wiadomosc>("dbo.Wiadomosc_Insert @idadresata, @idodbiorcy, @dzien, @godzina, @tytul, @tresc", dbobject);
            }
        }

        public void Update(_DatabaseObject dbobject_old, _DatabaseObject dbobject_new)
        {
            dbobject_new.id = dbobject_old.id;
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(DbHelper.CnnVal("cnMWS")))
            {
                connection.Execute("dbo.Wiadomosc_Update @id, @idadresata, @idodbiorcy, @dzien, @godzina, @tytul, @tresc", dbobject_new);
            }
        }

        public void Send(string title, string message, Logowanie sender, Logowanie receiver, Wniosek addition = null)
        {
            Wiadomosc wiadomosc = new Wiadomosc
            {
                idadresata = sender.id,
                idodbiorcy = receiver.id,
                dzien = DateTime.Now,
                godzina = DateTime.Now.TimeOfDay,
                tytul = title,
                tresc = message
            };
            if(sender.id != receiver.id)
            {
                wiadomosc = DataAccess.Wiadomosc.Insert(wiadomosc) as Wiadomosc;
                if(addition != null)
                {
                    addition.idwiadomosci = wiadomosc.id;
                    addition.zatwierdzone = false;
                    DataAccess.Wniosek.Insert(addition);
                }
            }
        }
    }
}
