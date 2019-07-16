using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using System;

namespace TestAutomationSolutionNetCore.PageObjects
{
    public class ShopAuthenticationPage
    {

        public ShopAuthenticationPage(IWebDriver driver)
        {
            PageFactory.InitElements(driver, this);
        }

        [FindsBy(How = How.CssSelector, Using = ".alert.alert-danger li")]
        public IWebElement WarningMessageLabel { get; set; }

        [FindsBy(How = How.Id, Using = "email")]
        public IWebElement LoginEmailTxt { get; set; }

        [FindsBy(How = How.Id, Using = "passwd")]
        public IWebElement LoginPasswordTxt { get; set; }

        [FindsBy(How = How.Id, Using = "SubmitLogin")]
        public IWebElement LoginBtn { get; set; }

        public void LoginWithCustomCredentials(string email, string password)
        {
            Console.WriteLine($"Log in using User email: {email} and password: {password}");
            LoginEmailTxt.SendKeys(email);
            LoginPasswordTxt.SendKeys(password);
            LoginBtn.Click();
        }
    }
}
