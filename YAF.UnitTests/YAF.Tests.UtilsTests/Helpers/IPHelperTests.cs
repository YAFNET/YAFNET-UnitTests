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

namespace YAF.Tests.UtilsTests.Helpers
{
    using NUnit.Framework;

    using YAF.Utils.Helpers;

    /// <summary>
    /// YAF.Utils IPHelper Tests
    /// </summary>
    [TestFixture]
    public class IPHelperTests
    {
        /// <summary>
        /// Converts a string to a IP Address test
        /// </summary>
        [Test]
        [Description("Converts a string to a IP Address test")]
        public void StringToIP_Test()
        {
            const string testMessage =
                "2001:0db8:85a3:0000:0000:8a2e:0370:7334";

            ulong  ipAddress = 1340406161;

            Assert.AreEqual(ipAddress, IPHelper.StringToIP(testMessage.Split('.')));
        }
    }
}
