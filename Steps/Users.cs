using NUnit.Framework;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Windows;
using System;
using TechTalk.SpecFlow;

namespace TelerikAssessment.Steps
{
    [Binding]
    public sealed class Users
    {

        private readonly ScenarioContext _scenarioContext;
        public Users(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        [Given(@"I launch the Telerik app")]
        public void GivenILaunchTheTelerikApp()
        {
            

        }



    }
}
