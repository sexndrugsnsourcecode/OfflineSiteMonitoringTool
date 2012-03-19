using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OfflineSiteMonitoringTool.DataAccessLayer;

namespace OfflineSiteMonitoringTool.Repository.Tests
{
    public static class AddDataTo_tbRPTOfflineSites_SuppliersToReceiveNotifications
    {
        public static tbRPT_OfflineSites_SuppliersToReceiveNotifications AddRow(string supplier)
        {
            tbRPT_OfflineSites_SuppliersToReceiveNotifications row = new tbRPT_OfflineSites_SuppliersToReceiveNotifications
            {
                rid = 1,
                supplier = supplier
            };

            return row;
        }
    }
}
