using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TALabSpaghettiTestsKapatsevich;
using TALabSpaghettiTestsKapatsevich.TestsConstants;
using TALabSpaghettiTestsKapatsevich.WebDriverFactory;

namespace TALabSpaghettiTestsKapatsevichTests.TestExtentions
{
    public class GroupAssert : IDisposable
    {
        private readonly IList<GroupedAssertion> assertions;

        private bool _hasVerifiedAlready = false;

        public GroupAssert()
        {
            this.assertions = new List<GroupedAssertion>();
        }

        public void Dispose()
        {
            if (!_hasVerifiedAlready)
            {
                this.Verify();
            }
        }

        public void Add(Action assertion)
        {
            try
            {
                assertion();
            }
            catch (AssertionException exception)
            {
                new WebDriverFactory(Constants.browserForTesting).GetDriver().MakeScreenshot();
                assertions.Add(new GroupedAssertion(exception.Message,
                    new StackTrace(exception, true)));
            }
        }

        public void Verify()
        {
            this._hasVerifiedAlready = true;
            var exceptionCount = 0;
            var exceptionTrace = new StringBuilder();
            var hasThrown = false;

            exceptionTrace.AppendLine("Test failed because one or more assertions failed: ");

            foreach (var assertion in assertions)
            {
                if (exceptionCount > 0) exceptionTrace.AppendLine();

                exceptionTrace.AppendLine(
                    string.Format("{0})\t{1}", ++exceptionCount, FormatExceptionMessage(assertion.Message)));

                exceptionTrace.AppendLine(assertion.StackTrace.ToString());

                hasThrown = true;
            }

            if (hasThrown)
            {
                throw new AssertionException(exceptionTrace.ToString());
            }
        }

        private static string FormatExceptionMessage(string message)
        {
            message = message.Trim();
            return message.Replace("\r\n", "\r\n\t");
        }
    }

    public class GroupedAssertion
    {
        public String Message { get; private set; }

        public StackTrace StackTrace { get; private set; }

        public GroupedAssertion(String message, StackTrace stackTrace)
        {
            this.Message = message;
            this.StackTrace = stackTrace;
        }
    }
}
