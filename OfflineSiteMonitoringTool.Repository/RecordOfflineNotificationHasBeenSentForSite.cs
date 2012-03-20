using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OfflineSiteMonitoringTool.Repository
{
    public partial class Repository
    {
        public void RecordOfflineNotificationHasBeenSentForSite(string offlineSite)
        {
            ExecuteDbAction(recordOfflineNotificationHasBeenSentForSite, offlineSite);
        }

        private void recordOfflineNotificationHasBeenSentForSite(string offlineSite)
        {
            var site = (from x in _reportingEntity.tbRPT_OfflineSites
                        where x.Org == offlineSite
                        select x).FirstOrDefault();

            if (site == null)
            {
                //_log.Add("ERROR: Tried to update the email was sent for a non-existant organisation from tbRPT_IOfflineSites. Org: " + org);
                return;
            }

            site.DateOfflineNotificationSent = DateTime.Now;
            _reportingEntity.SaveChanges();
            //_log.Add("DB updated for: " + org);
            return;
        }
    }
}
