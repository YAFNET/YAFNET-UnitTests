﻿/* Yet Another Forum.NET
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
    using NUnit.Framework;

    using OpenQA.Selenium;
    using OpenQA.Selenium.Chrome;

    using YAF.Tests.Utils;
    using YAF.Tests.Utils.Extensions;
    using YAF.Types.Extensions;

    /// <summary>
    /// The Signature tests.
    /// </summary>
    [TestFixture]
    public class SignatureTests : TestBase
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
        /// Change the user signature test.
        /// </summary>
        [Test]
        public void Change_User_SignatureTest()
        {
            this.Driver.Navigate()
                .GoToUrl(
                    "{0}{1}cp_signature.aspx".FormatWith(TestConfig.TestForumUrl, TestConfig.ForumUrlRewritingPrefix));

            Assert.IsTrue(
                this.Driver.PageSource.Contains("Edit Signature"),
                "Edit Signature is not available for that User");

            // Enter Signature
            this.Driver.FindElement(By.XPath("//textarea[contains(@id,'_YafTextEditor')]"))
                .SendKeys("This is a Test Signature created by an Unit Test");

            this.Driver.FindElement(By.XPath("//input[contains(@id,'_SignatureEditor_preview')]")).ClickAndWait();

            var previewCell =
                this.Driver.FindElement(By.XPath("//td[contains(@id,'_SignatureEditor_PreviewLine')]"))
                    .GetAttribute("textContent")
                    .Replace("\r\n      ", string.Empty);

            Assert.AreEqual(
                "This is a Test Signature created by an Unit Test",
                previewCell,
                "Signature changing failed");
        }
    }
}