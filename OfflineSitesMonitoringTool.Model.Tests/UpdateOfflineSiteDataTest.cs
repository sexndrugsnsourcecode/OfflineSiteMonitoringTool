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
        /// <summary>
        ///A test for RecordNewOfflineSites
        ///</summary>
        [TestMethod]
        public void RecordNewOfflineSitesTest()
        {
            
        }

        /// <summary>
        ///A test for RemoveOnlineSites
        ///</summary>
        [TestMethod]
        public void RemoveOnlineSites_NoOnlineSites_NothingRemoved()
        {
            List<string> offlineSites = new List<string>()
            {
                "offlineSite1", "offlineSite2", "offlineSite3"
            };
            List<string> sitesRecordedAsOffline = new List<string>()
            {
                "offlineSite1", "offlineSite2", "offlineSite3"
            };

            UpdateOfflineSiteData updateOfflineSiteData = new UpdateOfflineSiteData(repository);

            updateOfflineSiteData.RemoveOnlineSites(sitesRecordedAsOffline, offlineSites);

            repositoryMock.Verify(x => x.RemoveOnlineSite(It.IsAny<string>()), Times.Never());
        }

        [TestMethod]
        public void RemoveOnlineSites_OnlineSitesRemoved()
        {
            string onlineSite1 = "onlineSite1";
            string onlineSite2 = "onlineSite2";

            List<string> offlineSites = new List<string>()
            {
                "offlineSite1", "offlineSite2", "offlineSite3"
            };
            List<string> sitesRecordedAsOffline = new List<string>()
            {
                "offlineSite1", "offlineSite2", "offlineSite3", onlineSite1, onlineSite2
            };

            // Need to declare these setup methods or strictMock will throw exception
            repositoryMock.Setup(x => x.RemoveOnlineSite(onlineSite1));
            repositoryMock.Setup(x => x.RemoveOnlineSite(onlineSite2));

            UpdateOfflineSiteData updateOfflineSiteData = new UpdateOfflineSiteData(repository);

            updateOfflineSiteData.RemoveOnlineSites(sitesRecordedAsOffline, offlineSites);

            repositoryMock.Verify(x => x.RemoveOnlineSite(onlineSite1), Times.Exactly(1));
            repositoryMock.Verify(x => x.RemoveOnlineSite(onlineSite2), Times.Exactly(1));
        }

        /// <summary>
        ///A test for UpdateSitesAlreadyRecordedAsOffline
        ///</summary>
        [TestMethod]
        public void UpdateSitesAlreadyRecordedAsOfflineTest()
        {
            IRepository repository = null; // TODO: Initialize to an appropriate value
            IList<string> offlineSites = null; // TODO: Initialize to an appropriate value
            UpdateOfflineSiteData target = new UpdateOfflineSiteData(repository); // TODO: Initialize to an appropriate value
            List<string> sitesRecordedAsOffline = null; // TODO: Initialize to an appropriate value
            List<string> offlineSites1 = null; // TODO: Initialize to an appropriate value
            target.UpdateSitesAlreadyRecordedAsOffline(sitesRecordedAsOffline, offlineSites1);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }
    }
}
