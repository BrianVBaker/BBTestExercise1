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


            ValtecSite Valtecpage = new ValtecSite();

            // Test 1
            Valtecpage.HomePageTest(Fdriver, "http://www.valtech.com//");

            //  Test 2
            if (!Valtecpage.CasesPageTest(Fdriver, "http://www.valtech.com/cases", "Cases"))
                Pass = false;

            // Test 3
            if(!Valtecpage.ServicesPageTest(Fdriver, "http://www.valtech.com/services", "Services"))
                Pass = false;
            
            // Test 4
            if(!Valtecpage.JobsPageTest(Fdriver, "http://www.valtech.com/jobs", "Jobs"))
                Pass = false;

            // Test 5 - Count of offices
            if (!Valtecpage.ContactsPageTest(Fdriver, "http://www.valtech.com//", 29))
                Pass = false;
         

            Fdriver.Quit();

            // Fail if failed

            if (!Pass)
                Assert.Fail("Test Failed!!  (see messages)");

          }

    }
    public class ValtecSite
    {
        public void HomePageTest(IWebDriver Fdriver, string URL)
        {
            Fdriver.Navigate().GoToUrl(URL);

            if (!waitForElement(Fdriver, By.CssSelector("#container > div:nth-child(2) > div:nth-child(3) > div.news-post__listing-header > header > h2")))
                Assert.Fail("Time out failure : Home Page");

            Assert.IsTrue(Fdriver.FindElement(By.CssSelector("#container > div:nth-child(2) > div:nth-child(3) > div.news-post__listing-header > header > h2")).Displayed);

            Debug.WriteLine("Homepage : Latest news found : test passed!");

        }


        public bool CasesPageTest(IWebDriver Fdriver, string URL, string title)
        {
            Fdriver.Navigate().GoToUrl(URL);

            if (!waitForElement(Fdriver, By.CssSelector("#container > header > h1")))
                Assert.Fail("Time out failure : Cases Page");
                        
            Assert.IsTrue(Fdriver.FindElement(By.CssSelector("#container > header > h1")).Displayed);

            string name1 = Fdriver.FindElement(By.CssSelector("#container > header > h1")).Text;
            if (name1 != title)
            {
                Debug.WriteLine("Cases: incorrect name: " + name1 + " : test failed!");
                return false;
            }
            else
            {
                Debug.WriteLine("Cases: expected name: " + name1 + " : test passed!");
                return true;
            }
        }

        public bool ServicesPageTest(IWebDriver Fdriver, string URL, string title)
        {
            Fdriver.Navigate().GoToUrl(URL);

            if (!waitForElement(Fdriver, By.CssSelector("#container > section > header > h1")))
                Assert.Fail("Time out failure : Services Page");

            Assert.IsTrue(Fdriver.FindElement(By.CssSelector("#container > section > header > h1")).Displayed);

            string name2 = Fdriver.FindElement(By.CssSelector("#container > section > header > h1")).Text;
            if (name2 != title)
            {
                Debug.WriteLine("Services: incorrect name: " + name2 + " : test failed!");
                return false;
            }
            else
            {
                Debug.WriteLine("Services: expected name: " + name2 + " : test passed!");
                return true;
            }   
        }

        public bool JobsPageTest(IWebDriver Fdriver, string URL, string title)
        {
            
            Fdriver.Navigate().GoToUrl(URL);

            if (!waitForElement(Fdriver, By.CssSelector("#container > div.page-header > h1")))
                Assert.Fail("Time out failure : Jobs Page");

            Assert.IsTrue(Fdriver.FindElement(By.CssSelector("#container > div.page-header > h1")).Displayed);

            string name3 = Fdriver.FindElement(By.CssSelector("#container > div.page-header > h1")).Text;
            if (name3 != title)
            {
                Debug.WriteLine("Jobs: incorrect name: " + name3 + " : test failed!");
                return false;

            }
            else
            {
                Debug.WriteLine("Jobs: expected name: " + name3 + " : test passed!");
                return true;
            }
        }

        public bool ContactsPageTest(IWebDriver Fdriver, string URL, int officeCt)
        {
            WebDriverWait wait30 = new WebDriverWait(Fdriver, TimeSpan.FromSeconds(30));
            Fdriver.Navigate().GoToUrl(URL);

            if (!waitForElement(Fdriver, By.CssSelector("#contacticon > div > div > div.hamburger__front_lang > i")))
                Assert.Fail("Time out failure : Home Page");

            Fdriver.FindElement(By.CssSelector("#contacticon > div > div > div.hamburger__front_lang > i")).Click();

            if (!waitForElement(Fdriver, By.CssSelector("#contactbox > div > p")))
                Assert.Fail("Time out failure : Contacts Page");
                       

            IWebElement Locations = Fdriver.FindElement(By.CssSelector("#contactbox"));
            int ct = Locations.FindElements(By.TagName("li")).Count;

            
            if (ct == officeCt)
            {
                Debug.WriteLine("Number of offices = " + ct + " : test Passed!");
                return true;
            }
            else
            {
                Debug.WriteLine("Number of offices = " + ct + " : test Failed!");
                return false;
            }
            
        }
        public bool waitForElement(IWebDriver Fdriver, By Bystring)
        {
            WebDriverWait wait30 = new WebDriverWait(Fdriver, TimeSpan.FromSeconds(30));
            try
            {
                wait30.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(Bystring));
            }

            catch (Exception exp)
            {
                Debug.WriteLine(exp);
                return false;
            }

            return true;
        }

    }
    
}
