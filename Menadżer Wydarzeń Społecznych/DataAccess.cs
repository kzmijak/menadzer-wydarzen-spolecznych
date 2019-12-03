using System;
using System.Data;
using System.Collections.Generic;
using System.Text;
using Dapper;
using MWS.dbo;
using MWS.Procedures;

namespace MWS
{
    static class DataAccess
    {
        public static PSetWydarzenie Wydarzenie { get; set; } = new PSetWydarzenie();
    }
}
