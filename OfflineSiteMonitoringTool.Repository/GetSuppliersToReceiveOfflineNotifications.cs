using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OfflineSiteMonitoringTool.Repository
{
    public partial class Repository
    {
        public List<string> GetSuppliersToReceiveOfflineNotifications()
        {
            List<string> suppliersToReceiveNotifications = new List<string>();

            suppliersToReceiveNotifications = ExecuteDbQuery(getSuppliersToReceiveOfflineNotifications);

            return suppliersToReceiveNotifications;
        }

        private List<string> getSuppliersToReceiveOfflineNotifications()
        {
            List<string> suppliersRaw = (from x in _reportingEntity.tbRPT_OfflineSites_SuppliersToReceiveNotifications
                                        select x.supplier).ToList<string>();

            List<string> suppliers = CleanData(suppliersRaw);

            return suppliers;
        }
    }
}
