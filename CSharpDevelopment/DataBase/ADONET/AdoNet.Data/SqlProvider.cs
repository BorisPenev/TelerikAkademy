using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace AdoNet.Data
{
    public class SqlProvider
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

            query += "(" + keys + ") VALUES (" + values + ") SET @ID = @@Identity RETURN";

            RunSqlQueryWithReturnId(query, parametters, CommandType.Text, action);
        }

        private static void RunSqlQueryWithReturnId(string queryText, Dictionary<string, object> parametters, CommandType commandType, Action<int> action)
        {
            using (SqlConnection oConn = new SqlConnection(dbSetting.Default.SqlConnectionString))
            {
                using (SqlCommand oRS = oConn.CreateCommand())
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
                    SqlParameter param = new SqlParameter("@ID", SqlDbType.Int);
                    param.Direction = ParameterDirection.Output;
                    oRS.Parameters.Add(param);

                    oRS.ExecuteNonQuery();

                    if (action != null)
                    {
                        action(int.Parse(oRS.Parameters["@ID"].Value.ToString()));
                    }
                }
            }
        }

        public static void ExecuteSqlQueryUpdate(string query, Dictionary<string, object> parametters, Action<SqlDataReader> action)
        {
            RunSqlQueryUpdate(query, parametters, action);
        }

        private static void RunSqlQueryUpdate(string query, Dictionary<string, object> parametters, Action<SqlDataReader> action)
        {
            string keys = "";
            if (parametters != null && parametters.Count > 0)
            {
                foreach (var parametter in parametters)
                {
                    if (keys == string.Empty)
                    {
                        keys += "[" + parametter.Key + "] = @" + parametter.Key;
                    }
                    else
                    {
                        keys += ", [" + parametter.Key + "] = @" + parametter.Key;
                    }
                }
            }

            query += " SET " + keys;

            RunSqlQueryReturnValue(query, parametters, CommandType.Text, action);
        }

        public static void ExecuteSqlQueryReturnValue(string query, Dictionary<string, object> parametters, Action<SqlDataReader> action)
        {
            RunSqlQueryReturnValue(query, parametters, CommandType.Text, action);
        }

        private static void RunSqlQueryReturnValue(string queryText, Dictionary<string, object> parametters, CommandType commandType, Action<SqlDataReader> action)
        {
            using (SqlConnection oConn = new SqlConnection(dbSetting.Default.SqlConnectionString))
            {
                using (SqlCommand oRS = oConn.CreateCommand())
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

                    SqlDataReader rdr = oRS.ExecuteReader(CommandBehavior.CloseConnection);
                    if (action != null)
                    {
                        action(rdr);
                    }
                }
            }
        }
    }
}
