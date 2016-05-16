using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TALabSpaghettiTestsKapatsevich.TestsConstants;

namespace TALabSpaghettiTestsKapatsevich.Pages.Google
{
    public class GmailPage : Page
    {
        #region fields
        private WebDriverWait wait;
        private string windowHandleBefore;

        #region webelements
        [FindsBy(How = How.CssSelector, Using = "div.z0")]
        private IWebElement writeLetterButton;

        [FindsBy(How = How.XPath, Using = "//textarea[@name='to']")]
        private IWebElement inputTo;

        [FindsBy(How = How.XPath, Using = "//input[@name='subjectbox']")]
        private IWebElement inputTitle;

        [FindsBy(How = How.XPath, Using = "//table//table//table//div[contains(@class, 'editable')]")]
        private IWebElement inputText;

        [FindsBy(How = How.XPath, Using = "//div[contains(@aria-label, 'Ctrl') and contains(@aria-label, 'Enter')]")]
        private IWebElement sendLetterButon;

        [FindsBy(How = How.XPath, Using = "//a[contains(@href, 'accounts.google.com/SignOutOptions')]")]
        private IWebElement manageAccountsButton;

        [FindsBy(How = How.XPath, Using = "//a[contains(@href, 'accounts.google.com/AddSession')]")]
        private IWebElement addAccountButton;

        [FindsBy(How = How.XPath, Using = "//a[contains(@href, 'accounts.google.com/Logout')]")]
        private IWebElement logOutButton;

        [FindsBy(How = How.Name, Using = "q")]
        private IWebElement searchInGmailField;

        [FindsBy(How = How.XPath, Using = "//div[@role='search']//button[contains(@aria-label, 'Gmail')]")]
        private IWebElement searchInGmailButton;

        [FindsBy(How = How.XPath, Using = "//div[@gh='s']/div")]
        private IWebElement settingsButton;

        [FindsBy(How = How.XPath, Using = "//a[contains(@href, 'settings/fwdandpop')]")]
        private IWebElement forwardingCatButton;

        [FindsBy(How = How.XPath, Using = "//a[contains(@href, 'settings/filters')]")]
        private IWebElement filtersCatButton;

        [FindsBy(How = How.XPath, Using = "//input[@act='add']")]
        private IWebElement addEmailForForwardingButton;

        [FindsBy(How = How.XPath, Using = "//a[contains(@href, 'mail-settings.google.com/mail/')]")]
        private IWebElement confirmRequestLink;        

        [FindsBy(How = How.XPath, Using = "//input[@type='radio' and @value='1' and @name='sx_em']")]
        private IWebElement forwardEmailCheckbox;

        [FindsBy(How = How.XPath, Using = "//div[@gh='mtb']//span[@role='checkbox']")]
        private IWebElement checkAllMessagesInFolderCheckbox;

        [FindsBy(How = How.XPath, Using = "//span[contains(text(), 'Create a new filter')]")]
        private IWebElement createNewFilterButton;


        //[FindsBy(How = How.XPath, Using = "//body//div[@role='navigation']//a")]
        //private IList<IWebElement> setOfNavigationLinks;

        #endregion

        #endregion

        #region ctror
        public GmailPage(IWebDriver driver) : base(driver)
        {
            this.url = "https://mail.google.com";
            wait = new WebDriverWait(driver, Constants.WAITING_TIME);
            windowHandleBefore = driver.CurrentWindowHandle;
        }
        #endregion

        #region work with messages
        public void WriteAndSendMessage(string to, string title, string text)
        {
            writeLetterButton.Click();
            inputTo.SendKeys(to);
            inputTitle.SendKeys(title);
            inputText.SendKeys(text);
            sendLetterButon.Click();
        }

        public void OpenMessage(string from)
        {
            string xPathToUserMessage = "//span[contains(@email, '" + from + "')]";
            var message = driver.FindElement(By.XPath(xPathToUserMessage));
            wait.Until(ExpectedConditions.ElementToBeClickable(message));
            message.Click();
        }

