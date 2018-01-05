using System.Collections.Generic;
using DC.Utilities.SQLDb.Helpers;
using DC.Utilities.SQLDb.Config;

namespace ILR_Support_Tool.DPS
{
    public class TeardownIntraJobDatabases
    {
        private readonly ILogger _logger; 
       

        public TeardownIntraJobDatabases(ILogger logger)
        {
            _logger = logger;
        }


        public void Run()
        {
            System.Threading.ThreadPool.QueueUserWorkItem(delegate
            {
                _logger.Clear();
                _logger.Message("Tearing down IntraJob DPS databases");
                IList<string> dpsDatabases = new DpsIntraJobDatabaseProvider().GetIntraJobDatabaseNames();
                foreach (var dpsDatabase in dpsDatabases)
                {
                    _logger.Message("Tearing down " + dpsDatabase + " database");
                    SqlHelper.DropDatabase(dpsDatabase);
                    _logger.Message(dpsDatabase + " Teardown Complete");
                }

                _logger.Message("IntraJob DPS Databases Teardown Complete");
            }, null);
        }
    }
}
