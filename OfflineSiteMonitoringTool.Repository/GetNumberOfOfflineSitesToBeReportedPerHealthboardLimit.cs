using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OfflineSiteMonitoringTool.Repository
{
    public partial class Repository
    {
        /// <summary>
        /// Returns NumberOfOfflineSitesPerHealthboardLimit value from config or default value if no value is specified in config file
        /// </summary>
        public int GetNumberOfOfflineSitesToBeReportedPerHealthboardLimit()
        {
            int defaultLimit = 15;
            int numberOfOfflineSitesPerHealthboardLimit;

            try
            {
                numberOfOfflineSitesPerHealthboardLimit = _configHelper.GetNumberOfOfflineSitesToBeReportedPerHealthboardLimit();
            }
            catch   // Use default value if no value has been specified in config file
            {
                numberOfOfflineSitesPerHealthboardLimit = defaultLimit;
            }

            return numberOfOfflineSitesPerHealthboardLimit;
        }
    }
}
