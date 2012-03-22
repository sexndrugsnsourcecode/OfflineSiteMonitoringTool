using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OfflineSiteMonitoringTool.DataAccessLayer;

namespace OfflineSiteMonitoringTool.Repository.Tests
{
    public static class AddDataTo_tbRPT_OrgSupplier
    {
        public static tbRPT_OrgSupplier AddRow()
        {
            tbRPT_OrgSupplier row = new tbRPT_OrgSupplier
            {
                org = "test",
                epoc = "test",
                supplier = "test",
                product = "test",
                version = "test",
                latestMsg = DateTime.Now,
                X509SerialNumber = "test",
                downloaddate = DateTime.Now,
                AuthCertSerialNumber = "test",
                AuthCertDownloadDate = DateTime.Now,
                ipAddress = "test",
                reportingSupplier = "test",
                disp = false,
                extended = false,
                previousSupplier = "test",
                previousProduct = "test",
                CmsBetaSite = false,
                EpocCertBy = "test",
                ResignCertBy = "test",
                latestAMS = DateTime.Now,
                latestCMS = DateTime.Now,
                latestMAS = DateTime.Now,
                excludeFromOfflineNotifications = false
            };

            return row;
        }

        public static tbRPT_OrgSupplier AddRow(string orgId, DateTime latestAmsMessage)
        {
            DateTime date = DateTime.Now;

            tbRPT_OrgSupplier row = AddRow();
           
            row.org = orgId;
            row.latestAMS= latestAmsMessage;
                
            return row;
        }

        public static tbRPT_OrgSupplier AddRow(string orgId, bool excludeFromOfflineNotifications = false)
        {
            tbRPT_OrgSupplier row = AddRow();

            row.org = orgId;
            row.excludeFromOfflineNotifications = excludeFromOfflineNotifications;

            return row;
        }
    }
}
