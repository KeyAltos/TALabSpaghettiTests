using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TALabSpaghettiTestsKapatsevich.TestsConstants;

namespace TALabSpaghettiTestsKapatsevich.Pages.Google
{
    public class GoogleAccountPage: Page
    {
        [FindsBy(How = How.Name, Using = "Email")]
        private IWebElement inputEmail;

        [FindsBy(How = How.Name, Using = "signIn")]
        private IWebElement submitEmailButton;

        [FindsBy(How = How.Name, Using = "Passwd")]
        private IWebElement inputPasssword;

        [FindsBy(How = How.Name, Using = "signIn")]
        private IWebElement submitPasswordButton;

        [FindsBy(How = How.Id, Using = "account-chooser-link")]
        private IWebElement changeAccountButton;

        [FindsBy(How = How.Id, Using = "account-chooser-add-account")]
        private IWebElement changeAccountAddAccountButton;

        [FindsBy(How = How.Id, Using = "PersistentCookie")]
        private IWebElement leaveInSystemCheckBox;
        
        public GoogleAccountPage(IWebDriver driver) : base(driver)
        {
            this.url = "https://accounts.google.com/AddSession?hl=ru&continue=https://mail.google.com/mail&service=mail";
        }

        public void EnterLogin(User user)
        {            
            inputEmail.SendKeys(user.Username);
            submitEmailButton.Submit();
        }

        public void EnterPassword(User user)
        {            
            inputPasssword.SendKeys(user.Password);

            try
            {
                if (leaveInSystemCheckBox.Selected)
                {
                    leaveInSystemCheckBox.Click();
                }
            }
            catch (NoSuchElementException)
            {

            }

            submitPasswordButton.Submit();
        }
        
    }
}
