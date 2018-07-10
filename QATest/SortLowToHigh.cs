using log4net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Reflection;
using System.Globalization;
using QATest.ObjectRepository;

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

            HomePage h = new HomePage(driver);
            Blackout b = new Blackout(driver);
            
            try
            {
                //Settings
                driver.Manage().Window.Maximize();
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);

                //Launch Chrome
                driver.Navigate().GoToUrl(h.HomePageURL);

                //Navigate to the desired page
                h.Purpose.Click();
                h.DarkenARoom.Click();
                b.ShopAllRoomDarkeningShades.Click();
                b.PriceLowHigh.Click();

                //Let the sort take place
                System.Threading.Thread.Sleep(5000);
                
                log.Info("BEGIN");

                //Verifications
                for (int i = 0; i < b.prices.Count; i++)
                {
                    if ((i + 1) == b.prices.Count) //If true, we're at the end of the list
                        break;

                    price1 = decimal.Parse(b.prices[i].Text, NumberStyles.Currency);
                    price2 = decimal.Parse(b.prices[i + 1].Text, NumberStyles.Currency);

                    Assert.IsTrue((i + 1) < b.prices.Count && price1 <= price2);
                    log.Info("Price of " + b.brands[i].Text + " " + b.names[i].Text + " [$" + price1 + "] is less than or equal to " + b.brands[i + 1].Text + " " + b.names[i + 1].Text + " " + "[$" + price2 + "]");
                        
                }
                driver.Quit();
            }
            catch (Exception ex)
            {
                //log.Error("Price of " + brands[i].Text + " " + names[i].Text + " [$" + price1 + "] is not less than or equal to " + brands[i + 1].Text + " " + names[i + 1].Text + " " + "[$" + price2 + "]");
                log.Fatal(ex);
                driver.Quit();
            }

            log.Info("END");
        }
    }
}
