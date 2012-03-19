﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using OfflineSiteMonitoringTool.DataAccessLayer;
using OfflineSiteMonitoringTool.Model;
using Moq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace OfflineSiteMonitoringTool.Repository.Tests
{
    [TestClass]
    public class GetNumberOfOfflineSitesToBeReportedPerHealthboardLimitTests
    {
        private IReportingEntities mockReportingEntity;
        private Mock<IConfigHelper> mockConfigHelper;
        private IRepository repository;

        [TestInitialize]
        public void TestInitialize()
        {
            mockReportingEntity = new ReportingEntitiesMock();
            mockConfigHelper = new Mock<IConfigHelper>();
            repository = new Repository(mockReportingEntity, mockConfigHelper.Object);
        }

        [TestMethod]
        public void GetNumberOfOfflineSitesToBeReportedPerHealthboardLimit_ReturnsValueFromConfigFile()
        {
            int configOfflineSiteLimit = 30;

            mockConfigHelper.Setup(x => x.GetNumberOfOfflineSitesToBeReportedPerHealthboardLimit()).Returns(configOfflineSiteLimit);

            int result;

            result = repository.GetNumberOfOfflineSitesToBeReportedPerHealthboardLimit();

            Assert.AreEqual(configOfflineSiteLimit, result);
        }

        [TestMethod]
        public void GetNumberOfOfflineSitesToBeReportedPerHealthboardLimit_NoValueIsSpecifiedInConfigFile_ReturnsDefaultValue()
        {
            int defaultOfflineSiteLimit = 15;

            mockConfigHelper.Setup(x => x.GetNumberOfOfflineSitesToBeReportedPerHealthboardLimit()).Throws(new ConfigurationErrorsException());

            int result;

            result = repository.GetNumberOfOfflineSitesToBeReportedPerHealthboardLimit();

            Assert.AreEqual(defaultOfflineSiteLimit, result);
        }
    }
}
