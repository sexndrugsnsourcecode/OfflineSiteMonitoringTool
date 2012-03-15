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
            ExecuteDbQuery(removeOnlineSite, onlineSite);    
        }

        private void removeOnlineSite(string onlineSite)
        {
            var siteRecordedAsOnline = (from x in _reportingEntity.tbRPT_OfflineSites
                                        where onlineSite == x.Org
                                        select x).FirstOrDefault();

            if (siteRecordedAsOnline == null)
            {
                //_log.Add("Tool tried to remove an organisation from tbRPT_OfflineSites that " +
                //    "doesn't exist. Organisation: " + onlineOrg);

                return;
            }

            _reportingEntity.DeleteObject(siteRecordedAsOnline);
            _reportingEntity.SaveChanges();

            //_log.Add("Organisation has come back online: " + onlineOrg);
            return;
        }
    }
}
