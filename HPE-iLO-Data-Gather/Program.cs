using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using HPE_iLO_Data_Gather.Tests;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace HPE_iLO_Data_Gather
{
    class Program
    {
        static void Main(string[] args)
        {
            FanSpeedTest test = new FanSpeedTest();
            test.TestFanSpeed();
            
            // write output of fan speed test


        }
    }
}
