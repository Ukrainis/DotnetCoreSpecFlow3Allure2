using FluentAssertions;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;
using TechTalk.SpecFlow;
using TestAutomationSolutionNetCore.PageObjects;

namespace TestAutomationSolutionNetCore.Steps
{
    [Binding]
    public class ShopAuthenticationPageSteps
    {
        private IWebDriver _driver;
        private ShopAuthenticationPage shopAuthenticationPage;

        public ShopAuthenticationPageSteps(IWebDriver driver)
        {
            _driver = driver;
            shopAuthenticationPage = new ShopAuthenticationPage(driver);
        }

        [When(@"I login using (.*) and (.*)")]
        public void WhenILoginUsingUkGmail_ComAndPass(string email, string password)
        {
            shopAuthenticationPage.LoginWithCustomCredentials(email, password);
        }

        [Then(@"I should see next (.*) message")]
        public void ThenIShouldSeeNextAuthenticationFailed(string message)
        {
            shopAuthenticationPage.WarningMessageLabel.WaitForElementPresent(_driver);
            string pageMessage = shopAuthenticationPage.WarningMessageLabel.Text;

            pageMessage.Should().BeEquivalentTo(message);
        }
    }
}
