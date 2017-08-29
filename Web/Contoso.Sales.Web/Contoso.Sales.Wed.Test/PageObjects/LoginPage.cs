using Contoso.Sales.Wed.Test.Helpers;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contoso.Sales.Wed.Test.PageObjects
{
    public class LoginPage
    {
        private readonly IWebDriver _driver;

        private LoginPage(IWebDriver driver)
        {
            _driver = driver;
            PageFactory.InitElements(_driver, this);
        }

        public static LoginPage Create(IWebDriver driver)
        {
            return new LoginPage(driver);
        }

        [FindsBy(How = How.Id, Using = "btn_login")]
        public IWebElement LoginButton { get; set; }

        //[FindsBy(How = How.ClassName, Using = "title3_1f895c54-7ea2-436f-98a7-12246af93168")]
        //public IWebElement AccountTypeButton { get; set; }

        [FindsBy(How = How.Id, Using = "cred_userid_inputtext")]
        public IWebElement UserIdField { get; set; }

        [FindsBy(How = How.Id, Using = "cred_password_inputtext")]
        public IWebElement PasswordField { get; set; }
        

        [FindsBy(How = How.Id, Using = "cred_sign_in_button")]
        public IWebElement UserLoginButton { get; set; }

        [FindsBy(How = How.Id, Using = "mso_account_tile")]
        public IWebElement ConfirmAccountType { get; set; }


        [FindsBy(How = How.Name, Using = "loginfmt")]
        public IWebElement FinalUserIdField { get; set; }

        [FindsBy(How = How.Name, Using = "passwd")]
        public IWebElement FinalPasswordField { get; set; }


        [FindsBy(How = How.Name, Using = "SI")]
        public IWebElement FinalUserLoginButton { get; set; }
        

        public void LoginAsAdmin(string baseUrl)
        {
            _driver.Navigate().GoToUrl(baseUrl);          

            LoginButton.Click();
            //AccountTypeButton.Click();

            UserIdField.Clear();      
            UserIdField.SendKeys("kbanda@inforica.com");

            PasswordField.Clear();
            PasswordField.SendKeys("Power123@");

            UserLoginButton.Click();
            ConfirmAccountType.Click();

            FinalUserIdField.Clear();
            FinalUserIdField.SendKeys("kbanda@inforica.com");

            FinalPasswordField.Clear();
            FinalPasswordField.SendKeys("Power123@");

            FinalUserLoginButton.Click();
        }


        //public void LoginAsNobody(string baseUrl)
        //{
        //    _driver.Navigate().GoToUrl(baseUrl);

        //    LoginButton.Click();
        //    //AccountTypeButton.Click();

        //    UserIdField.Clear();
        //    UserIdField.SendKeys("testuser");

        //    PasswordField.Clear();
        //    PasswordField.SendKeys("testpass");

        //    UserLoginButton.Click();
        //    ConfirmAccountType.Click();
        //}
    }
}
