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

namespace YAF.Tests.Utils
{
    using OpenQA.Selenium;
    using OpenQA.Selenium.Chrome;

    using YAF.Tests.Utils.Extensions;
    using YAF.Types.Extensions;

    /// <summary>
    /// Test Helper Class
    /// </summary>
    public class TestHelper
    {
        /// <summary>
        /// Registers the standard test user.
        /// </summary>
        /// <param name="browser">The <paramref name="browser"/> instance.</param>
        /// <param name="userName">Name of the user.</param>
        /// <param name="password">The password.</param>
        /// <returns>
        /// If User was Registered or not
        /// </returns>
        public static bool RegisterStandardTestUser(ChromeDriver browser, string userName, string password)
        {
            browser.Navigate().GoToUrl("{0}{1}register.aspx".FormatWith(TestConfig.TestForumUrl, TestConfig.ForumUrlRewritingPrefix));

            var email = "{0}@test.com".FormatWith(userName.ToLower());

            // Check if Registrations are Disabled
            if (browser.PageSource.Contains("You tried to enter an area where you didn't have access"))
            {
                return false;
            }

            // Accept the Rules
            if (browser.PageSource.Contains("Forum Rules"))
            {
                browser.FindElementById("forum_ctl04_Login1_LoginButton").Click();
                browser.Navigate().Refresh();
            }

            if (browser.PageSource.Contains("Security Image"))
            {
                return false;
            }

            // Fill the Register Page
            browser.FindElement(By.XPath("//input[contains(@id,'CreateUserWizard1_CreateUserStepContainer_UserName')]")).SendKeys(
                userName);

            if (browser.PageSource.Contains("Display Name"))
            {
                browser.FindElement(By.XPath("//input[contains(@id,'CreateUserWizard1_CreateUserStepContainer_DisplayName')]")).SendKeys(userName);
            }

            browser.FindElement(By.XPath("//input[contains(@id,'CreateUserWizard1_CreateUserStepContainer_Password')]")).SendKeys(password);
            browser.FindElement(By.XPath("//input[contains(@id,'CreateUserWizard1_CreateUserStepContainer_ConfirmPassword')]")).SendKeys(password);
            browser.FindElement(By.XPath("//input[contains(@id,'CreateUserWizard1_CreateUserStepContainer_Email')]")).SendKeys(email);
            browser.FindElement(By.XPath("//input[contains(@id,'CreateUserWizard1_CreateUserStepContainer_Question')]")).SendKeys(password);
            browser.FindElement(By.XPath("//input[contains(@id,'CreateUserWizard1_CreateUserStepContainer_Answer')]")).SendKeys(password);

            ////browser.FindElement(By.XPath("CreateUserWizard1_CreateUserStepContainer_tbCaptcha")).SendKeys(captcha);

            // Create User
            browser.FindElement(By.XPath("//a[contains(@id,'CreateUserWizard1_CreateUserStepContainer_StepNextButton')]")).Click();

            if (!browser.PageSource.Contains("Forum Preferences"))
            {
                return false;
            }

            browser.FindElement(By.XPath("//a[contains(@id,'ProfileNextButton')]")).Click();

            return browser.FindElement(By.XPath("//a[contains(@id,'LogOutButton')]")) != null;
        }

        /// <summary>
        /// Logins the user.
        /// </summary>
        /// <param name="browser">The <paramref name="browser"/> instance.</param>
        /// <param name="userName">Name of the user.</param>
        /// <param name="userPassword">The user password.</param>
        /// <returns>If User login was successfully or not</returns>
        public static bool LoginUser(ChromeDriver browser, string userName, string userPassword)
        {
            // Login User
            browser.Navigate().GoToUrl("{0}{1}login.aspx".FormatWith(TestConfig.TestForumUrl, TestConfig.ForumUrlRewritingPrefix));

            // Check If User is already Logged In
            if (!browser.PageSource.Contains("Forum Login"))
            {
                browser.FindElement(By.XPath("//a[contains(@id,'_LogOutButton')]")).ClickAndWait();

                browser.FindElementById("forum_ctl02_OkButton").Click();

                browser.Navigate().GoToUrl("{0}{1}login.aspx".FormatWith(TestConfig.TestForumUrl, TestConfig.ForumUrlRewritingPrefix));
            }

            browser.FindElement(By.XPath("//input[contains(@id,'Login1_UserName')]")).SendKeys(userName);
            browser.FindElement(By.XPath("//input[contains(@id,'Login1_Password')]")).SendKeys(userPassword);

            browser.FindElement(By.XPath("//input[contains(@id,'Login1_LoginButton')]")).ClickAndWait();

            browser.Navigate().GoToUrl(TestConfig.TestForumUrl);

            return browser.ElementExists(By.XPath("//a[contains(@id,'LogOutButton')]"));
        }
    }
}
