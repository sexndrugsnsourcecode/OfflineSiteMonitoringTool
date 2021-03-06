﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OfflineSiteMonitoringTool.DataAccessLayer;
using OfflineSiteMonitoringTool.Model;
using Moq;

namespace OfflineSiteMonitoringTool.Repository.Tests
{
    [TestClass]
    public class UpdateSiteAlreadyRecordedAsOfflineTests
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
        public void UpdateSiteAlreadyRecordedAsOffline_SingleOfflineSiteToUpdate_SiteUpdated()
        {
            string orgId = "1111";
            DateTime date = DateTime.Now.AddDays(-7);
            string expectedLogMessage = "INFO: Site still offline: " + orgId;

            // Add org details to tbRPT_OfflineSites
            mockReportingEntity.tbRPT_OfflineSites.AddObject(AddDataTo_tbRPT_OfflineSites.AddRow(orgId, date));

            // Call proc
            repository.UpdateSiteAlreadyRecordedAsOffline(orgId);

            // Check org was updated
            var offlineOrg = (from x in mockReportingEntity.tbRPT_OfflineSites
                              select x).FirstOrDefault();

            Assert.AreEqual(DateTime.Now.Date, offlineOrg.AuditUpdatedOn.Value.Date);
            log.Verify(x => x.Add(expectedLogMessage));
        }

        [TestMethod]
        public void UpdateSiteAlreadyRecordedAsOffline_SiteToUpdateNotInTable_LogsError_NothingUpdated()
        {
            string offlineOrgId = "1111";
            DateTime offlineOrgDate = DateTime.Now.AddDays(-7);

            // Add org details to tbRPT_OfflineSites
            mockReportingEntity.tbRPT_OfflineSites.AddObject
                (AddDataTo_tbRPT_OfflineSites.AddRow(offlineOrgId, offlineOrgDate, "test", "test", null, offlineOrgDate));

            string orgNotInTable = "2222";
            string expectedLogMessage = "ERROR: Site has tried to update site: " + orgNotInTable + " which is not recorded in tbRPT_OfflineSites";

            // Call proc
            repository.UpdateSiteAlreadyRecordedAsOffline(orgNotInTable);

            // Check org was updated
            var offlineOrg = (from x in mockReportingEntity.tbRPT_OfflineSites
                              select x).FirstOrDefault();

            Assert.AreEqual(offlineOrgDate.Date, offlineOrg.AuditUpdatedOn.Value.Date);
            log.Verify(x => x.Add(expectedLogMessage));
        }
    }
}
