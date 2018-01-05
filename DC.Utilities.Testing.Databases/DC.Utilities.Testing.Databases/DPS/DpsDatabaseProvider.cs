using DC.Utilities.SQLDb.Config;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace ILR_Support_Tool.DPS
{
    public class DpsDatabaseProvider
    {
      

        public DpsDatabaseProvider(string dpsConnectionString)
        {
           
        }

        public IList<string> GetDpsDatabaseNames()
        {
            var sql = "SELECT [DatabaseName] FROM [InterJobDatabaseMetadata]";
            var databases = new List<string>();
            using (var conn = new SqlConnection(CommonConfig.DpsConnectionString))
            {
                using (var cmd = new SqlCommand(sql, conn))
                {
                    if (conn.State != ConnectionState.Open)
                    {
                        conn.Open();
                    }

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var db = reader.GetValue(0).ToString();
                            databases.Add(db);
                        }
                    }

                }
            }

            return databases;
        }
    }
}
