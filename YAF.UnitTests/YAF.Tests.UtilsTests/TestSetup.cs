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

namespace YAF.Tests.UtilsTests
{
    using System.IO;
    using System.Web.Security;

    using HttpSimulator;

    using Moq;

    using NUnit.Framework;

    using YAF.Tests.Utils;

    /// <summary>
    /// Testing a YAF Installation
    /// </summary>
    [SetUpFixture]
    public class TestSetup
    {
        /// <summary>
        /// Sets up the mocked YAF Forum instance
        /// </summary>
        [SetUp]
        public void Preparations()
        {
            var _factory = new MockRepository(MockBehavior.Strict);
            var _membership = _factory.Create<MembershipProvider>();
            var _roleProvider = _factory.Create<RoleProvider>();

            Membership.ApplicationName = "YetAnotherForum";

            Membership.Providers.AddMembershipProvider("MyMembershipProvider", _membership.Object);

            Roles.Providers.AddRoleProvider("MyRoleProvider", _roleProvider.Object);

            var currentPath = Path.GetFullPath(@"..\..\testfiles\forum\");

            HttpSimulator simulator = new HttpSimulator("yaf/", currentPath);

            simulator.SimulateRequest();
        }

        /// <summary>
        /// Removes the fake providers.
        /// </summary>
        [TearDown]
        public void TearDown()
        {
            Membership.Providers.RemoveMembershipProvider("MyMembershipProvider");

            Roles.Providers.RemoveRoleProvider("MyRoleProvider");
        }
    }
}
