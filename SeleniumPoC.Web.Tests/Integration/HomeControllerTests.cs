using FluentAssertions;
using NUnit.Framework;
using OpenQA.Selenium;

namespace SeleniumPoC.Web.Tests.Integration
{
    [TestFixture]
    public class HomeControllerTests : SeleniumBaseTest
    {
        public HomeControllerTests() : base(22588, "SeleniumPoC.Web") { }

        [Test]
        public void ShowMessageUsingTheButtonClick()
        {
            var url = GetAbsoluteUrl("/Home/Index");

            WebDriver.Navigate().GoToUrl(url);
            WebDriver.FindElement(By.Id("name")).SendKeys("Dyego");
            WebDriver.FindElement(By.Id("go")).Click();

            WebDriver.FindElement(By.Id("message")).Text.Should().Be("Hello Dyego");
        }

        [Test]
        public void ShowMessageUsingTypingEnterOnTheNameInput()
        {
            var url = GetAbsoluteUrl("/Home/Index");

            WebDriver.Navigate().GoToUrl(url);
            
            var element = WebDriver.FindElement(By.Id("name"));
            element.SendKeys("Dyego");
            element.SendKeys(Keys.Enter);

            WebDriver.FindElement(By.Id("message")).Text.Should().Be("Hello Dyego");
        }
    }
}
