/*
 * HPE-iLO-Data-Gather
 *
 * Copyright (C) 2019 Peter Upfold.
 *
 * This file is licensed to you under the Apache License, version 2.0 (the "License").
 * You may not use this file except in compliance with the License. You may obtain
 * a copy of the License at
 *
 * http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software distributed under
 * the License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY
 * KIND, either express or implied. See the License for the specific language governing
 * permissions and limitations under the License.
 *
 */
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using HPE_iLO_Data_Gather.Properties;
using HPE_iLO_Data_Gather.Tests;
using Newtonsoft.Json;
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
            File.WriteAllText(
                Environment.ExpandEnvironmentVariables(Settings.Default["JSONOutput"].ToString()), 
                JsonConvert.SerializeObject(test.FanPercentages)
                );

        }
    }
}
