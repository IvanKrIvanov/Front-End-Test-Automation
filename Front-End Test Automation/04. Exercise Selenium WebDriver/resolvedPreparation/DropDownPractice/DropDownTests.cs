using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System.Diagnostics.Metrics;
using System.Xml.Linq;

namespace DropDownPractice
{
    public class DropDownTests
    {
        IWebDriver driver;

        [SetUp]
        public void Setup()
        {
            // Create object of ChromeDriver
            driver = new ChromeDriver();

            // Launch Chrome browser with the given URL
            driver.Url = "http://practice.bpbonline.com/";

            // Add implicit wait
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
        }

        [TearDown]
        public void TearDown()
        {
            // Quit the driver
            driver.Quit();
            driver.Dispose();
        }

        [Test]
        public void ExtractDropDownInfoIntoFile()
        {
            string path = Directory.GetCurrentDirectory() + "/manufacturer.txt";

            if (File.Exists(path)) { 
                File.Delete(path);
            }

            SelectElement manufacturerDropdown = new SelectElement(driver.FindElement(By.XPath("//form[@name='manufacturers']//select")));

            IList<IWebElement> manOptions = manufacturerDropdown.Options;

            List<string> manOptionsString = new List<string>();

            foreach (IWebElement manOption in manOptions)
            {
                manOptionsString.Add(manOption.Text);
            }
            manOptionsString.RemoveAt(0);

            foreach (string option in manOptionsString)
            {
                //The StaleElementReferenceException occurs when the referenced element is no longer attached to the DOM. This can happen if the page refreshes, or any JavaScript modifies the DOM, causing the previously located elements to become stale.

                //After selecting an option from the dropdown and performing some actions, the DOM might get updated, making the previously located elements stale.

                //To resolve this, you should re - locate the SelectElement and other elements inside the loop where you interact with the dropdown. 

                manufacturerDropdown = new SelectElement(driver.FindElement(By.XPath("//form[@name='manufacturers']//select")));
                manufacturerDropdown.SelectByText(option);
                if (driver.PageSource.Contains("There are no products available in this category."))
                {
                    File.AppendAllText(path, $"The manufacturer {option} has no products\n");
                }
                else {
                    IWebElement productsTable = driver.FindElement(By.ClassName("productListingHeader"));
                    File.AppendAllText(path, $"\n\nThe manufacturer {option} products are listed -- \n");

                    IReadOnlyCollection<IWebElement> tableRows = productsTable.FindElements(By.XPath("//tbody/tr"));

                    foreach (IWebElement row in tableRows) {
                        File.AppendAllText(path, row.Text + "\n");
                    }
                }
            }
        }
    }
}