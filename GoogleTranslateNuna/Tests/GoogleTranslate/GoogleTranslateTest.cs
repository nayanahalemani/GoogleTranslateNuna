using GoogleTranslateNuna.BaseClass;
using GoogleTranslateNuna.PageObjects;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Translate.Tests
{
    [TestFixture]
    public class GoogleTranslateTest : BaseTest
    {
        protected GoogleTranslatePage gTranslation { get; private set; }

        [SetUp]
        public void setUp()
        {
            gTranslation = new GoogleTranslatePage(driver);
        }
        [Test, Order(1)]
        [Description("This method returns true after selecting languages")]
        public void SelectSourceLanguage()
        {
            Assert.IsTrue(gTranslation.SelectSourceLanguage());
        }

        [Test, Order(2)]
        [Description("This method returns true after selecting languages")]
        public void SelectDestinationLanguage()
        {
            Assert.IsTrue(gTranslation.SelectDestinationLanguage());
            
        }

        [Test, Order(3)]
        [Description("This method returns true after selecting languages")]
        public void SwipeText()
        {
            Assert.IsTrue(gTranslation.VerifyTextafterswipe());
        }
        [Test, Order(4)]
        [Description("This method returns true after selecting languages")]
        public void KeyBoardTextEnter()
        {
            Assert.IsTrue(gTranslation.clearAndenterfromKeyBoard());
        }

    }

}