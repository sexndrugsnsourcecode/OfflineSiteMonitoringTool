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
            Mock<IRepository> repositoryMock = new Mock<IRepository>(MockBehavior.Strict);
            IRepository repository = repositoryMock.Object;

            List<string> offlineSites = new List<string>();
            List<string> sitesRecordedAsOffline = new List<string>();

            UpdateOfflineSiteData updateOfflineSiteData = new UpdateOfflineSiteData(repository);

            updateOfflineSiteData.RemoveOnlineSites(sitesRecordedAsOffline, offlineSites);

            repositoryMock.Verify(x => x.RemoveOnlineSite(It.IsAny<string>()), Times.Never());
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
