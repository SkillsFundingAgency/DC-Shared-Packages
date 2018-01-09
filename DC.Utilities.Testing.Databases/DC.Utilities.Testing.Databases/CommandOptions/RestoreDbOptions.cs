using CommandLine;
using CommandLine.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DC.Utilities.SQLDb.CommandOptions
{

    
    public class RestoreDbOptions
    {
        internal  const string Verb = "restoredb";

        public RestoreDbOptions() { }

        [Option("dbname", Required =true, HelpText ="Name of database to be restored")]
        public string DatabaseName { get; set; }

        [Option("backupfile", Required = true, HelpText = "Name of the backup file with path on the disk")]
        public string BackupFile { get; set; }

        [Option("IsDedsDB", Required = false, HelpText = "Optional flag to indicate whether DB to be restored and published in deds")]
        public bool IsDedsDb { get; set; }

    }
   
    public class RemoveDbOptions
    {
        internal  const string Verb = "removedb";
        public RemoveDbOptions() { }

        private bool verbose = false;

        [Option("dbname", Required = true, HelpText = "Name of database to be removed")]
        public string DatabaseName { get; set; }

        [Option("IsDedsDB", Required = false, HelpText = "Optional flag to indicate whether DB to be restored and published in deds")]
        public bool IsDedsDb { get; set; }

        [Option("verbose", HelpText = "Logs the verbose messages for the debug purpose")]
        public bool Verbose { get { return verbose; } set { verbose = value; Config.CommonConfig.Verbose = value; } }
    }


    public class Options
    {

        

        [VerbOption(RestoreDbOptions.Verb, HelpText = "Restore a DB to deds databases")]
        public RestoreDbOptions RestoreDb { get; set; }

        [VerbOption(RemoveDbOptions.Verb, HelpText = "Remove a DB to deds databases")]
        public RemoveDbOptions RemoveDb { get; set; }

       
        
        public string GetUsage()
        {


            //return "";
            var help = HelpText.AutoBuild(this);
            if (this.RestoreDb != null) help.AddOptions(this.RestoreDb);
            if (this.RemoveDb != null) help.AddOptions(this.RemoveDb);
            return help.ToString();
            // this without using CommandLine.Text
            //or using HelpText.AutoBuild


        }


    }
}
