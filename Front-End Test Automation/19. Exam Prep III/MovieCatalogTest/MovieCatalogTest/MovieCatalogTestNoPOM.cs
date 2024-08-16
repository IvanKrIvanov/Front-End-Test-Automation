using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;

namespace MovieCatalog
{
	[TestFixture]
	public class MovieCatalogTests
	{
		protected IWebDriver driver;

		private static readonly string BaseUrl = "http://moviecatalog-env.eba-ubyppecf.eu-north-1.elasticbeanstalk.com/";
		private static string? lastMovieTitle;
		private static string? lastMovieDescription;
		private Actions actions;

		[OneTimeSetUp]
		public void OneTimeSetup()
		{
			var chromeOptions = new ChromeOptions();
			chromeOptions.AddUserProfilePreference("profile.password_manager_enabled", false);

			string chromeDriverPath = @"C:\webDrivers\chromedriver.exe";
			driver = new ChromeDriver(chromeDriverPath, chromeOptions);
			driver.Manage().Window.Maximize();
			driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);

			// Initialize Actions instance
			actions = new Actions(driver);

			// Log in to the application
			driver.Navigate().GoToUrl(BaseUrl);
			var loginBtn = driver.FindElement(By.Id("loginBtn"));

			// Scroll to the login button
			actions.MoveToElement(loginBtn).Perform();

			// Click the login button
			loginBtn.Click();

			// Fill in login details
			driver.FindElement(By.Id("form2Example17")).SendKeys("ivankr@mail.bg");
			driver.FindElement(By.Id("form2Example27")).SendKeys("123456");
			driver.FindElement(By.CssSelector("button.btn.warning[type='submit']")).Click();
		}

		[Test, Order(1)]
		public void AddMovieWithoutTitleTest()
		{
			string invalidMovieTitle = "";

			// Navigate to the Add Movie page
			driver.Navigate().GoToUrl($"{BaseUrl}Catalog/Add");

			// Enter an invalid movie title (empty)
			driver.FindElement(By.CssSelector("input#form2Example17")).SendKeys(invalidMovieTitle);

			// Find and click the 'Add' button
			var add = driver.FindElement(By.CssSelector("button.btn.warning[type='submit']"));
			actions.MoveToElement(add).Perform();
			add.Click();

			// Verify the error message
			var messageElement = driver.FindElement(By.CssSelector("div.toast-message"));
			Assert.That(messageElement.Text.Trim(), Is.EqualTo("The Title field is required."), "The title error message is not displayed as expected.");
		}

		[Test, Order(2)]
		public void AddMovieWithoutDescriptionTest()
		{
			lastMovieTitle = "Movie: " + GenerateRandomString(5);
			string invalidMovieDescription = "";

			// Navigate to the Add Movie page
			driver.Navigate().GoToUrl($"{BaseUrl}Catalog/Add");

			// Enter a valid movie title and an invalid description (empty)
			driver.FindElement(By.CssSelector("input#form2Example17")).SendKeys(lastMovieTitle);
			driver.FindElement(By.CssSelector("textarea#form2Example17")).SendKeys(invalidMovieDescription);

			// Find and click the 'Add' button
			var add = driver.FindElement(By.CssSelector("button.btn.warning[type='submit']"));
			actions.MoveToElement(add).Perform();
			add.Click();

			// Verify the error message
			var messageElement = driver.FindElement(By.CssSelector("div.toast-message"));
			Assert.That(messageElement.Text.Trim(), Is.EqualTo("The Description field is required."), "The description error message is not displayed as expected.");
		}

		[Test, Order(3)]
		public void AddMovieWithRandomTitleTest()
		{
			// Generate random movie title and description
			lastMovieTitle = "MOVIE: " + GenerateRandomString(5);
			lastMovieDescription = "Movie Description: " + GenerateRandomString(10);

			// Navigate to the Add Movie page
			driver.Navigate().GoToUrl($"{BaseUrl}Catalog/Add");

			// Enter the movie title and description
			driver.FindElement(By.CssSelector("input#form2Example17")).SendKeys(lastMovieTitle);
			driver.FindElement(By.CssSelector("textarea#form2Example17")).SendKeys(lastMovieDescription);

			// Find and click the 'Add' button
			var add = driver.FindElement(By.CssSelector("button.btn.warning[type='submit']"));
			actions.MoveToElement(add).Perform();
			add.Click();

			// Navigate to the last page of the movie listings
			NavigateToLastPage();

			// Verify that the last added movie title matches the expected value
			VerifyLastMovieTitle(lastMovieTitle);
		}

