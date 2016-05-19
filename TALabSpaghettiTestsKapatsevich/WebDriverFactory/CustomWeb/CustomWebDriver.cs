using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using OpenQA.Selenium.Support.UI;
using TALabSpaghettiTestsKapatsevich.TestsConstants;

namespace TALabSpaghettiTestsKapatsevich.WebDriverFactory
{
    public class CustomWebDriver : IWebDriver, IJavaScriptExecutor
    {
        private IWebDriver baseDriver;
        private WebDriverWait wait;

        public CustomWebDriver(IWebDriver baseDriver)
        {
            this.baseDriver = baseDriver;
            this.wait = new WebDriverWait(baseDriver, Constants.WAITING_TIME);
        }

        public string CurrentWindowHandle
        {
            get
            {
                return baseDriver.CurrentWindowHandle;
            }
        }

        public string PageSource
        {
            get
            {
                return baseDriver.PageSource;
            }
        }

        public string Title
        {
            get
            {
                return baseDriver.Title;
            }
        }

        public string Url
        {
            get
            {
                return baseDriver.Url;
            }

            set
            {
                baseDriver.Url = value;
            }
        }

        public ReadOnlyCollection<string> WindowHandles
        {
            get
            {
                return baseDriver.WindowHandles;
            }
        }

        public void Close()
        {
            baseDriver.Close();
        }

        public void Dispose()
        {
            baseDriver.Dispose();
        }

        public IWebElement FindElement(By by)
        {
            var baseElement = baseDriver.FindElement(by);
            var customElement = new CustomWebElement(baseElement, wait);
            return customElement;
        }

        public ReadOnlyCollection<IWebElement> FindElements(By by)
        {
            var baseElements = baseDriver.FindElements(by);
            var customElements = new List<IWebElement>();
            foreach (var element in baseElements)
            {
                customElements.Add(new CustomWebElement(element, wait));
            }
            return new ReadOnlyCollection<IWebElement>(customElements);
        }

        public IOptions Manage()
        {
            return baseDriver.Manage();
        }

        public INavigation Navigate()
        {
            return baseDriver.Navigate();
        }

        public void Quit()
        {
            baseDriver.Quit();
        }

        public ITargetLocator SwitchTo()
        {
            return baseDriver.SwitchTo();
        }

        object IJavaScriptExecutor.ExecuteAsyncScript(string script, params object[] args)
        {
            return ((IJavaScriptExecutor)baseDriver).ExecuteAsyncScript(script,args);
        }

        object IJavaScriptExecutor.ExecuteScript(string script, params object[] args)
        {
            return ((IJavaScriptExecutor)baseDriver).ExecuteScript(script, args);
        }
    }
}
