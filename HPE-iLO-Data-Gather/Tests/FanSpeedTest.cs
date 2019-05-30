using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using HPE_iLO_Data_Gather.Properties;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace HPE_iLO_Data_Gather.Tests
{
    class FanSpeedTest
    {

        public Dictionary<int, string> FanPercentages;

        [Test(ExpectedResult = true)]
        public bool TestFanSpeed()
        {
            IWebDriver driver = new ChromeDriver();
            Debug.WriteLine($"Starting driver");
            driver.Url = Settings.Default["iLOURL"].ToString();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(15);

            IWebElement frame = driver.FindElement(By.Id("modalFrame"));

            driver.SwitchTo().Frame(frame);
            Debug.WriteLine("About to check for u&p boxes");
            IWebElement usernameBox = driver.FindElement(By.Id("usernameInput"));
            IWebElement passwordBox = driver.FindElement(By.Id("passwordInput"));
            Debug.WriteLine($"Found {usernameBox} and {passwordBox}");

            Thread.Sleep(3000);

            usernameBox.Click();
            usernameBox.SendKeys(Settings.Default["iLOUsername"].ToString());

            passwordBox.Click();
            passwordBox.SendKeys(Settings.Default["iLOPassword"].ToString());

            driver.FindElement(By.Id("ID_LOGON")).Click();

            Debug.WriteLine("Clicked logon button");

            Thread.Sleep(8000);

            // focus appframe
            Debug.WriteLine("Waiting to focus appframe");
            driver.SwitchTo().ParentFrame();
            IWebElement appFrame = driver.FindElement(By.Id("appFrame"));
            driver.SwitchTo().Frame(appFrame);
            Debug.WriteLine("Switched to appframe");

            // frame directory
            driver.SwitchTo().Frame(driver.FindElement(By.Id("frameDirectory")));

            // click System Information
            driver.FindElement(By.Id("tabset_sysInfo")).Click();
            Debug.WriteLine("Clicked sysinfo");

            driver.SwitchTo().ParentFrame(); // back to appframe

            // wait for #frameContent to appear
            IWebElement frameContent = driver.FindElement(By.Id("frameContent"));
            driver.SwitchTo().Frame(frameContent);

            // click Fans
            driver.FindElement(By.Id("tab_fans")).Click();
            Debug.WriteLine("Clicked fans");

            // wait for #iframeContent (why you have so many frames)
            IWebElement iframeContent = driver.FindElement(By.Id("iframeContent"));
            driver.SwitchTo().Frame(iframeContent);
            Debug.WriteLine("Switched to iframeContent");

            

            // get fan speeds

            FanPercentages = new Dictionary<int, string>();

            // 6 fans

            bool shouldFailTest = false;

            for (int i = 1; i <= 6; i++)
            {
                IWebElement fanCell = driver.FindElement(By.Id($"fan_Fan {i}_speed"));
                FanPercentages.Add(i, fanCell.Text);
                Debug.WriteLine($"Fan {i} is at {fanCell.Text}");

                if (int.TryParse(fanCell.Text.Trim('%'), out int fanSpeedInt)) {
                    if (fanSpeedInt > 50)
                    {
                        Debug.WriteLine($"Fan {i} is running at {fanSpeedInt}, so the test should fail");
                        shouldFailTest = true;
                    }
                }

            }

            // find logout button in header frame
            driver.SwitchTo().ParentFrame(); // now in framecontent
            driver.SwitchTo().ParentFrame(); // now in appframe

            driver.SwitchTo().Frame(driver.FindElement(By.Id("headerFrame")));
            driver.FindElement(By.Id("logout_button")).Click();

            Thread.Sleep(5000);

            driver.Quit();

            return !shouldFailTest;
        }
    }
}
