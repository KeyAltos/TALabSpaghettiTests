using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Drawing;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Remote;

namespace TALabSpaghettiTestsKapatsevich.WebDriverFactory
{
    public class CustomWebElement : IWebElement
    {
        private IWebElement baseWebElement;
        private WebDriverWait wait;

        public RemoteWebElement WrappedElement
        {
            get
            {
                return baseWebElement as RemoteWebElement;
            }

        }

        public CustomWebElement(IWebElement baseWebElement, WebDriverWait wait)
        {
            this.baseWebElement = baseWebElement;
            this.wait = wait;
        }

        public bool Displayed
        {
            get
            {
                return baseWebElement.Displayed;
            }
        }

        public bool Enabled
        {
            get
            {
                return baseWebElement.Enabled;
            }
        }

        public Point Location
        {
            get
            {
                return baseWebElement.Location;
            }
        }

        public bool Selected
        {
            get
            {
                return baseWebElement.Selected;
            }
        }

        public Size Size
        {
            get
            {
                return baseWebElement.Size;
            }
        }

        public string TagName
        {
            get
            {
                return baseWebElement.TagName;
            }
        }

        public string Text
        {
            get
            {
                return baseWebElement.Text;
            }
        }

        public void Clear()
        {
            WaitForElementToBeClickable();
            baseWebElement.Clear();
        }

        public void Click()
        {
            WaitForElementToBeClickable();
            baseWebElement.Click();
        }

        public IWebElement FindElement(By by)
        {
            var baseElement = baseWebElement.FindElement(by);
            var customElement = new CustomWebElement(baseElement, wait);
            return customElement;
        }

        public ReadOnlyCollection<IWebElement> FindElements(By by)
        {
            var baseElements = baseWebElement.FindElements(by);
            var customElements = new List<IWebElement>();
            foreach (var element in baseElements)
            {
                customElements.Add(new CustomWebElement(element, wait));
            }
            return new ReadOnlyCollection<IWebElement>(customElements);
        }

        public string GetAttribute(string attributeName)
        {
            return baseWebElement.GetAttribute(attributeName);
        }

        public string GetCssValue(string propertyName)
        {
            return baseWebElement.GetCssValue(propertyName);
        }

        public void SendKeys(string text)
        {
            WaitForElementToBeClickable();
            baseWebElement.SendKeys(text);
        }

        public void Submit()
        {
            baseWebElement.Submit();
        }

        private void WaitForElementToBeClickable()
        {
            wait.Until(ExpectedConditions.ElementToBeClickable(baseWebElement));
        }
    }
}
