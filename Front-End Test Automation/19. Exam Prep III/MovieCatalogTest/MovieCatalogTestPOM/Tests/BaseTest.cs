using MovieCatalogTestPOM.Pages;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieCatalogTestPOM.Tests
{
	public class BaseTest
	{

		public IWebDriver driver;

		public LoginPage loginPage;

		public AddMoviePage addMoviePage;

		public AllMoviesPage allMoviesPage;

		public EditMoviePage editMoviePage;

		public WatchedMoviesPage watchedMoviesPage;

		public DeletePage deletePage;

		public Actions actions;

		[OneTimeSetUp]
		public void OneTimeSetup()
		{
			var chromeOptions = new ChromeOptions();
			chromeOptions.AddUserProfilePreference("profile.password_manager_enabled", false);
			chromeOptions.AddArgument("--disable-search-engine-choice-screen");

			driver = new ChromeDriver(chromeOptions);
			driver.Manage().Window.Maximize();
			driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);

			loginPage = new LoginPage(driver);
			addMoviePage = new AddMoviePage(driver);
			allMoviesPage = new AllMoviesPage(driver);
			editMoviePage = new EditMoviePage(driver);
			watchedMoviesPage = new WatchedMoviesPage(driver);
			deletePage = new DeletePage(driver);

			loginPage.OpenPage();
			loginPage.PerformLogin("ivankr@mail.com", "123456");
			actions = new Actions(driver);
		}

		[OneTimeTearDown]
		public void OneTimeTearDown()
		{
			driver.Quit();
			driver.Dispose();
		}

		public string GenerateRandomTitle()
		{
			var random = new Random();
			return "TITLE: " + random.Next(10000, 100000);
		}

		public string GenerateRandomDescription()
		{
			var random = new Random();
			return "DESCRIPTION: " + random.Next(10000, 100000);
		}
	}
	}
}
