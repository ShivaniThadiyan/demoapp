using Contoso.Sales.Wed.Test.Helpers;
using Contoso.Sales.Wed.Test.PageObjects;
using Contoso.Sales.Wed.Test.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contoso.Sales.Wed.Test.Tests
{
    [TestClass]
    public class LoginTest
    {
        private IWebDriver _driver;
        private string _baseUrl;

        [TestInitialize]
        public void SetupTest()
        {
            _driver = DriverFactory.Create();
            _baseUrl = ConfigurationHelper.Get<string>("TargetUrl");
        }

        [TestMethod]
        public void LoginWithValidCredentialsShouldSucceed()
        {
            LoginPage.Create(_driver).LoginAsAdmin(_baseUrl);
        }

        [TearDown]
        public void CloseTest()
        {
            try
            {
                _driver.Quit();
                _driver.Close();
            }
            catch (Exception)
            {
                // Ignore errors if we are unable to close the browser
            }
        }
    }
}
