using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DC.Utilities.SQLDb.Helpers
{
    public class BaseDatabaseHelper
    {

        protected readonly ILogger _logger;


        public BaseDatabaseHelper(ILogger logger)
        {
            _logger = logger;
        }
    }
}
