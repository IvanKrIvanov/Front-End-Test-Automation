using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using System.Collections.ObjectModel;
using System.Net;

namespace WorkingWithWebTables
{
    public class TableTests
    {
        IWebDriver driver;

        public static IEnumerable<TestCaseData> ProductData()
        {
            return new List<TestCaseData>
            {
                new TestCaseData("Courage Under Fire", "$29.99"),
                new TestCaseData("Matrox G400 32MB", "$499.99"),
                new TestCaseData("SWAT 3: Close Quarters Battle", "$79.99"),
                new TestCaseData("You've Got Mail", "$34.99"),
                new TestCaseData("A Bug's Life", "$35.99"),
                new TestCaseData("Hewlett Packard LaserJet 1100Xi", "$499.99"),
                new TestCaseData("Samsung Galaxy Tab", "$749.99"),
                new TestCaseData("Under Siege", "$29.99"),
                new TestCaseData("Disciples: Sacred Lands", "$90.00")
            };
        }

        [SetUp]
        public void SetUp()
        {
            // Create object of ChromeDriver
            driver = new ChromeDriver();

            // Launch Chrome browser with the given URL
            driver.Url = "http://practice.bpbonline.com/";

            // Add implicit wait
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
        }

        [Test]
        public void TestExtractProductInformation()
        {
            //locating table
            IWebElement productsTable = driver.FindElement(By.XPath("//div[@class='contentText']//table"));

            ReadOnlyCollection<IWebElement> tableRows = productsTable.FindElements(By.XPath("//tbody//tr"));

            string path = System.IO.Directory.GetCurrentDirectory() + "/productinformation.csv";

            if (File.Exists(path)) File.Delete(path);

            foreach (IWebElement tableRow in tableRows)
            {
                ReadOnlyCollection<IWebElement> tableCells = tableRow.FindElements(By.XPath(".//td"));
                foreach (IWebElement tableData in tableCells)
                {
                    String data = tableData.Text;
                    String[] productionInfo = data.Split('\n');

                    File.AppendAllText(path, productionInfo[0].Trim() + ", " + productionInfo[1].Trim() + "\n");
                }
            }

            // Verify the file was created and has content
            Assert.IsTrue(File.Exists(path), "CSV file was not created");
            Assert.IsTrue(new FileInfo(path).Length > 0, "CSV file is empty");
        }

        [Test, TestCaseSource(nameof(ProductData))]
        public void ValidateProductInfo(string expectedName, string expectedPrice)
        {
            // Locate the table
            IWebElement productsTable = driver.FindElement(By.XPath("//div[@class='contentText']//table"));

            // Find all rows within the table
            ReadOnlyCollection<IWebElement> tableRows = productsTable.FindElements(By.XPath(".//tbody//tr"));

            foreach (IWebElement tableRow in tableRows)
            {
                // Find all cells within the current row
                ReadOnlyCollection<IWebElement> tableCells = tableRow.FindElements(By.XPath(".//td"));

                foreach (IWebElement tableCell in tableCells)
                {
                    string cellText = tableCell.Text;
                    if (cellText.Contains(expectedName) && cellText.Contains(expectedPrice))
                    {
                        Assert.IsTrue(cellText.Contains(expectedName), $"Expected product name {expectedName} was not found in the cell.");
                        Assert.IsTrue(cellText.Contains(expectedPrice), $"Expected product price {expectedPrice} was not found in the cell.");
                        return; // Product found, exit the test successfully
                    }
                }
            }
        }

        [Test, TestCaseSource(typeof(TestDataGenerator), nameof(TestDataGenerator.GenerateTestCaseDataFromCsv))]
        public void ValidateProductInfoFromCSV(string expectedName, string expectedPrice) {
            // Locate the table
            IWebElement productsTable = driver.FindElement(By.XPath("//div[@class='contentText']//table"));

            // Find all rows within the table
            ReadOnlyCollection<IWebElement> tableRows = productsTable.FindElements(By.XPath(".//tbody//tr"));

            foreach (IWebElement tableRow in tableRows)
            {
                // Find all cells within the current row
                ReadOnlyCollection<IWebElement> tableCells = tableRow.FindElements(By.XPath(".//td"));

                foreach (IWebElement tableCell in tableCells)
                {
                    string cellText = tableCell.Text;
                    if (cellText.Contains(expectedName) && cellText.Contains(expectedPrice))
                    {
                        Assert.IsTrue(cellText.Contains(expectedName), $"Expected product name {expectedName} was not found in the cell.");
                        Assert.IsTrue(cellText.Contains(expectedPrice), $"Expected product price {expectedPrice} was not found in the cell.");
                        return; // Product found, exit the test successfully
                    }
                }
            }
        }

        [TearDown]
        public void TearDown()
        {
            // Quit the driver
            driver.Quit();
            driver.Dispose();
        }
    }
}