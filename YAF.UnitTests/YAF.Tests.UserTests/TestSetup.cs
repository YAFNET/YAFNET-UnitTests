﻿/* Yet Another Forum.NET
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

namespace YAF.Tests.UserTests
{
    using NUnit.Framework;

    using YAF.Tests.Utils.SetUp;

    /// <summary>
    /// Testing a YAF Installation
    /// </summary>
    [SetUpFixture]
    public class TestSetup
    {
        /// <summary>
        /// Gets the install base.
        /// </summary>
        /// <value>
        /// The install base.
        /// </value>
        public static InstallBase _testBase { get; private set; }

        /// <summary>
        /// Download YAF, Create Application and setup the Database
        /// </summary>
        [OneTimeSetUp]
        public void SetUp()
        {
            _testBase = new InstallBase();

            _testBase.SetUp();
        }

        /// <summary>
        /// The Tear Down
        /// </summary>
        [OneTimeTearDown]
        public void TearDown()
        {
            _testBase.TearDown();
        }
    }
}