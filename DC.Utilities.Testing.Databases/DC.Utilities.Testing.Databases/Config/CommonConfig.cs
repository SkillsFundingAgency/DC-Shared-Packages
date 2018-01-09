using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DC.Utilities.SQLDb.Config
{
    public static class CommonConfig
    {

        private static readonly string _dpsConnectionString;

        private static readonly string _masterConnectionString;

        private static readonly string _dedsConnectionString;
        private static readonly string _dedsPublishUserName;
        

        private const string DPS_CONNECTION_STRING_KEY = "DpsConnectionString";
        private const string MASTER_CON_STRING_KEY = "MasterConnectionString";
        private const string DEDS_CON_STRING_KEY = "DedsConnectionString";
        internal static readonly string DedsDatabaseName;
       

        static CommonConfig()
        {

            _dpsConnectionString = ConfigurationManager.ConnectionStrings[DPS_CONNECTION_STRING_KEY].ConnectionString;
            _masterConnectionString = ConfigurationManager.ConnectionStrings[MASTER_CON_STRING_KEY].ConnectionString;
            _dedsConnectionString = ConfigurationManager.ConnectionStrings[DEDS_CON_STRING_KEY].ConnectionString;
            DedsDatabaseName = ConfigurationManager.AppSettings["DedsDatabaseName"];
            _dedsPublishUserName = ConfigurationManager.AppSettings["DedsPublishUserName"];
        }

        public static bool Verbose { get; internal set; }

        public static string  DpsConnectionString {
            get {
                if (string.IsNullOrEmpty(_dpsConnectionString)) throw new NotImplementedException("DPS Connection string not provided, check the configuration files");
                return _dpsConnectionString;
            }
        
        }

        public static string MasterConnectionString
        {
            get
            {
                if (string.IsNullOrEmpty(_masterConnectionString)) throw new NotImplementedException("Master Connection string not provided, check the configuration files");
                return _masterConnectionString;
            }

        }
        public static string DedsConnectionString
        {
            get
            {
                if (string.IsNullOrEmpty(_dedsConnectionString)) throw new NotImplementedException("DEDS Connection string not provided, check the configuration files");
                return _dedsConnectionString;
            }

           
        }

        /// <summary>
        /// this is hardcoded at the moment 
        /// </summary>
        public static string DedsPublishUser
        {
            get { return Environment.MachineName + @"\"+_dedsPublishUserName; }
        }



    }
}
