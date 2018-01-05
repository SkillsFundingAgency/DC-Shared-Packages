using System.Collections.Generic;
using DC.Utilities.SQLDb.Helpers;
using DC.Utilities.SQLDb.Helpers;
using System;
using System.Configuration;
using DC.Utilities.SQLDb.Config;

namespace ILR_Support_Tool.DPS
{
    public class TeardownDpsDatabases:BaseDatabaseHelper
    {
       
        public TeardownDpsDatabases(ILogger logger):base(logger)
        {
            
        }

     
        public void Run()
        {
            System.Threading.ThreadPool.QueueUserWorkItem(delegate
                {
                    _logger.Clear();
                    _logger.Message("Tearing down DPS databases");
                    IList<string> dpsDatabases = new DpsDatabaseProvider(CommonConfig.DpsConnectionString).GetDpsDatabaseNames();
                    foreach (var dpsDatabase in dpsDatabases)
                    {
                        _logger.Message("Tearing down "+dpsDatabase+" database");
                        SqlHelper.DropDatabase(dpsDatabase);
                        _logger.Message(dpsDatabase + " Teardown Complete");
                    }

                    _logger.Message("DPS Databases Teardown Complete");
                    _logger.Message("Clearing DPS database");
                    ClearDps();
                    _logger.Message("DPS database Cleared");
                }, null);
        }

        private static void ClearDps()
        {
            IList<string> statementsToExecute = new List<string>();
            statementsToExecute.Add("DELETE From [DPS].[dbo].[RelatedAttributeIdentifer]");
            statementsToExecute.Add("DELETE From [DPS].[dbo].[AttributeIdentifier]");
            statementsToExecute.Add("DELETE From [DPS].[dbo].[audit]");
            statementsToExecute.Add("DELETE FROM [DPS].[dbo].[DependentJobTypeIds]");
            statementsToExecute.Add("DELETE FROM [DPS].[dbo].[ExcludedCollectionIds]");
            statementsToExecute.Add("DELETE FROM [DPS].[dbo].[ExcludedDataProviderIds]");
            statementsToExecute.Add("DELETE FROM [DPS].[dbo].[IncludedCollectionIds]");
            statementsToExecute.Add("DELETE FROM [DPS].[dbo].[IncludedDataProviderIds]");
            statementsToExecute.Add("DELETE FROM [DPS].[dbo].[DependentJobTypeIds]");
            statementsToExecute.Add("DELETE From [DPS].[dbo].[Job]");
            statementsToExecute.Add("DELETE From [DPS].[dbo].[QueryFilterProperties]");
            statementsToExecute.Add("DELETE From [DPS].[dbo].[QuerySortProperties]");
            statementsToExecute.Add("DELETE From [DPS].[dbo].[Task]");
            statementsToExecute.Add("DELETE From [DPS].[dbo].[ProcessingFlow]");
            statementsToExecute.Add("DELETE From [DPS].[dbo].[Procedure]");
            statementsToExecute.Add("DELETE From [DPS].[dbo].[DataSetTypeToComponentSet]");
            statementsToExecute.Add("DELETE From [DPS].[dbo].[ComponentSet]");
            statementsToExecute.Add("DELETE From [DPS].[dbo].[SubmissionUnit]");
            statementsToExecute.Add("DELETE From [DPS].[dbo].[CollectionUnit]");
            statementsToExecute.Add("DELETE From [DPS].[dbo].[SubmissionBundle]");
            statementsToExecute.Add("DELETE From [DPS].[dbo].[ComponentSet]");
            statementsToExecute.Add("DELETE From [DPS].[dbo].[InterJobDatabaseMetadata]");
            statementsToExecute.Add("DELETE From [DPS].[dbo].[DataSetType]");

            foreach (var statement in statementsToExecute)
            {
                SqlHelper.ExecuteSql(statement);
            }
        }
    }
}
