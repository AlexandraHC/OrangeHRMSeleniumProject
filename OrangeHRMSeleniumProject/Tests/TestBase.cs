using NUnit.Framework.Interfaces;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium;
using OrangeHRMSeleniumProject.Driver;
using OrangeHRMSeleniumProject.Utils.Common;
using OrangeHRMSeleniumProject.Utils;

namespace OrangeHRMSeleniumProject
{
    public class TestBase
    {
        protected IWebDriver _driver;
        private readonly DriverType _driverType;
        protected Browser Browser { get; private set; }

        public TestBase(DriverType driverType)
        {
            _driverType = driverType;
        }
        public void Setup()
        {
            ExtentReporting.CreateTest(TestContext.CurrentContext.Test.MethodName + " on " + _driverType.ToString());
            _driver = GetDriverType(_driverType);
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            _driver.Navigate().GoToUrl("https://opensource-demo.orangehrmlive.com/web/index.php/auth/login");
            _driver.Manage().Window.Maximize();

            Browser = new Browser(_driver);
        }

        //method to get the DriverType
        private IWebDriver GetDriverType(DriverType driverType)
        {
            var chromeOptions = new ChromeOptions();
            chromeOptions.AddArguments("--headless=new");

            var edgeOptions = new EdgeOptions();
            edgeOptions.AddArguments("headless");

            var firefoxOptions = new FirefoxOptions();
            firefoxOptions.AddArguments("--headless");

            return driverType switch
            {
                DriverType.Chrome => new ChromeDriver(chromeOptions),
                DriverType.Firefox => new FirefoxDriver(firefoxOptions),
                DriverType.Edge => new EdgeDriver(edgeOptions),
                _ => _driver
            };
        }

        [TearDown]
        public void TearDown()
        {
            EndTest();
            ExtentReporting.EndReporting();

            _driver.Quit();
            _driver.Dispose();
        }

        public void EndTest()
        {
            var testStatus = TestContext.CurrentContext.Result.Outcome.Status;
            var message = TestContext.CurrentContext.Result.Message;

            switch (testStatus)
            {
                case TestStatus.Failed:
                    ExtentReporting.LogFail($"Test has failed {message}");
                    break;
                case TestStatus.Skipped:
                    ExtentReporting.LogInfo($"Test skipped {message}");
                    break;
                case TestStatus.Passed:
                    ExtentReporting.LogPass($"Test passed {message}");
                    break;
                default:
                    break;
            }

            ExtentReporting.LogScreenshot("Ending test", Browser.GetScreenshot());
        }
    }
}