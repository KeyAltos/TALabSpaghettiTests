using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TALabSpaghettiTestsKapatsevich.TestsConstants;
using TALabSpaghettiTestsKapatsevich.WebDriverFactory;

namespace TALabSpaghettiTestsKapatsevichTests
{
    public class BaseTestClass
    {
        protected IWebDriver driver;
        protected IWebDriverFactory driverFactory;
        protected WebBrowsers browserForTesting;

        [TestFixtureSetUp]
        public void BaseTestFixtureSetUp()
        {
            browserForTesting = Constants.browserForTesting;
            driverFactory = new WebDriverFactory(browserForTesting);
        }

        [SetUp]
        public void BaseSetUp()
        {
            driver = driverFactory.GetDriver();
        }

        [TearDown]
        public void BaseTearDown()
        {
            //driverFactory.CloseDriver();
        }

        [TestFixtureTearDown]
        public void BaseTestFixtureTearDown()
        {
            //WebDriverSingletone.DisposeDriver();
        }


    }
}
