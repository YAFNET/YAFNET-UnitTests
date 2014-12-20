/* Yet Another Forum.NET
 * Copyright (C) 2003-2005 Bjørnar Henden
 * Copyright (C) 2006-2013 Jaben Cargman
 * Copyright (C) 2014 Ingo Herbote
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

    using YAF.Core;

    /// <summary>
    /// The role membership tests.
    /// </summary>
    [TestFixture]
    [Category("Basic Role MemberShip Tests")]
    public class RoleMembershipTests
    {
        /// <summary>
        /// Gets or sets TestContext.
        /// </summary>
        public TestContext TestContext { get; set; }

        /// <summary>
        /// Role In Role Array Test
        /// </summary>
        [Test]
        [Description("Role In Role Array Test")]
        public void Role_In_Role_Array_Test()
        {
            string[] roleArray = { "role1", "role2" };

            Assert.AreEqual(
                true,
                RoleMembershipHelper.RoleInRoleArray("role2", roleArray),
                "'role2' should be in Role Array");
            Assert.AreEqual(
                false,
                RoleMembershipHelper.RoleInRoleArray("norole", roleArray),
                "'norole' should not in the Role Array");
        }
    }
}