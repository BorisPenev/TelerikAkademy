using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;

namespace AdoNet.Data
{
    public class SqLiteProvider
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

            query += "(" + keys + ") VALUES (" + values + ")";

            RunSqlQueryWithReturnId(query, parametters, CommandType.Text, action);
        }

        private static void RunSqlQueryWithReturnId(string queryText, Dictionary<string, object> parametters, CommandType commandType, Action<int> action)
        {
            using (SQLiteConnection oConn = new SQLiteConnection(dbSetting.Default.SQLiteConnectionString))
            {
                using (SQLiteCommand oRS = oConn.CreateCommand())
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
                    //SQLiteParameter param = new SQLiteParameter("@ID", SqlDbType.Int);
                    //param.Direction = ParameterDirection.Output;
                    //oRS.Parameters.Add(param);

                    oRS.ExecuteNonQuery();

                    if (action != null)
                    {
                        action(0);
                        //action(int.Parse(oRS.ToString()));
                    }
                }
            }
        }

        public static void ExecuteSqlQueryUpdate(string query, string where, Dictionary<string, object> parametters, Action<SQLiteDataReader> action)
        {
            RunSqlQueryUpdate(query, where, parametters, action);
        }

        private static void RunSqlQueryUpdate(string query, string where, Dictionary<string, object> parametters, Action<SQLiteDataReader> action)
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

        public static void ExecuteSqlQueryReturnValue(string query, Dictionary<string, object> parametters, Action<SQLiteDataReader> action)
        {
            RunSqlQueryReturnValue(query, parametters, CommandType.Text, action);
        }

        private static void RunSqlQueryReturnValue(string queryText, Dictionary<string, object> parametters, CommandType commandType, Action<SQLiteDataReader> action)
        {
            using (SQLiteConnection oConn = new SQLiteConnection(dbSetting.Default.SQLiteConnectionString))
            {
                using (SQLiteCommand oRS = oConn.CreateCommand())
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

                    SQLiteDataReader rdr = oRS.ExecuteReader(CommandBehavior.CloseConnection);
                    if (action != null)
                    {
                        action(rdr);
                    }
                }
            }
        }
    }
}
