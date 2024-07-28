using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.Service;

namespace AppiumDemoProject
{
	public class SummatorAppTest
	{
		private AndroidDriver _driver;
		private AppiumLocalService _service;

		[OneTimeSetUp]
		public void Setup()
		{
			_service = new AppiumServiceBuilder().WithIPAddress("127.0.0.1").UsingPort(4723).Build();
			_service.Start();

			var androidOptions = new AppiumOptions
			{
				PlatformName = "Android",
				AutomationName = "UiAutomator2",
				DeviceName = "Medium Phone API 34",
				App = @"C:\Users\Ivan\Desktop\com.example.androidappsummator.apk",
				PlatformVersion = "14"
			};

			_driver = new AndroidDriver(_service, androidOptions);
		}
		[OneTimeTearDown] 
		public void TearDown()
		{
			_driver?.Quit();
			_driver?.Dispose();
			_service?.Dispose();
		}

		[Test]
		public void TestWithValidData()
		{
			IWebElement field1 = _driver.FindElement(MobileBy.Id("com.example.androidappsummator:id/editText1"));
			field1.Clear();
			field1.SendKeys("1");

			IWebElement field2 = _driver.FindElement(MobileBy.Id("com.example.androidappsummator:id/editText2"));
			field2.Clear();
			field2.SendKeys("2");

			IWebElement buttonCalculate = _driver.FindElement(MobileBy.Id("com.example.androidappsummator:id/buttonCalcSum"));
			buttonCalculate.Click();

			IWebElement resultField = _driver.FindElement(MobileBy.Id("com.example.androidappsummator:id/editTextSum"));

			Assert.That(resultField.Text, Is.EqualTo("3"));
		}

		[Test]
		public void TestWithInvalidData_ClickOnlyCalcButton()
		{
			IWebElement field1 = _driver.FindElement(MobileBy.Id("com.example.androidappsummator:id/editText1"));
			field1.Clear();

			IWebElement field2 = _driver.FindElement(MobileBy.Id("com.example.androidappsummator:id/editText2"));
			field2.Clear();

			IWebElement buttonCalculate = _driver.FindElement(MobileBy.Id("com.example.androidappsummator:id/buttonCalcSum"));
			buttonCalculate.Click();

			IWebElement resultField = _driver.FindElement(MobileBy.Id("com.example.androidappsummator:id/editTextSum"));

			Assert.That(resultField.Text, Is.EqualTo("error"));
		}

		[Test]
		public void TestWithInvalidData_FillOnlyFirstField()
		{
			IWebElement field1 = _driver.FindElement(MobileBy.Id("com.example.androidappsummator:id/editText1"));
			field1.Clear();
			field1.SendKeys("2");

			IWebElement field2 = _driver.FindElement(MobileBy.Id("com.example.androidappsummator:id/editText2"));
			field2.Clear();

			IWebElement buttonCalculate = _driver.FindElement(MobileBy.Id("com.example.androidappsummator:id/buttonCalcSum"));
			buttonCalculate.Click();

			IWebElement resultField = _driver.FindElement(MobileBy.Id("com.example.androidappsummator:id/editTextSum"));

			Assert.That(resultField.Text, Is.EqualTo("error"));
		}

		[Test]
		public void TestWithInvalidData_FillOnlySecondField()
		{
			IWebElement field1 = _driver.FindElement(MobileBy.Id("com.example.androidappsummator:id/editText1"));
			field1.Clear();

			IWebElement field2 = _driver.FindElement(MobileBy.Id("com.example.androidappsummator:id/editText2"));
			field2.Clear();
			field2.SendKeys("2");

			IWebElement buttonCalculate = _driver.FindElement(MobileBy.Id("com.example.androidappsummator:id/buttonCalcSum"));
			buttonCalculate.Click();

			IWebElement resultField = _driver.FindElement(MobileBy.Id("com.example.androidappsummator:id/editTextSum"));

			Assert.That(resultField.Text, Is.EqualTo("error"));
		}

		[Test]

		[TestCase("10", "10", "20")]
		[TestCase("100", "100", "200")]
		[TestCase("10.9", "10.20", "21.10")]
		public void TestWithValidData_Parametrized(string input1, string input2, string expectedResult)
		{
			IWebElement field1 = _driver.FindElement(MobileBy.Id("com.example.androidappsummator:id/editText1"));
			field1.Clear();
			field1.SendKeys(input1);

			IWebElement field2 = _driver.FindElement(MobileBy.Id("com.example.androidappsummator:id/editText2"));
			field2.Clear();
			field2.SendKeys(input2);

			IWebElement buttonCalculate = _driver.FindElement(MobileBy.Id("com.example.androidappsummator:id/buttonCalcSum"));
			buttonCalculate.Click();

			IWebElement resultField = _driver.FindElement(MobileBy.Id("com.example.androidappsummator:id/editTextSum"));

			Assert.That(resultField.Text, Is.EqualTo(expectedResult));
		}
	}
}