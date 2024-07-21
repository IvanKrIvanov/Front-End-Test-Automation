using OpenQA.Selenium;
using StudentsRegistryPOM.Pages;
using System.Collections.ObjectModel;
using System.Linq;


namespace StudentsRegistryPOM.Pages
{
    public class ViewStudents : BasePage
    {
        public ViewStudents(IWebDriver driver) : base(driver)
        {
        }
        public override string PageUrl =>
                 "http://softuni-qa-loadbalancer-2137572849.eu-north-1.elb.amazonaws.com:82/students";

        public ReadOnlyCollection<IWebElement> ListItemsStudents =>
            driver.FindElements(By.CssSelector("Body > ul > li"));

        public string[] GetRegisterStudents()
        {
            var elementsStudents = this.ListItemsStudents.Select(s => s.Text).ToArray();
            return elementsStudents;
        }
    }
}
