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
            Debug.Print("From Unhandled " + exception.Message);
            new WebDriverFactory(Constants.browserForTesting).GetDriver().MakeScreenshot();
        }

        public void RunFinished(TestResult result)
        {
            
        }

        public void RunStarted(string name, int testCount)
        {
            
        }

        public void SuiteFinished(TestResult result)
        {
            
        }

        public void SuiteStarted(TestName testName)
        {
           
        }

        public void TestFinished(TestResult result)
        {
            var isFailure =
            result.ResultState == ResultState.Error ||
            result.ResultState == ResultState.Failure;
            if (isFailure)
            {
                Debug.Print("TestFinished ");
                new WebDriverFactory(Constants.browserForTesting).GetDriver().MakeScreenshot();
            }
        }

        public void TestOutput(TestOutput testOutput)
        {
            
        }

        public void TestStarted(TestName testName)
        {
           
        }

        public void UnhandledException(Exception exception)
        {
            Debug.Print("From Unhandled "+exception.Message);
            new WebDriverFactory(Constants.browserForTesting).GetDriver().MakeScreenshot();
        }
    }
}
