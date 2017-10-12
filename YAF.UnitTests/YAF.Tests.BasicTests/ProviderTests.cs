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

namespace YAF.Tests.BasicTests
{
    using NUnit.Framework;

    using YAF.Providers.Membership;

    /// <summary>
    /// The YAF membership provider tests.
    /// </summary>
    [TestFixture]
    [Category("Basic Provider Tests")]
    public class ProviderTests
    {
        /// <summary>
        /// Gets or sets TestContext.
        /// </summary>
        public TestContext TestContext { get; set; }

        /// <summary>
        /// The test 193 default hash with incorrect salt.
        /// </summary>
        [Test]
        public void Test193DefaultHashWithIncorrectSalt()
        {
            const string TestPasswrd = ";Stupid12";
            const string ExpectedResult = "uNBoPpKz+S46wPPVCeIFyHW0lVE=";
            const string Salt = "UwB5AHMAdABlAG0ALgBCAHkAdABlAFsAXQA=";

            string result = YafMembershipProvider.Hash(
                TestPasswrd,
                "SHA1",
                Salt,
                true,
                false,
                "none",
                string.Empty,
                false);

            Assert.AreEqual(ExpectedResult, result, "ExpectedResult does not match the result");
        }

        /// <summary>
        /// The test 193 default hash with salt.
        /// </summary>
        [Test]
        public void Test193DefaultHashWithSalt()
        {
            const string TestPasswrd = ";Stupid12";
            const string ExpectedResult = "CoUcFgjFpOK1mt+vYrDtQO6IZ9I=";
            const string Salt = "9LeeEZnXUE81gAzeWFXCvw==";

            string result = YafMembershipProvider.Hash(
                TestPasswrd,
                "SHA1",
                Salt,
                true,
                false,
                "none",
                string.Empty,
                false);

            Assert.AreEqual(ExpectedResult, result, "ExpectedResult does not match the result");
        }

        /// <summary>
        /// The test 19 x password hash.
        /// </summary>
        [Test]
        public void Test19xPasswordHash()
        {
            const string TestPasswrd = ";Stupid12";
            const string ExpectedResult = "32ADF4DF9AE5B5184EDB8D8BA9D65AD9";

            string result = YafMembershipProvider.Hash(
                TestPasswrd,
                "MD5",
                string.Empty,
                false,
                true,
                "UPPER",
                string.Empty,
                false);

            Assert.AreEqual(ExpectedResult, result, "ExpectedResult does not match the result");
        }

        /// <summary>
        /// The test SHA1 Microsoft provider hash.
        /// </summary>
        [Test]
        public void TestSHA1MicrosoftProviderHash()
        {
            const string TestPasswrd = ";Stupid12";
            const string ExpectedResult = "8dCiTK3x/mjTG6p4uO72CzNG1mk=";
            const string Salt = "8tX6TMKiZtmA/GwOgpf6uw==";

            string result = YafMembershipProvider.Hash(
                TestPasswrd,
                "SHA1",
                Salt,
                true,
                false,
                "none",
                string.Empty,
                true);

            Assert.AreEqual(ExpectedResult, result, "ExpectedResult does not match the result");
        }

        /// <summary>
        /// The test snitz password hash.
        /// </summary>
        [Test]
        public void TestSnitzPasswordHash()
        {
            const string TestPasswrd = ";Stupid12";
            const string ExpectedResult = "fa679d16ac15713a5a305f45dce4295b241f6dd38e14f92daa330d599ce77d40";

            string result = YafMembershipProvider.Hash(
                TestPasswrd,
                "SHA256",
                string.Empty,
                false,
                true,
                "lower",
                string.Empty,
                false);

            Assert.AreEqual(ExpectedResult, result, "ExpectedResult does not match the result");
        }
    }
}