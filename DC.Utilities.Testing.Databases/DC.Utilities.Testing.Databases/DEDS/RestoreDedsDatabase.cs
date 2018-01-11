using System;
using Amor.DCFT.DES.Domain.Entities.DataContracts;
using DC.Utilities.SQLDb.Helpers;
using DC.Utilities.SQLDb.Config;
using System.Collections.Generic;
using System.Linq;
using System.Data;

namespace ILR_Support_Tool.DEDS
{
    public class RestoreDedsDatabase
    {

        private readonly ILogger _logger;
       

        public RestoreDedsDatabase(ILogger logger)
        {
            _logger = logger;
        }
        public void Run(string databaseName, string bakFile, string testData = null)
        {
            try
            {
                _logger.Message("Restoring " + databaseName + ".");
                string connectionString = CommonConfig.DedsConnectionString.Replace(CommonConfig.DedsDatabaseName, databaseName);
                 

                //restore database
                SqlHelper.RestoreDatabase(databaseName, bakFile);

                //execute test data scripts
                if (!string.IsNullOrEmpty(testData))
                {
                    SqlHelper.ExecuteSql(connectionString, testData);
                }

                Guid dataSetId = Guid.NewGuid();

                //add data set entry to Deds database (dcftdes)
                string sql = string.Format("SET IDENTITY_INSERT DataSet ON; " +
                                           "INSERT INTO DataSet (Id, Code, Name, Description, CollectionId, DataProviderId, RetentionDuration, SequenceNumber) VALUES ('{0}','{1}','{2}','{3}','{4}','{5}',{6},{7}); " +
                                           "SET IDENTITY_INSERT DataSet OFF;",
                                           dataSetId.ToString(),
                                           databaseName, databaseName,
                                           databaseName + " data set",
                                           Guid.NewGuid().ToString(),
                                           Guid.NewGuid().ToString(),
                                           316527660000000,
                                           1134);


                SqlHelper.ExecuteSql(CommonConfig.DedsConnectionString, sql);

                //map DedsPublishUser to database
                SqlHelper.ExecuteSql(connectionString, @"ALTER AUTHORIZATION ON DATABASE::[" + databaseName + @"] TO [" + CommonConfig.DedsPublishUser+ "]");

                using (var client = new DedsPublishServiceClient())
                {
                    var publishDataSetVersionProperties = new PublishDataSetVersionProperties
                        {
                            DataSetId = dataSetId,
                            IsMandatory = false,
                            ReasonForVersion = "Updated version",
                            ReplacePreviousVersions = true,
                            SubmissionId = Guid.NewGuid(),
                            Roles = new[] {"Role1"},
                            Queries = new QueryProperties[] {}
                        };

                    client.PublishDataSetVersion(publishDataSetVersionProperties);
                }

                _logger.Message(databaseName + " restored.");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);
            }
        }

        public bool DatasetExists(string datasetName, ILogger _logger)
        {

            string getDatasetIds = string.Format("Select id from dataset where code = '{0}'", datasetName);

            List<string> dbs = new List<string>();
            var reader = SqlHelper.GetDataRows(CommonConfig.DedsConnectionString, getDatasetIds, _logger);

            foreach (DataRow dr in reader)
            {
                var db = dr[0].ToString();
                dbs.Add(db);
            }
            return dbs.Any();
            
        }
    }
}