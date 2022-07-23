using GoogleTranslateNuna.DataProvider;
using GoogleTranslateNuna.Utilities;
using Newtonsoft.Json;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GoogleTranslateNuna.PageObjects
{
    public class GoogleTranslatePage
    {
        private readonly IWebDriver driver;
        private readonly PageActions pageActions;

        public TranslationDTO TranslationDTO { get; private set; }

        public GoogleTranslatePage(IWebDriver webDriver)
        {
            //The Selenium web driver to automate the browser
            driver = webDriver;
            pageActions = new PageActions();
            string fileName = (@"C:\Users\My Lap\Documents\GoogleTranslateNuna\GoogleTranslateNuna\Tests\Data\GTranslation.json");
            string path = UtilityTools.GetFilePath(fileName);
            TranslationDTO = ParseJson<TranslationDTO>(path);

        }

        private T ParseJson<T>(string file)
        {
            return JsonConvert.DeserializeObject<T>(System.IO.File.ReadAllText(file));
        }

        private static By sourceDropdown = By.XPath("//button[@aria-label='More source languages']");
        private static By sourceLanguages = By.XPath("//div[@class='OlSOob']//div[@class='OoYv6d']//div[@class='F29iQc']//div[@class='Llmcnf']");
        private static By targetDropdown = By.XPath("//button[@aria-label='More target languages']//span[@class='zQ0atf']");
        private static By targetLanguages = By.XPath("//div[@class='qSb8Pe']//div[@class='Llmcnf']");
        private static By sourceArea = By.XPath("//div[@class='QFw9Te']//textarea[@aria-label='Source text']");
        private static By translatedText = By.XPath("//div[@class='J0lOec']//span[@class='Q4iAWc']");
        private static By swipButton = By.XPath("//button[@class='VfPpkd-Bz112c-LgbsSe VfPpkd-Bz112c-LgbsSe-OWXEXe-e5LLRc-SxQuSe yHy1rc eT1oJ DiOXab qiN4Vb lRTpdf U2dVxe']");
        private static By textAreaAfterSwip = By.XPath("//div[@class='QFw9Te']//div[@class='D5aOJc vJwDU']");
        private static By clickTextButton = By.XPath("//button[@aria-label='Text translation']//div[@class='VfPpkd-Jh9lGc']");
        private static By clearText = By.XPath("//button[@aria-label='Clear source text']//div[@class='VfPpkd-Bz112c-RLmnJb']");
        private static By keyBoard = By.XPath("//*[@id='itamenu']/span/div/a[1]/span");
        private static By upperKeys = By.XPath("//*[@id='K16']/span");
        private static By clickH = By.XPath("//*[@id='K72']/span");
        private static By clickI = By.XPath("//*[@id='K73']/span");
        private static By clickexclim = By.XPath("//*[@id='K49']/span");
        private static By clickUp = By.XPath("//*[@id='K16']/span");



        /// <summary>
        /// Generic Method to select languages from dropdown
        /// </summary>
        /// <param name="language"></param>
        /// <param name="dropdownElement"></param>
        /// <param name="listOfLanguages"></param>
        /// <author>Nayana H</author>
        public bool SelectLanguage(string language, By dropdownElement, By listOfLanguages)
        {
            bool selectStatus = false;
            pageActions.waitAndClick(driver, dropdownElement);
            IList<IWebElement> nameList = driver.FindElements(listOfLanguages);
            List<String> allLanguage = new List<String>();
            foreach (IWebElement ele in nameList)
            {
                string languageinList = ele.Text;
                if (languageinList.Contains(language))
                {
                    ele.Click();
                    return selectStatus = true;
                }

            }
            return selectStatus;
        }

        /// <summary>
        /// Select source language from dropdown
        /// </summary>
        ///<author>Nayana H</author>
        public bool SelectSourceLanguage()
        {
            string text = TranslationDTO.sourceLanguage;
            return SelectLanguage(text, sourceDropdown, By.XPath("//div[@class='OlSOob']//div[@class='OoYv6d']//div[@class='F29iQc']//div[@class='Llmcnf']"));
        }

        /// <summary>
        /// Select destination language from dropdown
        /// </summary>
        ///<author>Nayana H</author>
        public bool SelectDestinationLanguage()
        {
            string text = TranslationDTO.targetLanguage;
            return SelectLanguage(text, targetDropdown, By.XPath("//div[@class='qSb8Pe']//div[@class='Llmcnf']"));
        }

        public void SendInitialTextandSwipe()
        {
            pageActions.jsClickElement(driver, sourceArea);
            driver.FindElement(sourceArea).SendKeys(TranslationDTO.initialText);
            //pageActions.SendKeys(driver, sourceArea, TranslationDTO.initialText);
            Thread.Sleep(5000);
            driver.FindElement(swipButton).Click();
            //pageActions.jsClickElement(driver, swipButton);
            Thread.Sleep(5000);
        }
        public bool VerifyTextafterswipe()
        {
            SendInitialTextandSwipe();
            bool spanishStatus = false;
            bool germanStatus = false;
            string spanishText = driver.FindElement(sourceArea).GetAttribute("value").ToLower();
            string germanText = driver.FindElement(translatedText).Text.ToLower();
            string expText = TranslationDTO.expectedText.ToLower();
            string initialValue = TranslationDTO.initialText.ToLower();
            if (spanishText.Contains(expText))
            {
                spanishStatus = true;
            }
            if (germanText.Contains(initialValue))
            {
               germanStatus = true;
            }

            return spanishStatus && germanStatus;
        }

        /// <summary>
        /// clears text and click text from key board
        /// </summary>
        /// <returns>boolean value indicating the the text from key board entered is as expected</returns>
        /// <author>Nayana H</author>
        public bool clearAndenterfromKeyBoard()
        {
            pageActions.jsClickElement(driver, clearText);
            pageActions.jsClickElement(driver, keyBoard);
            pageActions.jsClickElement(driver, upperKeys);
            driver.FindElement(clickH).Click();
            driver.FindElement(clickI).Click();
            pageActions.jsClickElement(driver, clickUp);
            driver.FindElement(clickexclim).Click();
            Thread.Sleep(2000);
            string spanishText = driver.FindElement(sourceArea).GetAttribute("value");
            return spanishText.Contains(TranslationDTO.keyBoard);

        }
    }

}