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
        private IWebDriver driver;
        private WebBrowsers browserType;

        public WebDriverFactory() : this(WebBrowsers.FireFox) { }      

        public WebDriverFactory(WebBrowsers browserType)
        {            
            SetDriver(browserType);
        }

        public void SetDriver(WebBrowsers browser)
        {
            this.browserType = browser;
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
            driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(10));
        }

        public IWebDriver GetDriver()
        {
            if (driver == null)
            {
                SetDriver(this.browserType);
            }
            //return driver;


            ////testing customDriver
            return new CustomWebDriver(driver);
        }

        public void CloseDriver()
        {
            driver.Quit();
            driver = null;
        }


    }
}
