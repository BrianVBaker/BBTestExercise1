using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System.Diagnostics;
using System.Collections.Generic;

namespace BBTestExercise1
{
    [TestClass]
    public class UnitTest1
    {
        IWebDriver Fdriver = new ChromeDriver(@"C:\Users\Brian_2\Documents\Selenium");
        
        [TestMethod]
        public void TestMethod1()
           
        {
            bool Pass = true;
            Fdriver.Navigate().GoToUrl("http://www.valtech.com/");

            WebDriverWait wait30 = new WebDriverWait(Fdriver, TimeSpan.FromSeconds(30));
            wait30.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(By.CssSelector("#wrapper > header > div > a > i")));
            // Test 1

            Assert.IsTrue(Fdriver.FindElement(By.CssSelector("#container > div:nth-child(2) > div:nth-child(3) > div.news-post__listing-header > header > h2")).Displayed);
           
            //  Test 2
            Fdriver.Navigate().GoToUrl("http://www.valtech.com/cases");
            wait30.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(By.CssSelector("#container > header > h1")));
            Assert.IsTrue(Fdriver.FindElement(By.CssSelector("#container > header > h1")).Displayed);

            string name1 = Fdriver.FindElement(By.CssSelector("#container > header > h1")).Text;
            if (name1 != "Cases")
            {
                Debug.WriteLine("Cases: incorrect name: " + name1);
                Pass = false;
            }
            
            // Test 3
            Fdriver.Navigate().GoToUrl("https://www.valtech.com/services/");
            wait30.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(By.CssSelector("#container > section > header > h1")));
            Assert.IsTrue(Fdriver.FindElement(By.CssSelector("#container > section > header > h1")).Displayed);

            string name2 = Fdriver.FindElement(By.CssSelector("#container > section > header > h1")).Text;
            if (name2 != "Services")
            {
                Debug.WriteLine("Services: incorrect name: " + name2);
                Pass = false;
            }

           // Test 4
            Fdriver.Navigate().GoToUrl("https://www.valtech.com/jobs");
            wait30.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(By.CssSelector("#container > div.page-header > h1")));
            Assert.IsTrue(Fdriver.FindElement(By.CssSelector("#container > div.page-header > h1")).Displayed);

            string name3 = Fdriver.FindElement(By.CssSelector("#container > div.page-header > h1")).Text;
            if (name3 != "Jobs")
            {
                Debug.WriteLine("Jobs: incorrect name: " + name3);
                Pass = false;
            }


            // Count of offices
            Fdriver.FindElement(By.CssSelector("#contacticon > div > div > div.hamburger__front_lang > i")).Click();
            wait30.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(By.CssSelector("#contactbox > div > p")));
            
            IWebElement Locations = Fdriver.FindElement(By.CssSelector("#contactbox"));
            int ct = Locations.FindElements(By.TagName("li")).Count;
                    
            
            Debug.WriteLine("Number of offices = " + ct);

            Fdriver.Quit();

            // Fail if failed

            if (!Pass)
                Assert.Fail("Test Failed!!  (see messages)");

          }

    }
}
