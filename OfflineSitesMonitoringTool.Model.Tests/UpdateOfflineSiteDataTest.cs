using OfflineSiteMonitoringTool.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using Moq;

namespace OfflineSitesMonitoringTool.Model.Tests
{
    [TestClass]
    public class UpdateOfflineSiteDataTest
    {
        Mock<IRepository> repositoryMock;
        IRepository repository;

        [TestInitialize]
        public void TestInitialize()
        {
            repositoryMock = new Mock<IRepository>(MockBehavior.Strict);
            repository = repositoryMock.Object;
        }
        
        [TestMethod]
        public void GetSitesRecordedAsOffline_ReturnsSitesRecordedAsOffline()
        {
            List<string> sitesRecordedAsOffline = AddOfflineSites(3);

            repositoryMock.Setup(x => x.GetSitesRecordedAsOffline()).Returns(sitesRecordedAsOffline);

            UpdateOfflineSiteData updateOfflineSiteData = new UpdateOfflineSiteData(repository);

            List<string> result = new List<string>();
            result = updateOfflineSiteData.GetSitesRecordedAsOffline();

            Assert.AreEqual(sitesRecordedAsOffline, result);
        }
        
        [TestMethod]
        public void RemoveOnlineSites_NoOnlineSites_NothingRemoved()
        {
            List<string> offlineSites = AddOfflineSites(3);
            List<string> sitesRecordedAsOffline = AddOfflineSites(3);

            UpdateOfflineSiteData updateOfflineSiteData = new UpdateOfflineSiteData(repository);

            updateOfflineSiteData.RemoveOnlineSites(sitesRecordedAsOffline, offlineSites);

            repositoryMock.Verify(x => x.RemoveOnlineSite(It.IsAny<string>()), Times.Never());
        }

        [TestMethod]
        public void RemoveOnlineSites_OnlineSitesRemoved()
        {
            string onlineSite1 = "onlineSite1";
            string onlineSite2 = "onlineSite2";

            List<string> offlineSites = AddOfflineSites(3);
            List<string> sitesRecordedAsOffline = AddOfflineSites(3);
            sitesRecordedAsOffline.AddRange(AddOnlineSites(2));

            // Need to declare these setup methods or strictMock will throw exception
            repositoryMock.Setup(x => x.RemoveOnlineSite(onlineSite1));
            repositoryMock.Setup(x => x.RemoveOnlineSite(onlineSite2));

            UpdateOfflineSiteData updateOfflineSiteData = new UpdateOfflineSiteData(repository);

            updateOfflineSiteData.RemoveOnlineSites(sitesRecordedAsOffline, offlineSites);

            repositoryMock.Verify(x => x.RemoveOnlineSite(onlineSite1), Times.Once());
            repositoryMock.Verify(x => x.RemoveOnlineSite(onlineSite2), Times.Once());
        }

        [TestMethod]
        public void UpdateSitesAlreadyRecordedAsOffline_NoSitesRecordedAsOffline_NoSitesUpdated()
        {
            List<string> offlineSites = AddOfflineSites(3);
            List<string> sitesRecordedAsOffline = new List<string>();

            UpdateOfflineSiteData updateOfflineSiteData = new UpdateOfflineSiteData(repository);

            updateOfflineSiteData.UpdateSitesAlreadyRecordedAsOffline(sitesRecordedAsOffline, offlineSites);

            repositoryMock.Verify(x => x.UpdateSiteAlreadyRecordedAsOffline(It.IsAny<string>()), Times.Never());
        }

        [TestMethod]
        public void UpdateSitesAlreadyRecordedAsOffline_UpdatesOfflineSites()
        {
            string offlineSite1 = "offlineSite1";
            string offlineSite2 = "offlineSite2";
            string offlineSite3 = "offlineSite3";

            List<string> offlineSites = AddOfflineSites(3);
            List<string> sitesRecordedAsOffline = AddOfflineSites(3);

            // Need to declare these setup methods or strictMock will throw exception
            repositoryMock.Setup(x => x.UpdateSiteAlreadyRecordedAsOffline(offlineSite1));
            repositoryMock.Setup(x => x.UpdateSiteAlreadyRecordedAsOffline(offlineSite2));
            repositoryMock.Setup(x => x.UpdateSiteAlreadyRecordedAsOffline(offlineSite3));

            UpdateOfflineSiteData updateOfflineSiteData = new UpdateOfflineSiteData(repository);

            updateOfflineSiteData.UpdateSitesAlreadyRecordedAsOffline(sitesRecordedAsOffline, offlineSites);

            repositoryMock.Verify(x => x.UpdateSiteAlreadyRecordedAsOffline(offlineSite1), Times.Once());
            repositoryMock.Verify(x => x.UpdateSiteAlreadyRecordedAsOffline(offlineSite2), Times.Once());
            repositoryMock.Verify(x => x.UpdateSiteAlreadyRecordedAsOffline(offlineSite3), Times.Once());
        }

        private List<string> AddOfflineSites(int numberOfOfflineSites)
        {
            List<string> offlineSites = new List<string>();

            for (int x = 1; x <= numberOfOfflineSites; x++)
                offlineSites.Add("offlineSite" + x);

            return offlineSites;
        }

        private List<string> AddOnlineSites(int numberOfOnlineSites)
        {
            List<string> onlineSites = new List<string>();

            for (int x = 1; x <= numberOfOnlineSites; x++)
                onlineSites.Add("onlineSite" + x);

            return onlineSites;
        }
    }
}
