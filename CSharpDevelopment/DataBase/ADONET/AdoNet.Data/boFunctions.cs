using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace AdoNet.Data
{
    public static class boFunctions
    {
        //public static void ExecuteSqlFunction(string StoredPRocedure, Dictionary<string, string> parametters)
        //{
        //    using (SqlConnection oConn = new SqlConnection(dbSetting.Default.AdoNetConnectionString))
        //    {
        //        using (SqlCommand oRS = oConn.CreateCommand())
        //        {
        //            oConn.Open();
        //            oRS.CommandType = CommandType.StoredProcedure;
        //            oRS.CommandText = StoredPRocedure;
        //            if (parametters != null && parametters.Count > 0)
        //            {
        //                foreach (var parametter in parametters)
        //                {
        //                    oRS.Parameters.AddWithValue(parametter.Key, parametter.Value);
        //                }
        //            }
        //            oRS.ExecuteNonQuery();
        //        }
        //    }
        //}

        //public static void ExecuteSqlFunctionReturnValue(string StoredPRocedure, Dictionary<string, string> parametters, Action<DataSet> action)
        //{
        //    using (SqlConnection oConn = new SqlConnection(dbSetting.Default.AdoNetConnectionString))
        //    {
        //        using (SqlCommand oRS = oConn.CreateCommand())
        //        {
        //            oConn.Open();
        //            oRS.CommandType = CommandType.StoredProcedure;
        //            oRS.CommandText = StoredPRocedure;
        //            if (parametters != null && parametters.Count > 0)
        //            {
        //                foreach (var parametter in parametters)
        //                {
        //                    oRS.Parameters.AddWithValue(parametter.Key, parametter.Value);
        //                }
        //            }

        //            var ds = new DataSet();
        //            var adapter = new SqlDataAdapter(oRS);
        //            adapter.Fill(ds);

        //            if (action != null)
        //            {
        //                action(ds);
        //            }
        //        }
        //    }
        //}

        
    }
}
