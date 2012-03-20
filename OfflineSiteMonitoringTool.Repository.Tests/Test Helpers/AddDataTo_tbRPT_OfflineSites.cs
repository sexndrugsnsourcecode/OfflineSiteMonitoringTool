using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OfflineSiteMonitoringTool.DataAccessLayer;

namespace OfflineSiteMonitoringTool.Repository.Tests
{
    public static class AddDataTo_tbRPT_OfflineSites
    {
        public static tbRPT_OfflineSites AddRow(string org, DateTime auditCreatedOn, string supplier = "test", string healthboard="test", 
                                                DateTime? dateOfflineNotificationSent=null, DateTime? auditUpdatedOn=null)
        {
            tbRPT_OfflineSites row = new tbRPT_OfflineSites
            {
                Org = org,
                Supplier = supplier,
                Healthboard = healthboard,
                DateOfflineNotificationSent = dateOfflineNotificationSent,
                AuditCreatedOn = auditCreatedOn,
                AuditUpdatedOn = auditUpdatedOn
            };

            return row;
        }
    }
}
