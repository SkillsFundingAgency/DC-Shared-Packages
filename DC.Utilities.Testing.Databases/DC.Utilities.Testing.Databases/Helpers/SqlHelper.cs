using DC.Utilities.SQLDb.Config;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace DC.Utilities.SQLDb.Helpers
{
    public static class SqlHelper
    {

        

        public static void ExecuteSql(string connectionString, string sql)
        {
            using (var conn = new SqlConnection(connectionString))
            {
                using (var cmd = new SqlCommand(sql, conn))
                {
                    cmd.CommandTimeout = 600;
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static void ExecuteSql(string sql)
        {
            ExecuteSql(CommonConfig.MasterConnectionString, sql);
        }

        public static object ExecuteQuery(string connectionString, string sql, int? commandTimeout)
        {
            object rawResult = null;

            using (var conn = new SqlConnection(connectionString))
            {
                using (var command = new SqlCommand(sql, conn))
                {
                    conn.Open();
                    //Build up the command to execute
                    command.CommandText = sql;
                    command.CommandType = CommandType.Text;
                    command.CommandTimeout = commandTimeout.HasValue
                        ? commandTimeout.GetValueOrDefault()
                        : TimeSpan.FromMinutes(10).Milliseconds;

                    command.CommandType = CommandType.Text;
                    //Execute
                    rawResult = command.ExecuteScalar();
                }
            }
            return rawResult;
        }

       public static object[][] ExecuteQuery(string connectionString, string sql)
        {
            DataTable results = new DataTable();

            using (var conn = new SqlConnection(connectionString))
            {
                using (var command = new SqlCommand(sql, conn))
                {
                    conn.Open();

                    command.CommandType = CommandType.Text;
                    command.CommandText = sql;
                    command.CommandTimeout = TimeSpan.FromMinutes(10).Milliseconds;

                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    adapter.Fill(results);
                }
            }

            object[][] rows = new object[results.Rows.Count][];

            for (int i = 0; i < results.Rows.Count; i++)
            {
                rows[i] = results.Rows[i].ItemArray;
            }

            return rows;
        }

       public static void RestoreDatabase(string databaseName, string bakFile)
        {
            string datapath = null;
            string logPath = null;
            GetMdfAndLogPath(out datapath, out logPath);
           RestoreDatabase(databaseName, bakFile, datapath, logPath);
        }

       private static void GetMdfAndLogPath(out string mdfPath, out string logPath)
       {
           object tempMdfPath = ExecuteQuery(CommonConfig.MasterConnectionString,
               "SELECT  SERVERPROPERTY('InstanceDefaultDataPath')",null);
           mdfPath = tempMdfPath == DBNull.Value ? null : tempMdfPath.ToString();

           object tempLogPath = ExecuteQuery(CommonConfig.MasterConnectionString,
               "SELECT  SERVERPROPERTY('InstanceDefaultLogPath')",null);
           logPath = tempLogPath == DBNull.Value ? null : tempLogPath.ToString();

       }

        /// <summary>
        /// Restores the database.
        /// </summary>
        /// <param name="databaseName">string database name</param>
        /// <param name="backupFilePath">The backup file path.</param>
        /// <param name="mdfPath"></param>
        /// <param name="logPath"></param>
        private static void RestoreDatabase(string databaseName, string backupFilePath, string mdfPath, string logPath)
       {
           bool applyMoveLogic = (mdfPath != null) && (logPath != null);

           var sqlFileListInfoBuilder = new StringBuilder().Append(String.Empty);


           if (applyMoveLogic)
           {
               sqlFileListInfoBuilder.AppendLine(FileListOnlyInfoSql(backupFilePath).ToString());
           }

           var sqlRestoreBuilder = new StringBuilder().AppendLine(String.Empty);
           sqlRestoreBuilder.AppendLine(string.Format(@"RESTORE DATABASE [{0}] from DISK = '{1}'", databaseName, backupFilePath));
           if (applyMoveLogic)
           {
               sqlRestoreBuilder.AppendLine(" With Replace,");
               sqlRestoreBuilder.AppendLine(String.Format(" Move @mdfFileName to N'{0}',", mdfPath + databaseName + ".mdf"));
               sqlRestoreBuilder.AppendLine(String.Format(" Move @ldfFileName to N'{0}'", logPath + databaseName + "_log.ldf"));
           }

           ExecuteSql(CommonConfig.MasterConnectionString, sqlFileListInfoBuilder.ToString() + sqlRestoreBuilder.ToString());
       }

        /// <summary>
        /// create a back up query
        /// </summary>
        /// <param name="backupFilePath"> bakup file path</param>
        /// <returns></returns>
       private static StringBuilder FileListOnlyInfoSql(string backupFilePath)
       {
           var sqlFileListInfoBuilder = new StringBuilder();
           sqlFileListInfoBuilder.AppendLine("DECLARE @mdfFileName NVARCHAR(200)");
           sqlFileListInfoBuilder.AppendLine("DECLARE @ldfFileName NVARCHAR(200)");
           sqlFileListInfoBuilder.AppendLine("DECLARE @fileListTable TABLE");
           sqlFileListInfoBuilder.AppendLine("(");
           sqlFileListInfoBuilder.AppendLine("		LogicalName          NVARCHAR(128),");
           sqlFileListInfoBuilder.AppendLine("		PhysicalName         NVARCHAR(260),");
           sqlFileListInfoBuilder.AppendLine("		[Type]               CHAR(1),");
           sqlFileListInfoBuilder.AppendLine("		FileGroupName        NVARCHAR(128),");
           sqlFileListInfoBuilder.AppendLine("		Size                 NUMERIC(20,0),");
           sqlFileListInfoBuilder.AppendLine("		MaxSize              NUMERIC(20,0),");
           sqlFileListInfoBuilder.AppendLine("		FileID               BIGINT,");
           sqlFileListInfoBuilder.AppendLine("		CreateLSN            NUMERIC(25,0),");
           sqlFileListInfoBuilder.AppendLine("		DropLSN              NUMERIC(25,0),");
           sqlFileListInfoBuilder.AppendLine("		UniqueID             UNIQUEIDENTIFIER,");
           sqlFileListInfoBuilder.AppendLine("		ReadOnlyLSN          NUMERIC(25,0),");
           sqlFileListInfoBuilder.AppendLine("		ReadWriteLSN         NUMERIC(25,0),");
           sqlFileListInfoBuilder.AppendLine("		BackupSizeInBytes    BIGINT,");
           sqlFileListInfoBuilder.AppendLine("		SourceBlockSize      INT,");
           sqlFileListInfoBuilder.AppendLine("		FileGroupID          INT,");
           sqlFileListInfoBuilder.AppendLine("		LogGroupGUID         UNIQUEIDENTIFIER,");
           sqlFileListInfoBuilder.AppendLine("		DifferentialBaseLSN  NUMERIC(25,0),");
           sqlFileListInfoBuilder.AppendLine("		DifferentialBaseGUID UNIQUEIDENTIFIER,");
           sqlFileListInfoBuilder.AppendLine("		IsReadOnl            BIT,");
           sqlFileListInfoBuilder.AppendLine("		IsPresent            BIT,");
           sqlFileListInfoBuilder.AppendLine("		TDEThumbprint        VARBINARY(32) ");
           sqlFileListInfoBuilder.AppendLine(")");
           sqlFileListInfoBuilder.AppendLine("insert into @fileListTable");
           sqlFileListInfoBuilder.AppendLine(String.Format("exec ('RESTORE FILELISTONLY FROM DISK = ''{0}''')", backupFilePath));
           sqlFileListInfoBuilder.AppendLine("SET @mdfFileName = (SELECT LogicalName FROM @fileListTable where Type='D')");
           sqlFileListInfoBuilder.AppendLine("SET @ldfFileName = (SELECT LogicalName FROM @fileListTable where Type='L')");
           return sqlFileListInfoBuilder;
       }

       public static void DeleteSnapshots(string databasename)
        {
            var snapshots = ExecuteGetAllSnapshotsForDatabase(databasename);

            foreach (var snapshot in snapshots)
            {
                DropDatabase(snapshot, true);
            }
        }

       public static void DropDatabase(string databaseName, bool isSnapshot = false)
        {
            if (!isSnapshot)
            {
                var terminateExistingConnectionsSql = string.Format("alter database [{0}] set single_user with rollback immediate", databaseName);
                ExecuteSql(CommonConfig.MasterConnectionString, terminateExistingConnectionsSql);
            }

            var dropDbSql = string.Format("DROP DATABASE [{0}]", databaseName);
            ExecuteSql(CommonConfig.MasterConnectionString, dropDbSql);
        }

       
        /// <summary>
        /// The execute sql.
        /// </summary>
        /// <returns>
        /// List of snapshots for the specified database.
        /// </returns>
        public static IList<string> ExecuteGetAllSnapshotsForDatabase(string databaseName)
        {
            var sql = string.Format("SELECT * from sys.databases WHERE name LIKE '{0}_%' AND source_database_id IS NOT NULL", databaseName);
            var snapshots = new List<string>();
            var reader = GetDataReader(CommonConfig.DedsConnectionString, sql, null);
            while (reader.Read())
            {
                var db = reader.GetValue(0).ToString();
                snapshots.Add(db);
            }
            return snapshots;
        }

        public static SqlDataReader GetDataReader(string connectionString, string sql, ILogger _logger)
        {
            if(CommonConfig.Verbose)
            _logger.Message("executing the query " + sql);

            using(var conn = new SqlConnection(CommonConfig.MasterConnectionString))
            {
                using (var cmd = new SqlCommand(sql, conn))
                {
                    if (conn.State != ConnectionState.Open)
                    {
                        conn.Open();
                    }

                   
                    using (var reader = cmd.ExecuteReader())
                    {
                        return reader;
                    }

                }
            }
        }

        public static int ExecuteNonReader(string connectionString, string sql, ILogger _logger)
        {
            if (CommonConfig.Verbose)
                _logger.Message("executing the query " + sql);

            using (var conn = new SqlConnection(CommonConfig.MasterConnectionString))
            {
                using (var cmd = conn.CreateCommand())
                {
                    if (conn.State != ConnectionState.Open)
                    {
                        conn.Open();
                    }
                    cmd.CommandText = sql;
                    return cmd.ExecuteNonQuery();

                }
            }
        }

    }
}
