using System;
using System.IO;
using System.Linq;
using NUnit.Framework;
using Xamarin.UITest;
using Xamarin.UITest.Queries;

namespace CheckQuotaUITest
{
    [TestFixture(Platform.Android)]
    //[TestFixture(Platform.iOS)]
    public class Tests
    {
        IApp app;
        Platform platform;

        public Tests(Platform platform)
        {
            this.platform = platform;
        }

        [SetUp]
        public void Setup()
        {
            app = ConfigureApp.Android.PreferIdeSettings().ApkFile(@"C:\Users\kbanda.INFORICA\Desktop\com.contoso.sales.apk").StartApp();            
        }

        [Test]
        public void UiTestCase1()
        {
            app.EnterText(c => c.Marked("Email"),string.Format("{0}@manesh.me", DateTime.Now.Ticks));
            app.EnterText(c => c.Marked("Password"), "RandomPassword");                                    
            app.DismissKeyboard();
            app.Tap(c => c.Marked("LoginButton"));
            app.WaitForElement(x => x.Marked("QuotaButton"), timeout: TimeSpan.FromSeconds(120));
            AppResult[] result = app.Query(c => c.Marked("QuotaLabel").Text("Your Quota"));
            Assert.IsTrue(result.Any(), "Success");
        }
    }
}

