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
            string xPathToUserMessage = "//span[@email]";
            foreach (var element in driver.FindElements(By.XPath(xPathToUserMessage)))
            {                
                if (element.Displayed && element.GetAttribute("email").Contains(fromWhoUser.Username))
                {                    
                    var parentElement = element.FindElement(By.XPath("..")).FindElement(By.XPath("..")).FindElement(By.XPath(".."));
                    HighLightElement(parentElement);
                    var titleElement = parentElement.FindElement(By.XPath("./td/div/div/div/span"));
                    HighLightElement(titleElement);
                    if (titleElement.Text.Contains(title))
                    {
                        return true;
                    }                    
                }
            }
            return false;
        }


        public void ConfirmForwardRequestInCurrentMessage()
        {
            confirmRequestLink.Click();
            SubmitButtonInNewWindow(Constants.XPATH_LOCATOR_FOR_SUBMIT_BUTTON_IN_NEW_WINDOW);            
            driver.Close();
            driver.SwitchTo().Window(windowHandleBefore);

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
            settingsButton.Click();
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
        }

        public void GoToFiltersFromSettings()
        {
            filtersCatButton.Click();
        }
        #endregion


        //public bool CheckNewInputMessages(string oldInputMessagesText, int timeForChecking)
        //{
        //    var elementForChecking = setOfNavigationLinks.GetElementOnLinkTextOnRussian(Constants.gmailInputMessagesIdentifier);

        //    WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeForChecking));

        //    try
        //    {
        //        wait.Until(ExpectedConditions.TextToBePresentInElement(elementForChecking, oldInputMessagesText));
        //    }
        //    catch (WebDriverTimeoutException)
        //    {

        //        return false;
        //    }

        //    return true;
        //}


        //public string RememberCurrentInputMessagesText()
        //{
        //    return setOfNavigationLinks.GetElementOnLinkTextOnRussian(Constants.gmailInputMessagesIdentifier).Text;
        //}

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
        #endregion
    }
}
