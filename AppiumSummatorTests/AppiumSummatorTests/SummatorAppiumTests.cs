using NUnit.Framework;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Enums;
using OpenQA.Selenium.Appium.Service;
using OpenQA.Selenium.Appium.Windows;
using System;

namespace AppiumSummatorTests
{
    public class SummatorAppiumTests
    {
        private WindowsDriver<WindowsElement> driver;
        private const string AppiumServer = "http://[::1]:4723/wd/hub";
        private AppiumOptions options;

        //private AppiumLocalService appiumLocal;
        
        [OneTimeSetUp]
        public void OpenApp()
        {
            this.options = new AppiumOptions() { PlatformName = "Windows" };
            //app location
            options.AddAdditionalCapability(MobileCapabilityType.App, @"C:\Users\Sunny\Desktop\QA auto\6\SummatorDesktopApp.exe");
            this.driver = new WindowsDriver<WindowsElement>(new Uri(AppiumServer), options);
           
            //appiumLocal = new AppiumServiceBuilder().UsingAnyFreePort().Build();
            //appiumLocal.Start();
            //this.driver = new WindowsDriver<WindowsElement>(appiumLocal, options);

        }

        [OneTimeTearDown]
        public void CloseApp()
        {
            driver.CloseApp();
            //appiumLocal.Dispose();
        }

        [Test]
        public void Test_SumTwoPositiveNums()
        {
            var num1 = driver.FindElementByAccessibilityId("textBoxFirstNum");
            num1.Clear();
            num1.SendKeys("5");

            var num2 = driver.FindElementByAccessibilityId("textBoxSecondNum");
            num2.Clear();
            num2.SendKeys("15");

            var calcButton = driver.FindElementByAccessibilityId("buttonCalc");
            calcButton.Click();

            var result= driver.FindElementByAccessibilityId("textBoxSum").Text;

            Assert.That(result, Is.EqualTo("20"));
        }

        [Test]
        public void Test_Sum_InvalidValues()
        {
            var num1 = driver.FindElementByAccessibilityId("textBoxFirstNum");
            num1.Clear();
            num1.SendKeys("ala");

            var num2 = driver.FindElementByAccessibilityId("textBoxSecondNum");
            num2.Clear();
            num2.SendKeys("bala");

            var calcButton = driver.FindElementByAccessibilityId("buttonCalc");
            calcButton.Click();

            var result = driver.FindElementByAccessibilityId("textBoxSum").Text;

            Assert.That(result, Is.EqualTo("error"));
        }

        [Test]
        public void Test_Sum_EmptyArrea()
        {
            var num1 = driver.FindElementByAccessibilityId("textBoxFirstNum");
            num1.Clear();
            num1.SendKeys("");

            var num2 = driver.FindElementByAccessibilityId("textBoxSecondNum");
            num2.Clear();
            num2.SendKeys("");

            var calcButton = driver.FindElementByAccessibilityId("buttonCalc");
            calcButton.Click();

            var result = driver.FindElementByAccessibilityId("textBoxSum").Text;

            Assert.That(result, Is.EqualTo("error"));
        }

        [Test]
        public void Test_Sum_LongValues()
        {
            var num1 = driver.FindElementByAccessibilityId("textBoxFirstNum");
            num1.Clear();
            num1.SendKeys("999999999999999999999999999999");

            var num2 = driver.FindElementByAccessibilityId("textBoxSecondNum");
            num2.Clear();
            num2.SendKeys("123");

            var calcButton = driver.FindElementByAccessibilityId("buttonCalc");
            calcButton.Click();

            var result = driver.FindElementByAccessibilityId("textBoxSum").Text;

            Assert.That(result, Is.EqualTo("error"));
        }

        [Test]
        public void Test_SumTwoNegativNums()
        {
            var num1 = driver.FindElementByAccessibilityId("textBoxFirstNum");
            num1.Clear();
            num1.SendKeys("-3");

            var num2 = driver.FindElementByAccessibilityId("textBoxSecondNum");
            num2.Clear();
            num2.SendKeys("-8");

            var calcButton = driver.FindElementByAccessibilityId("buttonCalc");
            calcButton.Click();

            var result = driver.FindElementByAccessibilityId("textBoxSum").Text;

            Assert.That(result, Is.EqualTo("-11"));
        }

        [Test]
        public void Test_SumNegativAndPositivNums()
        {
            var num1 = driver.FindElementByAccessibilityId("textBoxFirstNum");
            num1.Clear();
            num1.SendKeys("-3");

            var num2 = driver.FindElementByAccessibilityId("textBoxSecondNum");
            num2.Clear();
            num2.SendKeys("5");

            var calcButton = driver.FindElementByAccessibilityId("buttonCalc");
            calcButton.Click();

            var result = driver.FindElementByAccessibilityId("textBoxSum").Text;

            Assert.That(result, Is.EqualTo("2"));
        }
    }
}