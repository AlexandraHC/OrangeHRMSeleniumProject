using OrangeHRMSeleniumProject.Driver;
using OrangeHRMSeleniumProject.PageObjects;

namespace OrangeHRMSeleniumProject.Tests
{
    [TestFixture(DriverType.Firefox)]
    [TestFixture(DriverType.Chrome)]
    [TestFixture(DriverType.Edge)]

    public class ChangePasswordTest : TestBase
    {
        public ChangePasswordTest(DriverType driverType) : base(driverType)
        {
        }

        [SetUp]
        public new void Setup()
        {
            base.Setup();
            LoginPage login = new LoginPage(_driver);
            login.Login("Admin", "admin123");
            login.LoginBtn.Submit();
        }

        [Test]
        public void ChangePasswordWithCurrentPasswordFieldEmpty()
        {
            ChangePasswordPage changePassword = new ChangePasswordPage(_driver);
            changePassword.NavigateToChangePasswordPage();
            changePassword.EnterCurrentPassword("");
            changePassword.EnterNewPassword("test1234!");
            changePassword.EnterConfirmNewPassword("test1234");
            changePassword.SaveBtn.Submit();
            Assert.IsFalse(changePassword.PasswordIsSuccessfullyChanged());
        }

        [Test]
        public void ChangePasswordWithNewPasswordFieldEmpty()
        {
            ChangePasswordPage changePassword = new ChangePasswordPage(_driver);
            changePassword.NavigateToChangePasswordPage();
            changePassword.EnterCurrentPassword("admin123");
            changePassword.EnterNewPassword("");
            changePassword.EnterConfirmNewPassword("automation432");
            changePassword.SaveBtn.Submit();
            Assert.IsFalse(changePassword.PasswordIsSuccessfullyChanged());
        }

        [Test]
        public void ChangePasswordWithConfirmNewPasswordFieldEmpty()
        {
            ChangePasswordPage changePassword = new ChangePasswordPage(_driver);
            changePassword.NavigateToChangePasswordPage();
            changePassword.EnterCurrentPassword("admin123");
            changePassword.EnterNewPassword("miami96");
            changePassword.EnterConfirmNewPassword("");
            changePassword.SaveBtn.Submit();
            Assert.IsFalse(changePassword.PasswordIsSuccessfullyChanged());
        }

        [Test]
        public void FillInWithAWrongCurrentPassword()
        {
            ChangePasswordPage changePassword = new ChangePasswordPage(_driver);
            changePassword.NavigateToChangePasswordPage();
            changePassword.EnterCurrentPassword("Admin123!");
            changePassword.EnterNewPassword("812lambda");
            changePassword.EnterConfirmNewPassword("812lambda");
            changePassword.SaveBtn.Submit();
            Assert.IsFalse(changePassword.PasswordIsSuccessfullyChanged());
        }

        [Test]
        public void FillInNewPasswordWithOneChar() //password should have at least 7 characters
        {
            ChangePasswordPage changePassword = new ChangePasswordPage(_driver);
            changePassword.NavigateToChangePasswordPage();
            changePassword.EnterCurrentPassword("admin123");
            changePassword.EnterNewPassword("a");
            changePassword.EnterConfirmNewPassword("a");
            changePassword.SaveBtn.Submit();
            Assert.IsFalse(changePassword.PasswordIsSuccessfullyChanged());
        }

        [Test]
        public void FillInNewPasswordWithMoreThanMaxCharacters() //password should not exceed 64 characters
        {
            ChangePasswordPage changePassword = new ChangePasswordPage(_driver);
            changePassword.NavigateToChangePasswordPage();
            changePassword.EnterCurrentPassword("admin123");
            changePassword.EnterNewPassword("1234567891011121314562t1516718rewewret5tttttttttttggggggggggggggdffffffffffd5631");
            changePassword.EnterConfirmNewPassword("1234567891011121314562t1516718rewewret5tttttttttttggggggggggggggdffffffffffd5631");
            changePassword.SaveBtn.Submit();
            Assert.IsFalse(changePassword.PasswordIsSuccessfullyChanged());
        }

        [Test]
        public void FillInWithSameNewPasswordAsCurrent()
        {
            ChangePasswordPage changePassword = new ChangePasswordPage(_driver);
            changePassword.NavigateToChangePasswordPage();
            changePassword.EnterCurrentPassword("admin123");
            changePassword.EnterNewPassword("admin123");
            changePassword.EnterConfirmNewPassword("admin123");
            changePassword.SaveBtn.Submit();
            Assert.IsFalse(changePassword.PasswordIsSuccessfullyChanged());
        }
    }
}
