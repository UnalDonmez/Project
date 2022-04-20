
using System;
using System.Threading;
using TechTalk.SpecFlow;


using NUnit.Framework;

using System.Collections.Generic;

using System.IO;

using System.Xml;
using PayCoreApiTest;
using System.Text;

using System.Reflection;
using System.Linq;
using PayCoreApiTest.Helper;

namespace Testinium
{

    [Binding]
    public class StepImplementation
    {

        string api;
        Dictionary<string, object> hashMap = new Dictionary<string, object>();
        XmlDocument doc = new XmlDocument();
        public static bool pathInfo = false;
        StringBuilder sb = null;
        public static string BASE_PATH_CONSTANTS = "";

        private BrowserHelper _browserHelper;
        private BasePage _basePage;

        public StepImplementation()
        {

        }
        public StepImplementation(BrowserHelper browserHelper)
        {
            _browserHelper = browserHelper;
            _basePage = new BasePage(_browserHelper);
        }



        [BeforeScenario]
        [Obsolete]
        public void setUp()
        {
            if (TestContext.Parameters.Get("key") == null)
            {
                api = "https://myUrl.com/";
                Console.WriteLine("Test localde ayağa kalkacak");
                BASE_PATH_CONSTANTS = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"../../") + "Constants1";
            }
            else
            {
                Console.WriteLine("Test Testiniumda ayağa kalkacak");
                api = "https://" + TestContext.Parameters.Get("api") + ".myUrl.com/";
                Console.WriteLine("Testin ayağa kalktığı ortam: " + api);
                BASE_PATH_CONSTANTS = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"../demo/PayCoreApiTest/") + "Constants1";

            }


