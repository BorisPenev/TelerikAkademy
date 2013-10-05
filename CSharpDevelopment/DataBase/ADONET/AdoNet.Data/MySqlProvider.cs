using System;
using System.Collections.Generic;
using System.Data;
using MySql.Data.MySqlClient;

namespace AdoNet.Data
{
    public class MySqlProvider
    {
        public static void ExecuteSqlQueryInsert(string query, Dictionary<string, object> parametters, Action<int> action)
        {
            RunSqlQueryInsert(query, parametters, action);
        }

        private static void RunSqlQueryInsert(string query, Dictionary<string, object> parametters, Action<int> action)
        {
            string keys = "";
            string values = "";

            if (parametters != null && parametters.Count > 0)
            {
                foreach (var parametter in parametters)
                {
                    if (keys == string.Empty)
                    {
                        keys += parametter.Key;
                        values += "@" + parametter.Key;
                    }
                    else
                    {
                        keys += ", " + parametter.Key;
                        values += ", @" + parametter.Key;
                    }
                }
            }

            query += "(" + keys + ") VALUES (" + values + "); SELECT @@IDENTITY";

            RunSqlQueryWithReturnId(query, parametters, CommandType.Text, action);
        }

        private static void RunSqlQueryWithReturnId(string queryText, Dictionary<string, object> parametters, CommandType commandType, Action<int> action)
        {
            using (MySqlConnection oConn = new MySqlConnection(dbSetting.Default.MySqlConnectionString))
            {
                using (MySqlCommand oRS = oConn.CreateCommand())
                {
                    oConn.Open();
                    oRS.CommandType = commandType;
                    oRS.CommandText = queryText;
                    if (parametters != null && parametters.Count > 0)
                    {
                        foreach (var parametter in parametters)
                        {
                            oRS.Parameters.AddWithValue(parametter.Key, parametter.Value);
                        }
                    }

                    // output parameters
                    MySqlParameter param = new MySqlParameter("@ID", MySqlDbType.Int16);
                    param.Direction = ParameterDirection.Output;
                    oRS.Parameters.Add(param);

                    oRS.ExecuteNonQuery();

                    if (action != null)
                    {
                        action(int.Parse(oRS.LastInsertedId.ToString()));
                    }
                }
            }
        }

        public static void ExecuteSqlQueryUpdate(string query, string where, Dictionary<string, object> parametters, Action<MySqlDataReader> action)
        {
            RunSqlQueryUpdate(query, where, parametters, action);
        }

        private static void RunSqlQueryUpdate(string query, string where, Dictionary<string, object> parametters, Action<MySqlDataReader> action)
        {
            string keys = "";
            if (parametters != null && parametters.Count > 0)
            {
                foreach (var parametter in parametters)
                {
                    if (keys == string.Empty)
                    {
                        keys += parametter.Key + " = @" + parametter.Key;
                    }
                    else
                    {
                        keys += ", " + parametter.Key + " = @" + parametter.Key;
                    }
                }
            }

            query += " SET " + keys + " " + where;

            RunSqlQueryReturnValue(query, parametters, CommandType.Text, action);
        }

        public static void ExecuteSqlQueryReturnValue(string query, Dictionary<string, object> parametters, Action<MySqlDataReader> action)
        {
            RunSqlQueryReturnValue(query, parametters, CommandType.Text, action);
        }

        private static void RunSqlQueryReturnValue(string queryText, Dictionary<string, object> parametters, CommandType commandType, Action<MySqlDataReader> action)
        {
            using (MySqlConnection oConn = new MySqlConnection(dbSetting.Default.MySqlConnectionString))
            {
                using (MySqlCommand oRS = oConn.CreateCommand())
                {
                    oConn.Open();
                    oRS.CommandType = commandType;
                    oRS.CommandText = queryText;
                    if (parametters != null && parametters.Count > 0)
                    {
                        foreach (var parametter in parametters)
                        {
                            oRS.Parameters.AddWithValue(parametter.Key, parametter.Value);
                        }
                    }

                    MySqlDataReader rdr = oRS.ExecuteReader(CommandBehavior.CloseConnection);
                    if (action != null)
                    {
                        action(rdr);
                    }
                }
            }
        }
    }
}
