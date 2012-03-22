using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OfflineSiteMonitoringTool.DataAccessLayer;
using OfflineSiteMonitoringTool.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace OfflineSiteMonitoringTool.Repository.Tests
{
    [TestClass]
    public class RecordOfflineNotificationHasBeenSentForSiteTests
    {
        private IReportingEntities mockReportingEntity;
        private IConfigHelper configHelper;
        private IRepository repository;
        private Mock<ILogger> log;

        [TestInitialize]
        public void TestInitialize()
        {
            mockReportingEntity = new ReportingEntitiesMock();
            configHelper = new ConfigHelper();
            log = new Mock<ILogger>();
            repository = new Repository(mockReportingEntity, configHelper, log.Object);
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
        public void RecordOfflineNotificationHasBeenSentForSite_SiteDoesNotExistInOfflineSitesTable_LogsError_NothingAddedToTable()
        {
            string orgId = "1111";

            string expectedLogMessage = "ERROR: Tool has tried to record that an offline notification has been sent for site:" +
                orgId + " which is not present in tbRPT_OfflineSites.";

            // No need to add site details to offline sites table

            repository.RecordOfflineNotificationHasBeenSentForSite(orgId);

            int numberOfOfflineSites = (from x in mockReportingEntity.tbRPT_OfflineSites
                                        select x).Count();

            Assert.AreEqual(0, numberOfOfflineSites);
            log.Verify(x => x.Add(expectedLogMessage));
        }

        [TestMethod]
        public void RecordOfflineNotificationHasBeenSentForSite_SiteAlreadyHasDateNotificationSentValueSent_DoesNotOverwriteValue_LogsError()
        {
            // Add site details to offline sites table
            string orgId = "1111";
            DateTime auditCreatedOn = DateTime.Now.AddDays(-3);
            string supplier = "test";
            string healthboard = "test";
            DateTime dateOfflineNotificationSent = DateTime.Now.AddDays(-3);

            mockReportingEntity.tbRPT_OfflineSites.AddObject(AddDataTo_tbRPT_OfflineSites.AddRow(orgId, auditCreatedOn, supplier, healthboard, dateOfflineNotificationSent));

            repository.RecordOfflineNotificationHasBeenSentForSite(orgId);

            string expectedLogMessage = "ERROR: Tool just attempted to record an offline notification having been sent for site: " + orgId +
                " but offline notification has already been sent for this site.";

            DateTime? dateOfflineNotificationValueAfterUpdateAttempt = (from x in mockReportingEntity.tbRPT_OfflineSites
                                                                       where x.Org == orgId
                                                                       select x.DateOfflineNotificationSent).FirstOrDefault();

            Assert.AreEqual(dateOfflineNotificationSent, dateOfflineNotificationValueAfterUpdateAttempt);
            log.Verify(x => x.Add(expectedLogMessage));
        }
    }
}
