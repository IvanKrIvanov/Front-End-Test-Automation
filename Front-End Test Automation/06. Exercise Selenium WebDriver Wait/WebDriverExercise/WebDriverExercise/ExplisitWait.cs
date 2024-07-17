using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebDriverExercise
{
	internal class ExplisitWait
	{
		private IWebDriver driver;
		[SetUp]
		public void Setup()
		{
			driver = new ChromeDriver();
			driver.Navigate().GoToUrl("https://practice.bpbonline.com/");
			driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
		}

		[TearDown]
		public void TearDown()
		{
			driver.Close();
		}

		[Test]
		public void SearcKeyboardTest()
		{
			driver.FindElement(By.Name("keywords")).SendKeys("keyboard");

			driver.FindElement(By.XPath("//input[@title=' Quick Find ']")).Click();

			driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(0);

			try
			{
				WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

				IWebElement buyNowLink = wait.Until(e => e.FindElement(By.LinkText("Buy Now")));

				driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);

				buyNowLink.Click();

				Assert.IsTrue(driver.PageSource.Contains("keyboard"),
					"The product 'keyboard' was not found in the cart page.");
				Console.WriteLine("Scenario completed");
			}
			catch (Exception ex)
			{
				Assert.Fail("Unexpected exception: " + ex.Message);
			}
		}

		[Test]
		public void SearchJunkTest()
		{
			driver.FindElement(By.Name("keywords")).SendKeys("junk");

			driver.FindElement(By.XPath("//input[@title=' Quick Find ']")).Click();

			driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(0);

			try
			{
				WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

				IWebElement buyNowLink = wait.Until(e => e.FindElement(By.LinkText("Buy Now")));

				buyNowLink.Click();
				Assert.Fail("The 'Buy Now' link was found for a non-existing product.");
			}
			catch (WebDriverTimeoutException)
			{
				Assert.Pass("Expected WebDriverTimeoutException was thrown.");
			}
			catch (Exception ex)
			{
				Assert.Fail("Unexpected exception: " + ex.Message);
			}
			finally
			{
				driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
			}
		}
	}
}
