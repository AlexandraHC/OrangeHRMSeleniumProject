using OrangeHRMSeleniumProject.Driver;
using OrangeHRMSeleniumProject.PageObjects;

namespace OrangeHRMSeleniumProject.Tests
{
    [TestFixture(DriverType.Firefox)]
    [TestFixture(DriverType.Chrome)]
    [TestFixture(DriverType.Edge)]

    public class LoginPageTest : TestBase
    {
        public LoginPageTest(DriverType driverType) : base(driverType)
        {
        }

        [SetUp]
        public new void Setup()
        {
            base.Setup();
        }

        [Test]
        public void LoginWithValidCredentials()
        {
            LoginPage login = new LoginPage(_driver);

            login.Login("Admin", "admin123");
            login.LoginBtn.Submit();

            Assert.IsTrue(login.IsLoggedIn());
        }

        [Test]
        public void LoginWithBothFieldsEmpty()
        {
            LoginPage login = new LoginPage(_driver);

            login.Login("", "");
            login.LoginBtn.Submit();

            var errorElements = _driver.FindElements(login.ValidationErrorMessageLocator);
            Assert.IsTrue(errorElements.Count() == 2);
        }

        [Test]
        public void LoginWithUserFieldEmpty()
        {
            LoginPage login = new LoginPage(_driver);

            login.Login("", "admin123");
            login.LoginBtn.Submit();

            var errorElement = _driver.FindElement(login.ValidationErrorMessageLocator);
            Assert.IsTrue(errorElement.Displayed);
        }

        [Test]
        public void LoginWithPasswordFieldEmpty()
        {
            LoginPage login = new LoginPage(_driver);

            login.Login("Admin", "");
            login.LoginBtn.Submit();

            var errorElement = _driver.FindElement(login.ValidationErrorMessageLocator);
            Assert.IsTrue(errorElement.Displayed);
        }

        [Test]
        public void LoginWithUnvalidCredentials()
        {
            LoginPage login = new LoginPage(_driver);

            login.Login("@13!!**", "tes1#$");
            login.LoginBtn.Submit();
            Assert.IsTrue(login.IsLoggedOut());
        }

        [Test]
        public void LogoutTest()
        {
            LoginPage login = new LoginPage(_driver);

            login.Login("Admin", "admin123");
            login.LoginBtn.Submit();
            login.UserBtn.Click();
            login.Logout();

            Assert.IsTrue(login.IsLoggedOut());
        }
    }
}
