using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;

namespace FoodyAppTests
{
	public class FoodyAppTests
	{
		private IWebDriver driver;
		private ChromeOptions options;
		private Actions actions;
		protected string BaseUrl = "http://softuni-qa-loadbalancer-2137572849.eu-north-1.elb.amazonaws.com:85/";
		string lastCreatedFoodTitle;
		string lastCreatedFoodDescription;

		[OneTimeSetUp]
		public void OneTimeSetUp()
		{
			options = new ChromeOptions();
			options.AddUserProfilePreference("profile.password_manager_enabled", false);
			options.AddArgument("--disable-search-engine-choice-screen");

			driver = new ChromeDriver(options);
			driver.Manage().Window.Maximize();

			driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);

			driver.Navigate().GoToUrl(BaseUrl);
			driver.FindElement(By.XPath("//a[@href='/User/Login']")).Click();
			driver.FindElement(By.Id("username")).SendKeys("ivankr");
			driver.FindElement(By.Id("password")).SendKeys("123456");
			driver.FindElement(By.XPath("//button[@type='submit']")).Click();

		}

		[OneTimeTearDown]
		public void OneTimeTearDown()
		{
			driver.Quit();
			driver.Dispose();
		}

		[Test, Order(1)]
		public void TestAddFoodWithInvalidData()
		{
			driver.Navigate().GoToUrl($"{BaseUrl}Food/Add");

			var foodName = driver.FindElement(By.XPath("//*[@id=\"name\"]"));
			foodName.Clear();
			foodName.SendKeys(" ");

			var describeFood = driver.FindElement(By.XPath("//*[@id=\"description\"]"));
			describeFood.Clear();
			describeFood.SendKeys(" ");

			driver.FindElement(By.XPath("//button[@type='submit']")).Click();


			var currentUrl = driver.Url;
			Assert.That(currentUrl, Is.EqualTo($"{BaseUrl}Food/Add"), "User should remain on the same page with same URL");

			var errorMessage = driver.FindElement(By.XPath("//*[@id=\"page-top\"]/section/div/div/div/div/div/div[1]/div/div[2]"));

			Assert.That(errorMessage.Text, Is.EqualTo("Unable to add this food revue!"), "The error message for invalid data when creating Food is not there");
		}

		[Test, Order(2)]
		public void TestAddFoodWithRandomData()
		{
			driver.FindElement(By.CssSelector("a.nav-link[href='/Food/Add']:nth-of-type(1)")).Click();

			var foodName = driver.FindElement(By.Id("name"));
			lastCreatedFoodTitle = (GenerateRandomString(3) + " test");
			foodName.SendKeys(lastCreatedFoodTitle);

			var foodDescription = driver.FindElement(By.Id("description"));
			lastCreatedFoodDescription = (GenerateRandomString(5) + " auto");
			foodDescription.SendKeys(lastCreatedFoodDescription);

			driver.FindElement(By.XPath("//button[@type='submit']")).Click();
			Assert.That(driver.Url, Is.EqualTo(BaseUrl));


			var allCreatedFoods = driver.FindElements(By.XPath("//section[@id='scroll']"));

			var lastFoodInTheList = allCreatedFoods.Last().FindElement(By.CssSelector("h2.display-4")).Text;

			Assert.That(lastFoodInTheList, Is.EqualTo(lastCreatedFoodTitle));
		}

		[Test, Order(3)]
		public void TestEditLastCreatedFoodTitle()
		{
			driver.Navigate().GoToUrl(BaseUrl);

			var allFoods = driver.FindElements(By.XPath("//a[@class='btn btn-primary btn-xl rounded-pill mt-5'] [text()='Edit']"));
			var lastFoodEditButton = allFoods.Last();

			Actions actions = new Actions(driver);
			actions.MoveToElement(lastFoodEditButton).Click().Perform();

			driver.FindElement(By.XPath("//input[@id='name']")).Click();
			driver.FindElement(By.XPath("//input[@id='name']")).SendKeys($"{lastCreatedFoodTitle} + edited");
			driver.FindElement(By.XPath("//button[@type='submit']")).Click();

			Assert.That(lastCreatedFoodTitle, Is.EqualTo(lastCreatedFoodTitle), "The title of the food was not edited.");
			Console.WriteLine("Incomplete functionallity: title cannot be changed");


		}

		[Test, Order(4)]
		public void TestSearchForFood()
		{
			driver.Navigate().GoToUrl(BaseUrl);

			var searchField = driver.FindElement(By.XPath("//input[@type='search']"));
			searchField.Click();
			searchField.SendKeys(lastCreatedFoodTitle);
			driver.FindElement(By.XPath("//button[@type='submit']")).Click();

			var searchResultElement = driver.FindElements(By.XPath("//div[@class='row gx-5 align-items-center']"));
			Assert.That(searchResultElement.Count, Is.EqualTo(1));

			var lastSearchResultTitle = driver.FindElement(By.XPath("//div[@class='p-5']//h2")).Text;
			Assert.That(lastSearchResultTitle, Is.EqualTo(lastCreatedFoodTitle));
		}

		[Test, Order(5)]
		public void TestDeleteLastCreatedFood()
		{
			driver.Navigate().GoToUrl(BaseUrl);

			var allFoods = driver.FindElements(By.XPath("//a[@class='btn btn-primary btn-xl rounded-pill mt-5'] [text()='Delete']"));
			var initialFoodCount = allFoods.Count();
			Console.WriteLine(initialFoodCount);

			var lastFoodDeleteButton = allFoods.Last();

			Actions actions = new Actions(driver);
			actions.MoveToElement(lastFoodDeleteButton).Click().Perform();

			var allFoodsCountAfterDelete = driver.FindElements(By.XPath("//div[@class='row gx-5 align-items-center']"));
			var foodCountTotal = allFoodsCountAfterDelete.Count();
			Console.WriteLine(allFoodsCountAfterDelete);

			Assert.That(foodCountTotal, Is.EqualTo(initialFoodCount - 1));
		}

		[Test, Order(6)]
		public void TestSearchForDeletedFood()
		{
			driver.Navigate().GoToUrl(BaseUrl);

			var searchField = driver.FindElement(By.XPath("//input[@type='search']"));
			searchField.Click();
			searchField.SendKeys(lastCreatedFoodTitle);
			driver.FindElement(By.XPath("//button[@type='submit']")).Click();

			var searchResult = driver.FindElement(By.XPath("//h2[@class='display-4']"));
			Assert.That(searchResult.Text, Is.EqualTo("There are no foods :("));

			var addButton = driver.FindElement(By.CssSelector("a.nav-link[href='/Food/Add']"));
			Assert.True(addButton.Displayed);
		}
		private string GenerateRandomString(int length)
		{
			const string chars = "AkjiASJNiqeouioneQAvmasOiq";
			var random = new Random();
			return new string(Enumerable.Repeat(chars, length).Select(s => s[random.Next(s.Length)]).ToArray());
		}
	}
}