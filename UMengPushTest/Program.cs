﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using UMengPush;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            Push app = new Push("xxx", "xxx");
            app.sendAndroidUnicast();
            //app.sendIOSBroadcast();
            /* TODO these methods are all available, just fill in some fields and do the test
             * demo.sendAndroidCustomizedcastFile();
             * demo.sendAndroidBroadcast();
             * demo.sendAndroidGroupcast();
             * demo.sendAndroidCustomizedcast();
             * demo.sendAndroidFilecast();
             * 
             * demo.sendIOSBroadcast();
             * demo.sendIOSUnicast();
             * demo.sendIOSGroupcast();
             * demo.sendIOSCustomizedcast();
             * demo.sendIOSFilecast();
             */
            Console.ReadKey();
        }
    }
}
