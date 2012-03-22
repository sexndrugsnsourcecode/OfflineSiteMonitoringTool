using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OfflineSiteMonitoringTool.Repository
{
    public partial class Repository
    {
        public void RemoveOnlineSite(string onlineSite)
        {
            ExecuteDbAction(removeOnlineSite, onlineSite);    
        }

        private void removeOnlineSite(string onlineSite)
        {
            var siteRecordedAsOnline = (from x in _reportingEntity.tbRPT_OfflineSites
                                        where onlineSite == x.Org
                                        select x).FirstOrDefault();

            if (siteRecordedAsOnline == null)
            {
                _log.Add("ERROR: Tool attempted to mark a site: " + onlineSite + " as online but this site doesn't exist in tbRPT_OfflineSites");

                return;
            }

            _reportingEntity.DeleteObject(siteRecordedAsOnline);
            _reportingEntity.SaveChanges();

            _log.Add("INFO: Site has come back online: " + onlineSite);
            return;
        }
    }
}
