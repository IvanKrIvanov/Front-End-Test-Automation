using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.Service;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium;

namespace AppiumDemoProject
{
	public class SummatorAppPOMTest
	{
		private AndroidDriver _driver;
		private AppiumLocalService _service;
		private SummatorPage _page;

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
			_page =	new SummatorPage(_driver);
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
			var result = _page.Calculate("1", "2");

			Assert.That(result, Is.EqualTo("3"));
		}

		[Test]
		public void TestWithInvalidData_ClickOnlyCalcButton()
		{
			_page.ClearFields();
			_page.ButtonCalculate.Click();

			Assert.That(_page.ResultField.Text, Is.EqualTo("error"));
		}

		[Test]
		public void TestWithInvalidData_FillOnlyFirstField()
		{
			_page.ClearFields();
			_page.Field1.SendKeys("1");

			_page.ButtonCalculate.Click();

			Assert.That(_page.ResultField.Text, Is.EqualTo("error"));

		}

		[Test]
		public void TestWithInvalidData_FillOnlySecondField()
		{
			_page.ClearFields();
			_page.Field2.SendKeys("1");

			_page.ButtonCalculate.Click();

			Assert.That(_page.ResultField.Text, Is.EqualTo("error"));
		}

		[Test]

		[TestCase("10", "10", "20")]
		[TestCase("100", "100", "200")]
		[TestCase("10.9", "10.20", "21.10")]
		public void TestWithValidData_Parametrized(string input1, string input2, string expectedResult)
		{
			var result = _page.Calculate(input1, input2);

			Assert.That(result, Is.EqualTo(expectedResult));
		}

	}
}
