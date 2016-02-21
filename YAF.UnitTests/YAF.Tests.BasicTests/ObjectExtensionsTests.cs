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

namespace YAF.Tests.BasicTests
{
    using System;

    using NUnit.Framework;

    using YAF.Types.Extensions;

    /// <summary>
    /// Object Extensions Tests
    /// </summary>
    [TestFixture]
    [Category("Basic Object Extensions Tests")]
    public class ObjectExtensionsTests
    {
        /// <summary>
        /// Convert Object To Type Test
        /// </summary>
        [Test]
        [Description("Convert Object To Type Test")]
        public void ConvertObjectToType_Test()
        {
            var guid = ObjectExtensions.ConvertObjectToType("3021ABAA-991E-4AC3-9169-5FEAED33AA0A", "System.Guid");

            Assert.IsTrue(guid is Guid, "object should be an System.Guid");

            var int32 = ObjectExtensions.ConvertObjectToType("12345", "System.Int32");

            Assert.IsTrue(int32 is int, "object should be an System.Int32");

            var boolean = ObjectExtensions.ConvertObjectToType("True", "System.Boolean");

            Assert.IsTrue(boolean is bool, "object should be an System.Boolean");
        }
    }
}