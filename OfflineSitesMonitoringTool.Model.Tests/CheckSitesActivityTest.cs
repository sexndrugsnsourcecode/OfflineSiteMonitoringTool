using OfflineSiteMonitoringTool.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using Moq;

namespace OfflineSitesMonitoringTool.Model.Tests
{
    [TestClass]
    public class CheckSitesActivityTest
    {
        /// <summary>
        ///A test for GetOfflineSites
        ///</summary>
        [TestMethod]
        public void GetOfflineSites_ReturnsListOfOfflineSites()
        {
            Mock<IRepository> repositoryMock = new Mock<IRepository>(MockBehavior.Strict);
            IRepository repository = repositoryMock.Object;

            DateTime lastBusinessDay = new DateTime(2012, 1, 1);
            List<string> sitesToCheckMessagingActivityFor = new List<string>()
            {
                "site1", "site2", "site3", "site4", "site5"
            };
            List<string> offlineSites = new List<string>()
            {
                "site2", "site3", "site4"
            };

            // Setup repository to return a list of offline sites
            repositoryMock.Setup(x => x.GetSitesToCheckMessagingActivityFor()).Returns(sitesToCheckMessagingActivityFor);
            repositoryMock.Setup(x => x.GetOfflineSites(sitesToCheckMessagingActivityFor, lastBusinessDay)).Returns(offlineSites);

            CheckSitesActivity checkSitesActivity = new CheckSitesActivity(repository, lastBusinessDay);

            List<string> result = new List<string>();
            result = checkSitesActivity.GetOfflineSites;

            Assert.AreEqual(offlineSites, result);
        }
    }
}
