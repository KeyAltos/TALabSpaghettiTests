using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TALabSpaghettiTestsKapatsevich.TestsConstants;
using TALabSpaghettiTestsKapatsevich.Pages.Google;

namespace TALabSpaghettiTestsKapatsevich.GmailActions
{
    public class GmailService
    {
        private IWebDriver driver;
        private GmailPage gmailPage;
        private GoogleAccountPage googleAccountPage;
        private bool firstLoginFlag;

        public GmailService(IWebDriver driver)
        {
            this.driver = driver;
            gmailPage = new GmailPage(driver);
            googleAccountPage = new GoogleAccountPage(driver);
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
            gmailPage.WriteMessage(to, message.Title, message.Text);
            gmailPage.SendMessage();
        }

        public void WriteAndSendMessageWithAttachGDrive(string to, Message message, string fileName)
        {
            gmailPage.WriteMessage(to, message.Title, message.Text);
            gmailPage.AttachFileFromGDrive(fileName);
            gmailPage.SendMessage();
            gmailPage.IfAllertIsPresentAcceptWithIt();            
        }

        public void OpenLastMessage (string from)
        {
            gmailPage.OpenLastMessage(from);
        }

        public void OpenMessageFromUserWithTitle(string from, Message message)
        {
            gmailPage.OpenMessageFromUserWithTitle(from, message.Title);
        }

        public void MarkMessageAsSpam(string from, Message message)
        {
            gmailPage.GoToLabel(Constants.INBOX_FOLDER_LABEL);
            gmailPage.OpenMessageFromUserWithTitle(from, message.Title);
            gmailPage.MarkCurrentMessageAsSpam();
        }

        public void ConfirmForwardEmailRequset()
        {
            OpenLastMessage(Constants.GOOGLE_NOREPLY_EMAIL);
            gmailPage.ConfirmForwardRequestInCurrentMessage();
        }

        #region is message in...
        public bool IsMessageFromUserInSpam(User user, Message message)
        {
            gmailPage.SearchRequest(Constants.SPAM_FOLDER_LABEL, user.Username, message.Title);


            //gmailPage.GoToLabel(Constants.SPAM_FOLDER_LABEL);

            return gmailPage.IsMessageFromUserInCurrentFolder(user, message.Title);
        }

        public bool IsMessageFromUserInInbox(User user, Message message)
        {
            gmailPage.SearchRequest(Constants.INBOX_FOLDER_LABEL, user.Username, message.Title);

            //gmailPage.GoToLabel(Constants.INBOX_FOLDER_LABEL);

            return gmailPage.IsMessageFromUserInCurrentFolder(user, message.Title);
        }

        public bool IsMessageFromUserIsImportant(User user, Message message)
        {
            gmailPage.SearchRequest(Constants.IMPORTANT_FOLDER_LABEL, user.Username, message.Title);

            //gmailPage.GoToLabel(Constants.IMPORTANT_FOLDER_LABEL);

            return gmailPage.IsMessageFromUserInCurrentFolder(user, message.Title);
        }

        public bool IsMessageFromUserIsImportantInTrash(User user, Message message)
        {

            gmailPage.SearchRequest(Constants.TRASH_IMPORTANT_LABEL, user.Username, message.Title);

            //gmailPage.GoToLabel(Constants.TRASH_IMPORTANT_LABEL);

            return gmailPage.IsMessageFromUserInCurrentFolder(user, message.Title);
        }
        #endregion

        public void ClearSpam()
        {
            gmailPage.GoToLabel(Constants.SPAM_FOLDER_LABEL);

            if (gmailPage.GetAllMessagesFromCurrentFolder().Any())
            {
                gmailPage.ClearCurrentFolder();
            }
                    }


        #endregion

        #region work with settings
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

        public void CreateNewFilter(string from, bool hasAttachments, bool deleteMessage, bool markAsImportant, bool neverSendToSpam)
        {
            GoToForwardSetting();
            gmailPage.GoToFiltersFromSettings();
            gmailPage.CreateNewFilter(from, hasAttachments, deleteMessage, markAsImportant, neverSendToSpam);
        }

        public void ClearFilters()
        {
            GoToForwardSetting();
            gmailPage.GoToFiltersFromSettings();
            gmailPage.ClearFilters();
        }

        public void ClearForwarding()
        {
            GoToForwardSetting();
            gmailPage.ClearForwardingSettings();
            gmailPage.IfAllertIsPresentAcceptWithIt();
        }
        #endregion


    }
}
