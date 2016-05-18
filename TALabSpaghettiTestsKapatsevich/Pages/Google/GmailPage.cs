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
        private IWebElement inputMessageTo;

        [FindsBy(How = How.XPath, Using = "//input[@name='subjectbox']")]
        private IWebElement inputMessageTitle;

        [FindsBy(How = How.XPath, Using = "//table//table//table//div[contains(@class, 'editable')]")]
        private IWebElement inputMessageText;

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
        public void WriteMessage(string to, string title, string text)
        {
            writeLetterButton.Click();
            inputMessageTo.SendKeys(to);
            inputMessageTitle.SendKeys(title);
            inputMessageText.SendKeys(text);
        }

        public void AttachFileFromGDrive(string fileName)
        {
            driver.FindElement(By.XPath(Constants.XPATH_LOCATOR_FOR_ATTACH_FILE_FROM_GDRIVE_BUTTON)).Click();            
            var currentFrame = driver.FindElement(By.XPath(Constants.XPATH_LOCATOR_FOR_ATTACH_IFRAME)); 
            driver.SwitchTo().Frame(currentFrame.GetAttribute("name")).FindElement(By.XPath("//div[@aria-label='" + fileName + "']")).Click();

            driver.FindElement(By.XPath(Constants.XPATH_LOCATOR_FOR_ATTACH_SELECTOR)).Click();
            driver.FindElement(By.Id(Constants.ID_LOCATOR_FOR_INSERT_FILE_BUTTON)).Click();
            driver.SwitchTo().DefaultContent();
        }
        public void SendMessage()
        {
            sendLetterButon.Click();
        }

        public void OpenLastMessage(string from)
        {
            string xPathToUserMessage = "//span[contains(@email, '" + from + "')]";
            var message = driver.FindElement(By.XPath(xPathToUserMessage));
            wait.Until(ExpectedConditions.ElementToBeClickable(message));
            message.Click();
        }

        public IWebElement GetMessageFromUserWithTitleFromCurrentFoldder(string from, string title)
        {
            var tableRowsWithMessages = GetAllMessagesFromCurrentFolder();
            foreach (var message in tableRowsWithMessages)
            {
                var firstCondition = message.FindElement(By.XPath("//span[@email]")).GetAttribute("email").Contains(from);//Reciever equals
                var secondCondition = message.FindElement(By.XPath("./td/div/div/div/span")).Text.Contains(title);//Title equals
                if (firstCondition & secondCondition)
                {
                    HighLightElement(message);
                    return message;
                }
            }

            return null;
        }

        public void OpenMessageFromUserWithTitle(string from, string title)
        {
            var a = GetMessageFromUserWithTitleFromCurrentFoldder(from, title);
            a.Click();
        }

        public void MarkCurrentMessageAsSpam()
        {
            var spamButton = driver.FindElement(By.XPath(Constants.XPATH_LOCATOR_FOR_SPAM_BUTTON));
            wait.Until(ExpectedConditions.ElementToBeClickable(spamButton));
            spamButton.Click();

        }
        
        public void GoToLabel(string label)
        {
            var currentURL = driver.Url;
            searchInGmailField.Clear();
            searchInGmailField.SendKeys(label);
            searchInGmailButton.Click();            
            wait.Until<bool>(driver => !String.Equals(driver.Url, currentURL));
            Thread.Sleep(2000);    /////////CORRRRRRRRRRRRRRRREEEEEEEEEEEEEEEEEEEECCCCCCCCCCCCCCCCCTTTTTTTTTTTTTTTT                
        }  
        

        public bool IsMessageFromUserInCurrentFolder(User fromWhoUser, string title)
        {    
            return GetMessageFromUserWithTitleFromCurrentFoldder(fromWhoUser.Username, title) == null ? false: true;
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
            driver.SwitchTo().DefaultContent();            
            var messages = driver.FindElements(By.XPath("//div[@role='main']//tr//span[@email]"));            
            var parentElements = messages.Where(mes => mes.Displayed).Select(mes => mes.FindElement(By.XPath("..")).FindElement(By.XPath("..")).FindElement(By.XPath("..")));
            
            return parentElements;
        }
        
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
            wait.Until(ExpectedConditions.ElementToBeClickable(settingsButton));            
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
            wait.Until(ExpectedConditions.ElementToBeClickable(forwardingCatButton));
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

        public void CreateNewFilter(string from, bool hasAttachments, bool deleteMessage, bool markAsImportant, bool neverSendToSpam)
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

            if (neverSendToSpam)
            {
                GetCheckboxByLabelText("Never send it to Spam").Click();
            }

            driver.FindElement(By.XPath(Constants.XPATH_LOCATOR_FOR_SUBMIT_CREATE_NEW_FILTER_BUTTON)).Click();
            
        }

        public void ClearFilters()
        {
            var a = driver.FindElements(By.XPath("//table[@role='list']//tr/td/input"));
            if (a.Any())
            {
                driver.FindElement(By.XPath("//span[@selector='all']")).Click();
                var deleteFilterButton = driver.FindElement(By.XPath("//button[@class='qR' and contains(text(), 'Delete')]"));
                wait.Until(ExpectedConditions.ElementToBeClickable(deleteFilterButton));
                deleteFilterButton.Click();
                driver.FindElement(By.XPath("//button[@name='ok']")).Click();
                wait.Until(ExpectedConditions.ElementToBeClickable(searchInGmailField));

            }     
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