            Console.WriteLine("======================= Test Setup Before Scenario =======================");



        }


        [AfterScenario]
        public void afterScenario()
        {


            try
            {
                if (null != _browserHelper.driver)
                {
                    _browserHelper.driver.Quit();
                    Console.WriteLine("Driver Quited After Scenario");
                }
            }
            catch
            {
                Console.WriteLine("Driver önceden kapatılmış");
            }
        }



        //==================================================================== Driver ==============================================================================




        [Given(@"(.*) elementine tıkla")]
        public void clickElement(string key)
        {
            _basePage.clickBP(key);
        }

        [Given(@"(.*) elementine tıkla stale")]
        public void clickElementStale(string key)
        {
            _basePage.clickStale(key);
        }

        [Given(@"(.*) elementine js ile tıkla")]
        public void clickElementJS(string key)
        {
            _basePage.clickJS(key);
        }

        [Given(@"(.*),(.*) elementlerine sırayla tıkla")]
        public void clickElement(string key, string key2)
        {
            _basePage.clickDD(key, key2);
        }

        [Given(@"(.*) elementinin görünürlüğü kontrol edilir")]
        public void checkElementIsDisplayed(string key)
        {
            _basePage.checkElementIsDisplayed(key);
            
        }

        [Given(@"sayfa (.*) değerini içermiyor")]
        public void pageNotContainsText(string text)
        {
            _basePage.pageNotContainsText(text);
        }

        [Given(@"(.*) elementinden drop (.*) value değerini seç")]
        public void selectOptionDropDownByValue(string key, string value)
        {
            _basePage.selectOptionDropDownByValue(key, value);

        }
        [Given(@"(.*) elementinden drop (.*) indexini seç")]
        public void selectOptionDropDownByIndex(string key, int index)
        {
            _basePage.selectOptionDropDownByIndex(key, index);
        }

        [Given(@"(.*) elementinin varlığı kontrol edilir")]
        public void checkElementIsNull(string key)
        {
            _basePage.checkElement(key);
        }

        [Given(@"(.*) elementine (.*) textini yaz")]
        public void fillTextBox(string key, string text)
        {
            _basePage.fillTextBox(key, text);
        }

        [Given(@"(.*) adresine git")]
        public void goToUrl(string url)
        {
            _basePage.goToUrl(url);
            Console.WriteLine(url + " adresine gidildi");
        }

        [Given(@"tab text al")]
        public void getTabTitle()
        {
            string title = _browserHelper.driver.Title;
            Console.WriteLine(title);
            Assert.IsTrue(title == "Beymen.com – Lifestyle Destination","Sonuc dogru");
            
        }

        [Given(@"(.*) elementinin text'i (.*) hashMap keyi ile aynı mı?")]
        [Obsolete]
        public void ısEqualText(string element, string key)
        {
            Assert.IsTrue(hashMap[key].ToString() == _basePage.findElement(element).Text.ToString(),
            "Key değeriyle elementin text değeri aynı değil");

        }


        [Given(@"(.*) saniye bekle")]
        public void waitBySecond(int second)
        {
            Thread.Sleep(second * 1000);
        }

        [Given(@"Geri git")]
        public void goBack()
        {
            _basePage.goBack();
        }

        [Given(@"Ileri git")]
        public void forward()
        {
            _basePage.forward();
        }

        [Given(@"Sayfayı Yenile")]
        public void refreshPage()
        {
            _basePage.refreshPage();
        }

        [Given(@"Son Sekmeye Odaklan")]
        public void switchToLastWindow()
        {
            _basePage.switchToLastWindow();
        }

        [Given(@"File Upload")]
        public void fileUpload()
        {
            _basePage.fileUpload();
        }

        [Given(@"sayfa (.*) değerini içeriyor mu")]
        public void pageContainsText(string text)
        {
            _basePage.pageContainsText(text);
        }

        [Given(@"(.*) elementini hoverla")]
        public void HoverElement(string key)
        {
            _basePage.HoverElement(key);
        }

        [Given(@"(.*) element listesinden random elemente tıkla")]
        public void clickRandomElement(string key)
        {
            _basePage.clickRandomElement(key);
        }

        [Given(@"(.*) element listesinden tıklanabilir elemente tıkla")]
        public void IsclickableRandomElement(string key)
        {
            _basePage.IsclickableRandomElement(key);
        }

        [Given(@"(.*) elementi (.*) değerini içeriyor mu")]
        public void elementContainsText(string key, string text)
        {
            _basePage.elementContainsText(key, text);
        }


        [Given(@"Driver'i kapat")]
        public void driverClose()
        {
            _browserHelper.driver.Quit();
            Console.WriteLine("Driver Quited");
        }

        [Given(@"Url'de response kod ""(.*)"" olarak donuyor mu")]
        public void driverUrlResponse(string resCode)
        {
            _browserHelper.uri = new Uri(_browserHelper.driver.Url);
            string responseCodee = _browserHelper.uri.Query;
            Assert.AreEqual(responseCodee, "?responseCode=" + resCode, "Response Code eşleşmiyor");
        }


        [Given(@"(.*) elementinin text'ini (.*) keyi ile hashmap'e ekle")]
        [Obsolete]
        public void driverGetElementText(string element, string key)
        {
            hashMap.Add(key, (_basePage.findElement(element).Text).ToString());
            Console.WriteLine("Hashmape eklenen text: " + hashMap[key]);
        }


        [Given(@"(.*) elementine hashmapteki (.*) değerini gir")]
        [Obsolete]
        public void driverSetElementText(string element, string key)
        {
            _basePage.fillTextBox(element, hashMap[key].ToString());
            Console.WriteLine("yazılan değer şu: " + hashMap[key].ToString());
        }

        [Given(@"(.*) elementine (.*) tarihini gir")]
        public void leftpress(string key, string text)
        {
            _basePage.leftPress(key, text);
            Console.WriteLine(key + "'ine " + text + " tarihi eklendi.");
        }

        [Given(@"(.*) elementini temizle")]
        public void clearElement(string key)
        {
            _basePage.clearElement(key);
            Console.WriteLine(key + "'inin text'i temizlendi");
        }


        [Given(@"(.*) keyli hashmap adresi")]
        public void goToUrlHashMap(string key)
        {
            _basePage.goToUrl(hashMap[key].ToString());
        }


        [Given(@"(.*) keyli hashmap adresine (.*) parametresi ile git")]
        public void driverGoToUrlFromHashMapviaParam(string key, string param)
        {
            _basePage.goToUrl(hashMap[key].ToString() + "&" + param);
        }


        //-------------------------------------------------------------------- Endof Driver -----------------------------------------------------------------------------------






        //==================================================================== Others ==============================================================================

        [Given(@"Wait ""(.*)"" seconds")]
        public void waitSeconds(int i)
        {
            Console.WriteLine(i + " saniye bekleniyor");
            Thread.Sleep(TimeSpan.FromSeconds(i));
        }






    }

}


