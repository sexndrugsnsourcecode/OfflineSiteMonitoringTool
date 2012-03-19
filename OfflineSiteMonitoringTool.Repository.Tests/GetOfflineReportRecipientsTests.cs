using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using OfflineSiteMonitoringTool.DataAccessLayer;
using OfflineSiteMonitoringTool.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace OfflineSiteMonitoringTool.Repository.Tests
{
    [TestClass]
    public class GetOfflineReportRecipientsTests
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
        public void GetOfflineReportRecipients_NoHealthboardOrSupplierRecipients_ReturnsEmptyList()
        {
            string healthboard = "testHealthboard";
            string supplier = "testSupplier";

            //string expectedSupplierLogMessage = "WARNING: No contacts could be found in tbRPT_SupplierContacts for: "
            //    + supplier;
            //string expectedHealthboardLogMessage = "WARNING: No contacts could be found in tbRPT_HealthBoardContacts for: "
            //    + healthboard;

            List<MailAddress> result; 
            result = repository.GetOfflineReportRecipients(healthboard, supplier);

            Assert.AreEqual(0, result.Count);
            //log.Verify(x => x.Add(expectedSupplierLogMessage));
            //log.Verify(x => x.Add(expectedHealthboardLogMessage));
        }

        [TestMethod]
        public void GetOfflineReportRecipients_ReturnsHealthboardRecipients()
        {
            string healthboard = "testHealthboard";
            string contactString = "test@test.com";

            string supplier = "test";   // Required only to fufill parameter requirements in this test

            mockReportingEntity.tbRPT_HealthBoardContacts.AddObject(AddDataTo_tbRPT_HealthboardContacts.AddRow(healthboard, contactString));
            mockReportingEntity.tbRPT_HealthBoardContacts.AddObject(AddDataTo_tbRPT_HealthboardContacts.AddRow(healthboard, contactString));

            List<MailAddress> result;
            result = repository.GetOfflineReportRecipients(healthboard, supplier);

            MailAddress expectedReturnAddress = new MailAddress(contactString);

            Assert.AreEqual(2, result.Count);
            Assert.AreEqual(expectedReturnAddress, result[0]);
            Assert.AreEqual(expectedReturnAddress, result[1]);
        }

        [TestMethod]
        public void GetOfflineReportRecipients_ReturnsSupplierRecipients()
        {
            string supplier = "testSupplier";
            string contactString = "test@test.com";

            string healthboard = "test";   // Required only to fufill parameter requirements in this test

            mockReportingEntity.tbRPT_SupplierContacts.AddObject(AddDataTo_tbRPT_SupplierContacts.AddRow(supplier, contactString));
            mockReportingEntity.tbRPT_SupplierContacts.AddObject(AddDataTo_tbRPT_SupplierContacts.AddRow(supplier, contactString));

            List<MailAddress> result;
            result = repository.GetOfflineReportRecipients(healthboard, supplier);

            MailAddress expectedReturnAddress = new MailAddress(contactString);

            Assert.AreEqual(2, result.Count);
            Assert.AreEqual(expectedReturnAddress, result[0]);
            Assert.AreEqual(expectedReturnAddress, result[1]);
        }

        [TestMethod]
        public void GetOfflineReportRecipients_ReturnsHealthboardAndSupplierRecipients()
        {
            string healthboard = "testHealthboard";
            string healthboardContactString = "health@board.com";

            string supplier = "testSupplier";
            string supplierContactString = "test@supplier.com";

            mockReportingEntity.tbRPT_HealthBoardContacts.AddObject(AddDataTo_tbRPT_HealthboardContacts.AddRow(healthboard, healthboardContactString));
            mockReportingEntity.tbRPT_SupplierContacts.AddObject(AddDataTo_tbRPT_SupplierContacts.AddRow(supplier, supplierContactString));

            List<MailAddress> result;
            result = repository.GetOfflineReportRecipients(healthboard, supplier);

            Assert.AreEqual(2, result.Count);
        }
    }
}
