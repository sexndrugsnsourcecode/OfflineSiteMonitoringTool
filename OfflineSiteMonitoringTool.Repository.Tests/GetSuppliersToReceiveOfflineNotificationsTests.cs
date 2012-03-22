using System;
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
    public class GetSuppliersToReceiveOfflineNotificationsTests
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
        public void GetSuppliersToReceiveOfflineNotifications_NoSuppliersSetupToReceiveNotifications_ReturnsEmptyList()
        {
            // No need to add any data to tbRPT_OfflineSites_SuppliersToReceiveNotifications as we want to test how the app
            // works when there are no suppliers to return.

            List<string> suppliersToReceiveNotifications;

            suppliersToReceiveNotifications = repository.GetSuppliersToReceiveOfflineNotifications();

            Assert.AreEqual(0, suppliersToReceiveNotifications.Count);
        }

        [TestMethod]
        public void GetSuppliersToReceiveOfflineNotifications_SingleSupplierSetupToReceiveNotifications_ReturnsCorrectSupplier()
        {
            string supplier = "testSupplier";

            mockReportingEntity.tbRPT_OfflineSites_SuppliersToReceiveNotifications
                .AddObject(AddDataTo_tbRPTOfflineSites_SuppliersToReceiveNotifications.AddRow(supplier));

            List<string> suppliersToReceiveNotifications;

            suppliersToReceiveNotifications = repository.GetSuppliersToReceiveOfflineNotifications();

            Assert.AreEqual(1, suppliersToReceiveNotifications.Count);
            Assert.AreEqual(supplier, suppliersToReceiveNotifications[0]);
        }

        [TestMethod]
        public void GetSuppliersToReceiveOfflineNotifications_MultipleSuppliersSetupToReceiveNotifications_ReturnsCorrectSuppliers()
        {
            string supplier1 = "testSupplier1";
            string supplier2 = "testSupplier2";
            string supplier3 = "testSupplier3";

            mockReportingEntity.tbRPT_OfflineSites_SuppliersToReceiveNotifications
                .AddObject(AddDataTo_tbRPTOfflineSites_SuppliersToReceiveNotifications.AddRow(supplier1));
            mockReportingEntity.tbRPT_OfflineSites_SuppliersToReceiveNotifications
                .AddObject(AddDataTo_tbRPTOfflineSites_SuppliersToReceiveNotifications.AddRow(supplier2));
            mockReportingEntity.tbRPT_OfflineSites_SuppliersToReceiveNotifications
                .AddObject(AddDataTo_tbRPTOfflineSites_SuppliersToReceiveNotifications.AddRow(supplier3));

            List<string> suppliersToReceiveNotifications;

            suppliersToReceiveNotifications = repository.GetSuppliersToReceiveOfflineNotifications();

            Assert.AreEqual(3, suppliersToReceiveNotifications.Count);
            Assert.AreEqual(supplier1, suppliersToReceiveNotifications[0]);
            Assert.AreEqual(supplier2, suppliersToReceiveNotifications[1]);
            Assert.AreEqual(supplier3, suppliersToReceiveNotifications[2]);
        }
    }
}
