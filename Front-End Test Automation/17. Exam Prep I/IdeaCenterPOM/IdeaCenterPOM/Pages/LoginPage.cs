using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdeaCenterPOM.Pages
{
	internal class LoginPage : BasePage
	{
		public LoginPage(IWebDriver driver) : base(driver)
		{

		}

		public string Url = BaseURL + "/Users/Login";

		public IWebElement EmailField => driver.FindElement(By.XPath("//input[@name='Email']"));

		public IWebElement PasswordField => driver.FindElement(By.XPath("//input[@name='Password']"));

		public IWebElement SignInButton => driver.FindElement(By.XPath("//button[@class='btn btn-primary btn-lg btn-block']"));

		public void Login(string email, string password)
		{
			EmailField.SendKeys(email);
			PasswordField.SendKeys(password);
			SignInButton.Click();
		}

		public void OpenPage()
		{
			driver.Navigate().GoToUrl(Url);
		}
	}
}
