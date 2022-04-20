using Newtonsoft.Json;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TechTalk.SpecFlow;
using PayCoreApiTest.model;
using System.Reflection;
using NUnit.Framework;
using OpenQA.Selenium;

namespace PayCoreApiTest.Helper
{
    [Binding]
    public class BrowserHelper
    {

        public RemoteWebDriver driver = null;
        DesiredCapabilities capabilities;
        public bool isRemoteDriver = false;
        private readonly string chrome = "chrome";
        public Uri uri = null;

        public Dictionary<string, KeyValuePair<string, string>> keyValuePairs;
        private static string BASE_EXT = "*.json";


        public void GetDriver()
        {
            Console.WriteLine("Remote ChromeDriver Ayaga kalkiyor");

            ChromeOptions options = new ChromeOptions();
            options.AddArguments("disable-popup-blocking");
            options.AddArguments("ignore-certificate-errors");
            options.AddArguments("--ignore-ssl-errors=yes");
           /* options.AddArguments("test-type");
            options.AddArguments("--allow-insecure-localhost");
            options.AddArguments("disable-translate");
            options.AddArguments("disable-automatic-password-saving");
            options.AddArguments("allow-silent-push");
            options.AddArguments("disable-infobars");
            options.AddArguments("--enable-print-preview");
            options.AddArguments("kiosk-printing");
            options.AddArguments("--use-fake-device-for-media-stream");
            options.AddUserProfilePreference("profile.default_content_setting_values.media_stream_camera", 1);
            options.AddAdditionalCapability("useAutomationExtension", false);
            options.BinaryLocation = @"C:\Program Files\Google\Chrome\Application\chrome.exe"; */
            Console.WriteLine("options eklendi");

            capabilities = options.ToCapabilities() as DesiredCapabilities;
            capabilities.SetCapability("testinium:browserName", chrome);
            capabilities.SetCapability("testinium:key", TestContext.Parameters.Get("key"));
            capabilities.SetCapability("platformName", "Windows 10");
            capabilities.SetCapability("acceptInsecureCerts", true);
            Console.WriteLine("capability eklendi");

            driver = new RemoteWebDriver(new Uri("http://10.44.23.135:4444/wd/hub"), capabilities, TimeSpan.FromSeconds(60));
            isRemoteDriver = true;
            Console.WriteLine("ChromeDriver kalkti");
            keyValuePairs = Degerver();

        }



        [Given(@"Driver'i ayaga kaldir")]
        public void driverAwake()
        {
            if (TestContext.Parameters.Get("key") == null)
            {
                Console.WriteLine("Local ChromeDriver Ayaga kalkiyor");
                string driverPath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"../../") + "Driver";
                ChromeOptions option1 = new ChromeOptions();
                option1.AddArguments("disable-popup-blocking");
                option1.AddArguments("ignore-certificate-errors");
                option1.AddArguments("--ignore-ssl-errors=yes");
               /* option1.AddArguments("test-type");
                option1.AddArguments("--allow-insecure-localhost");
                option1.AddArguments("disable-translate");
                option1.AddArguments("disable-automatic-password-saving");
                option1.AddArguments("allow-silent-push");
                option1.AddArguments("disable-infobars");
                option1.AddArguments("--enable-print-preview");
                option1.AddArguments("kiosk-printing");
                option1.AddArguments("--use-fake-device-for-media-stream");
                option1.AddUserProfilePreference("profile.default_content_setting_values.media_stream_camera", 1);
                option1.AddAdditionalCapability("useAutomationExtension", false);
                option1.BinaryLocation = @"C:\Program Files\Google\Chrome\Application\chrome.exe"; */
                driver = new ChromeDriver(driverPath, option1);
                isRemoteDriver = false;
                Console.WriteLine("ChromeDriver kalkti");

            }
            else
            {
                GetDriver();
            }
            keyValuePairs = Degerver();
        }


        public Dictionary<string, KeyValuePair<string, string>> Degerver()
        {
            Dictionary<string, KeyValuePair<string, string>> dic = new Dictionary<string, KeyValuePair<string, string>>();
            var txtFiles = Directory.EnumerateFiles(Testinium.StepImplementation.BASE_PATH_CONSTANTS, BASE_EXT);
            foreach (string currentFile in txtFiles)
            {
                var json = File.ReadAllText(currentFile);
                Dictionary<string, Element> d = JsonConvert.DeserializeObject<IEnumerable<Element>>(json).
                 Select(p => (Id: p.key, Record: p)).
                 ToDictionary(t => t.Id, t => t.Record);
                //Console.WriteLine("Okunan dosya: " + currentFile + " element sayısı: " + d.Count);
                foreach (var item in d)
                {
                    dic.Add(item.Key.ToString(), new KeyValuePair<string, string>(item.Value.type, item.Value.value));
                    //Console.WriteLine("Sözlüğe eklenen element -> Key:" + item.Key + " type: " + item.Value.type + " value: " + item.Value.value);
                }

            }

            Console.WriteLine("Sözlükteki toplam element sayısı:" + dic.Count);
            return dic;
        }


    }
}
