using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TALabSpaghettiTestsKapatsevich.WebDriverFactory
{
    public interface IWebDriverFactorySetter
    {
        void SetDriver(WebBrowsers browser);
    }
}
