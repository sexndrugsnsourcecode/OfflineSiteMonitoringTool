using OfflineSiteMonitoringTool.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Net.Mail;
using Moq;

namespace OfflineSitesMonitoringTool.Model.Tests
{
    [TestClass]
    public class SendOfflineNotificationsTest
    {
        Mock<IRepository> repositoryMock;
        IRepository repository;
        Mock<ISMTPClientWrapper> smtpClientMock;
        ISMTPClientWrapper smtpClient;

        [TestInitialize]
        public void TestInitialize()
        {
            repositoryMock = new Mock<IRepository>(MockBehavior.Strict);
            repository = repositoryMock.Object;

            smtpClientMock = new Mock<ISMTPClientWrapper>(MockBehavior.Strict);
            smtpClient = smtpClientMock.Object;
        }

        [TestMethod]
        public void CreateOfflineReports_NoSitesToBeReportedOffline_ReturnsNoOfflineReports()
        {
            MailAddress testAddress = new MailAddress("test@test.com");
            List<MailAddress> emptyAddressList = new List<MailAddress>();

            // Setup repository methods we want to check if they were called
            repositoryMock.Setup(x => x.GetOfflineReportFromAddress()).Returns(testAddress);
            repositoryMock.Setup(x => x.GetOfflineReportReplyToAddress()).Returns(testAddress);
            repositoryMock.Setup(x => x.GetOfflineReportRecipients("healthboard", "supplier")).Returns(emptyAddressList);
            
            SendOfflineNotifications sendOfflineNotifications = new SendOfflineNotifications(repository, smtpClient);

            List<SiteDetails> sitesToBeReportedOffline = new List<SiteDetails>();

            Dictionary<string, MailMessage> result;
            result = sendOfflineNotifications.CreateOfflineReports(sitesToBeReportedOffline);

            Assert.AreEqual(0, result.Count);
            repositoryMock.Verify(x => x.GetOfflineReportFromAddress(), Times.Never());
            repositoryMock.Verify(x => x.GetOfflineReportReplyToAddress(), Times.Never());
            repositoryMock.Verify(x => x.GetOfflineReportRecipients("healthboard", "supplier"), Times.Never());
        }

        [TestMethod]
        public void CreateOfflineReports_SingleEmisSiteToBeReported_ReturnsEmisReport()
        {
            string orgId = "1111";
            string orgName = "testName";
            string healthboard = "testHealthboard";
            string supplier = "EMIS";
            string supplierReference = "testRef";
            DateTime lastMessageDate = DateTime.Now.AddDays(-7);
            SiteDetails emisSiteToBeReportedOffline = new SiteDetails(orgId, orgName, healthboard, supplier, supplierReference, lastMessageDate);
            List<SiteDetails> sitesToBeReportedOffline = new List<SiteDetails>();
            sitesToBeReportedOffline.Add(emisSiteToBeReportedOffline);

            MailAddress fromAddress = new MailAddress("from@test.com");
            MailAddress replyToAddress = new MailAddress("replyTo@test.com");
            MailAddress emisAddress = new MailAddress("test@emis.com");
            MailAddress healthboardAddress = new MailAddress("test@healthboard.com");
            List<MailAddress> receipients = new List<MailAddress>();
            receipients.Add(emisAddress);
            receipients.Add(healthboardAddress);

            // Setup repository to return expected email addresses
            repositoryMock.Setup(x => x.GetOfflineReportFromAddress()).Returns(fromAddress);
            repositoryMock.Setup(x => x.GetOfflineReportReplyToAddress()).Returns(replyToAddress);
            repositoryMock.Setup(x => x.GetOfflineReportRecipients(healthboard, supplier)).Returns(receipients);

            string expectedEmailSubject = "Transmission Fault in ePharmacy CDB testRef (OrgID 1111)";
            string expectedLastMessageDateString = lastMessageDate.ToString();
            string expectedEmailBody = "Transmission Fault in ePharmacy\n\n"
                + "This site (testName, CDB testRef, OrgID 1111) is reported as being offline. The last AMS message was received on "
                + expectedLastMessageDateString
                + "\n Please arrange for this to be investigated and brought back online as soon as possible.";

            SendOfflineNotifications sendOfflineNotifications = new SendOfflineNotifications(repository, smtpClient);

            Dictionary<string, MailMessage> result;
            result = sendOfflineNotifications.CreateOfflineReports(sitesToBeReportedOffline);

            Assert.AreEqual(1, result.Count);
            MailMessage offlineReport = result[orgId];
            Assert.AreEqual(fromAddress, offlineReport.From);
            Assert.AreEqual(replyToAddress, offlineReport.ReplyTo);
            Assert.AreEqual(receipients[0], offlineReport.To[0]);
            Assert.AreEqual(receipients[1], offlineReport.To[1]);
            Assert.AreEqual(expectedEmailSubject, offlineReport.Subject);
            Assert.AreEqual(expectedEmailBody, offlineReport.Body);
        }

