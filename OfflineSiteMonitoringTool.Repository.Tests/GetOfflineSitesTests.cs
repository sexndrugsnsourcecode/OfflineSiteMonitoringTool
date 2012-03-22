using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OfflineSiteMonitoringTool.DataAccessLayer;
using OfflineSiteMonitoringTool.Model;
using Moq;

namespace OfflineSiteMonitoringTool.Repository.Tests
{
    [TestClass]
    public class GetOfflineSitesTests
    {
        private IReportingEntities mockReportingEntity;
        private IConfigHelper configHelper;
        private IRepository repository;
        private Mock<ILogger> log;

        [TestInitialize]
        public void TestInitialize()
        {
            mockReportingEntity = new ReportingEntitiesMock();
            configHelper = new ConfigHelper();
            log = new Mock<ILogger>();
            repository = new Repository(mockReportingEntity, configHelper, log.Object);
        }

        [TestMethod]
        public void GetOfflineSites_SingleSiteOnline_ReturnEmptyList()
        {
            string org = "1111";
            DateTime msgDate = DateTime.Now;    // Message added date is more recent than the cut off date passed to GetOfflineSites
            byte msgTypeRid = Convert.ToByte(Model.Message.AMSPrescriptionRequestMsgTypeRid);

            // Add data to mock instance of database table
            mockReportingEntity.tbEPS_Msg.AddObject(AddDataTo_tbEPSMsg.AddRow(msgDate, msgTypeRid, org));

            // Data to be passed to method under test
            List<string> orgs = new List<string>();
            orgs.Add(org);
            DateTime reportDate = DateTime.Now.AddDays(-1);

            var result = repository.GetOfflineSites(orgs, reportDate);

            Assert.AreEqual(0, result.Count);
        }

        [TestMethod]
        public void GetOfflineSites_SingleSiteOffline_ReturnOfflineSite()
        {
            string orgIdRaw = "1111      ";     // Using this to replicate data returned from database
            DateTime msgDate = DateTime.Now.AddDays(-1);    // Message added date is older than the cut off date passed to GetOfflineSites
            byte msgTypeRid = Convert.ToByte(Model.Message.AMSPrescriptionRequestMsgTypeRid);

            // Add data to mock instance of database table
            mockReportingEntity.tbEPS_Msg.AddObject(AddDataTo_tbEPSMsg.AddRow(msgDate, msgTypeRid, orgIdRaw));

            // Data to be passed to method under test
            string orgId = "1111";
            List<string> orgs = new List<string>();
            orgs.Add(orgId);
            DateTime reportDate = DateTime.Now;

            var result = repository.GetOfflineSites(orgs, reportDate);

            Assert.AreEqual(1, result.Count);
            Assert.AreEqual(orgId, result[0]);
            Assert.AreEqual(orgId.Length, result[0].Length);
        }

        [TestMethod]
        public void GetOfflineSites_OnlineAndOfflineSitesExist_ReturnOfflineSitesInCorrectOrder()
        {
            // Setup two online sites
            string orgId1 = "1111";
            string orgId2 = "2222";
            DateTime onlineSiteMsgDate = DateTime.Now;    // Message added date is more recent than the cut off date passed to GetOfflineSites
            // Setup two offline sites
            string orgId3 = "3333";
            string orgId4 = "4444";
            DateTime offlineSiteMsgDate = DateTime.Now.AddDays(-5); // Message added date is older than the cut off date passed to GetOfflineSites

            byte msgTypeRid = Convert.ToByte(Model.Message.AMSPrescriptionRequestMsgTypeRid);

            // Add data to mock instance of database table
            mockReportingEntity.tbEPS_Msg.AddObject(AddDataTo_tbEPSMsg.AddRow(onlineSiteMsgDate, msgTypeRid, orgId1));
            mockReportingEntity.tbEPS_Msg.AddObject(AddDataTo_tbEPSMsg.AddRow(onlineSiteMsgDate, msgTypeRid, orgId2));
            mockReportingEntity.tbEPS_Msg.AddObject(AddDataTo_tbEPSMsg.AddRow(offlineSiteMsgDate, msgTypeRid, orgId4)); // Adding org 3 before 4 to test ordering
            mockReportingEntity.tbEPS_Msg.AddObject(AddDataTo_tbEPSMsg.AddRow(offlineSiteMsgDate, msgTypeRid, orgId3));

            List<string> sitesToCheckMessagingActivityFor = new List<string>() { orgId1, orgId2, orgId4, orgId3 };

            DateTime reportDate = DateTime.Now.AddDays(-3);

            List<string> results = new List<string>();
            results = repository.GetOfflineSites(sitesToCheckMessagingActivityFor, reportDate);

            Assert.AreEqual(2, results.Count);
            Assert.AreEqual(orgId3, results[0]);
            Assert.AreEqual(orgId4, results[1]);
        }
    }
}
