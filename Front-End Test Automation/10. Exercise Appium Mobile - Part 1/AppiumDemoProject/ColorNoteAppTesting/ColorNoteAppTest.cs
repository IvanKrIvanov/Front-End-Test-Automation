using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.Service;

namespace ColorNoteAppTesting
{
	public class ColorNoteAppTest
	{
		private AndroidDriver _driver;
		private AppiumLocalService _service;
		private AppiumLocalService _appiumLocalService;

		[OneTimeSetUp]
		public void Setup()
		{
			_appiumLocalService = new AppiumServiceBuilder().WithIPAddress("127.0.0.1").UsingPort(4723).Build();
			_appiumLocalService.Start();

			var androidOptions = new AppiumOptions()
			{
				PlatformName = "Android",
				AutomationName = "UiAutomator2",
				DeviceName = "Medium Phone API 34",
				App = @"C:\Users\Ivan\Desktop\Notepad.apk",
				PlatformVersion = "14"
			};

			androidOptions.AddAdditionalAppiumOption("autoGrantPermissions", true);
			_driver = new AndroidDriver(_appiumLocalService, androidOptions);
		
			_driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(30);

			try
			{
				var skipTutorialButton = _driver.FindElement(MobileBy.Id("com.socialnmobile.dictapps.notepad.color.note:id/btn_start_skip"));
				skipTutorialButton.Click();
			}
			catch (NoSuchElementException)
			{

			}
		}

		[OneTimeTearDown] 
		public void Teardown() 
		{ 
			_driver?.Quit();
			_driver?.Dispose();
			_appiumLocalService?.Dispose();
		}

		[Test, Order(1)]
		public void Test_CreateNote()
		{
			IWebElement newNoteButton = _driver.FindElement(MobileBy.Id("com.socialnmobile.dictapps.notepad.color.note:id/main_btn1"));
			newNoteButton.Click();

			IWebElement createNoteText = _driver.FindElement(MobileBy.AndroidUIAutomator("new UiSelector().text(\"Text\")"));
			createNoteText.Click();

			IWebElement noteTextField = _driver.FindElement(MobileBy.Id("com.socialnmobile.dictapps.notepad.color.note:id/edit_note"));
			noteTextField.SendKeys("Test_1");

			IWebElement backButton = _driver.FindElement(MobileBy.Id("com.socialnmobile.dictapps.notepad.color.note:id/back_btn"));
			backButton.Click();
			backButton.Click();

			IWebElement createdNote = _driver.FindElement(MobileBy.Id("com.socialnmobile.dictapps.notepad.color.note:id/title"));

			Assert.That(createdNote, Is.Not.Null, "Note was not created");

			Assert.That(createdNote.Text, Is.EqualTo("Test_1"));

		}

		[Test, Order(2)]
		public void Test_UpdateNote()
		{
			IWebElement newNoteButton = _driver.FindElement(MobileBy.Id("com.socialnmobile.dictapps.notepad.color.note:id/main_btn1"));
			newNoteButton.Click();

			IWebElement createNoteText = _driver.FindElement(MobileBy.AndroidUIAutomator("new UiSelector().text(\"Text\")"));
			createNoteText.Click();

			IWebElement noteTextField = _driver.FindElement(MobileBy.Id("com.socialnmobile.dictapps.notepad.color.note:id/edit_note"));
			noteTextField.SendKeys("Test_2");

			IWebElement backButton = _driver.FindElement(MobileBy.Id("com.socialnmobile.dictapps.notepad.color.note:id/back_btn"));

			backButton.Click();
			backButton.Click();

			IWebElement note = _driver.FindElement(MobileBy.AndroidUIAutomator("new UiSelector().text(\"Test_2\")"));
			note.Click();

			IWebElement editButton = _driver.FindElement(MobileBy.Id("com.socialnmobile.dictapps.notepad.color.note:id/edit_btn"));
			editButton.Click();

			noteTextField = _driver.FindElement(MobileBy.Id("com.socialnmobile.dictapps.notepad.color.note:id/edit_note"));
			noteTextField.Click();
			noteTextField.Clear();
			noteTextField.SendKeys("Edited");

			backButton = _driver.FindElement(MobileBy.Id("com.socialnmobile.dictapps.notepad.color.note:id/back_btn"));
			backButton.Click();
			backButton.Click();

			IWebElement editedNote = _driver.FindElement(MobileBy.AndroidUIAutomator("new UiSelector().text(\"Edited\")"));

			Assert.That(editedNote.Text, Is.EqualTo("Edited"));

		}

		[Test, Order(3)]
		public void Test_DeleteNote()
		{
			IWebElement newNoteButton = _driver.FindElement(MobileBy.Id("com.socialnmobile.dictapps.notepad.color.note:id/main_btn1"));
			newNoteButton.Click();

			IWebElement createNoteText = _driver.FindElement(MobileBy.AndroidUIAutomator("new UiSelector().text(\"Text\")"));
			createNoteText.Click();

			IWebElement noteTextField = _driver.FindElement(MobileBy.Id("com.socialnmobile.dictapps.notepad.color.note:id/edit_note"));
			noteTextField.SendKeys("Note for delete");

			IWebElement backButton = _driver.FindElement(MobileBy.Id("com.socialnmobile.dictapps.notepad.color.note:id/back_btn"));

			backButton.Click();
			backButton.Click();

			IWebElement note = _driver.FindElement(MobileBy.AndroidUIAutomator("new UiSelector().text(\"Note for delete\")"));
			note.Click();

			IWebElement menuButton = _driver.FindElement(MobileBy.Id("com.socialnmobile.dictapps.notepad.color.note:id/menu_btn"));
			menuButton.Click();

			IWebElement deleteButton = _driver.FindElement(MobileBy.AndroidUIAutomator("new UiSelector().text(\"Delete\")"));
			deleteButton.Click();

			IWebElement okButton = _driver.FindElement(MobileBy.Id("android:id/button1"));
			okButton.Click();

			IWebElement deleteNote = _driver.FindElement(MobileBy.XPath("//android.widget.TextView[@text=\"Note for delete\"]"));

			Assert.That(deleteNote.Text, Is.Empty, "Note was note deleted");

		}
	}
}