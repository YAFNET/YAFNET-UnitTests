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

namespace YAF.Tests.UserTests.UserSettings
{
    using System.IO;

    using NUnit.Framework;

    using OpenQA.Selenium;
    using OpenQA.Selenium.Chrome;

    using YAF.Tests.Utils;
    using YAF.Types.Extensions;

    /// <summary>
    /// The user Avatar tests.
    /// </summary>
    [TestFixture]
    public class AvatarTests : TestBase
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
        /// Logout Test User
        /// </summary>
        [TestFixtureTearDown]
        public void TearDownTest()
        {
            this.LogoutUser();
        }

        /// <summary>
        /// Select the avatar from collection test.
        /// </summary>
        [Test]
        public void Select_Avatar_From_Collection_Test()
        {
            // Go to Modify Avatar Page
            this.Driver.Navigate()
                .GoToUrl(
                    "{0}{1}cp_editavatar.aspx".FormatWith(TestConfig.TestForumUrl, TestConfig.ForumUrlRewritingPrefix));

            Assert.IsTrue(
                this.Driver.PageSource.Contains("Modify Avatar"),
                "Modify Avatar is not available for that User");

            // Select an Avatar from the Avatar Collection
            Assert.IsTrue(
                this.Driver.PageSource.Contains("Select your Avatar from our Collection"),
                "Avatar Collection not available");

            this.Driver.FindElement(By.XPath("//a[contains(@id,'_ProfileEditor_OurAvatar')]")).Click();

            // Select Common Category if exists
            this.Driver.FindElement(By.XPath("//img[contains(@alt,'Common')]")).Click();

            // Select SampleAvatar.gif
            this.Driver.FindElement(By.XPath("//img[contains(@alt,'SampleAvatar.gif')]")).Click();

            Assert.IsTrue(this.Driver.PageSource.Contains("Modify Avatar"), "Modify Avatar Failed");
        }

        /// <summary>
        /// Select the avatar from remote server test.
        /// </summary>
        [Test]
        public void Select_Avatar_From_Remote_Server_Test()
        {
            // Go to Modify Avatar Page
            this.Driver.Navigate()
                .GoToUrl(
                    "{0}{1}cp_editavatar.aspx".FormatWith(TestConfig.TestForumUrl, TestConfig.ForumUrlRewritingPrefix));

            Assert.IsTrue(
                this.Driver.PageSource.Contains("Modify Avatar"),
                "Modify Avatar is not available for that User");

            Assert.IsTrue(
                this.Driver.PageSource.Contains("Enter URL of Avatar on Remote Server to Use"),
                "Remote Avatar Url disabled");

            // Enter Test Avatar
            this.Driver.FindElement(By.XPath("//input[contains(@id,'_ProfileEditor_Avatar')]"))
                .SendKeys("http://www.gravatar.com/avatar/00000000000000000000000000000000");

            this.Driver.FindElement(By.XPath("//input[contains(@id,'_ProfileEditor_UpdateRemote')]")).Click();

            Assert.IsTrue(this.Driver.PageSource.Contains("Modify Avatar"), "Modify Avatar Failed");
        }

        /// <summary>
        /// Upload the avatar from computer test.
        /// </summary>
        [Test]
        public void Upload_Avatar_From_Computer_Test()
        {
            // Go to Modify Avatar Page
            this.Driver.Navigate()
                .GoToUrl(
                    "{0}{1}cp_editavatar.aspx".FormatWith(TestConfig.TestForumUrl, TestConfig.ForumUrlRewritingPrefix));

            Assert.IsTrue(
                this.Driver.PageSource.Contains("Modify Avatar"),
                "Modify Avatar is not available for that User");

            Assert.IsTrue(
                this.Driver.PageSource.Contains("Upload Avatar from Your Computer"),
                "Upload Avatars disabled");

            string filePath = Path.GetFullPath(@"..\..\testfiles\avatar.png");

            // Enter Test Avatar
            var fileUpload = this.Driver.FindElement(By.XPath("//input[contains(@id,'_ProfileEditor_File')]"));
            fileUpload.Click();
            fileUpload.SendKeys(filePath);

            this.Driver.FindElement(By.XPath("//input[contains(@id,'_ProfileEditor_UpdateUpload')]")).Click();

            Assert.IsTrue(this.Driver.PageSource.Contains("Modify Avatar"), "Modify Avatar Failed");
        }
    }
}
