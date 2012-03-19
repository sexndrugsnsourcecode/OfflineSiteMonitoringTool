﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OfflineSiteMonitoringTool.Repository
{
    public partial class Repository
    {
        public List<string> GetHealthboardsThatHaveExceededNumberOfOfflineSitesToBeReportedLimit(int numberOfOfflineSitesToBeReportedPerHealthboardLimit)
        {
            List<string> healthboardsThatHaveExceededNumberOfOfflineSitesToBeReportedLimit;

            healthboardsThatHaveExceededNumberOfOfflineSitesToBeReportedLimit =
                ExecuteDbQuery(getHealthboardsThatHaveExceededNumberOfOfflineSitesToBeReportedLimit, numberOfOfflineSitesToBeReportedPerHealthboardLimit);

            return healthboardsThatHaveExceededNumberOfOfflineSitesToBeReportedLimit;
        }

        private List<string> getHealthboardsThatHaveExceededNumberOfOfflineSitesToBeReportedLimit(int numberOfOfflineSitesToBeReportedPerHealthboardLimit)
        {
            // Setting number of ofline sites per healthboard limit to zero is equivalent of telling app to ignore limit
            if (numberOfOfflineSitesToBeReportedPerHealthboardLimit == 0)
            {
                // TODO: Update log
                return new List<string>();
            }
            
            // Get offline sites to be reported
            var sitesToBeReported = (from x in _reportingEntity.tbRPT_OfflineSites
                                     where x.DateOfflineNotificationSent == null
                                     select x);

            // Create dictionary to hold number of offline sites per healthboard
            Dictionary<String, Int16> inactiveSitesPerHealthboard = new Dictionary<string, short>();

            // Populate dictionary by iterating through offline sites to be reported collection
            foreach (var site in sitesToBeReported)
            {
                string healthboard = site.Healthboard;

                if (inactiveSitesPerHealthboard.ContainsKey(healthboard))
                    inactiveSitesPerHealthboard[healthboard]++;
                else
                    inactiveSitesPerHealthboard.Add(healthboard, 1);
            }

            List<string> healthboardsThatHaveExceededOfflineSiteLimitRaw = new List<string>();

            // for each key/value in dict, if value exceeds offline site limit, add healthboard string to return list
            foreach (KeyValuePair<string, Int16> healthboard in inactiveSitesPerHealthboard)
            {
                if (healthboard.Value > numberOfOfflineSitesToBeReportedPerHealthboardLimit)
                    healthboardsThatHaveExceededOfflineSiteLimitRaw.Add(healthboard.Key);
            }

            List<string> healthboardsThatHaveExceededOfflineSiteLimit = CleanData(healthboardsThatHaveExceededOfflineSiteLimitRaw);

            return healthboardsThatHaveExceededOfflineSiteLimit;
        }
    }
}
