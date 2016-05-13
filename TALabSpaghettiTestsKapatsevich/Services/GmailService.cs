using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TALabSpaghettiTestsKapatsevich.TestsConstants;
using TALabSpaghettiTestsKapatsevich.Pages.Google;
using TALabSpaghettiTestsKapatsevich.TestsConstants;

namespace TALabSpaghettiTestsKapatsevich.GmailActions
{
    public class GmailService
    {
        private IWebDriver driver;
        private GmailPage gmailPage;
        private GoogleAccountPage googleAccountPage;
        private GoogleStartPage googleStartPage;
        private bool firstLoginFlag;

        public GmailService(IWebDriver driver)
        {
            this.driver = driver;
            gmailPage = new GmailPage(driver);
            googleAccountPage = new GoogleAccountPage(driver);
            googleStartPage = new GoogleStartPage(driver);
            firstLoginFlag = true;
        }

        #region work with account
        public void LoginIn(User user)
        {
            if (firstLoginFlag)
            {
                googleAccountPage.OpenPage();
                firstLoginFlag = false;
            }
                
            googleAccountPage.EnterLogin(user);
            googleAccountPage.EnterPassword(user);
            
        }
        public void LogOut()
        {
            gmailPage.LogOut();
            gmailPage.IfAllertIsPresentAcceptWithIt();
        }
        public void LogOutAndChangeAccount(User newAccount)
        {
            LogOut();
            LoginIn(newAccount);            
        }
        #endregion

        #region work with messages
        public void WriteAndSendMessage(string to, Message message)
        {
            gmailPage.WriteAndSendMessage(to, message.Title, message.Text);
        }

        public void OpenLastMessage (string from)
        {
            gmailPage.OpenMessage(from);
        }

        public void MarkMessageAsSpam(string from)
        {
            OpenLastMessage(from);
            gmailPage.MarkCurrentMessageAsSpam();
        }

        public void ConfirmForwardEmailRequset()
        {
            OpenLastMessage(Constants.GOOGLE_NOREPLY_EMAIL);
            gmailPage.ConfirmForwardRequestInCurrentMessage();
        }


        public bool IsMessageFromUserInSpam(User user, Message message)
        {
            gmailPage.GoToSpam();
            return gmailPage.IsMessageFromUserInCurrentFolder(user, message.Title);
        }
        #endregion

        public void GoToSettings()
        {
            gmailPage.GoToSettings();
        }

        public void GoToForwardSetting()
        {
            GoToSettings();
            gmailPage.GoToForwardingFromSettings();
        }

        public void SetRequestForForwardMailToUser(User user)
        {
            GoToForwardSetting();
            gmailPage.SetNewEmailForForwarding(user);

        }

        public void SetForwardMailToConfirmedUser(User user)
        {
            GoToForwardSetting();
            gmailPage.SetForwardMailToConfirmedUser(user);
            
        }

        public void CreateNewFilter(string from, bool hasAttachments, bool deleteMessage, bool markAsImportant)
        {
            GoToForwardSetting();
            gmailPage.GoToFiltersFromSettings();
        }


    }
}
