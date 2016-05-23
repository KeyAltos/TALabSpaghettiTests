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
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Internal;
using OpenQA.Selenium.Interactions.Internal;

namespace TALabSpaghettiTestsKapatsevich.WebDriverFactory
{
    public class CustomWebElement : IWebElement, IWrapsElement, ILocatable
    {
        private IWebElement baseWebElement;
        private WebDriverWait wait;
        private IWebDriver driver;
        private Actions actions ;
        public RemoteWebElement WrappedElement
        {
            get
            {
                return baseWebElement as RemoteWebElement;
            }

        }

        public CustomWebElement(IWebElement baseWebElement, WebDriverWait wait, IWebDriver driver)
        {
            this.baseWebElement = baseWebElement;
            this.wait = wait;
            this.driver = driver;
            this.actions = new Actions(driver);
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

        IWebElement IWrapsElement.WrappedElement
        {
            get
            {
                return baseWebElement;
            }
        }

        Point ILocatable.LocationOnScreenOnceScrolledIntoView
        {
            get
            {
                return ((ILocatable)baseWebElement).LocationOnScreenOnceScrolledIntoView;
            }
        }

        ICoordinates ILocatable.Coordinates
        {
            get
            {
                return ((ILocatable)baseWebElement).Coordinates;
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

            actions.MoveToElement(baseWebElement).Click().Build().Perform();  
            
                     
            //baseWebElement.Click();
        }

        public IWebElement FindElement(By by)
        {
            var baseElement = baseWebElement.FindElement(by);
            var customElement = new CustomWebElement(baseElement, wait, driver);
            return customElement;
        }

        public ReadOnlyCollection<IWebElement> FindElements(By by)
        {
            var baseElements = baseWebElement.FindElements(by);
            var customElements = new List<IWebElement>();
            foreach (var element in baseElements)
            {
                customElements.Add(new CustomWebElement(element, wait, driver));
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
            //wait.Until<bool>(drv => baseWebElement.IsClickable());  
            //wait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy()            
            //actions.moveToElement(element).click().perform();

            //actions.MoveToElement(baseWebElement);
            
            //jse.ExecuteScript("arguments[0].scrollIntoView()", baseWebElement);


            wait.Until(ExpectedConditions.ElementToBeClickable(baseWebElement));

            //IJavaScriptExecutor jse = (IJavaScriptExecutor)driver;
            //jse.ExecuteScript("scroll(250, 0)");
        }
    }
}
