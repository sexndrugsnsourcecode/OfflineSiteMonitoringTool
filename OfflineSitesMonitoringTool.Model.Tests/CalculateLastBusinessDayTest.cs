using OfflineSiteMonitoringTool.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Moq;

namespace OfflineSitesMonitoringTool.Model.Tests
{
    [TestClass]
    public class CalculateLastBusinessDayTest
    {
        /// <summary>
        ///A test for GetLastBusinessDay
        ///</summary>
        [TestMethod]
        public void GetLastBusinessDay_ReturnsLastBusinessDay()
        {
            Mock<IRepository> repositoryMock = new Mock<IRepository>(MockBehavior.Strict);
            IRepository repository = repositoryMock.Object;
            DateTime currentDate = new DateTime(2012, 1, 2);
            DateTime lastBusinessDay = new DateTime(2012, 1, 1);

            // Setup repository to return lastBusinessDay
            repositoryMock.Setup(x => x.GetLastBusinessDay(currentDate)).Returns(lastBusinessDay);

            CalculateLastBusinessDay calculateLastBusinessDay = new CalculateLastBusinessDay(repository, currentDate);

            DateTime result;
            result = calculateLastBusinessDay.GetLastBusinessDay;

            Assert.AreEqual(lastBusinessDay, result);
        }
    }
}
