using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieCatalogTestPOM.Pages
{
	public class LoginPage : BasePage
	{
		public LoginPage(IWebDriver driver) : base(driver)
		{

		}

		public string Url = BaseUrl + "/User/Login";

		public IWebElement EmailInput => driver.FindElement(By.XPath("//input[@name='Email']"));

		public IWebElement PasswordInput => driver.FindElement(By.XPath("//input[@name='Password']"));

		public IWebElement LoginButton => driver.FindElement(By.XPath("//button[@class='btn warning']"));

		public void OpenPage()
		{
			driver.Navigate().GoToUrl(Url);
		}

		public void PerformLogin(string email, string password)
		{
			EmailInput.Clear();
			EmailInput.SendKeys(email);

			PasswordInput.Clear();
			PasswordInput.SendKeys(password);

			LoginButton.Click();
		}
	}
}
