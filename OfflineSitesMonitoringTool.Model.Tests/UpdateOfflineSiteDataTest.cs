using OfflineSiteMonitoringTool.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace OfflineSitesMonitoringTool.Model.Tests
{
    
    
    /// <summary>
    ///This is a test class for UpdateOfflineSiteDataTest and is intended
    ///to contain all UpdateOfflineSiteDataTest Unit Tests
    ///</summary>
    [TestClass()]
    public class UpdateOfflineSiteDataTest
    {


        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        /// <summary>
        ///A test for RecordNewOfflineSites
        ///</summary>
        [TestMethod()]
        public void RecordNewOfflineSitesTest()
        {
            IRepository repository = null; // TODO: Initialize to an appropriate value
            IList<string> offlineSites = null; // TODO: Initialize to an appropriate value
            UpdateOfflineSiteData target = new UpdateOfflineSiteData(repository, offlineSites); // TODO: Initialize to an appropriate value
            List<string> sitesRecordedAsOffline = null; // TODO: Initialize to an appropriate value
            List<string> offlineSites1 = null; // TODO: Initialize to an appropriate value
            target.RecordNewOfflineSites(sitesRecordedAsOffline, offlineSites1);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for RemoveOnlineSites
        ///</summary>
        [TestMethod()]
        public void RemoveOnlineSitesTest()
        {
            IRepository repository = null; // TODO: Initialize to an appropriate value
            IList<string> offlineSites = null; // TODO: Initialize to an appropriate value
            UpdateOfflineSiteData target = new UpdateOfflineSiteData(repository, offlineSites); // TODO: Initialize to an appropriate value
            List<string> sitesRecordedAsOffline = null; // TODO: Initialize to an appropriate value
            List<string> offlineSites1 = null; // TODO: Initialize to an appropriate value
            target.RemoveOnlineSites(sitesRecordedAsOffline, offlineSites1);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for UpdateSitesAlreadyRecordedAsOffline
        ///</summary>
        [TestMethod()]
        public void UpdateSitesAlreadyRecordedAsOfflineTest()
        {
            IRepository repository = null; // TODO: Initialize to an appropriate value
            IList<string> offlineSites = null; // TODO: Initialize to an appropriate value
            UpdateOfflineSiteData target = new UpdateOfflineSiteData(repository, offlineSites); // TODO: Initialize to an appropriate value
            List<string> sitesRecordedAsOffline = null; // TODO: Initialize to an appropriate value
            List<string> offlineSites1 = null; // TODO: Initialize to an appropriate value
            target.UpdateSitesAlreadyRecordedAsOffline(sitesRecordedAsOffline, offlineSites1);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }
    }
}
