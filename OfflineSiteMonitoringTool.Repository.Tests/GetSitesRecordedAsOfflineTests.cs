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
    public class GetSitesRecordedAsOfflineTests
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
            repository = new Repository(mockReportingEntity, configHelper, log.Object);
        }

        [TestMethod]
        public void GetSitesRecordedAsOffline_NoSitesRecordedAsOfflineInTable_ReturnsEmptyList()
        {
            // No need to add any data to table as we want to test what happens when table is empty

            var result = repository.GetSitesRecordedAsOffline();

            Assert.AreEqual(0, result.Count);
        }

        [TestMethod]
        public void GetSitesRecordedAsOffline_SingleSiteRecordedAsOffline_ReturnsOfflineSite()
        {
            string orgId = "11111";
            DateTime date = DateTime.Now;

            // Add data to mock instance of table
            mockReportingEntity.tbRPT_OfflineSites.AddObject(AddDataTo_tbRPT_OfflineSites.AddRow(orgId, date));

            var result = repository.GetSitesRecordedAsOffline();

            Assert.AreEqual(1, result.Count);
            Assert.AreEqual(orgId, result[0]);
        }

        [TestMethod]
        public void GetSitesRecordedAsOffline_MultipleSitesRecordedAsOffline_ReturnsAllOfflineSites()
        {
            string orgId1 = "11111";
            string orgId2 = "2222";
            string orgId3 = "33333";
            DateTime date = DateTime.Now;

            // Add data to mock instance of table
            mockReportingEntity.tbRPT_OfflineSites.AddObject(AddDataTo_tbRPT_OfflineSites.AddRow(orgId1, date));
            mockReportingEntity.tbRPT_OfflineSites.AddObject(AddDataTo_tbRPT_OfflineSites.AddRow(orgId2, date));
            mockReportingEntity.tbRPT_OfflineSites.AddObject(AddDataTo_tbRPT_OfflineSites.AddRow(orgId3, date));

            var result = repository.GetSitesRecordedAsOffline();

            Assert.AreEqual(3, result.Count);
            Assert.AreEqual(orgId1, result[0]);
            Assert.AreEqual(orgId2, result[1]);
            Assert.AreEqual(orgId3, result[2]);
        }
    }
}
