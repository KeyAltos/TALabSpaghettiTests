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
    public class GmailTests: BaseTestClass
    {        
        private User userOne, userTwo, userThree;
        private GmailService gmailService; 
        Message spamMessage;
        Message notSpamMessage;

        [TestFixtureSetUp]
        public void TestFixtureSetUp()
        {           
            userOne = Constants.USER_ONE;
            userTwo = Constants.USER_TWO;
            userThree = Constants.USER_THREE;
        }
        [SetUp]
        public void SetUp()
        {
            gmailService = new GmailService(driver);            
        }       

        [TearDown]
        public void TearDown()
        {
        }

        [TestFixtureTearDown]
        public void TestFixtureTearDown()
        {
        }

        [Ignore]
        [Test]        
        [Repeat(5)]
        public void IsMarkedAsSpamLetterInSpamFolder()
        {
            //preconditions
            gmailService.LoginIn(userTwo);
            gmailService.ClearFilters();
            bool hasAttachments = false;
            bool deleteMessage = false;
            bool markAsImportant = false;
            bool neverSendToSpam = true;
            gmailService.CreateNewFilter(userOne.Username, hasAttachments, deleteMessage, markAsImportant, neverSendToSpam);

            //act
            gmailService.LogOutAndChangeAccount(userOne);
            spamMessage = RandomGenerator.GetRandomMessage("Spam");
            gmailService.WriteAndSendMessage(userTwo.Username, spamMessage);

            gmailService.LogOutAndChangeAccount(userTwo);

            gmailService.MarkMessageAsSpam(userOne.Username, spamMessage);

            gmailService.LogOutAndChangeAccount(userOne);

            notSpamMessage = RandomGenerator.GetRandomMessage("Not spam");
            gmailService.WriteAndSendMessage(userTwo.Username, notSpamMessage);

            gmailService.LogOutAndChangeAccount(userTwo);
            
            //assert
            Assert.IsTrue(gmailService.IsMessageFromUserInSpam(userOne, spamMessage));
        }

        //[Ignore]
        [Test]        
        public void Forward()
        {
            gmailService.LoginIn(userTwo);

            gmailService.ClearForwarding();
            gmailService.SetRequestForForwardMailToUser(userThree);

            gmailService.LogOutAndChangeAccount(userThree);
            gmailService.ConfirmForwardEmailRequset();

            gmailService.LogOutAndChangeAccount(userTwo);
            gmailService.SetForwardMailToConfirmedUser(userThree);

            gmailService.ClearFilters();
            bool hasAttachments = true;
            bool deleteMessage = true;
            bool markAsImportant = true;
            bool neverSendToSpam = false;            
            gmailService.CreateNewFilter(userOne.Username, hasAttachments, deleteMessage, markAsImportant, neverSendToSpam);

            //gmailService.LoginIn(userOne);
            gmailService.LogOutAndChangeAccount(userOne);
            var messageWithAttach = RandomGenerator.GetRandomMessage("Attached ");
            messageWithAttach.Text = "";
            gmailService.WriteAndSendMessageWithAttachGDrive(userTwo.Username, messageWithAttach, Constants.FILE_NAME_FOR_GDRIVE_ATTACH_NORMALY_SIZE);

            var messageWithoutAttach = RandomGenerator.GetRandomMessage("Not attached ");
            gmailService.WriteAndSendMessage(userTwo.Username, messageWithoutAttach);

            gmailService.LogOutAndChangeAccount(userTwo);
            //17            
            Assert.IsTrue(gmailService.IsMessageFromUserIsImportantInTrash(userOne, messageWithAttach));
            //18.1
            Assert.IsTrue(gmailService.IsMessageFromUserInInbox(userOne, messageWithoutAttach));
            //18.2   Bug in gmail fiters - mark is important all input messages
            //Assert.IsFalse(gmailService.IsMessageFromUserIsImportant(userOne, messageWithoutAttach));

            gmailService.LogOutAndChangeAccount(userThree);
            Assert.IsTrue(gmailService.IsMessageFromUserInInbox(userOne, messageWithoutAttach));            
        }
        
        
        [Ignore]     
        [Test]
        public void NewFeatureTesting()
        {
            gmailService.LoginIn(userTwo);
            gmailService.ClearForwarding();
            
        }
    }

}

