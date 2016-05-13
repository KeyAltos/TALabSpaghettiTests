using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TALabSpaghettiTestsKapatsevich.WebDriverFactory
{
    public class WebDriverFactory: IWebDriverFactory, IWebDriverFactorySetter
    {
        private static IWebDriver driver;

        public void SetDriver(WebBrowsers browser)
        {            
            switch (browser)
            {
                case WebBrowsers.Chrome:
                    driver = new ChromeDriver();
                    break;

                case WebBrowsers.FireFox:
                    driver = new FirefoxDriver();
                    break;

                case WebBrowsers.IE:
                    driver = new InternetExplorerDriver();
                    break;                    
            }
            driver.Manage().Window.Maximize();            
        }

        public IWebDriver GetDriver()
        {
            if (driver==null)
            {
                driver = new FirefoxDriver();
            }            
            driver.Manage().Window.Maximize();
            return driver;
        }


    }
}
