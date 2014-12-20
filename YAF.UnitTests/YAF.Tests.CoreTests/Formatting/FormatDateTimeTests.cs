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

namespace YAF.Tests.CoreTests.Formatting
{
    using System;

    using Autofac;

    using NUnit.Framework;

    using YAF.Core;
    using YAF.Types.Attributes;
    using YAF.Types.Interfaces;

    /// <summary>
    ///  The Format Date Time tests.
    /// </summary>
    [TestFixture]
    [Category("Formatting")]
    public class FormatDateTimeTests : IHaveServiceLocator
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="FormatDateTimeTests"/> class.
        /// </summary>
        public FormatDateTimeTests()
        {
            GlobalContainer.Container.Resolve<IInjectServices>().Inject(this);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets ServiceLocator.
        /// </summary>
        [Inject]
        public IServiceLocator ServiceLocator { get; set; }

        /// <summary>
        /// Gets or sets TestContext.
        /// </summary>
        public TestContext TestContext { get; set; }

        #endregion

        /// <summary>
        /// Format date to long test.
        /// </summary>
        [Test]
        [Description("Format date to long test.")]
        public void Format_Date_To_Long_Test()
        {
            var currentDateTime = new DateTime(2012, 08, 19, 20, 20, 20);

            var dateTimeString = this.Get<IDateTime>().FormatDateLong(currentDateTime);

            Assert.AreEqual("Sonntag, 19. August 2012(UTC)", dateTimeString, dateTimeString);
        }
    }
}