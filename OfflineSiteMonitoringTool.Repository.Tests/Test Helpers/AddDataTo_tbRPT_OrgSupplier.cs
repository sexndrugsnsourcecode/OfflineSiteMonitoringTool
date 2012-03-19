using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OfflineSiteMonitoringTool.DataAccessLayer;

namespace OfflineSiteMonitoringTool.Repository.Tests
{
    public static class AddDataTo_tbRPT_OrgSupplier
    {
        public static tbRPT_OrgSupplier AddRow(string orgId, DateTime latestAmsMessage)
        {
            DateTime date = DateTime.Now;

            tbRPT_OrgSupplier row = new tbRPT_OrgSupplier
            {
                org = orgId,
                epoc = "test",
                supplier = "test",
                product = "test",
                version = "test",
                latestMsg = date,
                X509SerialNumber = "test",
                downloaddate = date,
                AuthCertSerialNumber = "test",
                AuthCertDownloadDate = date,
                ipAddress = "test",
                reportingSupplier = "test",
                disp = false,
                extended = false,
                previousSupplier = "test",
                previousProduct = "test",
                CmsBetaSite = false,
                EpocCertBy = "test",
                ResignCertBy = "test",
                latestAMS = latestAmsMessage,
                latestCMS = date,
                latestMAS = date
            };

            return row;
        }
    }
}
