using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TALabSpaghettiTestsKapatsevich.Pages
{
    public abstract class Page
    {
        protected IWebDriver driver;
        protected string url;
        protected static int screenshotCounter = 0;

        public Page(IWebDriver driver)
        {
            this.driver = driver;
            PageFactory.InitElements(driver, this);
            driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(10));
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
            string backgroundColor = element.GetCssValue("backgroundColor");
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("arguments[0].style.backgroundColor='" + "yellow" + "'", element);
            Thread.Sleep(2000);
            js.ExecuteScript("arguments[0].style.backgroundColor='" + backgroundColor + "'", element);
        }
            

        //public void MakeScreenshot()
        //{
        //    screenshotCounter++;
        //    Screenshot image = ((ITakesScreenshot)driver).GetScreenshot();
        //    string currentDate = DateTime.Now.ToString("G");
        //    currentDate = currentDate.Replace(':', '.').Replace('/', '_');
        //    string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures).Replace('\\', '/') + "/";
        //    string screenshotTitle = "Screenshot_#" + screenshotCounter + "_" + url + "_" + currentDate + ".gif";
        //    screenshotTitle = screenshotTitle.Replace(':', '.').Replace('/', '_');
        //    string fullPath = folderPath + screenshotTitle;
        //    image.SaveAsFile(fullPath, ImageFormat.Gif);

        //}
    }
}