        public void MarkCurrentMessageAsSpam()
        {
            var spamButton = driver.FindElement(By.XPath(Constants.XPATH_LOCATOR_FOR_SPAM_BUTTON));
            wait.Until(ExpectedConditions.ElementToBeClickable(spamButton));
            spamButton.Click();
        }

        public void GoToSpam()
        {
            searchInGmailField.SendKeys("in: spam");
            searchInGmailButton.Click();
        }
        public bool IsMessageFromUserInCurrentFolder(User fromWhoUser, string title)
        {
            //string xPathToUserMessage = "//span[@email]";
            //var a = driver.FindElements(By.XPath(xPathToUserMessage));
            //foreach (var element in driver.FindElements(By.XPath(Constants.XPATH_LOCATOR_FOR_ALL_MESSAGES_IN_CURRENT_FOLDER)))
            //{
            //    var c = element.Displayed;
            //    var b = element.GetAttribute("email").Contains(fromWhoUser.Username);
            //    if (element.Displayed && element.GetAttribute("email").Contains(fromWhoUser.Username))
            //    {                    
            //        var parentElement = element.FindElement(By.XPath("..")).FindElement(By.XPath("..")).FindElement(By.XPath(".."));                    
            //        var titleElement = parentElement.FindElement(By.XPath("./td/div/div/div/span"));

            //        //if (String.Equals(titleElement.Text, title))
            //        if (titleElement.Text.Contains(title))
            //        {
            //            HighLightElement(titleElement);
            //            return true;
            //        }                    
            //    }
            //}
            var tableRowsWithMessages = GetAllMessagesFromCurrentFolder();
            foreach (var message in tableRowsWithMessages)
            {
                var firstCondition = message.FindElement(By.XPath("//span[@email]")).GetAttribute("email").Contains(fromWhoUser.Username);//Reciever equals
                var secondCondition = message.FindElement(By.XPath("./td/div/div/div/span")).Text.Contains(title);//Title equals
                if (firstCondition & secondCondition)
                {
                    HighLightElement(message);
                    return true;
                }
            }

            return false;
        }

        public void ClearCurrentFolder()
        {
            wait.Until(ExpectedConditions.ElementToBeClickable(checkAllMessagesInFolderCheckbox));
            checkAllMessagesInFolderCheckbox.Click();
            var clearSpamButton = driver.FindElement(By.XPath(Constants.XPATH_LOCATOR_FOR_CLEAR_SPAM_BUTTON));
            wait.Until(ExpectedConditions.ElementToBeClickable(clearSpamButton));
            clearSpamButton.Click();
        }


        public void ConfirmForwardRequestInCurrentMessage()
        {
            confirmRequestLink.Click();
            SubmitButtonInNewWindow(Constants.XPATH_LOCATOR_FOR_SUBMIT_BUTTON_IN_NEW_WINDOW);            
            driver.Close();
            driver.SwitchTo().Window(windowHandleBefore);

        }

        public IEnumerable<IWebElement> GetAllMessagesFromCurrentFolder()
        {
            wait.Until(ExpectedConditions.ElementToBeClickable(driver.FindElement(By.XPath("//div[@role='main']"))));
            var messages = driver.FindElements(By.XPath("//div[@role='main']//tr//span[@email]"));            
            var parentElements = messages.Where(mes => mes.Displayed).Select(mes => mes.FindElement(By.XPath("..")).FindElement(By.XPath("..")).FindElement(By.XPath("..")));
            
            return parentElements;
        }

        //public bool IsAnyMessagesInCurrentFolder()
        //{
        //    foreach (var element in driver.FindElements(By.XPath(Constants.XPATH_LOCATOR_FOR_ALL_MESSAGES_IN_CURRENT_FOLDER)))
        //    {
        //        if (element.Displayed)
        //        {
        //            return true;
        //        }
        //    }
        //    return false;
        //}
        #endregion

