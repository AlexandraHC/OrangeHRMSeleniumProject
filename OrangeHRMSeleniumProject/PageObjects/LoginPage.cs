using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace OrangeHRMSeleniumProject.PageObjects
{
    public class LoginPage
    {
        private IWebDriver _driver;
        public LoginPage(IWebDriver driver) 
        { 
            _driver = driver;
        }

        public By TxtUserNameLocator => By.Name("username");
        public By TxtPasswordLocator => By.Name("password");
        public By UserBtnLocator => By.ClassName("oxd-userdropdown-name");
        public By LogoutBtnLocator => By.LinkText("Logout");
        public By ForgotPasswordElementLocator => By.ClassName("orangehrm-login-forgot-header");
        public By ValidationErrorMessageLocator => By.ClassName("oxd-input-field-error-message");
        public By InvalidCredentialsMessageLocator => By.ClassName("oxd-alert-content-text");
        public By UserDropdownLocator => By.ClassName("oxd-userdropdown-tab");
        public IWebElement TxtUserName => _driver.FindElement(TxtUserNameLocator);
        public IWebElement TxtPassword => _driver.FindElement(TxtPasswordLocator);
        public IWebElement LoginBtn => _driver.FindElement(By.ClassName("orangehrm-login-button"));     
        public IWebElement UserBtn => _driver.FindElement(UserBtnLocator);     
        public IWebElement LogoutBtn => _driver.FindElement(LogoutBtnLocator);

        public void Login(string username, string password)
        {
            WebDriverWait waitUserNameElement = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
            var userNameElement = waitUserNameElement.Until(ExpectedConditions.ElementToBeClickable(TxtUserNameLocator));

            WebDriverWait waitPasswordElement = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
            var passwordElement = waitPasswordElement.Until(ExpectedConditions.ElementToBeClickable(TxtPasswordLocator));

            userNameElement.SendKeys(username);
            passwordElement.SendKeys(password);
        }

        public bool IsLoggedIn()
        {
            return IsElementDisplayed(UserBtnLocator);
        }

        public void Logout()
        {
            LogoutBtn.Click();
        }

        public bool IsLoggedOut()
        {
            return IsElementDisplayed(TxtUserNameLocator);
        }

        private bool IsElementDisplayed(By locator)
        {
            WebDriverWait waitElement = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
            var element = waitElement.Until(ExpectedConditions.ElementIsVisible(locator));

            return element.Displayed;
        }

    }
}
