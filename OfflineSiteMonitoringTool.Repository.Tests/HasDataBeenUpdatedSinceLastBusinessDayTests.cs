using OfflineSiteMonitoringTool.Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using OfflineSiteMonitoringTool.DataAccessLayer;

namespace OfflineSiteMonitoringTool.Repository.Tests
{
    [TestClass]
    public class HasDataBeenUpdatedSinceLastBusinessDayTests
    {
        private IReportingEntities mockReportingEntity;

        [TestInitialize]
        public void TestInitialize()
        {
            mockReportingEntity = new ReportingEntitiesMock();
        }

        [TestMethod]
        public void HasDataBeenUpdatedSinceLastBusinessDay_NoDataExistsInMsgTable_ReturnsFalse()
        {
            DateTime lastBusinessDay = DateTime.Now.AddDays(-1);

            Repository repository = new Repository(mockReportingEntity);

            bool result;
            result = repository.HasDataBeenUpdatedSinceLastBusinessDay(lastBusinessDay);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void HasDataBeenUpdatedSinceLastBusinessDay_DataInMsgTableNotUpdatedSinceLastBusinessDay_ReturnsFalse()
        {
            DateTime lastBusinessDay = DateTime.Now.AddDays(-1);
            DateTime lastDateTableWasUpdated = DateTime.Now.AddDays(-3);

            mockReportingEntity.tbEPS_Msg.AddObject(AddDataTo_tbEPSMsg.AddRow(lastDateTableWasUpdated));

            Repository repository = new Repository(mockReportingEntity);

            bool result;
            result = repository.HasDataBeenUpdatedSinceLastBusinessDay(lastBusinessDay);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void HasDataBeenUpdatedSinceLastBusinessDay_DataInMsgTableHasBeenUpdatedSinceLastBusinessDay_ReturnsTrue()
        {
            DateTime lastBusinessDay = DateTime.Now.AddDays(-3);
            DateTime lastDateTableWasUpdated = DateTime.Now.AddDays(-1);

            mockReportingEntity.tbEPS_Msg.AddObject(AddDataTo_tbEPSMsg.AddRow(lastDateTableWasUpdated));

            Repository repository = new Repository(mockReportingEntity);

            bool result;
            result = repository.HasDataBeenUpdatedSinceLastBusinessDay(lastBusinessDay);

            Assert.IsTrue(result);
        }
    }
}
