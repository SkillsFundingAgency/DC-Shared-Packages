using System;
using System.Collections.Generic;
using DC.Utilities.SQLDb.Helpers;

namespace ILR_Support_Tool.DEDS
{
    public class TeardownDedsDatabases:BaseDatabaseHelper
    {

        public TeardownDedsDatabases(ILogger logger):base(logger)
        {

        }
        public void Run()
        {
            // System.Threading.ThreadPool.QueueUserWorkItem(delegate
            //{
            _logger.Clear();
            _logger.Message("Tearing down DES Databases");

            // Tear down databases
            new TeardownDedsDatabase(_logger).Run("LARS");
            new TeardownDedsDatabase(_logger).Run("ORGANISATION");
            new TeardownDedsDatabase(_logger).Run("ULNv2");
            new TeardownDedsDatabase(_logger).Run("DS_Employers");
            new TeardownDedsDatabase(_logger).Run("Postcodes_List");
            new TeardownDedsDatabase(_logger).Run("DS_ILR1516_Summarised_Actuals");
            new TeardownDedsDatabase(_logger).Run("FCS-Contracts");
            new TeardownDedsDatabase(_logger).Run("DS_EAS1617_Collection");
            new TeardownDedsDatabase(_logger).Run("DS_OLASSEAS1617_Collection");
            new TeardownDedsDatabase(_logger).Run("OLASS_Reference_Data");
            new TeardownDedsDatabase(_logger).Run("Validation_Messages_Reference_Data");
            new TeardownDedsDatabase(_logger).Run("ESF_Supplementary_Data");
            new TeardownDedsDatabase(_logger).Run("ESF_Contract_Reference_Data");
            new TeardownDedsDatabase(_logger).Run("ESF_Funding_Data");
            new TeardownDedsDatabase(_logger).Run("ESF_Summarised_Actuals");
            new TeardownDedsDatabase(_logger).Run("Large_Employers_Reference_Data");
            new TeardownDedsDatabase(_logger).Run("PostcodeFactorsReferenceData");
            new TeardownDedsDatabase(_logger).Run("EFA_CoF_Removal_Reference_Data");
            new TeardownDedsDatabase(_logger).Run("ONS_Postcode_Directory");
            new TeardownDedsDatabase(_logger).Run("DAS_DS_ILR1617_Summarised_Actuals");

            _logger.Message("DES Databases Teardown Complete");
            _logger.Message("Clearing DES database");

            //clear dcftdes
            ClearDES();
            _logger.Message("DES database Cleared");
            //}, null);
        }

        /// <summary>
        /// Clears the DES.
        /// </summary>
        private static void ClearDES()
        {
            IList<string> statementsToExecute = new List<string>();
            statementsToExecute.Add("DELETE From [dcftdes].[dbo].[DataSetVersionRoles]");
            statementsToExecute.Add("DELETE From [dcftdes].[dbo].[QueryRoles]");
            statementsToExecute.Add("DELETE From [dcftdes].[dbo].[QueryFilterPickListValues]");
            statementsToExecute.Add("DELETE From [dcftdes].[dbo].[QueryFilter]");
            statementsToExecute.Add("DELETE From [dcftdes].[dbo].[QuerySort]");
            statementsToExecute.Add("DELETE From [dcftdes].[dbo].[Query]");
            statementsToExecute.Add("DELETE From [dcftdes].[dbo].[DataScript]");
            statementsToExecute.Add("DELETE From [dcftdes].[dbo].[DataSetVersion]");
            statementsToExecute.Add("DELETE From [dcftdes].[dbo].[SchemaScript]");
            statementsToExecute.Add("DELETE From [dcftdes].[dbo].[DataSetSchema]");
            statementsToExecute.Add("DELETE From [dcftdes].[dbo].[DataSet]");
            statementsToExecute.Add("DELETE From [dcftdes].[dbo].[AuditEntry]");

            try
            {
                foreach (var statement in statementsToExecute)
                {
                    SqlHelper.ExecuteSql(statement);
                }
            }
            catch (Exception)
            {
            }
        }
    }
}
