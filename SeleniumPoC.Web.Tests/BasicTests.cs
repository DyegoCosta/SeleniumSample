using System;
using FluentAssertions;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace SeleniumPoC.Web.Tests
{
    [TestFixture]
    public class BasicTests
    {
        private IWebDriver _driver;

        [TestFixtureSetUp]
        public void TestSetUp()
        {
            _driver = new ChromeDriver(@"C:\Users\Dyego\Downloads"); 
            //_driver = new RemoteWebDriver(new Uri("http://127.0.0.1:4444/wd/hub"), DesiredCapabilities.HtmlUnitWithJavaScript());
            //_driver = new RemoteWebDriver(DesiredCapabilities.HtmlUnitWithJavaScript());            
        }             

        [TestFixtureTearDown]
        public void CleanUp()
        {
            if(_driver != null)
                _driver.Quit();
        }

        [Test]        
        public void Test()
        {
            _driver.Navigate().GoToUrl("http://www.google.com.br");

            var query = _driver.FindElement(By.Name("q"));

            query.SendKeys("Cheese");

            query.Submit();

            var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
            wait.Until(d => d.Title.ToLower().StartsWith("cheese"));

            _driver.Title.Should().Be("cheese - Pesquisa Google");
        }
    }
}
