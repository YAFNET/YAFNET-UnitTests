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

namespace YAF.Tests.UserTests.Features
{
    using NUnit.Framework;

    using OpenQA.Selenium;
    using OpenQA.Selenium.Chrome;

    using YAF.Tests.Utils;
    using YAF.Tests.Utils.Extensions;
    using YAF.Types.Extensions;

    /// <summary>
    /// Mobile Theme Tests
    /// </summary>
    [TestFixture]
    public class MobileThemeTests : TestBase
    {
        /// <summary>
        /// Gets or sets TestContext.
        /// </summary>
        public TestContext TestContext { get; set; }

        /// <summary>
        /// Login User Setup
        /// </summary>
        [TestFixtureSetUp]
        public void SetUpTest()
        {
            this.Driver = !TestConfig.UseExistingInstallation ? TestSetup._testBase.ChromeDriver : new ChromeDriver();

            Assert.IsTrue(this.LoginUser(), "Login failed");
        }

        /// <summary>
        /// Logout Test User and Switch Back to Default Theme
        /// </summary>
        [TestFixtureTearDown]
        public void TearDownTest()
        {
            this.Driver.Navigate()
                .GoToUrl(
                    "{0}{1}cp_editprofile.aspx".FormatWith(TestConfig.TestForumUrl, TestConfig.ForumUrlRewritingPrefix));

            // Switch Theme Back to Clean Slate
            this.Driver.SelectDropDownByValue(
                By.XPath("//select[contains(@id,'_ProfileEditor_Theme')]"),
                "cleanSlate.xml");

            // Save the Profile Changes
            this.Driver.FindElement(By.XPath("//input[contains(@id,'_ProfileEditor_UpdateProfile')]")).Click();

            this.LogoutUser();
        }

        /// <summary>
        /// Check if all Mobile Pages work without throwing an Error Test.
        /// </summary>
        [Test]
        [Description("Check if all Mobile Pages work without throwing an Error Test.")]
        public void Check_Mobile_Pages_Test()
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
                "YafMobile.xml");

            // Save the Profile Changes
            this.Driver.FindElement(By.XPath("//input[contains(@id,'_ProfileEditor_UpdateProfile')]")).Click();

            this.Driver.Navigate().Refresh();

            Assert.IsNotNull(
                this.Driver.ElementExists(By.XPath("//link[contains(@href,'Themes/Yafmobile/theme.css')]")),
                "Changing Forum Theme failed");

            // Now Check if each mobile page is displayed correctly
            // Check the main Page
            this.Driver.Navigate().GoToUrl(TestConfig.TestForumUrl);

            Assert.IsTrue(
                !this.Driver.PageSource.Contains("Server Error") && !this.Driver.PageSource.Contains("Forum Error")
                && !this.Driver.PageSource.Contains("NullReferenceException"),
                "There is something wrong with the mobile forum page");

            // Check the forum Category Page
            this.Driver.Navigate()
                .GoToUrl("{0}{1}mytopics.aspx".FormatWith(TestConfig.TestForumUrl, TestConfig.ForumUrlRewritingPrefix));

            Assert.IsTrue(
                !this.Driver.PageSource.Contains("Server Error") && !this.Driver.PageSource.Contains("Forum Error")
                && !this.Driver.PageSource.Contains("NullReferenceException"),
                "There is something wrong with the Forums Category page");

            // Check the forum topic view Page
            this.Driver.Navigate()
                .GoToUrl(
                    "{0}{2}postst{1}.aspx".FormatWith(
                        TestConfig.TestForumUrl,
                        TestConfig.TestTopicID,
                        TestConfig.ForumUrlRewritingPrefix));

            Assert.IsTrue(
                !this.Driver.PageSource.Contains("Server Error") && !this.Driver.PageSource.Contains("Forum Error")
                && !this.Driver.PageSource.Contains("NullReferenceException"),
                "There is something wrong with the posts page");

            // Check My Topics Page
            this.Driver.Navigate()
                .GoToUrl(
                    "{0}{2}topics{1}.aspx".FormatWith(
                        TestConfig.TestForumUrl,
                        TestConfig.TestForumID,
                        TestConfig.ForumUrlRewritingPrefix));

            Assert.IsTrue(
                !this.Driver.PageSource.Contains("Server Error") && !this.Driver.PageSource.Contains("Forum Error")
                && !this.Driver.PageSource.Contains("NullReferenceException"),
                "There is something wrong with the mobile forum category page");

            // Check the Post Message Page
            this.Driver.Navigate()
                .GoToUrl(
                    "{0}{2}postmessage.aspx?f={1}".FormatWith(
                        TestConfig.TestForumUrl,
                        TestConfig.TestForumID,
                        TestConfig.ForumUrlRewritingPrefix));

            Assert.IsTrue(
                !this.Driver.PageSource.Contains("Server Error") && !this.Driver.PageSource.Contains("Forum Error")
                && !this.Driver.PageSource.Contains("NullReferenceException"),
                "There is something wrong with the mobile post message page");

            // Check the Send PM Page
            this.Driver.Navigate()
                .GoToUrl("{0}{1}pmessage.aspx".FormatWith(TestConfig.TestForumUrl, TestConfig.ForumUrlRewritingPrefix));

            Assert.IsTrue(
                !this.Driver.PageSource.Contains("Server Error") && !this.Driver.PageSource.Contains("Forum Error")
                && !this.Driver.PageSource.Contains("NullReferenceException"),
                "There is something wrong with the mobile send private message page");

            // Check the Profile Page
            this.Driver.Navigate()
                .GoToUrl("{0}{1}profile2.aspx".FormatWith(TestConfig.TestForumUrl, TestConfig.ForumUrlRewritingPrefix));

            Assert.IsTrue(
                !this.Driver.PageSource.Contains("Server Error") && !this.Driver.PageSource.Contains("Forum Error")
                && !this.Driver.PageSource.Contains("NullReferenceException"),
                "There is something wrong with the mobile profile page");
        }
    }
}