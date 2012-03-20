using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OfflineSiteMonitoringTool.DataAccessLayer;
using OfflineSiteMonitoringTool.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace OfflineSiteMonitoringTool.Repository.Tests
{
    [TestClass]
    public class RecordOfflineNotificationHasBeenSentForSiteTests
    {
        private IReportingEntities mockReportingEntity;
        private IConfigHelper configHelper;
        private IRepository repository;

        [TestInitialize]
        public void TestInitialize()
        {
            mockReportingEntity = new ReportingEntitiesMock();
            repository = new Repository(mockReportingEntity, configHelper);
        }

        [TestMethod]
        public void RecordOfflineNotificationHasBeenSentForSite_SiteExistsInOfflineSitesTable_SetsSitesDateNotificationSentValueToCurrentDate()
        {
            // Add site details to offline sites table
            string orgId = "1111";
            DateTime auditCreatedOn = DateTime.Now;

            mockReportingEntity.tbRPT_OfflineSites.AddObject(AddDataTo_tbRPT_OfflineSites.AddRow(orgId, auditCreatedOn));

            repository.RecordOfflineNotificationHasBeenSentForSite(orgId);

            // Grab DateOfflineNotificationSent value for site from database table
            DateTime? result = (from x in mockReportingEntity.tbRPT_OfflineSites
                                where x.Org == orgId
                                select x.DateOfflineNotificationSent).First();

            Assert.AreEqual(DateTime.Now.Date, result.Value.Date);
        }

        [TestMethod]
        public void RecordOfflineNotificationHasBeenSentForSite_SiteDoesNotExistInOfflineSitesTable_LogsError()
        {
            // TODO: Write this test once logging has been implemented
        }

        [TestMethod]
        public void RecordOfflineNotificationHasBeenSentForSite_SiteAlreadyHasDateNotificationSentValueSent_DoesNotOverwriteValue_LogsError()
        {
            // TODO: implement log error check once logging implemented
        }
    }
}
