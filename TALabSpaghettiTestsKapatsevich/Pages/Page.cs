using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.PageObjects;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TALabSpaghettiTestsKapatsevich.WebDriverFactory;

namespace TALabSpaghettiTestsKapatsevich.Pages
{
    public abstract class Page
    {
        protected IWebDriver driver;
        protected string url;

        public Page(IWebDriver driver)
        {
            this.driver = driver;
            PageFactory.InitElements(driver, this);   
                    
        }

        public void OpenPage()
        {
            driver.Navigate().GoToUrl(url);
        }

        private IAlert CatchPresentAlert()
        {
            try
            {
                return driver.SwitchTo().Alert();
            }
            catch (NoAlertPresentException)
            {
                return null;
            }
        }

        public void IfAllertIsPresentAcceptWithIt()
        {
            var presentAlert = CatchPresentAlert();
            if (presentAlert != null)
            {
                presentAlert.Accept();
            }
        }

        public void HighLightElement(IWebElement element)
        {
            IWebElement elementForLighting;
            string backgroundColor = element.GetCssValue("backgroundColor");
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;

            //check is element is custom
            var customElement = element as CustomWebElement;
            if (customElement!=null)
            {
                elementForLighting = customElement.WrappedElement;
            }
            else
            {
                elementForLighting = element;
            }

            js.ExecuteScript("arguments[0].style.backgroundColor='" + "yellow" + "'", elementForLighting);
            Thread.Sleep(1000);
            js.ExecuteScript("arguments[0].style.backgroundColor='" + backgroundColor + "'", elementForLighting);
        }

        //public void MakeScreenshot()
        //{
        //    Screenshot image = ((ITakesScreenshot)driver).GetScreenshot();
        //    string currentDate = DateTime.Now.ToString("G");
        //    currentDate = currentDate.Replace(':', '.').Replace('/', '_');

        //    string folderPath = Environment.CurrentDirectory.Replace('\\', '/') + "/";
        //    string screenshotTitle = currentDate + "_" + "screenshot.png";
        //    screenshotTitle = screenshotTitle.Replace(':', '.').Replace('/', '_');

        //    string fullPath = folderPath + screenshotTitle;
        //    image.SaveAsFile(fullPath, ImageFormat.Png);
        //}
    }
}
