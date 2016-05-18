using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TALabSpaghettiTestsKapatsevich
{
    public static class Extention
    {
        public static bool IsClickable(this IWebElement element)
        {
            return element.Displayed && element.Enabled;
        }
    }
}
