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
    public class GetHealthboardsThatHaveExceededNumberOfOfflineSitesToBeReportedLimitTests
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
        public void GetHealthboardsThatHaveExceededNumberOfOfflineSitesToBeReportedLimit_NoOfflineSitesExist_ReturnsEmptyList()
        {
            // No need to add any offline sites to table

            int offlineSitePerHealthboardLimit = 10;
            List<string> healthboardsThatHaveExceededLimit;

            healthboardsThatHaveExceededLimit = repository.GetHealthboardsThatHaveExceededNumberOfOfflineSitesToBeReportedLimit(offlineSitePerHealthboardLimit);

            Assert.AreEqual(0, healthboardsThatHaveExceededLimit.Count);
        }

        [TestMethod]
        public void GetHealthboardsThatHaveExceededNumberOfOfflineSitesToBeReportedLimit_OfflineSitesExist_LimitHasNotBeenExceeded_ReturnsEmptyList()
        {
            string orgId1 = "1111";
            string orgId2 = "2222";
            string orgId3 = "3333";
            DateTime auditCreatedOn = DateTime.Now;
            string supplier = "testSupplier";
            string healthboard = "testHealthboard";
            DateTime? dateOfflineNotificationSent = null;

            // Add details of offline sites to table
            mockReportingEntity.tbRPT_OfflineSites.AddObject(AddDataTo_tbRPT_OfflineSites.AddRow(orgId1, auditCreatedOn, supplier, healthboard, dateOfflineNotificationSent));
            mockReportingEntity.tbRPT_OfflineSites.AddObject(AddDataTo_tbRPT_OfflineSites.AddRow(orgId2, auditCreatedOn, supplier, healthboard, dateOfflineNotificationSent));
            mockReportingEntity.tbRPT_OfflineSites.AddObject(AddDataTo_tbRPT_OfflineSites.AddRow(orgId3, auditCreatedOn, supplier, healthboard, dateOfflineNotificationSent));

            int offlineSitePerHealthboardLimit = 5;
            List<string> healthboardsThatHaveExceededLimit;

            healthboardsThatHaveExceededLimit = repository.GetHealthboardsThatHaveExceededNumberOfOfflineSitesToBeReportedLimit(offlineSitePerHealthboardLimit);

            Assert.AreEqual(0, healthboardsThatHaveExceededLimit.Count);
        }

        [TestMethod]
        public void GetHealthboardsThatHaveExceededNumberOfOfflineSitesToBeReportedLimit_SingleHealthboardHasExceededLimit_ReturnsCorrectHealthboard()
        {
            string orgId1 = "1111";
            string orgId2 = "2222";
            string orgId3 = "3333";
            string orgId4 = "4444";
            string orgId5 = "5555";
            DateTime auditCreatedOn = DateTime.Now;
            string supplier = "testSupplier";
            string healthboard = "testHealthboard";
            DateTime? dateOfflineNotificationSent = null;

            // Add details of offline sites to table
            mockReportingEntity.tbRPT_OfflineSites.AddObject(AddDataTo_tbRPT_OfflineSites.AddRow(orgId1, auditCreatedOn, supplier, healthboard, dateOfflineNotificationSent));
            mockReportingEntity.tbRPT_OfflineSites.AddObject(AddDataTo_tbRPT_OfflineSites.AddRow(orgId2, auditCreatedOn, supplier, healthboard, dateOfflineNotificationSent));
            mockReportingEntity.tbRPT_OfflineSites.AddObject(AddDataTo_tbRPT_OfflineSites.AddRow(orgId3, auditCreatedOn, supplier, healthboard, dateOfflineNotificationSent));
            mockReportingEntity.tbRPT_OfflineSites.AddObject(AddDataTo_tbRPT_OfflineSites.AddRow(orgId4, auditCreatedOn, supplier, healthboard, dateOfflineNotificationSent));
            mockReportingEntity.tbRPT_OfflineSites.AddObject(AddDataTo_tbRPT_OfflineSites.AddRow(orgId5, auditCreatedOn, supplier, healthboard, dateOfflineNotificationSent));

            int offlineSitePerHealthboardLimit = 3;
            List<string> healthboardsThatHaveExceededLimit;

            healthboardsThatHaveExceededLimit = repository.GetHealthboardsThatHaveExceededNumberOfOfflineSitesToBeReportedLimit(offlineSitePerHealthboardLimit);

            Assert.AreEqual(1, healthboardsThatHaveExceededLimit.Count);
            Assert.AreEqual(healthboard, healthboardsThatHaveExceededLimit[0]);
        }

        [TestMethod]
        public void GetHealthboardsThatHaveExceededNumberOfOfflineSitesToBeReportedLimit_MultipleHealthboardsHaveExceededLimit_ReturnsCorrectHealthboards()
        {
            // These orgs will belong to: testHealthboard1
            string orgId1 = "1111";
            string orgId2 = "2222";
            string healthboard1 = "testHealthboard1";
            // These orgs will belong to: testHealthboard2
            string orgId3 = "3333";
            string orgId4 = "4444";
            string healthboard2 = "testHealthboard2";
            // These orgs will belong to: testHealthboard3
            string orgId5 = "5555";
            string orgId6 = "6666";
            string healthboard3 = "testHealthboard3";

            // All sites will share following details
            DateTime auditCreatedOn = DateTime.Now;
            string supplier = "testSupplier";
            DateTime? dateOfflineNotificationSent = null;

            // Add details of offline sites to table
            mockReportingEntity.tbRPT_OfflineSites.AddObject(AddDataTo_tbRPT_OfflineSites.AddRow(orgId1, auditCreatedOn, supplier, healthboard1, dateOfflineNotificationSent));
            mockReportingEntity.tbRPT_OfflineSites.AddObject(AddDataTo_tbRPT_OfflineSites.AddRow(orgId2, auditCreatedOn, supplier, healthboard1, dateOfflineNotificationSent));
            mockReportingEntity.tbRPT_OfflineSites.AddObject(AddDataTo_tbRPT_OfflineSites.AddRow(orgId3, auditCreatedOn, supplier, healthboard2, dateOfflineNotificationSent));
            mockReportingEntity.tbRPT_OfflineSites.AddObject(AddDataTo_tbRPT_OfflineSites.AddRow(orgId4, auditCreatedOn, supplier, healthboard2, dateOfflineNotificationSent));
            mockReportingEntity.tbRPT_OfflineSites.AddObject(AddDataTo_tbRPT_OfflineSites.AddRow(orgId5, auditCreatedOn, supplier, healthboard3, dateOfflineNotificationSent));
            mockReportingEntity.tbRPT_OfflineSites.AddObject(AddDataTo_tbRPT_OfflineSites.AddRow(orgId6, auditCreatedOn, supplier, healthboard3, dateOfflineNotificationSent));

            int offlineSitePerHealthboardLimit = 1;
            List<string> healthboardsThatHaveExceededLimit;

            healthboardsThatHaveExceededLimit = repository.GetHealthboardsThatHaveExceededNumberOfOfflineSitesToBeReportedLimit(offlineSitePerHealthboardLimit);

            Assert.AreEqual(3, healthboardsThatHaveExceededLimit.Count);
            Assert.AreEqual(healthboard1, healthboardsThatHaveExceededLimit[0]);
            Assert.AreEqual(healthboard2, healthboardsThatHaveExceededLimit[1]);
            Assert.AreEqual(healthboard3, healthboardsThatHaveExceededLimit[2]);
        }

        [TestMethod]
        public void GetHealthboardsThatHaveExceededNumberOfOfflineSitesToBeReportedLimit_OfflineSitesExist_OfflineSiteLimitSetToZero_ReturnsEmptyList()
        {
            string orgId1 = "1111";
            string orgId2 = "2222";
            string orgId3 = "3333";
            string orgId4 = "4444";
            string orgId5 = "5555";
            DateTime auditCreatedOn = DateTime.Now;
            string supplier = "testSupplier";
            string healthboard = "testHealthboard";
            DateTime? dateOfflineNotificationSent = null;

            // Add details of offline sites to table
            mockReportingEntity.tbRPT_OfflineSites.AddObject(AddDataTo_tbRPT_OfflineSites.AddRow(orgId1, auditCreatedOn, supplier, healthboard, dateOfflineNotificationSent));
            mockReportingEntity.tbRPT_OfflineSites.AddObject(AddDataTo_tbRPT_OfflineSites.AddRow(orgId2, auditCreatedOn, supplier, healthboard, dateOfflineNotificationSent));
            mockReportingEntity.tbRPT_OfflineSites.AddObject(AddDataTo_tbRPT_OfflineSites.AddRow(orgId3, auditCreatedOn, supplier, healthboard, dateOfflineNotificationSent));
            mockReportingEntity.tbRPT_OfflineSites.AddObject(AddDataTo_tbRPT_OfflineSites.AddRow(orgId4, auditCreatedOn, supplier, healthboard, dateOfflineNotificationSent));
            mockReportingEntity.tbRPT_OfflineSites.AddObject(AddDataTo_tbRPT_OfflineSites.AddRow(orgId5, auditCreatedOn, supplier, healthboard, dateOfflineNotificationSent));

            int offlineSitePerHealthboardLimit = 0;
            List<string> healthboardsThatHaveExceededLimit;

            healthboardsThatHaveExceededLimit = repository.GetHealthboardsThatHaveExceededNumberOfOfflineSitesToBeReportedLimit(offlineSitePerHealthboardLimit);

            Assert.AreEqual(0, healthboardsThatHaveExceededLimit.Count);
        }
    }
}
