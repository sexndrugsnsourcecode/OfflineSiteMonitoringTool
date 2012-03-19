using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OfflineSiteMonitoringTool.DataAccessLayer;
using OfflineSiteMonitoringTool.Model;

namespace OfflineSiteMonitoringTool.Repository.Tests
{
    [TestClass]
    public class RemoveOnlineSiteTests
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
        public void RemoveOnlineSite_SingleOnlineSiteOnlySiteInTable_RemovesOnlineSite_TableEmpty()
        {
            string orgId = "1111";
            DateTime date = DateTime.Now;
            //string expectedLogMessage = "Organisation has come back online: " + orgId;

            mockReportingEntity.tbRPT_OfflineSites.AddObject(AddDataTo_tbRPTOfflineSites.AddRow(orgId, date));

            repository.RemoveOnlineSite(orgId);

            int numRowsInTable = (from x in mockReportingEntity.tbRPT_OfflineSites
                                  select x).Count();

            Assert.AreEqual(0, numRowsInTable);
            //log.Verify(x => x.Add(expectedLogMessage));
        }

        [TestMethod]
        public void RemoveOnlineSite_MultipleSitesInTable_SingleOnlineSite_RemoveOnlineSiteOnly()
        {
            string onlineSite = "1111";
            string offlineSite = "2222";
            DateTime date = DateTime.Now;
            //string expectedLogMessage = "Organisation has come back online: " + org1;

            mockReportingEntity.tbRPT_OfflineSites.AddObject(AddDataTo_tbRPTOfflineSites.AddRow(onlineSite, date));
            mockReportingEntity.tbRPT_OfflineSites.AddObject(AddDataTo_tbRPTOfflineSites.AddRow(offlineSite, date));

            repository.RemoveOnlineSite(onlineSite);

            int numRowsInTable = (from x in mockReportingEntity.tbRPT_OfflineSites
                                  select x).Count();

            Assert.AreEqual(1, numRowsInTable);
            //log.Verify(x => x.Add(expectedLogMessage));

            string siteStillRecordedAsOffline = (from x in mockReportingEntity.tbRPT_OfflineSites
                                      select x.Org).FirstOrDefault();

            Assert.AreEqual(offlineSite, siteStillRecordedAsOffline);
        }

        [TestMethod]
        public void RemoveOnlineSite_SpecifiedOnlineSiteNotRecordedInOfflineSitesTable_RemovesNothing_LogsError()
        {
            string siteThatDoesntExistInTable = "1111";
            string offlineSite = "2222";
            DateTime date = DateTime.Now;
            mockReportingEntity.tbRPT_OfflineSites.AddObject(AddDataTo_tbRPTOfflineSites.AddRow(offlineSite, date));
            //string expectedLogMessage = "Tool tried to remove an organisation from tbRPT_OfflineSites that " +
            //                    "doesn't exist. Organisation: " + org;

            repository.RemoveOnlineSite(siteThatDoesntExistInTable);

            int numRowsInTable = (from x in mockReportingEntity.tbRPT_OfflineSites
                                  select x).Count();

            string siteStillRecordedAsOffline = (from x in mockReportingEntity.tbRPT_OfflineSites
                                      select x.Org).FirstOrDefault();

            Assert.AreEqual(1, numRowsInTable);
            Assert.AreEqual(offlineSite, siteStillRecordedAsOffline);
            // log.Verify(x => x.Add(expectedLogMessage));
        }
    }
}
