using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace CalculatorDataDriven
{
    public class Tests
    {
        IWebDriver driver;
        IWebElement textBoxFirstNum;
        IWebElement textBoxSecondNum;
        IWebElement dropDownOperation;
        IWebElement calcBtn;
        IWebElement resetBtn;
        IWebElement divResult;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            driver = new ChromeDriver();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            driver.Url = "http://softuni-qa-loadbalancer-2137572849.eu-north-1.elb.amazonaws.com/number-calculator/";
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            driver.Quit();
            driver.Dispose();
        }

        [SetUp]
        public void Setup()
        {
            textBoxFirstNum = driver.FindElement(By.Id("number1"));
            textBoxSecondNum = driver.FindElement(By.XPath("//input[@name='number2']"));
            dropDownOperation = driver.FindElement(By.XPath("//label[@for='operation']//following-sibling::select"));
            calcBtn = driver.FindElement(By.XPath("//div[@class='buttons-bar']//input[@type='button']"));
            resetBtn = driver.FindElement(By.XPath("//div[@class='buttons-bar']//input[@type='Reset']"));
            divResult = driver.FindElement(By.XPath("//div[@id='result']"));
        }

        public void PerformCalculation(string firstNumber, string secondNumber, string operation, string expected) { 
            
            //click reset button
            resetBtn.Click();

            //send values to fields if they are not empty
            if (!string.IsNullOrEmpty(firstNumber)) { 
                textBoxFirstNum.SendKeys(firstNumber);
            }

            if (!string.IsNullOrEmpty(secondNumber))
            {
                textBoxSecondNum.SendKeys(secondNumber);
            }

            if (!string.IsNullOrEmpty(operation))
            {
                new SelectElement(dropDownOperation).SelectByText(operation);
            }

            //click calculate button
            calcBtn.Click();

            //assert the expected and actual results are equal
            Assert.That(divResult.Text, Is.EqualTo(expected));
        }

        [Test]
        [TestCase("5", "+ (sum)", "10", "Result: 15")]
        [TestCase("3.5", "- (subtract)", "1.2", "Result: 2.3")]
        [TestCase("2e2", "* (multiply)", "1.5", "Result: 300")]
        [TestCase("5", "/ (divide)", "0", "Result: Infinity")]
        [TestCase("invalid", "+ (sum)", "10", "Result: invalid input")]
        public void TestNumberCalculator(string firstNumber, string operation, string secondNumber, string expected)
        {
            PerformCalculation(firstNumber, secondNumber, operation, expected);
        }
    }
}