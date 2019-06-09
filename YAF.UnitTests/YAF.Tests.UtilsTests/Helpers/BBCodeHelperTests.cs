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

namespace YAF.Tests.UtilsTests.Helpers
{
    using NUnit.Framework;

    using YAF.Utils.Helpers;

    /// <summary>
    /// YAF.Utils BBCodeHelper Tests
    /// </summary>
    [TestFixture]
    public class BBCodeHelperTests
    {
        /// <summary>
        /// Strips all BBCodes from a string.
        /// </summary>
        [Test]
        [Description("Strips all BBCodes from a string.")]
        public void StripBBCode_Test()
        {
            const string TestMessage =
                "This is a test text containing [b]bold[/b] and [i]italic[i] text and other bbcodes [img]http://test.com/testimage.jpg[/img]";

            Assert.AreEqual(
                "This is a test text containing bold and italic text and other bbcodes http://test.com/testimage.jpg",
                BBCodeHelper.StripBBCode(TestMessage));
        }

        /// <summary>
        /// Strips all BBCode Quotes (including the Quote Text) from a string.
        /// </summary>
        [Test]
        [Description("Strips all BBCode Quotes (including the Quote Text) from a string.")]
        public void StripBBCodeQuotes_Test()
        {
            const string TestMessage = "This is a test text containing a[quote] Quoted Message[/quote].";

            Assert.AreEqual("This is a test text containing a.", BBCodeHelper.StripBBCodeQuotes(TestMessage));
        }

        /// <summary>
        /// Strips all BBCode Urls from a string.
        /// </summary>
        [Test]
        [Description("Strips all BBCode Urls from a string.")]
        public void StripBBCodeUrls_Test()
        {
            const string TestMessage =
                "This is a test text containing an URL: [url]http://test.com/[/url] and [url=http://test.com/]test url[/url].";

            Assert.AreEqual(
                "This is a test text containing an URL:  and .",
                BBCodeHelper.StripBBCodeUrls(TestMessage));
        }
    }
}
