/* Yet Another Forum.NET
 * Copyright (C) 2003-2005 Bjørnar Henden
 * Copyright (C) 2006-2013 Jaben Cargman
 * Copyright (C) 2014-2020 Ingo Herbote
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

namespace YAF.Tests.CoreTests
{
    using NUnit.Framework;
    using YAF.Utils.Helpers;

    /// <summary>
    /// Different IP Address Tests.
    /// </summary>
    [TestFixture]
    public class IPAddressTests
    {
        /// <summary>
        /// A test to convert a IP V4 String in to an IP Address
        /// </summary>
        [Test]
        [Description("A test to convert a String to an IP Address")]
        public void StringToIpV4_Test()
        {
            // IP v4 Address
            var ip = "79.228.254.119".Split('.');

            Assert.AreEqual("1340407415", IPHelper.StringToIP(ip).ToString(), IPHelper.StringToIP(ip).ToString());
        }

        /// <summary>
        /// A test to convert a IP V6 String in to an IP Address
        /// </summary>
        [Test]
        [Description("A test to convert a String to an IP Address")]
        public void StringToIpV6_Test()
        {
            // IP v6 Address
            var ip = "2001:0db8:0a0b:12f0:0000:0000:0000:0001".Split(':');

            Assert.AreEqual(
                "15060037153926938625",
                IPHelper.StringToIP(ip).ToString(),
                IPHelper.StringToIP(ip).ToString());
        }
    }
}