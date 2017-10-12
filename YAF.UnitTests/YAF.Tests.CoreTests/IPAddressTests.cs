/* Yet Another Forum.NET
 * Copyright (C) 2003-2005 Bj�rnar Henden
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

namespace YAF.Tests.CoreTests
{
    using System;

    using NUnit.Framework;

    using YAF.Core.Services.CheckForSpam;
    using YAF.Types.Extensions;
    using YAF.Utils.Helpers;

    /// <summary>
    /// Different IP Address Tests.
    /// </summary>
    [TestFixture]
    public class IPAddressTests
    {
        /// <summary>
        /// Gets or sets TestContext.
        /// </summary>
        public TestContext TestContext { get; set; }

        /// <summary>
        /// A test to check the Akismet API Key
        /// </summary>
        [Test]
        [Description("A test to convert a String to an IP Address")]
        public void StringToIp_Test()
        {
            // IP v4 Address
            //var ip = "79.228.254.119".Split('.');

            // IP v6 Address
            var ip = "2001:0db8:0a0b:12f0:0000:0000:0000:0001".Split(':');

            Assert.AreEqual(/*"1340407415"*/"15060037153926938625",
                IPHelper.StringToIP(ip).ToString(),
                IPHelper.StringToIP(ip).ToString());
        }
    }
}