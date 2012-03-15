using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OfflineSiteMonitoringTool.Repository
{
    public partial class Repository
    {
        public List<string> GetSitesRecordedAsOffline()
        {
            List<string> sitesRecordedAsOffline = new List<string>();

            sitesRecordedAsOffline = ExecuteDbQuery(getSitesRecordedAsOffline);

            return sitesRecordedAsOffline;
        }

        private List<string> getSitesRecordedAsOffline()
        {
            List<string> sitesRecordedAsOfflineRaw = (from x in _reportingEntity.tbRPT_OfflineSites
                                                      orderby x.Org
                                                      select x.Org).ToList<string>();

            //foreach (string org in orgs)
            //    _log.Add("Org already recorded as offline: " + org);

            List<string> sitesRecordedAsOffline = CleanData(sitesRecordedAsOfflineRaw);

            return sitesRecordedAsOffline;
        }
    }
}