        [TestMethod]
        public void CreateOfflineReports_SingleInpsSiteToBeReported_ReturnsInpsReport()
        {
            string orgId = "1111";
            string orgName = "testName";
            string healthboard = "testHealthboard";
            string supplier = "INPS";
            string supplierReference = "testRef";
            DateTime lastMessageDate = DateTime.Now.AddDays(-7);
            SiteDetails emisSiteToBeReportedOffline = new SiteDetails(orgId, orgName, healthboard, supplier, supplierReference, lastMessageDate);
            List<SiteDetails> sitesToBeReportedOffline = new List<SiteDetails>();
            sitesToBeReportedOffline.Add(emisSiteToBeReportedOffline);

            MailAddress fromAddress = new MailAddress("from@test.com");
            MailAddress replyToAddress = new MailAddress("replyTo@test.com");
            MailAddress emisAddress = new MailAddress("test@inps.com");
            MailAddress healthboardAddress = new MailAddress("test@healthboard.com");
            List<MailAddress> receipients = new List<MailAddress>();
            receipients.Add(emisAddress);
            receipients.Add(healthboardAddress);

            // Setup repository to return expected email addresses
            repositoryMock.Setup(x => x.GetOfflineReportFromAddress()).Returns(fromAddress);
            repositoryMock.Setup(x => x.GetOfflineReportReplyToAddress()).Returns(replyToAddress);
            repositoryMock.Setup(x => x.GetOfflineReportRecipients(healthboard, supplier)).Returns(receipients);

            string expectedEmailSubject = "eAMS Offline - INPS Reference testRef (OrgID 1111)";
            string expectedLastMessageDateString = lastMessageDate.ToString();
            string expectedEmailBody = "eAMS Offline\n\n"
                + "This site (testName, INPS Reference testRef, OrgID 1111) is reported as being offline. The last AMS message was received on "
                + expectedLastMessageDateString
                + "\n Please arrange for this to be investigated and brought back online as soon as possible.";

            SendOfflineNotifications sendOfflineNotifications = new SendOfflineNotifications(repository, smtpClient);

            Dictionary<string, MailMessage> result;
            result = sendOfflineNotifications.CreateOfflineReports(sitesToBeReportedOffline);

            Assert.AreEqual(1, result.Count);
            MailMessage offlineReport = result[orgId];
            Assert.AreEqual(fromAddress, offlineReport.From);
            Assert.AreEqual(replyToAddress, offlineReport.ReplyTo);
            Assert.AreEqual(receipients[0], offlineReport.To[0]);
            Assert.AreEqual(receipients[1], offlineReport.To[1]);
            Assert.AreEqual(expectedEmailSubject, offlineReport.Subject);
            Assert.AreEqual(expectedEmailBody, offlineReport.Body);
        }

        [TestMethod]
        public void CreateOfflineReports_MultipleOfflineSitesToBeReported_ReturnsExpectedOfflineReports()
        {
            string orgId1 = "1111";
            string orgName1 = "testName1";
            string healthboard1 = "testHealthboard";
            string supplier1 = "EMIS";
            string supplierReference1 = "emisRef";

            string orgId2 = "2222";
            string orgName2 = "testName2";
            string healthboard2 = "testHealthboard2";
            string supplier2 = "INPS";
            string supplierReference2 = "inpsRef";

            DateTime lastMessageDate = DateTime.Now.AddDays(-7);

            SiteDetails siteToBeReportedOffline1 = new SiteDetails(orgId1, orgName1, healthboard1, supplier1, supplierReference1, lastMessageDate);
            SiteDetails siteToBeReportedOffline2 = new SiteDetails(orgId2, orgName2, healthboard2, supplier2, supplierReference2, lastMessageDate);
            List<SiteDetails> sitesToBeReportedOffline = new List<SiteDetails>();
            sitesToBeReportedOffline.Add(siteToBeReportedOffline1);
            sitesToBeReportedOffline.Add(siteToBeReportedOffline2);

            MailAddress fromAddress = new MailAddress("from@test.com");
            MailAddress replyToAddress = new MailAddress("replyTo@test.com");
            MailAddress supplierAddress = new MailAddress("test@inps.com");
            MailAddress healthboardAddress = new MailAddress("test@healthboard.com");
            List<MailAddress> receipients = new List<MailAddress>();
            receipients.Add(supplierAddress);
            receipients.Add(healthboardAddress);

            // Setup repository to return expected email addresses
            repositoryMock.Setup(x => x.GetOfflineReportFromAddress()).Returns(fromAddress);
            repositoryMock.Setup(x => x.GetOfflineReportReplyToAddress()).Returns(replyToAddress);
            repositoryMock.Setup(x => x.GetOfflineReportRecipients(It.IsAny<string>(), It.IsAny<string>())).Returns(receipients);

            string expectedLastMessageDateString = lastMessageDate.ToString();

            string expectedEmisEmailSubject = "Transmission Fault in ePharmacy CDB emisRef (OrgID 1111)";
            string expectedEmisEmailBody = "Transmission Fault in ePharmacy\n\n"
                + "This site (testName1, CDB emisRef, OrgID 1111) is reported as being offline. The last AMS message was received on "
                + expectedLastMessageDateString
                + "\n Please arrange for this to be investigated and brought back online as soon as possible.";

            string expectedInpsEmailSubject = "eAMS Offline - INPS Reference inpsRef (OrgID 2222)";
            string expectedInpsEmailBody = "eAMS Offline\n\n"
                + "This site (testName2, INPS Reference inpsRef, OrgID 2222) is reported as being offline. The last AMS message was received on "
                + expectedLastMessageDateString
                + "\n Please arrange for this to be investigated and brought back online as soon as possible.";

            SendOfflineNotifications sendOfflineNotifications = new SendOfflineNotifications(repository, smtpClient);

            Dictionary<string, MailMessage> result;
            result = sendOfflineNotifications.CreateOfflineReports(sitesToBeReportedOffline);

            Assert.AreEqual(2, result.Count);
            MailMessage offlineReport1 = result[orgId1];
            Assert.AreEqual(fromAddress, offlineReport1.From);
            Assert.AreEqual(replyToAddress, offlineReport1.ReplyTo);
            Assert.AreEqual(receipients[0], offlineReport1.To[0]);
            Assert.AreEqual(receipients[1], offlineReport1.To[1]);
            Assert.AreEqual(expectedEmisEmailSubject, offlineReport1.Subject);
            Assert.AreEqual(expectedEmisEmailBody, offlineReport1.Body);
            MailMessage offlineReport2 = result[orgId2];
            Assert.AreEqual(fromAddress, offlineReport2.From);
            Assert.AreEqual(replyToAddress, offlineReport2.ReplyTo);
            Assert.AreEqual(receipients[0], offlineReport2.To[0]);
            Assert.AreEqual(receipients[1], offlineReport2.To[1]);
            Assert.AreEqual(expectedInpsEmailSubject, offlineReport2.Subject);
            Assert.AreEqual(expectedInpsEmailBody, offlineReport2.Body);
        }

