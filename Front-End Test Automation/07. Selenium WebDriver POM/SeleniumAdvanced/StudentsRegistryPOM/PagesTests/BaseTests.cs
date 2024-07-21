using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using StudentsRegistryPOM.Pages;

namespace StudentsRegistryPOM.Tests
{
    public class BaseTests
    { 
   protected IWebDriver driver;

    [OneTimeSetUp]
    public void Setup()
    {
            var chromeDriverPath = @"C:\Program Files\ChromeDriver\chromedriver.exe"; // Replace with the actual path
            this.driver = new ChromeDriver(chromeDriverPath);
        }

    [OneTimeTearDown]
    public void ShutDown()
    {
        driver.Quit();
        driver.Dispose();
        }
}
}
