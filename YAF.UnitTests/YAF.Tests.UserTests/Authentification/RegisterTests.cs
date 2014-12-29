/* Yet Another Forum.NET
 * Copyright (C) 2003-2005 Bjørnar Henden
 * Copyright (C) 2006-2013 Jaben Cargman
 * Copyright (C) 2014 Ingo Herbote
 * http://www.yetanotherforum.net/
 * 
 * Licensed to the Apache Software Foundation (ASF) under one
 * or more contributor license agreements.  See the NOTICE file
 * distributed with this work for additional information
 * regarding copyright ownership.  The ASF licenses this file
 * to you under the Apache License, Version 2.0 (the
 * "License"); you may not use this file except in compliance
 * with the License.  You may obtain a copy of the License at

 * http://www.apache.org/licenses/LICENSE-2.0

 * Unless required by applicable law or agreed to in writing,
 * software distributed under the License is distributed on an
 * "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY
 * KIND, either express or implied.  See the License for the
 * specific language governing permissions and limitations
 * under the License.
 */

namespace YAF.Tests.UserTests.Authentification
{
    using System;

    using NUnit.Framework;

    using OpenQA.Selenium;
    using OpenQA.Selenium.Chrome;

    using YAF.Tests.Utils;
    using YAF.Tests.Utils.Extensions;
    using YAF.Types.Extensions;

    /// <summary>
    /// The Register a test user Test.
    /// </summary>
    [TestFixture]
    public class RegisterUser
    {
        /// <summary>
        /// The Browser Instance
        /// </summary>
        private ChromeDriver driver;

        /// <summary>
        /// Gets or sets TestContext.
        /// </summary>
        public TestContext TestContext { get; set; }

        /// <summary>
        /// Logout New User
        /// </summary>
        [TearDown]
        public void TearDown()
        {
            this.driver.Navigate().GoToUrl(TestConfig.TestForumUrl);

            try
            {
                this.driver.FindElement(By.XPath("//a[contains(@id,'forum_ctl01_LogOutButton')]")).Click();

                this.driver.FindElementById("forum_ctl02_OkButton").Click();

                Assert.IsTrue(this.driver.PageSource.Contains("Welcome Guest"), "Logout Failed");

                this.driver.Quit();
            }
            catch (Exception)
            {
                this.driver.Quit();
            }
        }

        /// <summary>
        /// Register Random Test User Test
        /// </summary>
        [Category("Authentifcation")]
        [Test]
        public void Register_Random_New_User_Test()
        {
            this.driver = !TestConfig.UseExistingInstallation ? TestSetup._testBase.ChromeDriver : new ChromeDriver();

            // Create New Random Test User
            var random = new Random();

            var userName = "TestUser{0}".FormatWith(random.Next());

            Assert.IsTrue(this.driver.RegisterUser(userName, TestConfig.TestUserPassword), "Registration failed");
        }

        /// <summary>
        /// Register Random Test User Test
        /// </summary>
        [Category("Authentifcation")]
        [Test]
        public void Register_Bot_User_Test()
        {
            this.driver = !TestConfig.UseExistingInstallation ? TestSetup._testBase.ChromeDriver : new ChromeDriver();

            const string USERNAME = "aqiuliqemi";
            const string EMAIL = "ikocec@coveryourpills.org";

            // Check if Registrations are Disabled
            Assert.IsFalse(
                this.driver.PageSource.Contains("You tried to enter an area where you didn't have access"),
                "Registrations are disabled");

            // Accept the Rules
            if (this.driver.PageSource.Contains("Forum Rules"))
            {
                this.driver.FindElementById("forum_ctl04_Login1_LoginButton").Click();
                this.driver.Navigate().Refresh();
            }

            Assert.IsFalse(
                this.driver.PageSource.Contains("Security Image"),
                "Captchas needs to be disabled in order to run the tests");

            // Fill the Register Page
            this.driver.FindElement(
                By.XPath("//input[contains(@id,'CreateUserWizard1_CreateUserStepContainer_UserName')]"))
                .SendKeys(USERNAME);

            if (this.driver.PageSource.Contains("Display Name"))
            {
                this.driver.FindElement(
                    By.XPath("//input[contains(@id,'CreateUserWizard1_CreateUserStepContainer_DisplayName')]"))
                    .SendKeys(USERNAME);
            }

            this.driver.FindElement(
                By.XPath("//input[contains(@id,'CreateUserWizard1_CreateUserStepContainer_Password')]"))
                .SendKeys(TestConfig.TestUserPassword);
            this.driver.FindElement(
                By.XPath("//input[contains(@id,'CreateUserWizard1_CreateUserStepContainer_ConfirmPassword')]"))
                .SendKeys(TestConfig.TestUserPassword);
            this.driver.FindElement(
                By.XPath("//input[contains(@id,'CreateUserWizard1_CreateUserStepContainer_Email')]")).SendKeys(EMAIL);
            this.driver.FindElement(
                By.XPath("//input[contains(@id,'CreateUserWizard1_CreateUserStepContainer_Question')]"))
                .SendKeys(TestConfig.TestUserPassword);
            this.driver.FindElement(
                By.XPath("//input[contains(@id,'CreateUserWizard1_CreateUserStepContainer_Answer')]"))
                .SendKeys(TestConfig.TestUserPassword);

            // Create User
            this.driver.FindElement(
                By.XPath("//input[contains(@id,'_StepNextButton')]")).Click();

            Assert.IsTrue(
                this.driver.PageSource.Contains("Sorry Spammers are not allowed in the Forum!"),
                "Spam Check Failed");
        }
    }
}