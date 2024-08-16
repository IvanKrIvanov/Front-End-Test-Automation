namespace MovieCatalogPomTests.Pages
{
    public class AddMoviePage : BasePage
    {
        public AddMoviePage(IWebDriver driver) : base(driver)
        {
            
        }

        public string Url = BaseUrl + "/Catalog/Add#add";

        public IWebElement TitleInput => driver.FindElement(By.XPath("//input[@name='Title']"));

        public IWebElement DescriptionInput => driver.FindElement(By.XPath("//textarea[@name='Description']"));

        public IWebElement PosterUrlInput => driver.FindElement(By.XPath("//input[@name='PosterUrl']"));

        public IWebElement TrailerLinkInput => driver.FindElement(By.XPath("//input[@name='TrailerLink']"));

        public IWebElement MarkAsWatchedCheckBox => driver.FindElement(By.XPath("//input[@class='form-check-input']"));



        public IWebElement AddButton => driver.FindElement(By.XPath("//button[@class='btn warning']"));

        public IWebElement ToastMessage => driver.FindElement(By.XPath("//div[@class='toast-message']"));

        public void AssertEmptyTitleMessage()
        {
            Assert.That(ToastMessage.Text.Trim(), Is.EqualTo("The Title field is required."), "Title Error message was not expected");
        }
        
        public void AssertEmptyDescriptionMessage()
        {
            Assert.That(ToastMessage.Text.Trim(), Is.EqualTo("The Description field is required."), "Description Error message was not expected");
        }

        public void OpenPage()
        { 
            driver.Navigate().GoToUrl(Url);
        }
    }
}
