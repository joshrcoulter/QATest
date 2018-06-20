using log4net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Globalization;

namespace QATest
{
    [TestClass]
    public class SortLowToHigh
    {
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        static void Main()
        {
            IWebDriver driver = new ChromeDriver();
            Decimal price1; 
            Decimal price2;

            try
            {
                //Settings
                driver.Manage().Window.Maximize();
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);

                //Launch Chrome
                driver.Navigate().GoToUrl("http://www.blinds.com");
                
                //Navigate to the desired page
                driver.FindElement(By.LinkText("Purpose")).Click();
                driver.FindElement(By.LinkText("Darken A Room")).Click();
                driver.FindElement(By.XPath("//*[@title='Top Room Darkening Shades']")).Click();
                driver.FindElement(By.LinkText("Price Low-High")).Click();

                //Let the sort take place
                System.Threading.Thread.Sleep(5000);

                //Collect the prices
                IList<IWebElement> prices = driver.FindElements(By.XPath("//*[@data-testid='gcc-product-discount-price']"));

                log.Info("BEGIN");

                //Verifications
                for (int i = 0; i < prices.Count; i++)
                {
                    if ((i + 1) == prices.Count) //If true, we're at the end of the list
                        break;

                    price1 = decimal.Parse(prices[i].Text, NumberStyles.Currency);
                    price2 = decimal.Parse(prices[i + 1].Text, NumberStyles.Currency);

                    if ((i + 1) < prices.Count && (price1 <= price2)) //success
                        log.Info("Price of item " + (i + 1) + " [$" + price1 + "] is less than or equal to item " + (i + 2) + " [$" + price2 + "]");
                    else //fail
                        log.Error("Price of item " + (i + 1) + " [$" + price1 + "] is not less than or equal to item " + (i + 2) + " [$" + price2 + "]");
                }

                driver.Quit();
            }
            catch (Exception ex)
            {
                log.Fatal(ex);
                driver.Quit();
            }

            log.Info("END");
        }
    }
}
