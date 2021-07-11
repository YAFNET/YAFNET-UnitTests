﻿
/* Yet Another Forum.NET
 * Copyright (C) 2003-2005 Bjørnar Henden
 * Copyright (C) 2006-2013 Jaben Cargman
 * Copyright (C) 2014-2021 Ingo Herbote
 * https://www.yetanotherforum.net/
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

    using YAF.Core.Utilities;

    /// <summary>
    /// The role membership tests.
    /// </summary>
    [TestFixture]
    [Category("Basic Util Tests")]
    public class UtilTests
    {
        /// <summary>
        /// Simple URL Parameter Parser Test
        /// </summary>
        [Test]
        [Description("Simple URL Parameter Parser Test")]
        public void Simple_URL_Parameter_Parser_Test()
        {
            var parser = new SimpleURLParameterParser("g=forum&t=1&url=&pg=43#cool");

            Assert.AreEqual(true, parser.HasAnchor, "Url should have an Anchor");
            Assert.AreEqual("cool", parser.Anchor, "The Url should be called : cool");
            Assert.AreEqual("forum", parser.Parameters["g"], "The Parameter g should equals forum");
            Assert.AreEqual(
                "url=&pg=43",
                parser.CreateQueryString(new[] { "g", "t" }),
                "The Parameter g should equals forum");
            Assert.AreEqual("43", parser.Parameters[3], "The Parameter Index 3 should be 43");
        }
    }
}