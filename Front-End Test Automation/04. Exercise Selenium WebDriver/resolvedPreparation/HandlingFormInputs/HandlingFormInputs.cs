using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace HandlingFormInputs
{
    public class Tests
    {
        WebDriver driver;

        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
            driver.Navigate().GoToUrl("http://practice.bpbonline.com/");
        }

        [TearDown]
        public void TearDown()
        {
            //closes all browser windows and safely ends the session.
            driver.Quit();
            driver.Dispose();
        }

        [Test]
        public void Test1()
        {
            // Click on My Account link
            var myAccountLink = driver.FindElements(By.XPath("//span[@class='ui-button-text']"))[2];
            myAccountLink.Click();

            // Click on Continue button
            driver.FindElement(By.LinkText("Continue")).Click();

            //Click on male radio button
            driver.FindElement(By.CssSelector("input[type='radio'][value='f']")).Click();

            //fill firstname field
            driver.FindElement(By.XPath("//td[@class='fieldValue']//input[@name='firstname']")).SendKeys("Ayrola");
            //fill lastname field
            driver.FindElement(By.XPath("//td[@class='fieldValue']//input[@name='lastname']")).SendKeys("Hodzhov");
            //fill in date
            //driver.FindElement(By.XPath("//td[@class='fieldValue']//input[@name='dob']")).SendKeys("06/23/1997");

            //using date picker created by us
            DatePicker datePicker = new DatePicker(driver);
            datePicker.OpenDatePicker(By.XPath("//td[@class='fieldValue']//input[@name='dob']"));

            datePicker.SelectDate("Jun", "1997", 23);


            // Generate a unique email address
            Random rnd = new Random();
            // Generate a random number between 1000 and 9999
            int num = rnd.Next(1000, 9999);
            String email = "fiona.apple" + num.ToString() + "@example.com";

            //fill email field
            driver.FindElement(By.XPath("//td[@class='fieldValue']//input[@name='email_address']")).SendKeys(email);

            //fill company field
            driver.FindElement(By.XPath("//td[@class='fieldValue']//input[@name='company']")).SendKeys("Example Company");

            //fill street_address field
            driver.FindElement(By.XPath("//td[@class='fieldValue']//input[@name='street_address']")).SendKeys("Bulgaria Boulevard Number 10");

            //fill suburb field
            driver.FindElement(By.XPath("//td[@class='fieldValue']//input[@name='suburb']")).SendKeys("Sofia");

            //fill postcode field
            driver.FindElement(By.XPath("//td[@class='fieldValue']//input[@name='postcode']")).SendKeys("1000");

            //fill city field
            driver.FindElement(By.XPath("//td[@class='fieldValue']//input[@name='city']")).SendKeys("Sofia");

            //fill state field
            driver.FindElement(By.XPath("//td[@class='fieldValue']//input[@name='state']")).SendKeys("Sofia");

            //fill Country dropdown
            new SelectElement(driver.FindElement(By.Name("country"))).SelectByText("Bulgaria");

            //fill telephone input
            driver.FindElement(By.Name("telephone")).SendKeys("2432424112");

            //click newsLetter checkbox
            driver.FindElement(By.Name("newsletter")).Click();

            //fill password input
            driver.FindElement(By.Name("password")).SendKeys("fiona_123456");

            //fill confirmation input
            driver.FindElement(By.Name("confirmation")).SendKeys("fiona_123456");

            //click continue button
            driver.FindElements(By.XPath("//*[@class='ui-button-icon-primary ui-icon ui-icon-person']//following-sibling::span"))[1].Click();

            // Assert account creation success
            Assert.AreEqual(driver.FindElement(By.XPath("//div[@id='bodyContent']//h1")).Text, "Your Account Has Been Created!", "Account creation failed.");

            // Click on Log Off link
            driver.FindElement(By.LinkText("Log Off")).Click();

            // Click on Continue button
            driver.FindElement(By.LinkText("Continue")).Click();

            //print message
            Console.WriteLine("User Account Created with email: " + email);
        }
    }
}