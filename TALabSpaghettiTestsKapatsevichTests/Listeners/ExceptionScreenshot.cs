using NUnit.Core;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TALabSpaghettiTestsKapatsevich;
using TALabSpaghettiTestsKapatsevich.TestsConstants;
using TALabSpaghettiTestsKapatsevich.WebDriverFactory;

namespace TALabSpaghettiTestsKapatsevichTests.Listeners
{
    public class ExceptionScreenshot : EventListener
    {
        public ExceptionScreenshot()
        {

        }

        public void RunFinished(Exception exception)
        {
            Debug.WriteLine("Screenshot Listener: From RunFinished ");
            new WebDriverFactory(Constants.browserForTesting).GetDriver().MakeScreenshot();
        }

        public void RunFinished(TestResult result)
        {
            Debug.WriteLine("Screenshot Listener: From RunFinished ");
        }

        public void RunStarted(string name, int testCount)
        {
            Debug.WriteLine("Screenshot Listener: From RunStarted");
        }

        public void SuiteFinished(TestResult result)
        {
            Debug.WriteLine("Screenshot Listener: From SuiteFinished ");
        }

        public void SuiteStarted(TestName testName)
        {
            Debug.WriteLine("Screenshot Listener: From SuiteStarted " + testName.Name);
        }

        public void TestFinished(TestResult result)
        {
            var isFailure =
            result.ResultState == ResultState.Error ||
            result.ResultState == ResultState.Failure;
            if (isFailure)
            {
                Debug.WriteLine("Screenshot Listener: From TestFinished: Test failed:");
                Debug.WriteLine("Screenshot Listener: From TestFinished: " + result.Message);
                Debug.WriteLine("Screenshot Listener: From TestFinished: making screenshot");
                //new WebDriverFactory(Constants.browserForTesting).GetDriver().MakeScreenshot();
            }
        }

        public void TestOutput(TestOutput testOutput)
        {            
        }

        public void TestStarted(TestName testName)
        {
            Debug.WriteLine("Screenshot Listener: From TestStarted " + testName.Name);
        }

        public void UnhandledException(Exception exception)
        {
            Debug.WriteLine("Screenshot Listener: From Unhandled Exception" + exception.Message);
            new WebDriverFactory(Constants.browserForTesting).GetDriver().MakeScreenshot();
        }
    }
}
