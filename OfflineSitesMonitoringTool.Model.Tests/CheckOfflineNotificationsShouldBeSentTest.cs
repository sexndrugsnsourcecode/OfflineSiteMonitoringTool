using OfflineSiteMonitoringTool.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using Moq;

namespace OfflineSitesMonitoringTool.Model.Tests
{
    [TestClass]
    public class CheckOfflineNotificationsShouldBeSentTest
    {
        Mock<IRepository> repositoryMock;
        IRepository repository;

        [TestInitialize]
        public void TestInitialize()
        {
            repositoryMock = new Mock<IRepository>();
            repository = repositoryMock.Object;
        }

        [TestMethod]
        public void GetSuppliersToReceiveOfflineNotifications_NoSuppliersSetupToReceiveNotifications_ReturnsEmptyList()
        {
            List<string> suppliersToReceiveNotifications = new List<string>();

            // Setup repository to return no suppliers to receive offline notifications
            repositoryMock.Setup(x => x.GetSuppliersToReceiveOfflineNotifications()).Returns(suppliersToReceiveNotifications);

            CheckOfflineNotificationsShouldBeSent checkOfflineNotificationsShouldBeSent = new CheckOfflineNotificationsShouldBeSent(repository);

            List<string> result;
            result = checkOfflineNotificationsShouldBeSent.GetSuppliersToReceiveOfflineNotifications;

            Assert.AreEqual(0, result.Count);
        }

        [TestMethod]
        public void GetSuppliersToReceiveOfflineNotifications_ReturnsSuppliersSetupToReceiveNotifications()
        {
            List<string> suppliersToReceiveNotifications = new List<string>()
            {
                "supplier1", "supplier2", "supplier3"
            };

            // Setup repository to return no suppliers to receive offline notifications
            repositoryMock.Setup(x => x.GetSuppliersToReceiveOfflineNotifications()).Returns(suppliersToReceiveNotifications);

            CheckOfflineNotificationsShouldBeSent checkOfflineNotificationsShouldBeSent = new CheckOfflineNotificationsShouldBeSent(repository);

            List<string> result;
            result = checkOfflineNotificationsShouldBeSent.GetSuppliersToReceiveOfflineNotifications;

            Assert.AreEqual(3, result.Count);
            Assert.AreEqual(suppliersToReceiveNotifications, result);
        }

        [TestMethod]
        public void AreThereAnySuppliersToReceiveOfflineNotifications_NoSuppliersSetupToReceiveNotifications_ReturnsFalse()
        {
            List<string> suppliersToReceiveNotifications = new List<string>();

            // Setup repository to return no suppliers to receive offline notifications
            repositoryMock.Setup(x => x.GetSuppliersToReceiveOfflineNotifications()).Returns(suppliersToReceiveNotifications);

            CheckOfflineNotificationsShouldBeSent checkOfflineNotificationsShouldBeSent = new CheckOfflineNotificationsShouldBeSent(repository);

            bool result;
            result = checkOfflineNotificationsShouldBeSent.AreThereAnySuppliersToReceiveOfflineNotifications;

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void AreThereAnySuppliersToReceiveOfflineNotifications_SuppliersToReceiveNotificationsExist_ReturnsTrue()
        {
            List<string> suppliersToReceiveNotifications = new List<string>()
            {
                "supplier1", "supplier2", "supplier3"
            };

            // Setup repository to return no suppliers to receive offline notifications
            repositoryMock.Setup(x => x.GetSuppliersToReceiveOfflineNotifications()).Returns(suppliersToReceiveNotifications);

            CheckOfflineNotificationsShouldBeSent checkOfflineNotificationsShouldBeSent = new CheckOfflineNotificationsShouldBeSent(repository);

            bool result;
            result = checkOfflineNotificationsShouldBeSent.AreThereAnySuppliersToReceiveOfflineNotifications;

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void GetOfflineSitesToBeReported_NoSitesToBeReported_ReturnsEmptyList()
        {
            List<string> offlineSitesToBeReported = new List<string>();

            // Setup repository to return no offline sites to be reported
            repositoryMock.Setup(x => x.GetOfflineSitesToReport()).Returns(offlineSitesToBeReported);

            CheckOfflineNotificationsShouldBeSent checkOfflineNotificationsShouldBeSent = new CheckOfflineNotificationsShouldBeSent(repository);

            List<string> result;
            result = checkOfflineNotificationsShouldBeSent.GetOfflineSitesToBeReported;

            Assert.AreEqual(0, result.Count);
        }

        [TestMethod]
        public void GetOfflineSitesToBeReported_ReturnsSitesToBeReportedOffline()
        {
            List<string> offlineSitesToBeReported = new List<string>()
            {
                "offlineSite1", "offlineSite2", "offlineSite3"
            };

            // Setup repository to return no offline sites to be reported
            repositoryMock.Setup(x => x.GetOfflineSitesToReport()).Returns(offlineSitesToBeReported);

            CheckOfflineNotificationsShouldBeSent checkOfflineNotificationsShouldBeSent = new CheckOfflineNotificationsShouldBeSent(repository);

            List<string> result;
            result = checkOfflineNotificationsShouldBeSent.GetOfflineSitesToBeReported;

            Assert.AreEqual(3, result.Count);
            Assert.AreEqual(offlineSitesToBeReported, result);
        }
    }
}