		[Test, Order(4)]
		public void EditLastAddedMovieTest()
		{
			// Navigate to the last page of the movie listings
			NavigateToLastPage();

			// Find the last movie element and click the edit button
			var movies = driver.FindElements(By.CssSelector(".col-lg-4"));
			var lastMovieElement = movies.Last();
			var editButton = lastMovieElement.FindElement(By.CssSelector("a.btn.btn-outline-success[href*='/Movie/Edit']"));
			editButton.Click();

			// Change the movie title
			lastMovieTitle = "CHANGED " + lastMovieTitle; // Update the lastMovieTitle
			var inputTitle = driver.FindElement(By.CssSelector("input#form2Example17"));
			inputTitle.Clear();
			inputTitle.SendKeys(lastMovieTitle);

			// Find and click the 'Save Changes' button
			var saveChangesButton = driver.FindElement(By.CssSelector("button.btn.warning[type='submit']"));
			actions.MoveToElement(saveChangesButton).Perform();
			saveChangesButton.Click();

			// Verify the success message
			var messageElement = driver.FindElement(By.CssSelector("div.toast-message"));
			Assert.That(messageElement.Text.Trim(), Is.EqualTo("The Movie is edited successfully!"), "The successfully edited message is not displayed as expected.");
		}

		[Test, Order(5)]
		public void MarkLastAddedMovieAsWatchedTest()
		{
			// Navigate to the last page of the movie listings
			NavigateToLastPage();

			// Find the last movie element and click the 'Mark as Watched' button
			var movies = driver.FindElements(By.CssSelector(".col-lg-4"));
			var lastMovieElement = movies.Last();
			var watchedButton = lastMovieElement.FindElement(By.CssSelector("a.btn.btn-info[href*='Movie/MarksAsWatched']"));
			watchedButton.Click();

			// Navigate to the Watched Movies page
			driver.Navigate().GoToUrl($"{BaseUrl}Catalog/Watched#watched");

			// Navigate to the last page of the watched movies listings
			NavigateToLastPage();

			// Verify that the last watched movie title matches the expected value
			VerifyLastMovieTitle(lastMovieTitle);
		}

		[Test, Order(6)]
		public void DeleteLastAddedMovieTest()
		{
			// Navigate to the last page of the movie listings
			NavigateToLastPage();

			// Find the last movie element and click the delete button
			var movies = driver.FindElements(By.CssSelector(".col-lg-4"));
			var lastMovieElement = movies.Last();
			var deleteButton = lastMovieElement.FindElement(By.CssSelector("a.btn.btn-danger[href*='/Movie/Delete']"));
			deleteButton.Click();

			// Confirm the deletion
			driver.FindElement(By.CssSelector("button.btn.warning[type='submit']")).Click();

			// Verify the success message
			var messageElement = driver.FindElement(By.CssSelector("div.toast-message"));
			Assert.That(messageElement.Text.Trim(), Is.EqualTo("The Movie is deleted successfully!"), "The delete success message is not displayed as expected.");
		}



		[OneTimeTearDown]
		public void OneTimeTearDown()
		{
			// Quit the driver after all tests are executed
			driver.Quit();
		}

		private string GenerateRandomString(int length)
		{
			// Generate a random string of specified length
			const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
			var random = new Random();
			return new string(Enumerable.Repeat(chars, length)
			  .Select(s => s[random.Next(s.Length)]).ToArray());
		}

		private void NavigateToLastPage()
		{
			// Find the pagination elements to navigate to the last page
			var paginationItems = driver.FindElements(By.CssSelector("ul.pagination li.page-item"));
			var lastPageItem = paginationItems.Last();
			actions.MoveToElement(lastPageItem).Perform();

			// Click the link of the actual last page
			var lastPageLink = lastPageItem.FindElement(By.CssSelector("a.page-link"));
			lastPageLink.Click();
		}

		private void VerifyLastMovieTitle(string expectedTitle)
		{
			// Re-locate the movie elements on the last page
			var movies = driver.FindElements(By.CssSelector(".col-lg-4"));
			var lastMovieElement = movies.Last();
			var lastMovieElementTitle = lastMovieElement.FindElement(By.CssSelector("h2"));

			// Verify that the last movie title matches the expected value
			string actualMovieTitle = lastMovieElementTitle.Text.Trim();
			Assert.That(actualMovieTitle, Is.EqualTo(expectedTitle), "The last movie title does not match the expected value.");
		}
	}
}
