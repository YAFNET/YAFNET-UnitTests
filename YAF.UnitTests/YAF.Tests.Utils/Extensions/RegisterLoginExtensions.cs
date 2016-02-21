/* Yet Another Forum.NET
 * Copyright (C) 2003-2005 Bjørnar Henden
 * Copyright (C) 2006-2013 Jaben Cargman
 * Copyright (C) 2014-2016 Ingo Herbote
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

namespace YAF.Tests.Utils.Extensions
{
    using OpenQA.Selenium;
    using OpenQA.Selenium.Chrome;

    using YAF.Types.Extensions;

    /// <summary>
    /// Test Helper Class
    /// </summary>
    public static class RegisterLoginExtensions
    {
        /// <summary>
        /// Registers the standard test user.
        /// </summary>
        /// <param name="driver">The <paramref name="driver"/> instance.</param>
        /// <param name="userName">Name of the user.</param>
        /// <param name="password">The password.</param>
        /// <returns>
        /// If User was Registered or not
        /// </returns>
        public static bool RegisterUser(this ChromeDriver driver, string userName, string password)
        {
            return RegisterUser(driver, userName, "{0}@test.com".FormatWith(userName.ToLower()), password);
        }

        /// <summary>
        /// Registers the standard test user.
        /// </summary>
        /// <param name="driver">The <paramref name="driver" /> instance.</param>
        /// <param name="userName">Name of the user.</param>
        /// <param name="email">The email.</param>
        /// <param name="password">The password.</param>
        /// <returns>
        /// If User was Registered or not
        /// </returns>
        public static bool RegisterUser(this ChromeDriver driver, string userName, string email, string password)
        {
            driver.Navigate().GoToUrl("{0}{1}register.aspx".FormatWith(TestConfig.TestForumUrl, TestConfig.ForumUrlRewritingPrefix));

            // Check if Registrations are Disabled
            if (driver.PageSource.Contains("You tried to enter an area where you didn't have access"))
            {
                return false;
            }

            // Accept the Rules
            if (driver.PageSource.Contains("Forum Rules"))
            {
                driver.FindElementById("forum_ctl04_Login1_LoginButton").Click();
                driver.Navigate().Refresh();
            }

            if (driver.PageSource.Contains("Security Image"))
            {
                return false;
            }

            // Fill the Register Page
            driver.FindElement(By.XPath("//input[contains(@id,'CreateUserWizard1_CreateUserStepContainer_UserName')]")).SendKeys(
                userName);

            if (driver.PageSource.Contains("Display Name"))
            {
                driver.FindElement(By.XPath("//input[contains(@id,'CreateUserWizard1_CreateUserStepContainer_DisplayName')]")).SendKeys(userName);
            }

            driver.FindElement(By.XPath("//input[contains(@id,'CreateUserWizard1_CreateUserStepContainer_Password')]")).SendKeys(password);
            driver.FindElement(By.XPath("//input[contains(@id,'CreateUserWizard1_CreateUserStepContainer_ConfirmPassword')]")).SendKeys(password);
            driver.FindElement(By.XPath("//input[contains(@id,'CreateUserWizard1_CreateUserStepContainer_Email')]")).SendKeys(email);
            driver.FindElement(By.XPath("//input[contains(@id,'CreateUserWizard1_CreateUserStepContainer_Question')]")).SendKeys(password);
            driver.FindElement(By.XPath("//input[contains(@id,'CreateUserWizard1_CreateUserStepContainer_Answer')]")).SendKeys(password);

            ////driver.FindElement(By.XPath("CreateUserWizard1_CreateUserStepContainer_tbCaptcha")).SendKeys(captcha);

            // Create User
            driver.FindElement(By.XPath("//input[contains(@id,'_StepNextButton')]")).ClickAndWait();

            if (!driver.PageSource.Contains("Forum Preferences"))
            {
                return false;
            }

            driver.FindElement(By.XPath("//input[contains(@id,'ProfileNextButton')]")).ClickAndWait();

            driver.Navigate().Refresh();

            return driver.FindElement(By.XPath("//a[contains(@id,'LogOutButton')]")) != null;
        }

        /// <summary>
        /// Logins the user.
        /// </summary>
        /// <param name="driver">The <paramref name="driver"/> instance.</param>
        /// <param name="userName">Name of the user.</param>
        /// <param name="userPassword">The user password.</param>
        /// <returns>If User login was successfully or not</returns>
        public static bool LoginUser(this ChromeDriver driver, string userName, string userPassword)
        {
            // Login User
            driver.Navigate().GoToUrl("{0}{1}login.aspx".FormatWith(TestConfig.TestForumUrl, TestConfig.ForumUrlRewritingPrefix));

            // Check If User is already Logged In
            if (!driver.PageSource.Contains("Forum Login"))
            {
                driver.FindElement(By.XPath("//a[contains(@id,'_LogOutButton')]")).ClickAndWait();

                driver.FindElementById("forum_ctl02_OkButton").Click();

                driver.Navigate().GoToUrl("{0}{1}login.aspx".FormatWith(TestConfig.TestForumUrl, TestConfig.ForumUrlRewritingPrefix));
            }

            driver.FindElement(By.XPath("//input[contains(@id,'Login1_UserName')]")).SendKeys(userName);
            driver.FindElement(By.XPath("//input[contains(@id,'Login1_Password')]")).SendKeys(userPassword);

            driver.FindElement(By.XPath("//input[contains(@id,'Login1_LoginButton')]")).ClickAndWait();

            driver.Navigate().GoToUrl(TestConfig.TestForumUrl);

            return driver.ElementExists(By.XPath("//a[contains(@id,'LogOutButton')]"));
        }
    }
}
