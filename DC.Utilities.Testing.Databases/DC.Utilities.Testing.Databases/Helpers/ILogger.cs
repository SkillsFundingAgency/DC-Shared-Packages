using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DC.Utilities.SQLDb.Helpers
{
    /// <summary>
    /// Just to refactore the existing utility classes which usages specific loggers for the purpos of output on different screens
    /// </summary>
   public interface ILogger
    {
        void Message(string message);
        void Clear();
    }
}
