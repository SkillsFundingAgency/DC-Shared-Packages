using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DC.Utilities.SQLDb.Helpers
{
    /// <summary>
    /// Implementation of the Ilogger to refactore existing logging calls
    /// </summary>
    public class ConsoleLogger : ILogger
    {
        public void Clear()
        {
            Console.Clear();
        }

        public void Message(string message)
        {
            Console.WriteLine(message);
        }
    }
}
