﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Mail;
using System.Text;
using OfflineSiteMonitoringTool.DataAccessLayer;
using OfflineSiteMonitoringTool.Model;
using Moq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace OfflineSiteMonitoringTool.Repository.Tests
{
    [TestClass]
    public class GetOfflineReportFromAddressTests
    {
        private IReportingEntities mockReportingEntity;
        private Mock<IConfigHelper> mockConfigHelper;
        private IRepository repository;
        private Mock<ILogger> log;

        [TestInitialize]
        public void TestInitialize()
        {
            mockReportingEntity = new ReportingEntitiesMock();
            mockConfigHelper = new Mock<IConfigHelper>();
            log = new Mock<ILogger>();
            repository = new Repository(mockReportingEntity, mockConfigHelper.Object, log.Object);
        }

        [TestMethod]
        public void GetOfflineReportFromAddress_ReturnsValueFromConfigFile()
        {
            string fromAddressString = "test@test.com";

            mockConfigHelper.Setup(x => x.GetFromAddress()).Returns(fromAddressString);

            MailAddress expectedReturnFromAddress = new MailAddress(fromAddressString);

            MailAddress result;
            result = repository.GetOfflineReportFromAddress();

            Assert.AreEqual(expectedReturnFromAddress, result);
        }
    }
}
