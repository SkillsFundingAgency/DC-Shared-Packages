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
                    Console.Write(options.GetUsage());

            }
            else {
                Console.Write(options.GetUsage());
            }
           
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






    /*
     pre-release
      var parser = new Parser();
                var result = parser.ParseArguments(args, typeof(RestoreDbOptions), typeof(RemoveDbOptions), typeof(Options));
                if (result is NotParsed<object>) {
                    System.Diagnostics.Debug.Write(((NotParsed<object>)result).Errors.ToErrorString());
                    return;
                }

                var parsed = ((Parsed<object>)result).Value;
                if(parsed is RestoreDbOptions)
                { Console.Write("Restored"); }
               // var result = Parser.Default.ParseArguments<RestoreDbOptions, RemoveDbOptions, Options>(args);
                if (result == null)
                {
                    Console.Write(options.GetUsage());
                }
                
     
     */
    //public static class Ex
    //{
    //    public static string ToErrorString(this IEnumerable<CommandLine.Error> errors)
    //    {
    //        StringBuilder sb = new StringBuilder();
    //        errors.ToList().ForEach(err => {
    //            sb.Append(err.ToString());
    //                 }
    //                 );
    //        return sb.ToString();
    //    }
    //}
}