        #region logout
        public void LogOut()
        {
            wait.Until(ExpectedConditions.ElementToBeClickable(manageAccountsButton));
            manageAccountsButton.Click();
            logOutButton.Click();
        }
        #endregion

        #region work with settings

        public void GoToSettings()
        {
            
            settingsButton.Click();
            //var settingsMenuItem = driver.FindElement(By.XPath("//div[@role='menu']//div[contains(text(), 'Settings')]"));
            //wait.Until(ExpectedConditions.ElementToBeClickable(settingsMenuItem));
            //HighLightElement(settingsMenuItem);

            //settingsMenuItem.FindElement(By.XPath("..")).Click();

            var currentUrl = driver.Url;
            var settingURL = currentUrl.Remove(currentUrl.Length - "index".Length).Insert(currentUrl.Length - "index".Length, "settings/general");
            driver.Navigate().GoToUrl(settingURL);
        }

        public void GoToForwardingFromSettings()
        {
            forwardingCatButton.Click();
        }

        public void SetNewEmailForForwarding(User user)
        {
            addEmailForForwardingButton.Click();
            driver.SwitchTo().ActiveElement().SendKeys(user.Username);
            driver.FindElement(By.Name("next")).Click();
            SubmitButtonInNewWindow(Constants.XPATH_LOCATOR_FOR_SUBMIT_BUTTON_IN_NEW_WINDOW);            
            driver.SwitchTo().Window(windowHandleBefore).FindElement(By.Name("ok")).Click();
        }

        public void SetForwardMailToConfirmedUser(User user)
        {            
            wait.Until(ExpectedConditions.ElementToBeClickable(forwardEmailCheckbox));
            forwardEmailCheckbox.Click();

            var saveForwardingChangesButtons = driver.FindElements(By.XPath(Constants.XPATH_LOCATOR_FOR_FORWARDING_SETTINGS_SAVE_CHANGES_BUTTONS)).Where(tr=>tr.Displayed);
            var submitButton = saveForwardingChangesButtons.First();
            wait.Until(ExpectedConditions.ElementToBeClickable(submitButton));
            submitButton.Click();
        }

        public void GoToFiltersFromSettings()
        {
            filtersCatButton.Click();
        }

        public void CreateNewFilter(string from, bool hasAttachments, bool deleteMessage, bool markAsImportant)
        {
            createNewFilterButton.Click();

            GetInputTextByLabelText("From").SendKeys(from);            

            if (hasAttachments)
            {
                GetCheckboxByLabelText("Has attachment").Click();
            }

            driver.FindElement(By.XPath(Constants.XPATH_LOCATOR_FOR_CREATE_NEW_FILTER_BASED_ON_SEARCH_LINK)).Click();            


            if (deleteMessage)
            {
                GetCheckboxByLabelText("Delete it").Click();
            }

            if (markAsImportant)
            {
                GetCheckboxByLabelText("Always mark it as important").Click();
            }

            driver.FindElement(By.XPath(Constants.XPATH_LOCATOR_FOR_SUBMIT_CREATE_NEW_FILTER_BUTTON)).Click();
            
        }
        #endregion               

        #region private methods
        private void SubmitButtonInNewWindow(string buttonXpathLocator)
        {
            foreach (var window in driver.WindowHandles)
            {
                if (!String.Equals(windowHandleBefore, window))
                {
                    driver.SwitchTo().Window(window).FindElement(By.XPath(buttonXpathLocator)).Click();
                }
            }
        }

        private IWebElement GetCheckboxByLabelText(string text)
        {
            var element = driver.FindElement(By.XPath("//label[contains(text(),'"+text+ "')]")).FindElement(By.XPath(".."));
            return element = element.FindElement(By.XPath("./input"));
        }

        private IWebElement GetInputTextByLabelText(string text)
        {
            var element = driver.FindElement(By.XPath("//label[contains(text(),'" + text + "')]")).FindElement(By.XPath("..")).FindElement(By.XPath(".."));
            return element = element.FindElement(By.XPath("./span/input"));
        }
        #endregion
    }
}
