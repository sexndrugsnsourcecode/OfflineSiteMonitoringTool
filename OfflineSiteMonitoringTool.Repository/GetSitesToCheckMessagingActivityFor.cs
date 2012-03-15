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
                                                    where x.endDate == null
                                                    && x.archived == false
                                                    && x.dispensing == false
                                                    orderby x.id
                                                    select x.id).ToList<string>();

            List<string> sitesToCheckMessagingActivityFor = CleanData(sitesToCheckMessagingActivityForRaw);

            return sitesToCheckMessagingActivityFor;
        }
    }
}
