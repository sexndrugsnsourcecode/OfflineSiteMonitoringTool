using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OfflineSiteMonitoringTool.Repository
{
    public partial class Repository
    {
        public void UpdateSiteAlreadyRecordedAsOffline(string offlineSite)
        {
            ExecuteDbAction(updateSiteAlreadyRecordedAssOffline, offlineSite);
        }

        private void updateSiteAlreadyRecordedAssOffline(string offlineSite)
        {
            var org = (from x in _reportingEntity.tbRPT_OfflineSites
                       where x.Org == offlineSite
                       select x).FirstOrDefault();

            if (org == null)
            {
                //_log.Add("Tool attempted to update an organisation that isn't present in " +
                //    "tbRPT_OfflineSites. Organisation: " + offlineOrg);

                return;
            }

            org.AuditUpdatedOn = DateTime.Now;

            _reportingEntity.SaveChanges();

            //_log.Add("Organisation still offline: " + org.Org);
            return;
        }
    }
}
