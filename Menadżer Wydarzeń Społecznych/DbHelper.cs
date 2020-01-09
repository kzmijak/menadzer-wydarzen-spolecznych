using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using System.Reflection;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text.RegularExpressions;
using Dapper;

namespace MWS
{
    public static class DbHelper
    {

        private static string databaseScript = File.ReadAllText(@"MWS.sql");
        private static string proceduresScript = File.ReadAllText(@"Procedures.sql");
        private static string dropDatabaseScript = File.ReadAllText(@"MWS_Drop.sql");
        private static string dropProceduresScript = File.ReadAllText(@"Procedures_Drop.sql");



        public static string CnnVal(string name)
        {
            return ConfigurationManager.ConnectionStrings[name].ConnectionString;
        }

        public static bool IsAnyNullOrEmpty(object obj)
        {
            foreach (PropertyInfo pi in obj.GetType().GetProperties())
            {
                if (pi.PropertyType == typeof(string))
                {
                    string value = (string)pi.GetValue(obj);
                    if (string.IsNullOrEmpty(value))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public static bool CheckConnection()
        {
            IDbConnection connection = new System.Data.SqlClient.SqlConnection(DbHelper.CnnVal("cnMWS"));
            try
            {
                connection.Open();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static void InstallDatabase()
        {
            IEnumerable<string> commandStrings = Regex.Split(databaseScript, @"^\s*GO\s*$", RegexOptions.Multiline | RegexOptions.IgnoreCase);
            IDbConnection connection = new System.Data.SqlClient.SqlConnection(DbHelper.CnnVal("initializer"));
            connection.Open();
               
            foreach (string commandString in commandStrings)
            {
                if (commandString.Trim() != "")
                {
                    connection.Execute(commandString);
                }
            }

            connection.Close();
        }

        public static void InstallProcedures()
        {
            IEnumerable<string> commandStrings = Regex.Split(proceduresScript, @"^\s*GO\s*$", RegexOptions.Multiline | RegexOptions.IgnoreCase);
            IDbConnection connection = new System.Data.SqlClient.SqlConnection(DbHelper.CnnVal("cnMWS"));
            connection.Open();

            foreach (string commandString in commandStrings)
            {
                if (commandString.Trim() != "")
                {
                    connection.Execute(commandString);
                }
            }

            connection.Close();
        }

        public static void RemoveDatabase()
        {
            IEnumerable<string> commandStrings = Regex.Split(dropDatabaseScript, @"^\s*GO\s*$", RegexOptions.Multiline | RegexOptions.IgnoreCase);
            IDbConnection connection = new System.Data.SqlClient.SqlConnection(DbHelper.CnnVal("cnMWS"));
            connection.Open();

            foreach (string commandString in commandStrings)
            {
                if (commandString.Trim() != "")
                {
                    connection.Execute(commandString);
                }
            }

            connection.Close();
        }

        public static void RemoveProcedures()
        {
            IEnumerable<string> commandStrings = Regex.Split(dropProceduresScript, @"^\s*GO\s*$", RegexOptions.Multiline | RegexOptions.IgnoreCase);
            IDbConnection connection = new System.Data.SqlClient.SqlConnection(DbHelper.CnnVal("cnMWS"));
            connection.Open();

            foreach (string commandString in commandStrings)
            {
                if (commandString.Trim() != "")
                {
                    connection.Execute(commandString);
                }
            }

            connection.Close();
        }
    }
}
