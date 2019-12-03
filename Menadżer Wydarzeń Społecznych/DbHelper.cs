using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;

namespace MWS
{
    public static class DbHelper
    {
        public static string CnnVal(string name)
        {
            return ConfigurationManager.ConnectionStrings[name].ConnectionString;
        }
    }
}
