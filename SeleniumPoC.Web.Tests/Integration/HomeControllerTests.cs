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
            // Arrange
            var url = GetAbsoluteUrl("/Home/Index");

            // Act
            WebDriver.Navigate().GoToUrl(url);
            WebDriver.FindElement(By.Id("name")).SendKeys("Dyego");
            WebDriver.FindElement(By.Id("go")).Click();

            // Assert
            WebDriver.FindElement(By.Id("message")).Text.Should().Be("Hello Dyego");
        }

        [Test]
        public void ShowMessageUsingTypingEnterOnTheNameInput()
        {
            // Arrange                                                                                                                                                                                                                                                                                          
            var url = GetAbsoluteUrl("/Home/Index");

            // Act
            WebDriver.Navigate().GoToUrl(url);
            
            var element = WebDriver.FindElement(By.Id("name"));
            element.SendKeys("Dyego");
            element.SendKeys(Keys.Enter);

            // Assert
            WebDriver.FindElement(By.Id("message")).Text.Should().Be("Hello Dyego");
        }
    }
}
