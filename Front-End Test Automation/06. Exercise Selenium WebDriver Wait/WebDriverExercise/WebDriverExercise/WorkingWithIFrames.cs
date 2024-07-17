using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebDriverExercise
{
	public class WorkingWithIFrames
	{
		IWebDriver driver;
		WebDriverWait wait;

		[SetUp]
		public void Setup()
		{
			
			driver = new ChromeDriver();
			driver.Url = "https://codepen.io/pervillalva/full/abPoNLd";
			driver.Manage().Window.Maximize();
			driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
			wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
		}

		[Test]
		public void TestFrameByIndex()
		{
			
			wait.Until(ExpectedConditions.FrameToBeAvailableAndSwitchToIt(By.TagName("iframe")));

			var dropdownButton = wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector(".dropbtn")));
			dropdownButton.Click();

			var dropdownLinks = wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(By.CssSelector(".dropdown-content a")));

			foreach (var link in dropdownLinks)
			{
				Console.WriteLine(link.Text);
				Assert.IsTrue(link.Displayed, "Link inside the dropdown is not displayed as expected.");
			}

			driver.SwitchTo().DefaultContent();
		}


		[Test]
		public void TestFrameById()
		{
			wait.Until(ExpectedConditions.FrameToBeAvailableAndSwitchToIt("result"));

			var dropdownButton = wait.Until(ExpectedConditions
				.ElementIsVisible(By.CssSelector(".dropbtn")));
			dropdownButton.Click();

			var dropdownLinks = wait.Until(ExpectedConditions
				.VisibilityOfAllElementsLocatedBy(By.CssSelector(".dropdown-content a")));

			foreach (var link in dropdownLinks)
			{
				Console.WriteLine(link.Text);
				Assert.IsTrue(link.Displayed, "Link inside the dropdown is not displayed as expected.");
			}

			driver.SwitchTo().DefaultContent();
		}

		[Test]
		public void TestFrameByWebElement()
		{
			var frameElement = wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("#result")));

			driver.SwitchTo().Frame(frameElement);

			var dropdownButton = wait.Until(ExpectedConditions
				.ElementIsVisible(By.CssSelector(".dropbtn")));
			dropdownButton.Click();

			var dropdownLinks = wait.Until(ExpectedConditions
				.VisibilityOfAllElementsLocatedBy(By.CssSelector(".dropdown-content a")));

			foreach (var link in dropdownLinks)
			{
				Console.WriteLine(link.Text);
				Assert.IsTrue(link.Displayed, "Link inside the dropdown is not displayed as expected.");
			}

			driver.SwitchTo().DefaultContent();
		}

		[TearDown]
		public void TearDown()
		{
			driver.Quit();
		}
	}
}
