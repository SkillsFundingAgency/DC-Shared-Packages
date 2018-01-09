using System;
using DC.Utilities.SQLDb.Helpers;
using DC.Utilities.SQLDb.Config;
using System.Collections.Generic;

namespace ILR_Support_Tool.DEDS
{
    public class TeardownDedsDatabase:BaseDatabaseHelper
    {

        public TeardownDedsDatabase(ILogger logger):base(logger)
        {
           
        }
        public void Run(string databaseName)
        {
            try
            {
                _logger.Message("Tearing down " + databaseName);
                //remove dataset records
                removeDatasetRecords(databaseName);
                //delete snapshots
                SqlHelper.DeleteSnapshots(databaseName);
                //delete databases
                SqlHelper.DropDatabase(databaseName);

                _logger.Message(databaseName + " Teardown Complete" );
            }
            catch (Exception ex)
            {
                _logger.Message(databaseName + " Teardown Failed");
                if (CommonConfig.Verbose)
                    _logger.Message(ex.StackTrace);
            }
        }

        private void removeDatasetRecords(string databaseName)
        {

            string[] queries = { "delete from DataSetVersionRoles where  DataSetVersion_id in (select id from  DataSetVersion where dataset_id = '{0}');",
            "delete from QueryRoles where query_id in (select id from query where  DataSetVersion_id in (select id from  DataSetVersion where dataset_id = '{0}'));",
            "delete from QueryFilterPickListValues where QueryFilter_id in "+
            "(select id from QueryFilter where query_id in (select id from query where  DataSetVersion_id in (select id from  DataSetVersion where dataset_id = '{0}')));",
             "delete from QueryFilter where query_id in (select id from query where  DataSetVersion_id in (select id from  DataSetVersion where dataset_id = '{0}'));",
             "delete from query where  DataSetVersion_id in (select id from  DataSetVersion where dataset_id = '{0}');",
             "delete from DataSetVersion where dataset_id = '{0}';",
             "delete from SchemaScript where DataSetSchema_id in (select id from DatasetSchema where dataset_id = '{0}');",
             "delete from DataSetSchema  where DataSet_id = '{0}';",
             "delete from dataset where id = '{0}';" };

            

            string getDatasetIds = string.Format("Select id from dataset where code = '{0}'", databaseName);
            
            List<string> dbs = new List<string>();
            var reader = SqlHelper.GetDataReader(CommonConfig.DedsConnectionString, getDatasetIds, _logger);
            while (reader.Read())
            {
                var db = reader.GetValue(0).ToString();
                dbs.Add(db);
              
            }
            //remove all the records in the dataset tables
            dbs.ForEach(db =>
            {
                string singleQuery = string.Format(string.Concat(queries), db);
                SqlHelper.ExecuteSql(CommonConfig.DedsConnectionString, singleQuery);
            });

        }

    }
}
