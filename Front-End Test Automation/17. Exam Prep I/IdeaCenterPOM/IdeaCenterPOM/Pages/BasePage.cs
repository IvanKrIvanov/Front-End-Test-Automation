﻿using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace IdeaCenterPOM.Pages
{
	public class BasePage
	{
		protected IWebDriver driver;
		protected WebDriverWait wait;
		protected static readonly string BaseURL = "http://softuni-qa-loadbalancer-2137572849.eu-north-1.elb.amazonaws.com:83/";

        public BasePage(IWebDriver driver)
        {
			this.driver = driver;
			wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        }

		public IWebElement HomeLink => driver.FindElement(By.XPath("//img[@class='rounded-circle']"));

		public IWebElement MyProfileLink => driver.FindElement(By.XPath("//a[@class='nav-link' and text()='My Profile']"));

		public IWebElement MyIdeasLink => driver.FindElement(By.XPath("//a[@class='nav-link' and text()='My Ideas']"));

		public IWebElement CreateIdeaLink => driver.FindElement(By.XPath("//a[@class='nav-link' and text()='Create Idea']"));

		public IWebElement LogoutLink => driver.FindElement(By.XPath("//a[@class='btn btn-primary me-3' and contains(text(),'Logout')]"));

		public IWebElement LoginLink => driver.FindElement(By.XPath("//a[@class='btn btn-outline-info px-3 me-2' and contains(text(),'Login')]"));

		public IWebElement SignUpLink => driver.FindElement(By.XPath("//a[@class='btn btn-primary me-3' and contains(text(),'Sign up for free')]"));
	}
}
