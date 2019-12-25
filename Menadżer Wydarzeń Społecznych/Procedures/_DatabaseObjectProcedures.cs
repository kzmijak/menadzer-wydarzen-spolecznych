using MWS.dbo;
using System;
using System.Collections.Generic;
using System.Text;

namespace MWS.Procedures
{
    interface _DatabaseObjectProcedures: _Procedures
    {
        _DatabaseObject Insert(_DatabaseObject dbobject);
        void Update(_DatabaseObject dbobject_old, _DatabaseObject dbobject_new);
        void Delete(_DatabaseObject dbobject);
        _DatabaseObject GetRecord(_DatabaseObject dbobject);
        _DatabaseObject GetRecordById(int id);
        List<_DatabaseObject> GetCollection();
    }
}
