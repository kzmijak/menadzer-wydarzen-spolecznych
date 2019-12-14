using MWS.dbo;
using System;
using System.Collections.Generic;
using System.Text;

namespace MWS.Procedures
{
    interface _JoiningTableProcedures
    {
        void Insert(_DatabaseObject object1, _DatabaseObject object2);
        void Delete(_DatabaseObject object1, _DatabaseObject object2);
        IEnumerable<_JoiningTable> GetCollection();
    }
}
