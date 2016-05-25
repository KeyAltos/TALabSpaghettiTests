using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
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

        public static void MakeScreenshot(this IWebDriver driver)
        {
            Screenshot image = ((ITakesScreenshot)driver).GetScreenshot();
            string currentDate = DateTime.Now.ToString("G");
            currentDate = currentDate.Replace(':', '.').Replace('/', '_');

            string folderPath = Environment.CurrentDirectory.Replace('\\', '/') + "/";
            string screenshotTitle = currentDate + "_" + "screenshot.png";
            screenshotTitle = screenshotTitle.Replace(':', '.').Replace('/', '_');

            string fullPath = folderPath + screenshotTitle;
            image.SaveAsFile(fullPath, ImageFormat.Png);
        }
    }
}
