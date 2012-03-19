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
        private string offlineReportFromAddress;
        private string offlineReportReplyToAddress;

        public ConfigHelper()
        {
            numberOfOfflineSitesToBeReportedPerHealthboardLimit = Convert.ToInt32(ConfigurationManager.AppSettings["HealthboardOfflineOrganisationsLimit"]);
            offlineReportFromAddress = ConfigurationManager.AppSettings["FromEmailAddress"];
        }

        public int GetNumberOfOfflineSitesToBeReportedPerHealthboardLimit()
        {
            return numberOfOfflineSitesToBeReportedPerHealthboardLimit;
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
