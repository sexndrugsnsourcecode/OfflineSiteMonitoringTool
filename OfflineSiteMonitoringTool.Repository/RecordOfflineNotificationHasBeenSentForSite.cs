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
                _log.Add("ERROR: Tool has tried to record that an offline notification has been sent for site:" +
                    offlineSite + " which is not present in tbRPT_OfflineSites.");

                return;     
            }

            // Check that an offline notification has not already been sent to this site
            if (site.DateOfflineNotificationSent != null)
            {
                _log.Add("ERROR: Tool just attempted to record an offline notification having been sent for site: " + site.Org +
                    " but offline notification has already been sent for this site.");

                return;
            }

            site.DateOfflineNotificationSent = DateTime.Now;
            _reportingEntity.SaveChanges();
            
            return;
        }
    }
}
