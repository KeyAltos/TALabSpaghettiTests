using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TALabSpaghettiTestsKapatsevich.TestsConstants;

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
            byte[] imageBytes = Convert.FromBase64String(image.ToString());

            //---------------------generate filename---------------------------------
            string currentDate = DateTime.Now.ToString("G");
            currentDate = currentDate.Replace(':', '.').Replace('/', '_');

            //string folderPath = Environment.CurrentDirectory.Replace('\\', '/') + "/";

            string screenshotTitle = currentDate + "_screenshot.png";
            //screenshotTitle = screenshotTitle.Replace(':', '.').Replace('/', '_');

            string fileName = Constants.FOLDER_FOR_SCREENSHOT_PATH + screenshotTitle;
            //------------------------------------------------------------------------

            using (BinaryWriter bw = new BinaryWriter(new FileStream(fileName, FileMode.Append,
            FileAccess.Write)))
            {
                bw.Write(imageBytes);
                bw.Close();
            }
        }

    }
}
