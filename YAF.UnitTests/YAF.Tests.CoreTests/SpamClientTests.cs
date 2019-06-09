/* Yet Another Forum.NET
 * Copyright (C) 2003-2005 Bjørnar Henden
 * Copyright (C) 2006-2013 Jaben Cargman
 * Copyright (C) 2014-2019 Ingo Herbote
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

namespace YAF.Tests.CoreTests
{
    using System;

    using NUnit.Framework;

    using YAF.Core.Services.CheckForSpam;

    /// <summary>
    /// The spam client tester.
    /// </summary>
    [TestFixture]
    public class SpamClientTests
    {
        /// <summary>
        /// Gets or sets TestContext.
        /// </summary>
        public TestContext TestContext { get; set; }

        /// <summary>
        /// A test to check the Akismet API Key
        /// </summary>
        [Test]
        [Description("A test to check the Akismet API Key")]
        public void Akismet_Spam_Client_Verify_Key_Test()
        {
            var service = new AkismetSpamClient("XXXX", new Uri("http://www.google.com"));

            Assert.AreEqual(false, service.VerifyApiKey(), "The Verify of the API Key should be false");
        }

        /// <summary>
        /// A Test to Check for Bot via StopForumSpam.com API
        /// </summary>
        [Test]
        [Description("A Test to Check for Bot via StopForumSpam.com API")]
        public void Check_For_Bot_Test_via_StopForumSpam()
        {
            string responseText;
            Assert.IsTrue(
                new StopForumSpam().IsBot("84.16.230.111", "uuruznfdxw@gmail.com", "someone", out responseText),
                "This should be a Bot" + responseText);
        }

        /// <summary>
        /// A Test to Check for Bot via BotScout.com API
        /// </summary>
        [Test]
        [Description("A Test to Check for Bot via BotScout.com API")]
        public void Check_For_Bot_Test_via_BotScout()
        {
            string responseText;

            Assert.IsTrue(
                new BotScout().IsBot("84.16.230.111", "krasnhello@mail.ru", "someone", out responseText),
                "This should be a Bot" + responseText);
        }

        /// <summary>
        /// A Test to Check for Bot via BotScout.com or StopForumSpam.com API
        /// </summary>
        [Test]
        [Description("A Test to Check for Bot via BotScout.com API or StopForumSpam.com API")]
        public void Check_For_Bot_Test()
        {
            string responseText, responseText2;
            var botScoutCheck = new BotScout().IsBot("84.16.230.111", "krasnhello@mail.ru", "someone", out responseText);
            var stopForumSpamCheck = new StopForumSpam().IsBot(
                "84.16.230.111",
                "krasnhello@mail.ru",
                "someone",
                out responseText2);

            Assert.IsTrue(botScoutCheck | stopForumSpamCheck, "This should be a Bot");
        }

        /// <summary>
        /// The report_ user_ as_ bot_ test.
        /// </summary>
        [Test]
        public void Report_User_As_Bot_Test()
        {
            string responseText, responseText2;
            var parameters =
                $"username={"someone"}&ip_addr={"84.16.230.111"}&email={"krasnhello@mail.ru"}&api_key={"XXXXXXXXXXX"}";

            var result = new HttpClient().PostRequest(
                new Uri("http://www.stopforumspam.com/add.php"),
                null,
                5000,
                parameters);

            Assert.IsTrue(result.Equals("success"), result);
        }
    }
}