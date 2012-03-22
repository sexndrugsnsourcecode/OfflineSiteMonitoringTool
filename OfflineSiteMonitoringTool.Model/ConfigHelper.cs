using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace OfflineSiteMonitoringTool.Model
{
    public class ConfigHelper : IConfigHelper
    {
        private int numberOfOfflineSitesToBeReportedPerHealthboardLimit;
        private int numberOfRetriesAfterDatabaseError;
        private string offlineReportFromAddress;
        private string offlineReportReplyToAddress;

        public ConfigHelper()
        {
            numberOfOfflineSitesToBeReportedPerHealthboardLimit = Convert.ToInt32(ConfigurationManager.AppSettings["HealthboardOfflineOrganisationsLimit"]);
            numberOfRetriesAfterDatabaseError = Convert.ToInt32(ConfigurationManager.AppSettings["NumberOfRetriesAfterSQLTimeout"]);
            offlineReportFromAddress = ConfigurationManager.AppSettings["FromEmailAddress"];
            offlineReportReplyToAddress = ConfigurationManager.AppSettings["ReplyToEmailAddress"];
        }

        public int GetNumberOfOfflineSitesToBeReportedPerHealthboardLimit()
        {
            return numberOfOfflineSitesToBeReportedPerHealthboardLimit;
        }

        public int GetNumberOfRetriesAfterDatabaseError()
        {
            return numberOfRetriesAfterDatabaseError;
        }

        public string GetFromAddress()
        {
            return offlineReportFromAddress;
        }

        public string GetReplyToAddress()
        {
            return offlineReportReplyToAddress;
        }
    }
}
