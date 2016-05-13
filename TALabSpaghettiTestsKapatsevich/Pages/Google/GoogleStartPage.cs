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
    public class GoogleStartPage: Page
    {        
        [FindsBy(How = How.XPath, Using = "//a[contains(@href, 'accounts.google')]")]
        private IWebElement enterInLogInButton;        

        //[FindsBy(How = How.Name, Using = "q")]
        //private IWebElement searchField;

        //[FindsBy(How = How.Name, Using = "btnK")]
        //private IWebElement searchButton;

        public GoogleStartPage(IWebDriver driver): base (driver)
        {
            this.url = "http://www.google.com";
        }

        public void EnterInAccount(User user)
        {
            enterInLogInButton.Click();
            var googleAccountPage = new GoogleAccountPage(driver);
            googleAccountPage.EnterLogin(user);
            googleAccountPage.EnterPassword(user);
        }

        //public void EnterInAccount(string Email, string password)
        //{
        //    enterInLogInButton.Click();
        //    inputEmail.SendKeys(email);
        //    submitEmailButton.Submit();
        //    inputPasssword.SendKeys(password);
        //    submitPasswordButton.Submit();
        //}

        //public void SearchInGoogle(string textForSearching)
        //{
        //    searchField.SendKeys(textForSearching);
        //    searchButton.Click();
        //}

        //public void GoToLink(string link)
        //{
        //    driver.FindElement(By.XPath("//a[contains(@href,'" + link + "')]")).Click();
        //}


    }
}
