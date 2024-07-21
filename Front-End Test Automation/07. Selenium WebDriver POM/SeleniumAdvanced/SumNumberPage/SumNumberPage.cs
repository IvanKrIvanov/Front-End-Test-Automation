using OpenQA.Selenium;


namespace SumNumberPage
{
    public class SumNumberPage
    {
        private readonly IWebDriver driver;

        public SumNumberPage(IWebDriver driver)
        {
            this.driver = driver;
            driver.Manage().Timeouts().
                ImplicitWait = TimeSpan.FromSeconds(2);
        }

        const string PageUrl =
            "https://91981363-bfd4-4e4d-8872-e4c073bbf03e-00-o3fpq10yr45o.janeway.replit.dev/";

        public IWebElement FieldNum1 => 
            driver.FindElement(By.CssSelector("input#number1"));
        public IWebElement FieldNum2 => 
            driver.FindElement(By.CssSelector("input#number2"));
        public IWebElement ButtonCalc => 
            driver.FindElement(By.CssSelector("#calcButton"));
        public IWebElement ButtonReset => 
            driver.FindElement(By.CssSelector("#resetButton"));
        public IWebElement ElementResult => 
            driver.FindElement(By.CssSelector("#result"));

        public void OpenPage()
        {
            driver.Navigate().GoToUrl(PageUrl);
        }

        public void ResetForm()
        {
            ButtonReset.Click();
        }

        public bool IsFormEmpty()
        {
            return FieldNum1.Text + FieldNum2.Text + 
                ElementResult.Text == "";
        }

        public string AddNumbers(string num1, string num2)
        {
            FieldNum1.SendKeys(num1);
            FieldNum2.SendKeys(num2);
            ButtonCalc.Click();
            string result = ElementResult.Text;
            return result;
        }
    }
}
