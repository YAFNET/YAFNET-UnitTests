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
    using NUnit.Framework;

    using YAF.Core;
    using YAF.Types.Flags;
    using YAF.Types.Interfaces;

    /// <summary>
    /// BB Code Formatting Tests
    /// </summary>
    [TestFixture]
    [Category("Formatting")]
    public class BBCodeTests
    {
        #region Properties

        /// <summary>
        /// Gets or sets TestContext.
        /// </summary>
        public TestContext TestContext { get; set; }

        #endregion

        /// <summary>
        /// Formats the BB code strong to HTML test.
        /// </summary>
        [Test]
        [Description("Formats the BB code strong to HTML.")]
        public void Format_BBCode_Strong_To_HTML_Test()
        {
            var testMessage = YafContext.Current.Get<IFormatMessage>()
                .FormatMessage("[B]test[/B]", new MessageFlags(MessageFlags.Flags.IsBBCode));

            Assert.AreEqual("<strong>test</strong>", testMessage, testMessage);
        }
    }
}