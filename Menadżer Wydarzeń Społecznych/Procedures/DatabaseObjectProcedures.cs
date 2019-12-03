using MWS.dbo;
using System;
using System.Collections.Generic;
using System.Text;

namespace MWS.Procedures
{
    interface DatabaseObjectProcedures
    {
        void Insert(DatabaseObject dbobject);
        void Update(DatabaseObject dbobject_old, DatabaseObject dbobject_new);
        void Delete(DatabaseObject dbobject);
        DatabaseObject GetRecord(DatabaseObject dbobject);
        DatabaseObject GetRecordById(int id);
        List<DatabaseObject> GetCollection();
    }
}
