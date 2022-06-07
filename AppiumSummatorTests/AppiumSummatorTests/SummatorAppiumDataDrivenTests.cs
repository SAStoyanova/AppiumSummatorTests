using NUnit.Framework;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Enums;
using OpenQA.Selenium.Appium.Service;
using OpenQA.Selenium.Appium.Windows;
using System;

namespace AppiumSummatorTests
{
    public class SummatorAppiumDataDrivenTests
    {
        private WindowsDriver<WindowsElement> driver;
        private const string AppiumServer = "http://[::1]:4723/wd/hub";
        private AppiumOptions options;

        WindowsElement field1;
        WindowsElement field2;
        WindowsElement calcButton;
        WindowsElement result;
        
        [OneTimeSetUp]
        public void OpenApp()
        {
            this.options = new AppiumOptions() { PlatformName = "Windows" };
            //app location
            options.AddAdditionalCapability(MobileCapabilityType.App, @"C:\Users\Sunny\Desktop\QA auto\6\SummatorDesktopApp.exe");
            this.driver = new WindowsDriver<WindowsElement>(new Uri(AppiumServer), options);

            field1 = driver.FindElementByAccessibilityId("textBoxFirstNum");
            field2 = driver.FindElementByAccessibilityId("textBoxSecondNum");
            calcButton = driver.FindElementByAccessibilityId("buttonCalc");
            result = driver.FindElementByAccessibilityId("textBoxSum");
        }

        [OneTimeTearDown]
        public void CloseApp()
        {
            driver.CloseApp();
        }

        [TestCase("5", "15", "20")]
        [TestCase("-6", "-3", "-9")]
        [TestCase("2", "-5", "-3")]
        [TestCase("0", "0", "0")]
        [TestCase("99999999999999999999999999999", "153", "error")]
        [TestCase("invalid", "string", "error")]
        [TestCase("", "", "error")]
        public void Test_Summator_DataDriven(string num1, string num2, string sumResult)
        {
            field1.Clear();
            field1.SendKeys(num1);

            field2.Clear();
            field2.SendKeys(num2);

            calcButton.Click();

            Assert.That(sumResult, Is.EqualTo(result.Text));
        }

    }
}