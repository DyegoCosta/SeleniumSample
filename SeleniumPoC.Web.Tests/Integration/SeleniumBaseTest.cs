using System;
using System.Diagnostics;
using System.IO;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using SimpleBrowser.WebDriver;

namespace SeleniumPoC.Web.Tests.Integration
{
    [TestFixture]
    public abstract class SeleniumBaseTest
    {
        private readonly int _iisPort;
        private readonly string _applicationName;
        private Process _iisProcess;

        protected IWebDriver WebDriver;

        protected SeleniumBaseTest(int iisPort, string applicationName)
        {
            _iisPort = iisPort;
            _applicationName = applicationName;            
        }

        [TestFixtureSetUp]
        public void SeleniumEnviromentSetUp()
        {
            StartIIS();

            WebDriver = new FirefoxDriver();
        }

        [TestFixtureTearDown]
        public void SeleniumEnviromentCleanUp()
        {
            if(!_iisProcess.HasExited)
                _iisProcess.Kill();

            if(WebDriver != null)
                WebDriver.Quit();
        }

        private void StartIIS()
        {
            var applicationPath = GetApplicationPath(_applicationName);
            var programsFilesPath = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles);

            _iisProcess = new Process();
            _iisProcess.StartInfo.FileName = programsFilesPath + "\\IIS Express\\iisexpress.exe";
            _iisProcess.StartInfo.Arguments = string.Format("/path:{0} /port:{1}", applicationPath, _iisPort);
            
            _iisProcess.Start();
        }

        protected virtual string GetApplicationPath(string applicationName)
        {
            var solutionFolder = Path.GetDirectoryName(Path.GetDirectoryName(Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory)));
            
            return Path.Combine(solutionFolder, applicationName);
        }

        public string GetAbsoluteUrl(string relativeUrl)
        {
            if (!relativeUrl.StartsWith("/"))           
                relativeUrl = "/" + relativeUrl;
            
            return String.Format("http://localhost:{0}{1}", _iisPort, relativeUrl);
        }
    }    
}
