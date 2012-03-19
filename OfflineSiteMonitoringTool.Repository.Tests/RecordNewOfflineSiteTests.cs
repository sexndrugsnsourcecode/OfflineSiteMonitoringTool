﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OfflineSiteMonitoringTool.DataAccessLayer;

namespace OfflineSiteMonitoringTool.Repository.Tests
{
    [TestClass]
    public class RecordNewOfflineSiteTests
    {
        private IReportingEntities mockReportingEntity;

        [TestInitialize]
        public void TestInitialize()
        {
            mockReportingEntity = new ReportingEntitiesMock();
        }

        [TestMethod]
        public void RecordNewOfflineSite_RecordsNewOfflineSite()
        {
            string orgId = "11111";
            string healthboard = "testHealthboard";
            string supplier = "testSupplier";
            //string expectedLogMessage = "Offline organisation recorded: " + org;

            // Add details to tbEPS_Organisation
            mockReportingEntity.tbEPS_Organisation.AddObject(AddDataTo_tbEPSOrganisation.AddRow(orgId, healthboard, supplier));

            Repository repository = new Repository(mockReportingEntity);

            repository.RecordNewOfflineSite(orgId);

            var offlineOrg = (from x in mockReportingEntity.tbRPT_OfflineSites
                              select x).FirstOrDefault();

            Assert.AreEqual(orgId, offlineOrg.Org);
            Assert.AreEqual(healthboard, offlineOrg.Healthboard);
            Assert.AreEqual(supplier, offlineOrg.Supplier);
            Assert.AreEqual(DateTime.Now.Date, offlineOrg.AuditCreatedOn.Date);
            //log.Verify(x => x.Add(expectedLogMessage));
        }

        [TestMethod]
        public void RecordNewOfflineSite_SpecifiedSiteAlreadyRecordedAsOffline_LogsError_DoesNotRecordSiteAgain()
        {
            string siteAlreadyRecordedAsOfflineOrgId = "11111";
            string healthboard = "testHealthboard";
            string supplier = "testSupplier";
            //string expectedLogMessage = "Offline organisation recorded: " + org;

            // Add details to tbEPS_Organisation
            mockReportingEntity.tbEPS_Organisation.AddObject(AddDataTo_tbEPSOrganisation.AddRow(siteAlreadyRecordedAsOfflineOrgId, healthboard, supplier));

            // Add details of site already recorded as offline to offline sites table
            mockReportingEntity.tbRPT_OfflineSites.AddObject(AddDataTo_tbRPTOfflineSites.AddRow(siteAlreadyRecordedAsOfflineOrgId, DateTime.Now));

            Repository repository = new Repository(mockReportingEntity);

            repository.RecordNewOfflineSite(siteAlreadyRecordedAsOfflineOrgId);

            var offlineSiteCount = (from x in mockReportingEntity.tbRPT_OfflineSites
                                    select x).Count();

            Assert.AreEqual(1, offlineSiteCount);
            // TODO: Verify log message
            //log.Verify(x => x.Add(expectedLogMessage));
        }

        [TestMethod]
        public void RecordNewOfflineSite_SpecifiedSiteDoesNotExistInOrgTable_LogsError_DoesNotRecordSite()
        {
            string offlineSiteOrgId = "1111";

            Repository repository = new Repository(mockReportingEntity);

            repository.RecordNewOfflineSite(offlineSiteOrgId);

            var offlineSiteCount = (from x in mockReportingEntity.tbRPT_OfflineSites
                                    select x).Count();

            Assert.AreEqual(0, offlineSiteCount);
            // TODO: Check error is logged
        }
    }
}