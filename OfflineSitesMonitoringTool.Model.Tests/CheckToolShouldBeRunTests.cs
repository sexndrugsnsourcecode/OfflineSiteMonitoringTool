using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using OfflineSiteMonitoringTool.Model;

namespace OfflineSitesMonitoringTool.Model.Tests
{
    /// <summary>
    /// Summary description for CheckToolShouldBeRunTests
    /// </summary>
    [TestClass]
    public class CheckToolShouldBeRunTests
    {
        [TestMethod]
        public void ShouldToolBeRun_ReportingDataHasBeenUpdatedSinceLastBusinessDay_ReturnsTrue()
        {
            Mock<IRepository> repositoryMock = new Mock<IRepository>(MockBehavior.Strict);
            IRepository repository = repositoryMock.Object;
            DateTime lastBusinessDay = new DateTime();

            // Setup mock to look like reporting data has been updated
            repositoryMock.Setup(x => x.HasDataBeenUpdatedSinceLastBusinessDay(lastBusinessDay)).Returns(true);

            CheckToolShouldBeRun checkToolShouldBeRun = new CheckToolShouldBeRun(repository, lastBusinessDay);

            bool result;
            result = checkToolShouldBeRun.ShouldToolBeRun;

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void ShouldToolBeRun_ReportingDataHasNotBeenUpdatedSinceLastBusinessDay_ReturnsFalse()
        {
            Mock<IRepository> repositoryMock = new Mock<IRepository>(MockBehavior.Strict);
            IRepository repository = repositoryMock.Object;
            DateTime lastBusinessDay = new DateTime();

            // Setup mock to look like reporting data has not been updated
            repositoryMock.Setup(x => x.HasDataBeenUpdatedSinceLastBusinessDay(lastBusinessDay)).Returns(false);

            CheckToolShouldBeRun checkToolShouldBeRun = new CheckToolShouldBeRun(repository, lastBusinessDay);

            bool result;
            result = checkToolShouldBeRun.ShouldToolBeRun;

            Assert.IsFalse(result);
        }
    }
}
