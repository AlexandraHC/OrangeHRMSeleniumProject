using OpenQA.Selenium;

namespace OrangeHRMSeleniumProject.PageObjects
{
    public class ChangePasswordPage
    {
        private IWebDriver _driver;
        public ChangePasswordPage(IWebDriver driver)
        {
            _driver = driver;
        }

        public By UserBtnLocator => By.ClassName("oxd-userdropdown-name");
        public By ChangePasswordBtnLocator => By.LinkText("Change Password");
        public IWebElement UserBtn => _driver.FindElement(UserBtnLocator);
        public IWebElement ChangePasswordBtn => _driver.FindElement(ChangePasswordBtnLocator);
        public By TxtCurrentPasswordLocator => By.XPath("//*[@id=\"app\"]/div[1]/div[2]/div[2]/div/div/form/div[1]/div/div[2]/div/div[2]/input");
        public By TxtNewPasswordLocator => By.XPath("//*[@id=\"app\"]/div[1]/div[2]/div[2]/div/div/form/div[2]/div/div[1]/div/div[2]/input");
        public By TxtNewCurrentPasswordLocator => By.XPath("//*[@id=\"app\"]/div[1]/div[2]/div[2]/div/div/form/div[2]/div/div[2]/div/div[2]/input");
        public IWebElement TxtCurrentPassword => _driver.FindElement(TxtCurrentPasswordLocator);
        public IWebElement TxtNewPassword => _driver.FindElement(TxtNewPasswordLocator);
        public IWebElement TxtNewCurrentPassword => _driver.FindElement(TxtNewCurrentPasswordLocator);
        public IWebElement SaveBtn => _driver.FindElement(By.ClassName("orangehrm-left-space"));
        public IWebElement SuccessfullyPopup => _driver.FindElement(By.Id("oxd-toaster_1"));

        public void NavigateToChangePasswordPage()
        {
            UserBtn.Click();
            ChangePasswordBtn.Click();
        }

        public void EnterCurrentPassword(string currentPassword)
        {
            TxtCurrentPassword.SendKeys(currentPassword);
        }
        public void EnterNewPassword(string newPassword)
        {
            TxtNewPassword.SendKeys(newPassword);
        }
        public void EnterConfirmNewPassword(string confirmNewCurrentPassword)
        {
            TxtNewCurrentPassword.SendKeys(confirmNewCurrentPassword);
        }
        public void ClickLoginButton()
        {
            SaveBtn.Click();
        }
        public bool PasswordIsSuccessfullyChanged()
        {
            return SuccessfullyPopup.Displayed;
        }

    }
}
