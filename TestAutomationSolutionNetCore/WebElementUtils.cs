using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace TestAutomationSolutionNetCore
{
    public static class WebElementUtils
    {
        public static void WaitForElementPresent(this IWebElement element, IWebDriver _driver)
        {
            var wait = new WebDriverWait(_driver, TimeSpan.FromMinutes(1));
            wait.Until(driver => element.Displayed);
        }
    }
}