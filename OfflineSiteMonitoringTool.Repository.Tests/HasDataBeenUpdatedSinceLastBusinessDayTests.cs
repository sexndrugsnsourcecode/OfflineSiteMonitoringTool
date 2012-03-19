using OfflineSiteMonitoringTool.Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using OfflineSiteMonitoringTool.DataAccessLayer;
using OfflineSiteMonitoringTool.Model;

namespace OfflineSiteMonitoringTool.Repository.Tests
{
    [TestClass]
    public class HasDataBeenUpdatedSinceLastBusinessDayTests
    {
        private IReportingEntities mockReportingEntity;
        private IConfigHelper configHelper;
        private IRepository repository;

        [TestInitialize]
        public void TestInitialize()
        {
            mockReportingEntity = new ReportingEntitiesMock();
            repository = new Repository(mockReportingEntity, configHelper);
        }

        [TestMethod]
        public void HasDataBeenUpdatedSinceLastBusinessDay_NoDataExistsInMsgTable_ReturnsFalse()
        {
            DateTime lastBusinessDay = DateTime.Now.AddDays(-1);

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

            bool result;
            result = repository.HasDataBeenUpdatedSinceLastBusinessDay(lastBusinessDay);

            Assert.IsTrue(result);
        }
    }
}
