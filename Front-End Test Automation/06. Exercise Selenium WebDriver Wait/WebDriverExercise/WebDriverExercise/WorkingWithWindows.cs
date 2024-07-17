using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebDriverExercise
{
	public class WorkingWithWindows
	{
		private IWebDriver driver;

		[SetUp]
		public void Setup()
		{
			driver = new ChromeDriver();

			driver.Navigate().GoToUrl("https://the-internet.herokuapp.com/windows");

		}		
		
		[TearDown]
		public void TearDown()
		{
			driver.Close();
			driver.Quit();
		}

		[Test]
		public void HandleMultipleWindows()
		{
			driver.FindElement(By.LinkText("Click Here")).Click();

			ReadOnlyCollection<string> windowHandles = driver.WindowHandles;

			Assert.That(windowHandles.Count, Is.EqualTo(2), "There should be two windows open.");

			driver.SwitchTo().Window(windowHandles[1]);

			string newWindowContent = driver.PageSource;

			Assert.IsTrue(newWindowContent.Contains("New Window"),
				"The content of the new window is not as expected.");

			string path = Path.Combine(Directory.GetCurrentDirectory(), "windows.txt");
			if (File.Exists(path))
			{
				File.Delete(path);
			}
			File.AppendAllText(path, "Window handle for new window: "
				+ driver.CurrentWindowHandle + "\n\n");
			File.AppendAllText(path, "The page content: " + newWindowContent + "\n\n");
			
			driver.SwitchTo().Window(windowHandles[0]);

			string originalWindowContent = driver.PageSource;
			Assert.IsTrue(originalWindowContent.Contains("Opening a new window"),
				"The content of the original window is not as expected.");

			File.AppendAllText(path, "Window handle for original window: "
	+ driver.CurrentWindowHandle + "\n\n");
			File.AppendAllText(path, "The page content: " + originalWindowContent + "\n\n");

		}

		[Test]
		public void HandleNoSuchWindowException()
		{
			driver.FindElement(By.LinkText("Click Here")).Click();

			ReadOnlyCollection<string> windowHandles = driver.WindowHandles;

			driver.SwitchTo().Window(windowHandles[1]);

			driver.Close();

			try
			{
				driver.SwitchTo().Window(windowHandles[1]);
			}
			catch (NoSuchWindowException ex)
			{
				string path = Path.Combine(Directory.GetCurrentDirectory(), "windows.txt");
				File.AppendAllText(path, "NoSuchWindowException caught: " + ex.Message + "\n\n");
				Assert.Pass("NoSuchWindowException was correctly handled.");
			}
			catch (Exception ex)
			{
				Assert.Fail("An unexpected exception was thrown: " + ex.Message);
			}
			finally
			{
				driver.SwitchTo().Window(windowHandles[0]);
			}
		}
	}
}
