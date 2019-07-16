using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using Allure.Commons;
using Allure.Commons.Model;
using BoDi;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using TechTalk.SpecFlow;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;

namespace Tests.Steps
{
    [Binding]
    public class Hooks
    {
        private IWebDriver _driver;
        private readonly IObjectContainer _objectContainer;
        private readonly ScenarioContext _scenarioContext;
        private AllureLifecycle _allureLifecycle;
        internal static string AllureConfigDir => TestContext.CurrentContext.WorkDirectory;

        public Hooks(IObjectContainer objectContainer, ScenarioContext scenarioContext)
        {
            _objectContainer = objectContainer;
            _scenarioContext = scenarioContext;
            _allureLifecycle = AllureLifecycle.Instance;
        }

        [OneTimeSetUp]
        public void SetupForAllure()
        {
            Environment.CurrentDirectory = Path.GetDirectoryName(GetType().Assembly.Location);
        }

        [BeforeScenario]
        public void Setup()
        {
            new DriverManager().SetUpDriver(new ChromeConfig());
            _driver = new ChromeDriver();
            _objectContainer.RegisterInstanceAs(_driver);
        }

        [AfterScenario]
        public void TearDown()
        {
            if (_scenarioContext.TestError != null)
                {
                    var path = MakeScreenshot(_driver);
                    _allureLifecycle.AddAttachment(path);
                }

            _driver.Close();

            AddBrowserStackLink();

            AllureHackForScenarioOutlineTests();
        }

        private void AllureHackForScenarioOutlineTests()
        {
            var testResult = GetTestResult();

            var testFullName = TestContext.CurrentContext.Test.FullName;

            var paramsMatch = Regex.Match(testFullName, @"\((.*)\)$");
            if (paramsMatch.Success)
            {
                AllureLifecycle.Instance.UpdateTestCase(testResult.uuid, tc =>
                {
                    tc.name += " " + paramsMatch.Groups[0].Value.Replace(",null", string.Empty);
                    tc.historyId = testFullName;
                });
            }
        }

        [AfterTestRun]
        public static void AfterTests()
        {
            CloseChromeDriverProcesses();
        }

        private static void CloseChromeDriverProcesses()
        {
            var chromeDriverProcesses = Process.GetProcesses().
                Where(pr => pr.ProcessName == "chromedriver");

            if (chromeDriverProcesses.Count() == 0)
            {
                return;
            }

            foreach (var process in chromeDriverProcesses)
            {
                process.Kill();
            }
        }

        private void AddBrowserStackLink()
        {
            var testResult = GetTestResult();
            _allureLifecycle.UpdateTestCase(testResult.uuid, tc =>
            {
                tc.links.Add(new Link()
                {
                    name = "TestLink",
                    url = "http://google.com"
                });
            });
        }

        private TestResult GetTestResult()
        {
            _scenarioContext.TryGetValue(out TestResult testresult);
            return testresult;
        }

        public static string MakeScreenshot(IWebDriver driver, string testName = "screen")
        {
            string projectPath = Path.GetDirectoryName(GetTestAssemblyFolder());
            Screenshot ss = ((ITakesScreenshot)driver).GetScreenshot();
            string fileLocation = $"{projectPath}\\{testName}.png";
            ss.SaveAsFile(fileLocation, ScreenshotImageFormat.Png);
            return fileLocation;
        }

        private static string GetTestAssemblyFolder()
        {
            return Assembly.GetExecutingAssembly().Location;
        }
    }

}