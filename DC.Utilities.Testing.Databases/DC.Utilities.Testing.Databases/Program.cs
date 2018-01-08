using CommandLine;
using DC.Utilities.SQLDb.CommandOptions;
using DC.Utilities.SQLDb.Helpers;
using ILR_Support_Tool.DEDS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DC.Utilities.SQLDb
{
    /// <summary>
    /// Excecutable should be move in a new proj to allow other c# project use the assembly
    /// </summary>
    static class Program
    {

        static void Main(string[] args)
        {

            Options options = new Options();
            if (args.Length > 0)
            {
                ConsoleLogger logger = new ConsoleLogger();

                var result = Parser.Default.ParseArguments(args, options, OnVerbCommand);
                if (!result)
                {
                    WriteUsagesAndThrowError(options);
                   
                }

            }
            else {
                WriteUsagesAndThrowError(options);
            }
           
        }

        private static void WriteUsagesAndThrowError(Options options)
        {
            Console.Write(options.GetUsage());
            throw new InvalidOperationException("Please see Usages of the tool and pass the appropriate parameters");
        }

        private static void OnVerbCommand(string verbArgument, object verbOptions)
        {
           if(verbOptions != null)//parsing successful
            {
                Console.Write(verbArgument);
                if (verbArgument.ToLower().Equals(RestoreDbOptions.Verb))
                {
                    RestoreDbOptions dbOptions = (RestoreDbOptions)verbOptions;
                    restoreDatabase(dbOptions);
                }
                else if (verbArgument.ToLower().Equals(RemoveDbOptions.Verb))
                {
                    RemoveDbOptions dbOptions = (RemoveDbOptions)verbOptions;
                    removeDatabase(dbOptions);
                }

            }
            // throw new NotImplementedException();
        }
        

        private static void restoreDatabase(RestoreDbOptions dbOptions)
        {

            if(dbOptions.IsDedsDb) //this is a db needs to be restored in deds and publishing is needed
            {
                RestoreDedsDatabase db = new RestoreDedsDatabase(new ConsoleLogger());
                db.Run(dbOptions.DatabaseName, dbOptions.BackupFile);
            }
            
        }

        private static void removeDatabase(RemoveDbOptions dbOptions)
        {

            if (dbOptions.IsDedsDb) //this is a db needs to be restored in deds and publishing is needed
            {
                TeardownDedsDatabase db = new TeardownDedsDatabase(new ConsoleLogger());
                db.Run(dbOptions.DatabaseName);
            }

        }

    }


    
}
