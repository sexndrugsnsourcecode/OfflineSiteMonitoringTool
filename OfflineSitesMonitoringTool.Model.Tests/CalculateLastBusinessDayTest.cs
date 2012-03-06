using OfflineSiteMonitoringTool.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace OfflineSitesMonitoringTool.Model.Tests
{
    
    
    /// <summary>
    ///This is a test class for CalculateLastBusinessDayTest and is intended
    ///to contain all CalculateLastBusinessDayTest Unit Tests
    ///</summary>
    [TestClass()]
    public class CalculateLastBusinessDayTest
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
        ///A test for GetLastBusinessDay
        ///</summary>
        [TestMethod()]
        public void GetLastBusinessDayTest()
        {
            IRepository repository = null; // TODO: Initialize to an appropriate value
            DateTime currentDate = new DateTime(); // TODO: Initialize to an appropriate value
            CalculateLastBusinessDay target = new CalculateLastBusinessDay(repository, currentDate); // TODO: Initialize to an appropriate value
            DateTime actual;
            actual = target.GetLastBusinessDay;
            Assert.Inconclusive("Verify the correctness of this test method.");
        }
    }
}
