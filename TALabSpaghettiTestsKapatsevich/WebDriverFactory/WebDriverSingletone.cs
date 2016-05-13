using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TALabSpaghettiTestsKapatsevich.WebDriverFactory
{
    public sealed class WebDriverSingletone
    {
        private WebDriverSingletone() { }
        static WebDriverSingletone() { }

        private static readonly IWebDriver driver = InitDriver();

        public static IWebDriver GetDriver()
        {
            return driver;
        }

        private static IWebDriver InitDriver()
        {
            return new WebDriverFactory().GetDriver();
        }

        public static void ClearCookies()
        {
            driver.Manage().Cookies.DeleteAllCookies();            
        }

        public static void CloseBrowser()
        {
            driver.Quit();
        }


        public static void DisposeDriver()
        {
            if (driver != null)
            {
                driver.Dispose();
            }

        }
    }
}
