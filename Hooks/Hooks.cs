using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Windows;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using TechTalk.SpecFlow;

namespace TelerikAssessment.Hooks
{
    [Binding]
    public class Hooks
    {
        private const string WindowsApplicationDriverUrl = "http://127.0.0.1:4723";
        protected static WindowsDriver<WindowsElement> Session;
        
        
        private const string WindowControlName = "Telerik UI for WPF - Demos - 1 running window";
        private string TelerikappPath = @"C:\Users\tgbadebo\Desktop\Telerik UI for WPF - Demos.appref-ms"; 
        //string TelerikappPath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "\\Desktop\\Telerik UI for WPF - Demos.appref-ms"; 
        private readonly dynamic MenuListControls = "goToControlButton";
        [BeforeScenario]
        public void BeforeScenario()
        {
            if (Session == null)
            {
                var cap = new AppiumOptions();
                cap.AddAdditionalCapability("app", TelerikappPath);
                cap.AddAdditionalCapability("deviceName", "Windows");

                try
                {
                    Session = new WindowsDriver<WindowsElement>(new Uri(WindowsApplicationDriverUrl),
                        cap, TimeSpan.FromMinutes(1));

                }
                catch (Exception)
                {
                    var desktopCap = new AppiumOptions();
                    desktopCap.AddAdditionalCapability("platformName", "Windows");
                    desktopCap.AddAdditionalCapability("app", "Root");
                    desktopCap.AddAdditionalCapability("deviceName", "WindowsPC");
                  
                    Session = new WindowsDriver<WindowsElement>(new Uri(WindowsApplicationDriverUrl), cap);
                    var window = Session.FindElementByName(WindowControlName);
                }
                WaitForElementBy("Name", WindowControlName);
                Session.FindElementByName(WindowControlName).Click();
                Session.Manage().Timeouts().ImplicitWait = TimeSpan.FromMinutes(10);
                Session.FindElementByAccessibilityId(MenuListControls[2]).Click();
            }
            Console.WriteLine(Session.Title);
        }

        [AfterScenario]
        public void AfterScenario()
        {

            if (Session != null)
            {
                Session.Quit();
            }
            Session = null;
         
        }


        public static void WaitForElementBy(string by, string element)
        {
            var wait = new DefaultWait<WindowsDriver<WindowsElement>>(Session)
            {
                Timeout = TimeSpan.FromSeconds(120),
                PollingInterval = TimeSpan.FromSeconds(1)
            };
            wait.IgnoreExceptionTypes(typeof(InvalidOperationException));

            WindowsElement mainWindows = null;

            wait.Until(session =>
            {
                try
                {
                    session.SwitchTo().Window(session.WindowHandles[0]);
                    mainWindows = @by switch
                    {
                        "AutomationId" => session.FindElementByAccessibilityId(element),
                        "Xpath" => session.FindElementByXPath(element),
                        "Name" => session.FindElementByName(element),
                        _=> mainWindows
                    };
                }
                catch (Exception)
                {

                    mainWindows = null;
                }

                return mainWindows != null;
            });
        }
    }
}
