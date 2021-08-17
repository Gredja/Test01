using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.Events;
using Serilog;

namespace Saucedemo.PageObjects.Base
{
    public class BaseTest
    {
        protected readonly ILogger _logger;
        protected static IWebDriver _driver;
        protected EventFiringWebDriver _eventDriver;

        public BaseTest()
        {
            _logger = new LoggerConfiguration().MinimumLevel.Debug().WriteTo
                .File(@"C:\log\log.txt", rollingInterval: RollingInterval.Day).CreateLogger();
        }

        [SetUp]
        public void Setup()
        {
            _driver = new ChromeDriver();

            _eventDriver = new EventFiringWebDriver(_driver);
            _eventDriver.ElementClicked += firingDriver_ElementClicked;
            _eventDriver.FindElementCompleted += firingDriver_FindElementCompleted;
            _eventDriver.ExceptionThrown += _eventDriver_ExceptionThrown;
            _driver = _eventDriver;

            _driver.Manage().Window.Maximize();
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(1);
        }

        private void _eventDriver_ExceptionThrown(object sender, WebDriverExceptionEventArgs e)
        {
            _logger.Error($"Source: {e.ThrownException.Source}, message: {e.ThrownException.Message}");
        }

        private void firingDriver_FindElementCompleted(object? sender, FindElementEventArgs e)
        {
           _logger.Debug($"Driver {e.Driver}. {e.Element} in {e.FindMethod} was found");
        }

        private void firingDriver_ElementClicked(object? sender, WebElementEventArgs e)
        {
            _logger.Debug($"Driver {e.Driver}. {e.Element} was clicked");
        }
    }
}