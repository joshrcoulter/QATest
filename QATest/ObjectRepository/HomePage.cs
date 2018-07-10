
using OpenQA.Selenium;

namespace QATest.ObjectRepository
{
    class HomePage
    {
        public string HomePageURL = "http://www.blinds.com";

        private IWebDriver driver;

        public HomePage(IWebDriver driver)
        {
            this.driver = driver;
        }

        public IWebElement Purpose
        {
            get
            {
                return driver.FindElement(By.LinkText("Purpose"));
            }
        }

        public IWebElement DarkenARoom
        {
            get
            {
                return driver.FindElement(By.LinkText("Darken A Room"));
            }
        }
    }
}