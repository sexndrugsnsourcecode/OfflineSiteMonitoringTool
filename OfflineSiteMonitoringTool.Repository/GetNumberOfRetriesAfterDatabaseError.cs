using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OfflineSiteMonitoringTool.Repository
{
    public partial class Repository
    {
        /// <summary>
        /// Returns NumberOfRetriesAfterSQLTimeout value from config or default value if no value is specified in config file
        /// </summary>
        public int GetNumberOfRetriesAfterDatabaseError()
        {
            int defaultLimit = 5;
            int numberOfRetriesAfterSQLTimeout;

            try
            {
                numberOfRetriesAfterSQLTimeout = _configHelper.GetNumberOfRetriesAfterDatabaseError();
            }
            catch   // Use default value if no value has been specified in config file
            {
                numberOfRetriesAfterSQLTimeout = defaultLimit;
            }

            return numberOfRetriesAfterSQLTimeout;
        }
    }
}
