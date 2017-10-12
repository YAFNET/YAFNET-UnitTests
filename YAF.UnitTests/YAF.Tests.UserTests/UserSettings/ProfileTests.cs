/* Yet Another Forum.NET
 * Copyright (C) 2003-2005 Bjørnar Henden
 * Copyright (C) 2006-2013 Jaben Cargman
 * Copyright (C) 2014-2017 Ingo Herbote
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

namespace YAF.Tests.UserTests.UserSettings
{
    using System;

    using NUnit.Framework;

    using OpenQA.Selenium;
    using OpenQA.Selenium.Chrome;

    using YAF.Tests.Utils;
    using YAF.Tests.Utils.Extensions;
    using YAF.Types.Extensions;

    /// <summary>
    /// The User Profile tests.
    /// </summary>
    [TestFixture]
    public class ProfileTests : TestBase
    {
        /// <summary>
        /// Gets or sets TestContext.
        /// </summary>
        public TestContext TestContext { get; set; }

        /// <summary>
        /// Login User Setup
        /// </summary>
        [OneTimeSetUp]
        public void SetUpTest()
        {
            this.Driver = !TestConfig.UseExistingInstallation ? TestSetup._testBase.ChromeDriver : new ChromeDriver();

            Assert.IsTrue(this.LoginUser(), "Login failed");
        }

        /// <summary>
        /// Logout Test User
        /// </summary>
        [OneTimeTearDown]
        public void TearDownTest()
        {
            this.LogoutUser();
        }

        /// <summary>
        /// Changes the forum language test.
        /// </summary>
        [Test]
        public void Change_Forum_Language_Test()
        {
            this.Driver.Navigate()
                .GoToUrl(
                    "{0}{1}cp_editprofile.aspx".FormatWith(TestConfig.TestForumUrl, TestConfig.ForumUrlRewritingPrefix));

            Assert.IsTrue(
                this.Driver.PageSource.Contains("Edit Profile"),
                "Edit Profile is not available for that User");

            Assert.IsTrue(
                this.Driver.PageSource.Contains("What language do you want to use"),
                "Changing the Language is disabled for Users");

            // Switch Language to German
            this.Driver.SelectDropDownByValue(By.XPath("//select[contains(@id,'_ProfileEditor_Culture')]"), "de-DE");

            // Save the Profile Changes
            this.Driver.FindElement(By.XPath("//input[contains(@id,'_ProfileEditor_UpdateProfile')]")).Click();

            this.Driver.Navigate().Refresh();

            Assert.IsTrue(this.Driver.PageSource.Contains("Angemeldet als"), "Changing Language failed");

            this.Driver.Navigate()
                .GoToUrl(
                    "{0}{1}cp_editprofile.aspx".FormatWith(TestConfig.TestForumUrl, TestConfig.ForumUrlRewritingPrefix));

            // Switch Language Back to English
            this.Driver.SelectDropDownByValue(By.XPath("//select[contains(@id,'_ProfileEditor_Culture')]"), "en-US");

            // Save the Profile Changes
            this.Driver.FindElement(By.XPath("//input[contains(@id,'_ProfileEditor_UpdateProfile')]")).Click();
        }

        /// <summary>
        /// Changes the forum theme test.
        /// </summary>
        [Test]
        public void Change_Forum_Theme_Test()
        {
            this.Driver.Navigate()
                .GoToUrl(
                    "{0}{1}cp_editprofile.aspx".FormatWith(TestConfig.TestForumUrl, TestConfig.ForumUrlRewritingPrefix));

            Assert.IsTrue(
                this.Driver.PageSource.Contains("Edit Profile"),
                "Edit Profile is not available for that User");

            Assert.IsTrue(
                this.Driver.PageSource.Contains("Select your preferred theme"),
                "Changing the Theme is disabled for Users");

            // Switch Theme to "Black Grey"
            this.Driver.SelectDropDownByValue(
                By.XPath("//select[contains(@id,'_ProfileEditor_Theme')]"),
                "BlackGrey.xml");

            // Save the Profile Changes
            this.Driver.FindElement(By.XPath("//input[contains(@id,'_ProfileEditor_UpdateProfile')]")).Click();

            this.Driver.Navigate().Refresh();

            Assert.IsNotNull(
                this.Driver.ElementExists(By.XPath("//link[contains(@href,'Themes/BlackGrey/theme.css')]")),
                "Changing Forum Theme failed");

            this.Driver.Navigate()
                .GoToUrl(
                    "{0}{1}cp_editprofile.aspx".FormatWith(TestConfig.TestForumUrl, TestConfig.ForumUrlRewritingPrefix));

            // Switch Theme Back to Clean Slate
            this.Driver.SelectDropDownByValue(
                By.XPath("//select[contains(@id,'_ProfileEditor_Theme')]"),
                "cleanSlate.xml");

            // Save the Profile Changes
            this.Driver.FindElement(By.XPath("//input[contains(@id,'_ProfileEditor_UpdateProfile')]")).Click();

            Assert.IsNotNull(
                this.Driver.ElementExists(By.XPath("//link[contains(@href,'Themes/cleanSlate/theme.css')]")),
                "Changing Forum Theme failed");
        }

        /// <summary>
        /// Change the forum user text editor test.
        /// </summary>
        [Test]
        public void Change_Forum_User_TextEditor_Test()
        {
            this.Driver.Navigate()
                .GoToUrl(
                    "{0}{1}cp_editprofile.aspx".FormatWith(TestConfig.TestForumUrl, TestConfig.ForumUrlRewritingPrefix));

            Assert.IsTrue(
                this.Driver.PageSource.Contains("Edit Profile"),
                "Edit Profile is not available for that User");

            Assert.IsTrue(
                this.Driver.PageSource.Contains("Select your text editor"),
                "Changing the TextEditor is disabled for Users");

            // Switch Editor to CKEditor (BBCode)
            this.Driver.SelectDropDownByValue(
                By.XPath("//select[contains(@id,'ProfileEditor_ForumEditor')]"),
                "-1743422651");

            // Save the Profile Changes
            this.Driver.FindElement(By.XPath("//input[contains(@id,'_ProfileEditor_UpdateProfile')]")).ClickAndWait();

            // Check if Editor Is Correct
            this.Driver.Navigate()
                .GoToUrl(
                    "{0}{2}postmessage.aspx?f={1}".FormatWith(
                        TestConfig.TestForumUrl,
                        TestConfig.TestForumID,
                        TestConfig.ForumUrlRewritingPrefix));

            Assert.IsNotNull(
                this.Driver.ElementExists(By.XPath("//script[contains(@src,'Scripts/ckeditor/config.js')]")),
                "Changing Text Editor Failed failed");

            // Switch Editor Back
            this.Driver.Navigate()
                .GoToUrl(
                    "{0}{1}cp_editprofile.aspx".FormatWith(TestConfig.TestForumUrl, TestConfig.ForumUrlRewritingPrefix));

            this.Driver.SelectDropDownByValue(By.XPath("//select[contains(@id,'ProfileEditor_ForumEditor')]"), "1");

            // Save the Profile Changes
            this.Driver.FindElement(By.XPath("//input[contains(@id,'_ProfileEditor_UpdateProfile')]")).ClickAndWait();

            // Check if Editor Is Correct
            this.Driver.Navigate()
                .GoToUrl(
                    "{0}{2}postmessage.aspx?f={1}".FormatWith(
                        TestConfig.TestForumUrl,
                        TestConfig.TestForumID,
                        TestConfig.ForumUrlRewritingPrefix));

            // Check if Editor Is Correct
            Assert.IsNotNull(this.Driver.FindElementByClassName("ButtonOut"), "Changing Text Editor Failed failed");
        }

        /// <summary>
        /// Change the user email address test.
        /// </summary>
        [Test]
        public void Change_User_Email_Address_Test()
        {
            this.Driver.Navigate()
                .GoToUrl(
                    "{0}{1}cp_editprofile.aspx".FormatWith(TestConfig.TestForumUrl, TestConfig.ForumUrlRewritingPrefix));

            Assert.IsTrue(
                this.Driver.PageSource.Contains("Edit Profile"),
                "Edit Profile is not available for that User");

            Assert.IsTrue(
                this.Driver.PageSource.Contains("Change Email Address"),
                "Changing the Email Address is disabled for Users");

            // Switch Theme to "Black Grey"
            var emailInput = this.Driver.FindElement(By.XPath("//input[contains(@id,'_ProfileEditor_Email')]"));

            var oldEmailAddress = emailInput.GetAttribute("value");
            const string NewEmailAddress = "testmail123@localhost.com";

            emailInput.Clear();

            emailInput.SendKeys(NewEmailAddress);

            // Save the Profile Changes
            this.Driver.FindElement(By.XPath("//input[contains(@id,'_ProfileEditor_UpdateProfile')]")).Click();

            this.Driver.Navigate()
                .GoToUrl(
                    "{0}{1}cp_profile.aspx".FormatWith(TestConfig.TestForumUrl, TestConfig.ForumUrlRewritingPrefix));

            Assert.IsTrue(
                this.Driver.PageSource.Contains(NewEmailAddress),
                "Email Address Changing failed, or email verification is turned on");

            this.Driver.Navigate()
                .GoToUrl(
                    "{0}{1}cp_editprofile.aspx".FormatWith(TestConfig.TestForumUrl, TestConfig.ForumUrlRewritingPrefix));

            // Switch Email Address back
            emailInput = this.Driver.FindElement(By.XPath("//input[contains(@id,'_ProfileEditor_Email')]"));
            emailInput.Clear();
            emailInput.SendKeys(oldEmailAddress);

            // Save the Profile Changes
            this.Driver.FindElement(By.XPath("//input[contains(@id,'_ProfileEditor_UpdateProfile')]")).Click();

            this.Driver.Navigate()
                .GoToUrl(
                    "{0}{1}cp_profile.aspx".FormatWith(TestConfig.TestForumUrl, TestConfig.ForumUrlRewritingPrefix));

            Assert.IsTrue(this.Driver.PageSource.Contains(oldEmailAddress), "Email Address Changing back failed");
        }

        /// <summary>
        /// Set the user birthday via jQuery DatePicker Plugin test.
        /// </summary>
        [Test]
        public void Set_User_Birthday_Test()
        {
            this.Driver.Navigate()
                .GoToUrl(
                    "{0}{1}cp_editprofile.aspx".FormatWith(TestConfig.TestForumUrl, TestConfig.ForumUrlRewritingPrefix));

            Assert.IsTrue(
                this.Driver.PageSource.Contains("Edit Profile"),
                "Edit Profile is not available for that User");

            Assert.IsTrue(this.Driver.PageSource.Contains("Birthday"), "Birthday Selector is disabled for Users");

            // Switch Theme to "Black Grey"
            var birthdayInput = this.Driver.FindElement(By.XPath("//input[contains(@id,'_ProfileEditor_Birthday')]"));

            birthdayInput.Click();

            // Select Month
            this.Driver.SelectDropDownByValue(By.ClassName("ui-datepicker-month"), "9");

            // Select Year
            this.Driver.SelectDropDownByValue(By.ClassName("ui-datepicker-year"), "1982");

            // Select Day
            this.Driver.FindElement(By.LinkText("12")).Click();

            // Save the Profile Changes
            this.Driver.FindElement(By.XPath("//input[contains(@id,'_ProfileEditor_UpdateProfile')]")).ClickAndWait();

            this.Driver.Navigate()
                .GoToUrl(
                    "{0}{1}cp_editprofile.aspx".FormatWith(TestConfig.TestForumUrl, TestConfig.ForumUrlRewritingPrefix));

            birthdayInput = this.Driver.FindElement(By.XPath("//input[contains(@id,'_ProfileEditor_Birthday')]"));

            Assert.IsTrue(birthdayInput.GetAttribute("value").Equals("10/12/1982", StringComparison.InvariantCulture));

            // Change Back
            birthdayInput.Clear();
            birthdayInput.SendKeys("1/1/2001");

            this.Driver.FindElement(By.XPath("//input[contains(@id,'_ProfileEditor_UpdateProfile')]")).Click();
        }

        /// <summary>
        /// Set the user country and region test.
        /// </summary>
        [Test]
        public void Set_User_Country_And_Region_Test()
        {
            this.Driver.Navigate()
                .GoToUrl(
                    "{0}{1}cp_editprofile.aspx".FormatWith(TestConfig.TestForumUrl, TestConfig.ForumUrlRewritingPrefix));

            Assert.IsTrue(
                this.Driver.PageSource.Contains("Edit Profile"),
                "Edit Profile is not available for that User");

            Assert.IsTrue(this.Driver.PageSource.Contains("Country"), "Changing the Country is disabled for Users");

            // Switch Country to "Germany"
            this.Driver.FindElement(By.CssSelector("span.ui-selectmenu-text")).Click();
            this.Driver.FindElement(By.Id("ui-id-83")).Click();

            // Switch Region to "Berlin"
            this.Driver.SelectDropDownByValue(By.XPath("//select[contains(@id,'_ProfileEditor_Region')]"), "BER");

            // Save the Profile Changes
            this.Driver.FindElement(By.XPath("//input[contains(@id,'_ProfileEditor_UpdateProfile')]")).Click();
        }
    }
}