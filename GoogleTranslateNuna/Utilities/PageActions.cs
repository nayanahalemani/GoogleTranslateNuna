using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GoogleTranslateNuna.Utilities
{
    public class PageActions
    {
        /// <summary>
        /// Searches for a given element and clicks on it by using javascript syntax.
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="element"></param>
        public void jsClickElement(IWebDriver driver, By element)
        {
            var webElem = WaitForLocator(driver, element);
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("arguments[0].click();", webElem);
        }

        /// <summary>
        /// Waits 30 seconds by default for a web element to be present.
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="element"></param>
        /// /// <param name="time"> by default</param>
        public IWebElement WaitForLocator(IWebDriver driver2, By element, int time = 40)
        {
            IWebElement webElement = null;
            var wait = new WebDriverWait(driver2, TimeSpan.FromSeconds(time));
            wait.Until(driver => ((IJavaScriptExecutor)driver).ExecuteScript("return document.readyState").Equals("complete"));

            wait.IgnoreExceptionTypes(typeof(NoSuchElementException), typeof(WebDriverTimeoutException), typeof(UnhandledAlertException));
            try
            {
                webElement = wait.Until(driver =>
                {
                    try
                    {
                        return driver.FindElement(element);
                    }
                    catch (NoSuchElementException ex)
                    {
                        Console.WriteLine("searching" + " webElement " + element.ToString() + "..." + ex);
                        return null;
                    }
                });
            }
            catch (WebDriverTimeoutException ex)
            {
                Assert.Fail($"Exception in SendKeys(): element located by {element} not visible and enabled within {time} seconds::" + ex);
            }
            return webElement;
        }

        /// <summary>
        /// Searches for a given element and types any text.
        /// Performs an enter action.
        /// Performs a click action
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="element"></param>
        /// /// <param name="keyword"></param>
        public void sendKeysAndEnter(IWebDriver driver, By element, string keyword)
        {
            verifyWebElementExists(driver, element);
            IWebElement keysEventInput = driver.FindElement(element);
            //IWebElement keysEventCLick = driver.FindElement(By.XPath("//span[contains(.,'North Memorial Health')]"));
            //keysEventInput.Clear();
            keysEventInput.Click();
            keysEventInput.SendKeys(keyword);
            Actions actionProvider = new Actions(driver);
            Thread.Sleep(3);
            IAction pressEnter = actionProvider.KeyDown(keysEventInput, Keys.Enter).Build();
            //actionProvider.Click(keysEventCLick).Build();
            pressEnter.Perform();
            // verifyWebElementExists(driver, By.XPath("//li[@class='header'][contains(.,'Requests matching:')]"));
        }

        /// <summary>
        /// Retuns true or false if the web element exists or not.
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="element"></param>
        public bool verifyWebElementExists(IWebDriver driver, By element, int time = 15)
        {
            IWebElement webElement = WaitForLocator(driver, element, time);
            bool exists = webElement != null;
            return exists;
        }

        ///
        public void selectFromDropdown(IWebDriver driver, By elemen, string text)
        {
            new SelectElement(driver.FindElement(elemen)).SelectByText(text);
        }

        public IWebElement waitAndClick(IWebDriver driver, By element, int time = 30)
        {
            IWebElement webElement = null;
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(time));
            wait.IgnoreExceptionTypes(typeof(NoSuchElementException), typeof(WebDriverTimeoutException), typeof(UnhandledAlertException), typeof(NoSuchElementException));
            try
            {
                webElement = new WebDriverWait(driver, TimeSpan.FromSeconds(20)).Until(drv => drv.FindElement(element));
            }
            catch (WebDriverTimeoutException)
            {
                Assert.Fail($"Exception while trying to ckick: element located by {element.ToString()} not visible and enabled within {time} seconds.");
                return null;
            }
            jsClickElement(driver, element);
            Thread.Sleep(1000);
            return webElement;
        }

        public void scrollDown(IWebDriver driver, By element)
        {
            WaitForLocator(driver, element);
            IWebElement Webelement = driver.FindElement(element);
            IJavaScriptExecutor je = (IJavaScriptExecutor)driver;
            je.ExecuteScript("arguments[0].scrollIntoView(true);", Webelement);
            Thread.Sleep(2000);
        }

        /// <summary>
        /// Searches for a given element and types any text
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="element"></param>
        /// /// <param name="keys"></param>
        public void SendKeys(IWebDriver driver, By element, string keys)
        {
            IWebElement webElem = new WebDriverWait(driver, TimeSpan.FromSeconds(20)).Until(drv => drv.FindElement(element));
            webElem.SendKeys(keys);
        }
        public void CopyPaste(IWebDriver driver, By element, string text)
        {
            IWebElement webElement = driver.FindElement(element);
            Actions action = new Actions(driver);
            action.MoveToElement(webElement).Click().SendKeys(text).KeyDown(Keys.Control).SendKeys("v");
            action.KeyUp(Keys.Control).Build().Perform();
        }

    }
}
