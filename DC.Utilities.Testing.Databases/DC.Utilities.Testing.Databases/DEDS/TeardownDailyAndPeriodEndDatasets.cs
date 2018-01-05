using DC.Utilities.SQLDb.Helpers;

namespace ILR_Support_Tool.DEDS
{
    public class TeardownDailyAndPeriodEndDatasets:BaseDatabaseHelper
    {

        public TeardownDailyAndPeriodEndDatasets(ILogger logger) : base(logger) { }
        public void Run()
        {
            _logger.Clear();
            _logger.Message("Tearing down Daily and Monthly Databases");
            new TeardownDedsDatabase(_logger).Run("DS_OLASSEAS1314_Collection_Daily");
            new TeardownDedsDatabase(_logger).Run("DS_OLASSEAS1314_Collection_Monthly");
            new TeardownDedsDatabase(_logger).Run("DS_EAS1314_Collection_Daily");
            new TeardownDedsDatabase(_logger).Run("DS_EAS1314_Collection_Monthly");
            new TeardownDedsDatabase(_logger).Run("DS_SILR1314_Collection_Daily");
            new TeardownDedsDatabase(_logger).Run("DS_SILR1314_Collection_Monthly");
            RemoveDesEntry("DS_OLASSEAS1314_Collection_Daily");
            RemoveDesEntry("DS_OLASSEAS1314_Collection_Monthly");
            RemoveDesEntry("DS_EAS1314_Collection_Daily");
            RemoveDesEntry("DS_EAS1314_Collection_Monthly");
            RemoveDesEntry("DS_SILR1314_Collection_Daily");
            RemoveDesEntry("DS_SILR1314_Collection_Monthly");
            _logger.Message("Daily and Monthly Databases Teardown Complete");
        }
        
        private void RemoveDesEntry(string databaseName)
        {
            var sql = string.Format("DELETE FROM [dcftdes].[dbo].[DataSet] WHERE Code = '{0}'", databaseName);
            SqlHelper.ExecuteSql(sql);
        }
    }
}
