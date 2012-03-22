using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OfflineSiteMonitoringTool.Repository
{
    public partial class Repository
    {
        public List<string> GetSitesToCheckMessagingActivityFor() 
        {
            List<string> sitesToCheckMessagingActivityFor = new List<string>();

            sitesToCheckMessagingActivityFor = ExecuteDbQuery(getSitesToCheckMessagingActivityFor);

            return sitesToCheckMessagingActivityFor;
        }

        private List<string> getSitesToCheckMessagingActivityFor()
        {
            var sitesToCheckMessagingActivityForRaw = (from x in _reportingEntity.tbEPS_Organisation
                                                       join y in _reportingEntity.tbRPT_OrgSupplier 
                                                       on x.id equals y.org
                                                       where x.endDate == null
                                                       && x.archived == false
                                                       && x.dispensing == false
                                                       && y.excludeFromOfflineNotifications == false
                                                       select x.id).ToList<string>();

            List<string> sitesToCheckMessagingActivityFor = CleanData(sitesToCheckMessagingActivityForRaw);

            return sitesToCheckMessagingActivityFor;
        }
    }
}
