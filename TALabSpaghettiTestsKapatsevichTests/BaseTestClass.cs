using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TALabSpaghettiTestsKapatsevich;
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
            Debug.WriteLine("BaseTestClass: FixtureSetUp");
            browserForTesting = Constants.browserForTesting;
            driverFactory = new WebDriverFactory(browserForTesting);
        }

        [SetUp]
        public void BaseSetUp()
        {
            Debug.WriteLine("BaseTestClass: SetUp");
            driver = driverFactory.GetDriver();
        }

        [TearDown]
        public void BaseTearDown()
        {
            Debug.WriteLine("BaseTestClass: TearDown");
            if (TestContext.CurrentContext.Result.State==TestState.Error)
            {
                Debug.WriteLine("BaseTestClass: TearDown: Making screenshot");
                driver.MakeScreenshot();
            }
            //driverFactory.CloseDriver();
        }

        [TestFixtureTearDown]
        public void BaseTestFixtureTearDown()
        {
            Debug.WriteLine("BaseTestClass: FixtureTearDown");            
        }


    }
}
