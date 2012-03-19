using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OfflineSiteMonitoringTool.DataAccessLayer;
using OfflineSiteMonitoringTool.Model;

namespace OfflineSiteMonitoringTool.Repository.Tests
{
    [TestClass]
    public class UpdateSiteAlreadyRecordedAsOfflineTests
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
        public void UpdateSiteAlreadyRecordedAsOffline_SingleOfflineSiteToUpdate_SiteUpdated()
        {
            string orgId = "1111";
            DateTime date = DateTime.Now.AddDays(-7);
            //string expectedLogMessage = "Organisation still offline: " + org;

            // Add org details to tbRPT_OfflineSites
            mockReportingEntity.tbRPT_OfflineSites.AddObject(AddDataTo_tbRPTOfflineSites.AddRow(orgId, date));

            // Call proc
            repository.UpdateSiteAlreadyRecordedAsOffline(orgId);

            // Check org was updated
            var offlineOrg = (from x in mockReportingEntity.tbRPT_OfflineSites
                              select x).FirstOrDefault();

            Assert.AreEqual(DateTime.Now.Date, offlineOrg.AuditUpdatedOn.Value.Date);
            //log.Verify(x => x.Add(expectedLogMessage));
        }

        [TestMethod]
        public void UpdateSiteAlreadyRecordedAsOffline_SiteToUpdateNotInTable_LogsError_NothingUpdated()
        {
            string offlineOrgId = "1111";
            DateTime offlineOrgDate = DateTime.Now.AddDays(-7);
            //string expectedLogMessage = "Organisation still offline: " + org;

            // Add org details to tbRPT_OfflineSites
            mockReportingEntity.tbRPT_OfflineSites.AddObject
                (AddDataTo_tbRPTOfflineSites.AddRow(offlineOrgId, offlineOrgDate, "test", "test", null, offlineOrgDate));

            string orgNotInTable = "2222";

            // Call proc
            repository.UpdateSiteAlreadyRecordedAsOffline(orgNotInTable);

            // Check org was updated
            var offlineOrg = (from x in mockReportingEntity.tbRPT_OfflineSites
                              select x).FirstOrDefault();

            Assert.AreEqual(offlineOrgDate.Date, offlineOrg.AuditUpdatedOn.Value.Date);
            //log.Verify(x => x.Add(expectedLogMessage));
        }
    }
}
