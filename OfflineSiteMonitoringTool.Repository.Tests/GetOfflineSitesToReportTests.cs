using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OfflineSiteMonitoringTool.DataAccessLayer;
using OfflineSiteMonitoringTool.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace OfflineSiteMonitoringTool.Repository.Tests
{
    [TestClass]
    public class GetOfflineSitesToReportTests
    {
        private IReportingEntities mockReportingEntity;
        private IConfigHelper configHelper;
        private IRepository repository;
        private Mock<ILogger> log;

        [TestInitialize]
        public void TestInitialize()
        {
            mockReportingEntity = new ReportingEntitiesMock();
            log = new Mock<ILogger>();
            repository = new Repository(mockReportingEntity, configHelper, log.Object);
        }

        [TestMethod]
        public void GetOfflineSitesToReport_NoOfflineSitesInTable_NoHealthboardsExceededLimit_ReturnsEmptyList()
        {
            // No need to add any data to offline sites table

            List<string> healthboardsThatHaveExceededOfflineSiteLimit = new List<string>();

            List<SiteDetails> offlineSitesToReport;

            offlineSitesToReport = repository.GetOfflineSitesToReport(healthboardsThatHaveExceededOfflineSiteLimit);

            Assert.AreEqual(0, offlineSitesToReport.Count);
        }

        [TestMethod]
        public void GetOfflineSitesToReport_OnlyOfflineSitesToBeReportedExist_NoHealthboardsExceededLimit_ReturnsAllOfflineSitesToBeReported()
        {
            // Details of offline sites to be reported
            string orgId1 = "orgId1";
            string orgId2 = "orgId2";
            string orgId3 = "orgId3";
            DateTime auditCreatedOn = DateTime.Now;
            string supplier = "supplier";
            string healthboard = "healthboard";
            DateTime? dateOfflineNotificationSent = null;

            // Add details of offline sites to be reported to offline sites table
            mockReportingEntity.tbRPT_OfflineSites.AddObject(AddDataTo_tbRPT_OfflineSites.AddRow(orgId1, auditCreatedOn, supplier, healthboard, dateOfflineNotificationSent));
            mockReportingEntity.tbRPT_OfflineSites.AddObject(AddDataTo_tbRPT_OfflineSites.AddRow(orgId2, auditCreatedOn, supplier, healthboard, dateOfflineNotificationSent));
            mockReportingEntity.tbRPT_OfflineSites.AddObject(AddDataTo_tbRPT_OfflineSites.AddRow(orgId3, auditCreatedOn, supplier, healthboard, dateOfflineNotificationSent));

            // Add details to Organisation table
            string orgName1 = "Org Name 1";
            string orgName2 = "Org Name 2";
            string orgName3 = "Org Name 3";
            string supplierReference1 = "Supplier Ref 1";
            string supplierReference2 = "Supplier Ref 2";
            string supplierReference3 = "Supplier Ref 3";

            // Add details to organisation table
            mockReportingEntity.tbEPS_Organisation.AddObject(AddDataTo_tbEPSOrganisation.AddRow(orgId1, healthboard, supplier, orgName1, supplierReference1));
            mockReportingEntity.tbEPS_Organisation.AddObject(AddDataTo_tbEPSOrganisation.AddRow(orgId2, healthboard, supplier, orgName2, supplierReference2));
            mockReportingEntity.tbEPS_Organisation.AddObject(AddDataTo_tbEPSOrganisation.AddRow(orgId3, healthboard, supplier, orgName3, supplierReference3));

            DateTime lastMsgDate = DateTime.Now.AddDays(-3);

            // Add details to orgSupplier table
            mockReportingEntity.tbRPT_OrgSupplier.AddObject(AddDataTo_tbRPT_OrgSupplier.AddRow(orgId1, lastMsgDate));
            mockReportingEntity.tbRPT_OrgSupplier.AddObject(AddDataTo_tbRPT_OrgSupplier.AddRow(orgId2, lastMsgDate));
            mockReportingEntity.tbRPT_OrgSupplier.AddObject(AddDataTo_tbRPT_OrgSupplier.AddRow(orgId3, lastMsgDate));

            List<string> healthboardsThatHaveExceededOfflineSiteLimit = new List<string>();

            List<SiteDetails> offlineSitesToReport;

            offlineSitesToReport = repository.GetOfflineSitesToReport(healthboardsThatHaveExceededOfflineSiteLimit);

            Assert.AreEqual(3, offlineSitesToReport.Count);
            int iter = 1;
            foreach (SiteDetails site in offlineSitesToReport)
            {
                Assert.AreEqual("orgId" + iter, site.OrgId);
                Assert.AreEqual("Org Name " + iter, site.OrgName);
                Assert.AreEqual(healthboard, site.Healthboard);
                Assert.AreEqual(supplier, site.Supplier);
                Assert.AreEqual("Supplier Ref " + iter, site.SupplierReference);
                Assert.AreEqual(lastMsgDate.Date, site.LastMessageDate.Value.Date);
                iter++;
            }
        }

        [TestMethod]
        public void GetOfflineSitesToReport_OfflineSitesToBeReportedAndThatHaveAlreadyBeenReportedExist_NoHealthboardsExceededLimit_ReturnsAllOfflineSitesToBeReported()
        {
            // Details of offline sites to be reported
            string orgId1 = "orgId1";
            string orgId2 = "orgId2";
            DateTime auditCreatedOn = DateTime.Now;
            string supplier = "supplier";
            string healthboard = "healthboard";
            DateTime? dateOfflineNotificationSent = null;

            // Details of offline site already reported
            string orgId3 = "orgId3";
            DateTime dateOfflineNotificationSentForSiteAlreadyReported = DateTime.Now.AddDays(-2);

            // Add details of offline sites to be reported to offline sites table
            mockReportingEntity.tbRPT_OfflineSites.AddObject(AddDataTo_tbRPT_OfflineSites.AddRow(orgId1, auditCreatedOn, supplier, healthboard, dateOfflineNotificationSent));
            mockReportingEntity.tbRPT_OfflineSites.AddObject(AddDataTo_tbRPT_OfflineSites.AddRow(orgId2, auditCreatedOn, supplier, healthboard, dateOfflineNotificationSent));
            mockReportingEntity.tbRPT_OfflineSites.AddObject(AddDataTo_tbRPT_OfflineSites.AddRow(orgId3, auditCreatedOn, supplier, healthboard, dateOfflineNotificationSentForSiteAlreadyReported));

            // Add details to Organisation table
            string orgName1 = "Org Name 1";
            string orgName2 = "Org Name 2";
            string orgName3 = "Org Name 3";
            string supplierReference1 = "Supplier Ref 1";
            string supplierReference2 = "Supplier Ref 2";
            string supplierReference3 = "Supplier Ref 3";

            // Add details to organisation table
            mockReportingEntity.tbEPS_Organisation.AddObject(AddDataTo_tbEPSOrganisation.AddRow(orgId1, healthboard, supplier, orgName1, supplierReference1));
            mockReportingEntity.tbEPS_Organisation.AddObject(AddDataTo_tbEPSOrganisation.AddRow(orgId2, healthboard, supplier, orgName2, supplierReference2));
            mockReportingEntity.tbEPS_Organisation.AddObject(AddDataTo_tbEPSOrganisation.AddRow(orgId3, healthboard, supplier, orgName3, supplierReference3));

            DateTime lastMsgDate = DateTime.Now.AddDays(-3);

            // Add details to orgSupplier table
            mockReportingEntity.tbRPT_OrgSupplier.AddObject(AddDataTo_tbRPT_OrgSupplier.AddRow(orgId1, lastMsgDate));
            mockReportingEntity.tbRPT_OrgSupplier.AddObject(AddDataTo_tbRPT_OrgSupplier.AddRow(orgId2, lastMsgDate));
            mockReportingEntity.tbRPT_OrgSupplier.AddObject(AddDataTo_tbRPT_OrgSupplier.AddRow(orgId3, lastMsgDate));

            List<string> healthboardsThatHaveExceededOfflineSiteLimit = new List<string>();

            List<SiteDetails> offlineSitesToReport;

            offlineSitesToReport = repository.GetOfflineSitesToReport(healthboardsThatHaveExceededOfflineSiteLimit);

            Assert.AreEqual(2, offlineSitesToReport.Count);
            int iter = 1;
            foreach (SiteDetails site in offlineSitesToReport)
            {
                Assert.AreEqual("orgId" + iter, site.OrgId);
                Assert.AreEqual("Org Name " + iter, site.OrgName);
                Assert.AreEqual(healthboard, site.Healthboard);
                Assert.AreEqual(supplier, site.Supplier);
                Assert.AreEqual("Supplier Ref " + iter, site.SupplierReference);
                Assert.AreEqual(lastMsgDate.Date, site.LastMessageDate.Value.Date);
                iter++;
            }
        }

        [TestMethod]
        public void GetOfflineSitesToReport_OfflineSitesToBeReportedExist_SingleHealthboardsExceededLimit_NoSitesBelongToHealthboardThatExceededLimit_ReturnsAllOfflineSitesToBeReported()
        {
            // Details of offline sites to be reported
            string orgId1 = "orgId1";
            string orgId2 = "orgId2";
            string orgId3 = "orgId3";
            DateTime auditCreatedOn = DateTime.Now;
            string supplier = "supplier";
            string healthboard = "healthboard";
            DateTime? dateOfflineNotificationSent = null;

            // Add details of offline sites to be reported to offline sites table
            mockReportingEntity.tbRPT_OfflineSites.AddObject(AddDataTo_tbRPT_OfflineSites.AddRow(orgId1, auditCreatedOn, supplier, healthboard, dateOfflineNotificationSent));
            mockReportingEntity.tbRPT_OfflineSites.AddObject(AddDataTo_tbRPT_OfflineSites.AddRow(orgId2, auditCreatedOn, supplier, healthboard, dateOfflineNotificationSent));
            mockReportingEntity.tbRPT_OfflineSites.AddObject(AddDataTo_tbRPT_OfflineSites.AddRow(orgId3, auditCreatedOn, supplier, healthboard, dateOfflineNotificationSent));

            // Add details to Organisation table
            string orgName1 = "Org Name 1";
            string orgName2 = "Org Name 2";
            string orgName3 = "Org Name 3";
            string supplierReference1 = "Supplier Ref 1";
            string supplierReference2 = "Supplier Ref 2";
            string supplierReference3 = "Supplier Ref 3";

            // Add details to organisation table
            mockReportingEntity.tbEPS_Organisation.AddObject(AddDataTo_tbEPSOrganisation.AddRow(orgId1, healthboard, supplier, orgName1, supplierReference1));
            mockReportingEntity.tbEPS_Organisation.AddObject(AddDataTo_tbEPSOrganisation.AddRow(orgId2, healthboard, supplier, orgName2, supplierReference2));
            mockReportingEntity.tbEPS_Organisation.AddObject(AddDataTo_tbEPSOrganisation.AddRow(orgId3, healthboard, supplier, orgName3, supplierReference3));

            DateTime lastMsgDate = DateTime.Now.AddDays(-3);

            // Add details to orgSupplier table
            mockReportingEntity.tbRPT_OrgSupplier.AddObject(AddDataTo_tbRPT_OrgSupplier.AddRow(orgId1, lastMsgDate));
            mockReportingEntity.tbRPT_OrgSupplier.AddObject(AddDataTo_tbRPT_OrgSupplier.AddRow(orgId2, lastMsgDate));
            mockReportingEntity.tbRPT_OrgSupplier.AddObject(AddDataTo_tbRPT_OrgSupplier.AddRow(orgId3, lastMsgDate));

            List<string> healthboardsThatHaveExceededOfflineSiteLimit = new List<string>() { "healthboardExceededOfflineLimit" };

            List<SiteDetails> offlineSitesToReport;

            offlineSitesToReport = repository.GetOfflineSitesToReport(healthboardsThatHaveExceededOfflineSiteLimit);

            Assert.AreEqual(3, offlineSitesToReport.Count);
        }

        [TestMethod]
        public void GetOfflineSitesToReport_OfflineSitesToBeReportedExist_SingleHealthboardExceededLimit_ReturnsSitesToBeReportedNotBelongingToHealthboardThatExceededLimit()
        {
            // Details of offline sites that belong to healthboard that has exceeded limit
            string orgId1 = "orgId1";
            string orgId2 = "orgId2";
            DateTime auditCreatedOn = DateTime.Now;
            string supplier = "supplier";
            string healthboardThatHasExceededLimit = "healthboardThatHasExceededLimit";
            DateTime? dateOfflineNotificationSent = null;

            // Details of offline site to be reported
            string orgId3 = "orgId3";
            string healthboardNotExceededLimit = "healthboardNotExceededLimit";

            // Add details of offline sites to be reported to offline sites table
            mockReportingEntity.tbRPT_OfflineSites.AddObject(AddDataTo_tbRPT_OfflineSites.AddRow(orgId1, auditCreatedOn, supplier, healthboardThatHasExceededLimit, dateOfflineNotificationSent));
            mockReportingEntity.tbRPT_OfflineSites.AddObject(AddDataTo_tbRPT_OfflineSites.AddRow(orgId2, auditCreatedOn, supplier, healthboardThatHasExceededLimit, dateOfflineNotificationSent));
            mockReportingEntity.tbRPT_OfflineSites.AddObject(AddDataTo_tbRPT_OfflineSites.AddRow(orgId3, auditCreatedOn, supplier, healthboardNotExceededLimit, dateOfflineNotificationSent));

            // Add details to Organisation table
            string orgName1 = "Org Name 1";
            string orgName2 = "Org Name 2";
            string orgName3 = "Org Name 3";
            string supplierReference1 = "Supplier Ref 1";
            string supplierReference2 = "Supplier Ref 2";
            string supplierReference3 = "Supplier Ref 3";

            // Add details to organisation table
            mockReportingEntity.tbEPS_Organisation.AddObject(AddDataTo_tbEPSOrganisation.AddRow(orgId1, healthboardThatHasExceededLimit, supplier, orgName1, supplierReference1));
            mockReportingEntity.tbEPS_Organisation.AddObject(AddDataTo_tbEPSOrganisation.AddRow(orgId2, healthboardThatHasExceededLimit, supplier, orgName2, supplierReference2));
            mockReportingEntity.tbEPS_Organisation.AddObject(AddDataTo_tbEPSOrganisation.AddRow(orgId3, healthboardNotExceededLimit, supplier, orgName3, supplierReference3));

            DateTime lastMsgDate = DateTime.Now.AddDays(-3);

            // Add details to orgSupplier table
            mockReportingEntity.tbRPT_OrgSupplier.AddObject(AddDataTo_tbRPT_OrgSupplier.AddRow(orgId1, lastMsgDate));
            mockReportingEntity.tbRPT_OrgSupplier.AddObject(AddDataTo_tbRPT_OrgSupplier.AddRow(orgId2, lastMsgDate));
            mockReportingEntity.tbRPT_OrgSupplier.AddObject(AddDataTo_tbRPT_OrgSupplier.AddRow(orgId3, lastMsgDate));

            List<string> healthboardsThatHaveExceededOfflineSiteLimit = new List<string>() { healthboardThatHasExceededLimit };

            List<SiteDetails> offlineSitesToReport;

            offlineSitesToReport = repository.GetOfflineSitesToReport(healthboardsThatHaveExceededOfflineSiteLimit);

            Assert.AreEqual(1, offlineSitesToReport.Count);
            Assert.AreEqual("orgId3", offlineSitesToReport[0].OrgId);
            Assert.AreEqual("Org Name 3", offlineSitesToReport[0].OrgName);
            Assert.AreEqual(healthboardNotExceededLimit, offlineSitesToReport[0].Healthboard);
            Assert.AreEqual(supplier, offlineSitesToReport[0].Supplier);
            Assert.AreEqual("Supplier Ref 3", offlineSitesToReport[0].SupplierReference);
            Assert.AreEqual(lastMsgDate.Date, offlineSitesToReport[0].LastMessageDate.Value.Date);
        }

        [TestMethod]
        public void GetOfflineSitesToReport_OfflineSitesToBeReportedExist_MultipleHealthboardsExceededLimit_ReturnsSitesToBeReportedNotBelongingToHealthboardsThatExceededLimit()
        {
            // Details of offline sites that belong to first healthboard that has exceeded limit
            string orgId1 = "orgId1";
            string orgId2 = "orgId2";
            string healthboardThatHasExceededLimit1 = "healthboardThatHasExceededLimit1";

            // Details of offline sites that belong to first healthboard that has exceeded limit
            string orgId3 = "orgId3";
            string orgId4 = "orgId4";
            string healthboardThatHasExceededLimit2 = "healthboardThatHasExceededLimit2";

            // Details of offline site to be reported
            string orgId5 = "orgId5";
            string healthboardNotExceededLimit = "healthboardNotExceededLimit";

            // Details all sites share
            DateTime auditCreatedOn = DateTime.Now;
            string supplier = "supplier";
            DateTime? dateOfflineNotificationSent = null;

            // Add details of offline sites to be reported to offline sites table
            mockReportingEntity.tbRPT_OfflineSites.AddObject(AddDataTo_tbRPT_OfflineSites.AddRow(orgId1, auditCreatedOn, supplier, healthboardThatHasExceededLimit1, dateOfflineNotificationSent));
            mockReportingEntity.tbRPT_OfflineSites.AddObject(AddDataTo_tbRPT_OfflineSites.AddRow(orgId2, auditCreatedOn, supplier, healthboardThatHasExceededLimit1, dateOfflineNotificationSent));
            mockReportingEntity.tbRPT_OfflineSites.AddObject(AddDataTo_tbRPT_OfflineSites.AddRow(orgId3, auditCreatedOn, supplier, healthboardThatHasExceededLimit2, dateOfflineNotificationSent));
            mockReportingEntity.tbRPT_OfflineSites.AddObject(AddDataTo_tbRPT_OfflineSites.AddRow(orgId4, auditCreatedOn, supplier, healthboardThatHasExceededLimit2, dateOfflineNotificationSent));
            mockReportingEntity.tbRPT_OfflineSites.AddObject(AddDataTo_tbRPT_OfflineSites.AddRow(orgId5, auditCreatedOn, supplier, healthboardNotExceededLimit, dateOfflineNotificationSent));

            // Add details to Organisation table
            string orgName1 = "Org Name 1";
            string orgName2 = "Org Name 2";
            string orgName3 = "Org Name 3";
            string orgName4 = "Org Name 4";
            string orgName5 = "Org Name 5";
            string supplierReference1 = "Supplier Ref 1";
            string supplierReference2 = "Supplier Ref 2";
            string supplierReference3 = "Supplier Ref 3";
            string supplierReference4 = "Supplier Ref 4";
            string supplierReference5 = "Supplier Ref 5";

            // Add details to organisation table
            mockReportingEntity.tbEPS_Organisation.AddObject(AddDataTo_tbEPSOrganisation.AddRow(orgId1, healthboardThatHasExceededLimit1, supplier, orgName1, supplierReference1));
            mockReportingEntity.tbEPS_Organisation.AddObject(AddDataTo_tbEPSOrganisation.AddRow(orgId2, healthboardThatHasExceededLimit1, supplier, orgName2, supplierReference2));
            mockReportingEntity.tbEPS_Organisation.AddObject(AddDataTo_tbEPSOrganisation.AddRow(orgId3, healthboardThatHasExceededLimit2, supplier, orgName3, supplierReference3));
            mockReportingEntity.tbEPS_Organisation.AddObject(AddDataTo_tbEPSOrganisation.AddRow(orgId4, healthboardThatHasExceededLimit2, supplier, orgName4, supplierReference4));
            mockReportingEntity.tbEPS_Organisation.AddObject(AddDataTo_tbEPSOrganisation.AddRow(orgId5, healthboardNotExceededLimit, supplier, orgName5, supplierReference5));

            DateTime lastMsgDate = DateTime.Now.AddDays(-3);

            // Add details to orgSupplier table
            mockReportingEntity.tbRPT_OrgSupplier.AddObject(AddDataTo_tbRPT_OrgSupplier.AddRow(orgId1, lastMsgDate));
            mockReportingEntity.tbRPT_OrgSupplier.AddObject(AddDataTo_tbRPT_OrgSupplier.AddRow(orgId2, lastMsgDate));
            mockReportingEntity.tbRPT_OrgSupplier.AddObject(AddDataTo_tbRPT_OrgSupplier.AddRow(orgId3, lastMsgDate));
            mockReportingEntity.tbRPT_OrgSupplier.AddObject(AddDataTo_tbRPT_OrgSupplier.AddRow(orgId4, lastMsgDate));
            mockReportingEntity.tbRPT_OrgSupplier.AddObject(AddDataTo_tbRPT_OrgSupplier.AddRow(orgId5, lastMsgDate));

            List<string> healthboardsThatHaveExceededOfflineSiteLimit = new List<string>() { healthboardThatHasExceededLimit1, healthboardThatHasExceededLimit2 };

            List<SiteDetails> offlineSitesToReport;

            offlineSitesToReport = repository.GetOfflineSitesToReport(healthboardsThatHaveExceededOfflineSiteLimit);

            Assert.AreEqual(1, offlineSitesToReport.Count);
            Assert.AreEqual("orgId5", offlineSitesToReport[0].OrgId);
            Assert.AreEqual("Org Name 5", offlineSitesToReport[0].OrgName);
            Assert.AreEqual(healthboardNotExceededLimit, offlineSitesToReport[0].Healthboard);
            Assert.AreEqual(supplier, offlineSitesToReport[0].Supplier);
            Assert.AreEqual("Supplier Ref 5", offlineSitesToReport[0].SupplierReference);
            Assert.AreEqual(lastMsgDate.Date, offlineSitesToReport[0].LastMessageDate.Value.Date);
        }
    }
}
