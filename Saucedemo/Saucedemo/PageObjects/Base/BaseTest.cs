using NUnit.Framework.Interfaces;

namespace Saucedemo.PageObjects.Base
{
    public class BaseTest : ITestListener
    {
        public void TestStarted(ITest test)
        {
            throw new System.NotImplementedException();
        }

        public void TestFinished(ITestResult result)
        {
            throw new System.NotImplementedException();
        }

        public void TestOutput(TestOutput output)
        {
            throw new System.NotImplementedException();
        }

        public void SendMessage(TestMessage message)
        {
            throw new System.NotImplementedException();
        }
    }
}