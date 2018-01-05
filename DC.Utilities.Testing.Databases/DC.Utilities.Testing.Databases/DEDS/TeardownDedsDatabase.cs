using System;
using DC.Utilities.SQLDb.Helpers;

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
                //delete snapshots
                SqlHelper.DeleteSnapshots(databaseName);
                //delete databases
                SqlHelper.DropDatabase(databaseName);

                _logger.Message(databaseName + " Teardown Complete" );
            }
            catch (Exception)
            {
                _logger.Message(databaseName + " Teardown Failed");
            }
        }
    }
}
