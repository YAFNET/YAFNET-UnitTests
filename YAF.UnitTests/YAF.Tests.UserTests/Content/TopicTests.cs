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

namespace YAF.Tests.UserTests.Content
{
    using System;

    using NUnit.Framework;

    using OpenQA.Selenium;
    using OpenQA.Selenium.Chrome;

    using YAF.Tests.Utils;
    using YAF.Tests.Utils.Extensions;
    using YAF.Types.Extensions;

    /// <summary>
    /// The topic tests.
    /// </summary>
    [TestFixture]
    public class TopicTests : TestBase
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
        /// Create the new topic in forum 1 test.
        /// </summary>
        [Test]
        public void Create_New_Topic_Test()
        {
            // Go to Post New Topic
            this.Driver.Navigate()
                .GoToUrl(
                    "{0}{2}postmessage.aspx?f={1}".FormatWith(
                        TestConfig.TestForumUrl,
                        TestConfig.TestForumID,
                        TestConfig.ForumUrlRewritingPrefix));

            Assert.IsTrue(this.Driver.PageSource.Contains("Post New Topic"), "Post New Topic not possible");

            // Create New Topic
            this.Driver.FindElement(By.XPath("//input[contains(@id,'_TopicSubjectTextBox')]"))
                .SendKeys("Auto Created Test Topic - {0}".FormatWith(DateTime.UtcNow));

            if (this.Driver.PageSource.Contains("Description"))
            {
                this.Driver.FindElement(By.XPath("//input[contains(@id,'_TopicDescriptionTextBox')]"))
                    .SendKeys("Test Description");
            }

            if (this.Driver.PageSource.Contains("Status"))
            {
                this.Driver.SelectDropDownByValue(By.XPath("//select[contains(@id,'_TopicStatus')]"), "INFORMATIC");
            }

            this.Driver.FindElement(By.XPath("//textarea[contains(@id,'_YafTextEditor')]"))
                .SendKeys("This is a Test Message Created by an automated Unit Test");

            // Post New Topic
            this.Driver.FindElement(By.XPath("//a[contains(@id,'_PostReply')]")).Click();

            Assert.IsTrue(this.Driver.PageSource.Contains("Next Topic"), "Topic Creating failed");
        }
    }
}