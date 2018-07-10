using OpenQA.Selenium;
using System.Collections.Generic;

namespace QATest.ObjectRepository
{
    class Blackout
    {
        private IWebDriver driver;

        public Blackout(IWebDriver driver)
        {
            this.driver = driver;
        }

        public IWebElement ShopAllRoomDarkeningShades
        {
            get
            {
                return driver.FindElement(By.XPath("//*[@title='Top Room Darkening Shades']"));
            }
        }

        public IWebElement PriceLowHigh
        {
            get
            {
                return driver.FindElement(By.LinkText("Price Low-High"));
            }
        }

        public IList<IWebElement> prices
        {
            get
            {
                return driver.FindElements(By.XPath("//*[@data-testid='gcc-product-discount-price']"));
            }
        }
        
        public IList<IWebElement> brands
        {
            get
            {
                return driver.FindElements(By.XPath("//*[@data-testid='gcc-product-brand']"));
            }
        }

        public IList<IWebElement> names
        {
            get
            {
                return driver.FindElements(By.XPath("//*[@data-testid='gcc-product-name']"));
            }
        }
    }
}
