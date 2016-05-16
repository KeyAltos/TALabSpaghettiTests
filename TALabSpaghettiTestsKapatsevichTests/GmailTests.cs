using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TALabSpaghettiTestsKapatsevich.WebDriverFactory;
using TALabSpaghettiTestsKapatsevich.Pages.Google;
using TALabSpaghettiTestsKapatsevich.TestsConstants;
using OpenQA.Selenium.Support.UI;
using TALabSpaghettiTestsKapatsevich.GmailActions;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using System.Threading;
using OpenQA.Selenium.Chrome;
using TALabSpaghettiTestsKapatsevich.Utilities;

namespace TALabSpaghettiTestsKapatsevichTests
{

    //[TestFixture(typeof(FirefoxDriver))]
    //[TestFixture(typeof(InternetExplorerDriver))]
    //[TestFixture(typeof(ChromeDriver))]
    //public class TestWithMultipleBrowsers<TWebDriver> where TWebDriver : IWebDriver, new()
    //{
    //    private IWebDriver driver;

    //    [SetUp]
    //    public void CreateDriver()
    //    {
    //        this.driver = new TWebDriver();
    //    }

    //    [TearDown]
    //    public void TearDown()
    //    {
    //        driver.Close();
    //    }

    //    [Test]
    //    public void GoogleTest()
    //    {
    //        driver.Navigate().GoToUrl("http://www.google.com/");
    //        IWebElement query = driver.FindElement(By.Name("q"));
    //        query.SendKeys("Bread" + Keys.Enter);            
    //    }
    //}
    

    [TestFixture]
    public class GmailTests
    {
        private IWebDriver driver;
        private User userOne, userTwo, userThree;
        private GmailService gmailService;
        //private IWebDriverFactorySetter driverFactory;
        private IWebDriverFactory driverFactory;
        private WebBrowsers browserForTesting;
        Message spamMessage;
        Message notSpamMessage;

        [TestFixtureSetUp]
        public void TestFixtureSetUp()
        {           
            userOne = Constants.USER_ONE;
            userTwo = Constants.USER_TWO;
            userThree = Constants.USER_THREE;
            browserForTesting = Constants.browserForTesting;
            

            //configure browser for testing(singletone - new test method - new driver)
            driverFactory = new WebDriverFactory(browserForTesting);


            //configure browser for testing(singletone - one solution - one driver)
            //driverFactory = new WebDriverFactory();
            //driverFactory.SetDriver(browserForTesting);
            //driver = WebDriverSingletone.GetDriver();

            //gmailService = new GmailService(driver);
        }
        [SetUp]
        public void SetUp()
        {
            driver = driverFactory.GetDriver();
            gmailService = new GmailService(driver);            
        }       

        [TearDown]
        public void TearDown()
        {
            //driverFactory.CloseDriver();
        }

        [TestFixtureTearDown]
        public void TestFixtureTearDown()
        {
            //WebDriverSingletone.DisposeDriver();
        }
        [Ignore]
        [Test]        
        public void IsMarkedAsSpamLetterInSpamFolder()
        {

            gmailService.LoginIn(userOne);

            //gmailService.ClearSpam();
            //gmailService.LogOutAndChangeAccount(userOne);

            spamMessage = RandomGenerator.GetRandomMessage("Spammarking");
            gmailService.WriteAndSendMessage(userTwo.Username, spamMessage);

            gmailService.LogOutAndChangeAccount(userTwo);

            gmailService.MarkMessageAsSpam(userOne.Username);

            gmailService.LogOutAndChangeAccount(userOne);

            notSpamMessage = RandomGenerator.GetRandomMessage("Simple");
            gmailService.WriteAndSendMessage(userTwo.Username, notSpamMessage);

            gmailService.LogOutAndChangeAccount(userTwo);

            gmailService.LoginIn(userTwo);
            //assert
            Assert.IsTrue(gmailService.IsMessageFromUserInSpam(userOne, spamMessage));          
            }

        [Test]        
        public void Forward()
        {
            gmailService.LoginIn(userTwo);

            //gmailService.SetRequestForForwardMailToUser(userThree);
            //gmailService.LogOutAndChangeAccount(userThree);
            //gmailService.ConfirmForwardEmailRequset();
            //gmailService.LogOutAndChangeAccount(userTwo);

            //gmailService.SetForwardMailToConfirmedUser(userThree);

            gmailService.CreateNewFilter(userOne.Username, true, true, true);




        }  
    }

}

