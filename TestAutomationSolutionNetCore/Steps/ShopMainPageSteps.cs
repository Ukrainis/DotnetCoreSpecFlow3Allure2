using FluentAssertions;
using NUnit.Framework.Internal;
using OpenQA.Selenium;
using TechTalk.SpecFlow;
using TestAutomationSolutionNetCore;
using TestAutomationSolutionNetCore.PageObjects;

namespace Tests.Steps
{
    [Binding]
    public class ShopMainPageSteps
    {
        private IWebDriver _driver;
        private ShopMainPage shopMainPage;

        public ShopMainPageSteps(IWebDriver driver)
        {
            _driver = driver;
            shopMainPage = new ShopMainPage(driver);
        }

        [Given(@"I am navigated to Shop application main page")]
        public void GivenIAmNavigatedToShopApplication()
        {
            _driver.Navigate().GoToUrl("http://automationpractice.com/index.php");
        }

        [Then(@"I am redirected to Shop application main page")]
        public void ThenIAmRedirectedToShopApplicationMainPage()
        {
            shopMainPage.MainPageSlider.WaitForElementPresent(_driver);
        }

        [Then(@"I see that page title equals to ""(.*)""")]
        public void ThenISeeThatPageTitleEqualsTo(string pageTitleExpected)
        {
            var titleActual = _driver.Title;

            titleActual.Should().BeEquivalentTo(pageTitleExpected);
        }

        [Then(@"I see that shop phone number is ""(.*)""")]
        public void ThenISeeThatShopPhoneNumberIs(string phoneNumberExpected)
        {
            var phoneNumberActual = shopMainPage.ShopPhoneNumberLabel.Text;

            phoneNumberActual.Should().BeEquivalentTo(phoneNumberExpected);
        }


    }
}