        [TestMethod]
        public void SendOfflineReports_NoReportsToSend_DoesntAttemptToSendAnyReports()
        {
            Dictionary<string, MailMessage> offlineReports = new Dictionary<string, MailMessage>();

            // Setup repository so that we can verify methods were not called later
            smtpClientMock.Setup(x => x.Send(It.IsAny<MailMessage>()));
            repositoryMock.Setup(x => x.RecordOfflineNotificationHasBeenSentForSite(It.IsAny<string>()));

            SendOfflineNotifications sendOfflineNotifications = new SendOfflineNotifications(repository, smtpClient);

            sendOfflineNotifications.SendOfflineReports(offlineReports);

            smtpClientMock.Verify(x => x.Send(It.IsAny<MailMessage>()), Times.Never());
            repositoryMock.Verify(x => x.RecordOfflineNotificationHasBeenSentForSite(It.IsAny<string>()), Times.Never());
        }

        [TestMethod]
        public void SendOfflineReports_SingleOfflineReportToSend_SendsReport()
        {
            string orgId = "1111";
            MailMessage offlineReport = new MailMessage();

            Dictionary<string, MailMessage> offlineReports = new Dictionary<string, MailMessage>();
            offlineReports.Add(orgId, offlineReport);

            // Setup repository so that we can verify methods were not called later
            smtpClientMock.Setup(x => x.Send(offlineReport));
            repositoryMock.Setup(x => x.RecordOfflineNotificationHasBeenSentForSite(orgId));

            SendOfflineNotifications sendOfflineNotifications = new SendOfflineNotifications(repository, smtpClient);

            sendOfflineNotifications.SendOfflineReports(offlineReports);

            smtpClientMock.Verify(x => x.Send(offlineReport), Times.Once());
            repositoryMock.Verify(x => x.RecordOfflineNotificationHasBeenSentForSite(orgId), Times.Once());
        }

        [TestMethod]
        public void SendOfflineReports_MultipleOfflineReportsToSend_SendsReports()
        {
            string orgId1 = "1111";
            MailMessage offlineReport1 = new MailMessage();
            string orgId2 = "2222";
            MailMessage offlineReport2 = new MailMessage();

            Dictionary<string, MailMessage> offlineReports = new Dictionary<string, MailMessage>();
            offlineReports.Add(orgId1, offlineReport1);
            offlineReports.Add(orgId2, offlineReport2);

            // Setup repository so that we can verify methods were not called later
            smtpClientMock.Setup(x => x.Send(It.IsAny<MailMessage>()));
            repositoryMock.Setup(x => x.RecordOfflineNotificationHasBeenSentForSite(It.IsAny<string>()));

            SendOfflineNotifications sendOfflineNotifications = new SendOfflineNotifications(repository, smtpClient);

            sendOfflineNotifications.SendOfflineReports(offlineReports);

            smtpClientMock.Verify(x => x.Send(It.IsAny<MailMessage>()), Times.Exactly(2));
            repositoryMock.Verify(x => x.RecordOfflineNotificationHasBeenSentForSite(It.IsAny<string>()), Times.Exactly(2));
        }
    }
}
