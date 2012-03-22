using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OfflineSiteMonitoringTool.DataAccessLayer;
using OfflineSiteMonitoringTool.Model;
using Moq;

namespace OfflineSiteMonitoringTool.Repository.Tests
{
    [TestClass]
    public class GetSitesToCheckMessagingActivityForTests
    {
        private IReportingEntities mockReportingEntity;
        private IConfigHelper configHelper;
        private IRepository repository;
        private Mock<ILogger> log;

        [TestInitialize]
        public void TestInitialize()
        {
            mockReportingEntity = new ReportingEntitiesMock();
            log = new Mock<ILogger>();
            configHelper = new ConfigHelper();
            repository = new Repository(mockReportingEntity, configHelper, log.Object);
        }

        /// <summary>
        /// This test checks that the method doesn't return 'closed' sites (sites who have their endDate set)
        ///</summary>
        [TestMethod]
        public void GetSitesToCheckMessagingActivityFor_IgnoresSitesWithEndDateSet_ReturnsEmptyList()
        {
            string orgId = "1111";
            DateTime orgEndDate = DateTime.Now;

            mockReportingEntity.tbEPS_Organisation.AddObject(AddDataTo_tbEPSOrganisation.AddRow(orgId, orgEndDate));

            List<string> result;
            result = repository.GetSitesToCheckMessagingActivityFor();

            Assert.AreEqual(0, result.Count);
        }

        [TestMethod]
        public void GetSitesToCheckMessagingActivityFor_IgnoresArchivedSites_ReturnsEmptyList()
        {
            string orgId = "1111";
            DateTime? orgEndDate = null;
            bool archived = true;

            mockReportingEntity.tbEPS_Organisation.AddObject(AddDataTo_tbEPSOrganisation.AddRow(orgId, orgEndDate, archived));

            List<string> result;
            result = repository.GetSitesToCheckMessagingActivityFor();

            Assert.AreEqual(0, result.Count);
        }

        [TestMethod]
        public void GetSitesToCheckMessagingActivityFor_IgnoresDispensingSites_ReturnsEmptyList()
        {
            string orgId = "1111";
            DateTime? orgEndDate = null;
            bool archived = false;
            bool dispensing = true;

            mockReportingEntity.tbEPS_Organisation.AddObject(AddDataTo_tbEPSOrganisation.AddRow(orgId, orgEndDate, archived, dispensing));

            List<string> result;
            result = repository.GetSitesToCheckMessagingActivityFor();

            Assert.AreEqual(0, result.Count);
        }

        [TestMethod]
        public void GetSitesToCheckMessagingActivityFor_IgnoresSitesWithExcludeFromOfflineNotificationsFlagSet_ReturnsEmptyList()
        {
            string orgId = "1111";
            DateTime? orgEndDate = null;
            bool archived = false;
            bool dispensing = false;
            bool excludeFromOfflineNotifications = true;

            mockReportingEntity.tbEPS_Organisation.AddObject(AddDataTo_tbEPSOrganisation.AddRow(orgId, orgEndDate, archived, dispensing));
            mockReportingEntity.tbRPT_OrgSupplier.AddObject(AddDataTo_tbRPT_OrgSupplier.AddRow(orgId, excludeFromOfflineNotifications));

            List<string> result;
            result = repository.GetSitesToCheckMessagingActivityFor();

            Assert.AreEqual(0, result.Count);
        }

        [TestMethod]
        public void GetSitesToCheckMessagingActivityFor_EligableSitesExistInTable_ReturnsSitesInCorrectOrder()
        {
            string orgId1 = "1111";
            string orgId2 = "2222";
            string orgId3 = "3333";

            mockReportingEntity.tbEPS_Organisation.AddObject(AddDataTo_tbEPSOrganisation.AddRow(orgId1));
            mockReportingEntity.tbEPS_Organisation.AddObject(AddDataTo_tbEPSOrganisation.AddRow(orgId2));
            mockReportingEntity.tbEPS_Organisation.AddObject(AddDataTo_tbEPSOrganisation.AddRow(orgId3));

            mockReportingEntity.tbRPT_OrgSupplier.AddObject(AddDataTo_tbRPT_OrgSupplier.AddRow(orgId1));
            mockReportingEntity.tbRPT_OrgSupplier.AddObject(AddDataTo_tbRPT_OrgSupplier.AddRow(orgId2));
            mockReportingEntity.tbRPT_OrgSupplier.AddObject(AddDataTo_tbRPT_OrgSupplier.AddRow(orgId3));

            List<string> results;
            results = repository.GetSitesToCheckMessagingActivityFor();

            Assert.AreEqual(3, results.Count);
            Assert.AreEqual(orgId1, results[0]);
            Assert.AreEqual(orgId1.Length, results[0].Length);
            Assert.AreEqual(orgId2, results[1]);
            Assert.AreEqual(orgId2.Length, results[1].Length);
            Assert.AreEqual(orgId3, results[2]);
            Assert.AreEqual(orgId3.Length, results[2].Length);
        }
    }
}
