using MWS.dbo;
using System;
using System.Collections.Generic;
using System.Text;

namespace MWS.Procedures
{
    interface IJoiningTableProcedures
    {
        void Insert(DatabaseObject object1, DatabaseObject object2);
        void Delete(DatabaseObject object1, DatabaseObject object2);
    }
}
