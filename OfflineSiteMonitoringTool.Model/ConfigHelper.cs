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

        public ConfigHelper()
        {
            numberOfOfflineSitesToBeReportedPerHealthboardLimit = Convert.ToInt32(ConfigurationManager.AppSettings["HealthboardOfflineOrganisationsLimit"]);
            // _log.Add("healthboardOfflineOrganisationsLimit: " + healthboardOfflineOrganisationsLimit.ToString());
        }

        public int GetNumberOfOfflineSitesToBeReportedPerHealthboardLimit()
        {
            return numberOfOfflineSitesToBeReportedPerHealthboardLimit;
        }
    }
}
