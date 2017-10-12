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

namespace YAF.Tests.Utils
{
    using System;
    using System.Configuration;

    using YAF.Types.Extensions;

    /// <summary>
    /// Configuration Settings
    /// </summary>
    public class TestConfig
    {
        /// <summary>
        /// Gets the test forum ID.
        /// </summary>
        public static int TestForumID
        {
            get
            {
                return GetConfigValueAsInt("YAF.TestForumID", 1);
            }
        }

        /// <summary>
        /// Gets the test topic ID.
        /// </summary>
        public static int TestTopicID
        {
            get
            {
                return GetConfigValueAsInt("YAF.TestTopicID", 1);
            }
        }

        /// <summary>
        /// Gets a value indicating whether [use existing installation].
        /// </summary>
        /// <value>
        ///   <c>true</c> if use existing installation; otherwise, <c>false</c>.
        /// </value>
        public static bool UseExistingInstallation
        {
            get
            {
                return GetConfigValueAsBoolean("YAF.UseExistingInstallation", false);
            }
        }

        /// <summary>
        /// Gets the config password.
        /// </summary>
        public static string ConfigPassword
        {
            get
            {
                return GetConfigValueAsString("YAF.ConfigPassword") ?? "pass";
            }
        }

        /// <summary>
        /// Gets the test database.
        /// </summary>
        public static string TestDatabase
        {
            get
            {
                return GetConfigValueAsString("YAF.TestDatabase") ?? "YAFNETTEST";
            }
        }

        /// <summary>
        /// Gets the database server.
        /// </summary>
        public static string DatabaseServer
        {
            get
            {
                return GetConfigValueAsString("YAF.DatabaseServer") ?? "(local)";
            }
        }

        /// <summary>
        /// Gets the install test site URL.
        /// </summary>
        public static string InstallTestSiteURL
        {
            get
            {
                return "http://localhost/{0}/".FormatWith(TestApplicationName);
            }
        }

        /// <summary>
        /// Gets the test application pool.
        /// </summary>
        public static string TestApplicationPool
        {
            get
            {
                return GetConfigValueAsString("YAF.TestApplicationPool") ?? "ASP.NET v4.0 Classic";
            }
        }

        /// <summary>
        /// Gets the name of the test application.
        /// </summary>
        /// <value>
        /// The name of the test application.
        /// </value>
        public static string TestApplicationName
        {
            get
            {
                return GetConfigValueAsString("YAF.TestApplicationName") ?? "YAFNETTEST";
            }
        }

        /// <summary>
        /// Gets the package location.
        /// </summary>
        public static string PackageLocation
        {
            get
            {
                return GetConfigValueAsString("YAF.PackageLocation") ?? "Local";
            }
        }

        /// <summary>
        /// Gets the install physical path.
        /// </summary>
        public static string InstallPhysicalPath
        {
            get
            {
                return GetConfigValueAsString("YAF.InstallPhysicalPath") ?? @"C:\Tests\";
            }
        }

        /// <summary>
        /// Gets the Release download URL.
        /// </summary>
        public static string ReleaseDownloadUrl
        {
            get
            {
                return GetConfigValueAsString("YAF.ReleaseDownloadUrl") ??
                       "https://github.com/YAFNET/YAFNET/releases/download/v2.1.2/YAF-v2.1.2.0-Install.zip";
            }
        }

        /// <summary>
        /// Gets the local release package file.
        /// </summary>
        public static string LocalReleasePackageFile
        {
            get
            {
                return GetConfigValueAsString("YAF.LocalReleasePackageFile") ??
                       @"..\..\testfiles\YAF-v2.1.2.0-Install.zip";
            }
        }

        /// <summary>
        /// Gets the name of the admin user.
        /// </summary>
        public static string AdminUserName
        {
            get
            {
                return GetConfigValueAsString("YAF.AdminUserName") ?? "Admin";
            }
        }

        /// <summary>
        /// Gets the admin password.
        /// </summary>
        public static string AdminPassword
        {
            get
            {
                return GetConfigValueAsString("YAF.AdminPassword") ?? "AdminAdmin1234?!";
            }
        }

        /// <summary>
        /// Gets the name of the test user.
        /// </summary>
        /// <value>
        /// The name of the test user.
        /// </value>
        public static string TestUserName
        {
            get
            {
                return GetConfigValueAsString("YAF.TestUserName") ?? "TestUser";
            }
        }

        /// <summary>
        /// Gets the test user password.
        /// </summary>
        public static string TestUserPassword
        {
            get
            {
                return GetConfigValueAsString("YAF.TestUserPassword") ?? "TestUserTestUser1234?!";
            }
        }

        /// <summary>
        /// Gets the name of the test user 2.
        /// </summary>
        /// <value>
        /// The name of the test user 2.
        /// </value>
        public static string TestUserName2
        {
            get
            {
                return GetConfigValueAsString("YAF.TestUserName2") ?? "TestUser2";
            }
        }

        /// <summary>
        /// Gets the test user 2 password.
        /// </summary>
        public static string TestUser2Password
        {
            get
            {
                return GetConfigValueAsString("YAF.TestUser2Password") ?? "TestUser2TestUser21234?!";
            }
        }

        /// <summary>
        /// Gets Default Website Name.
        /// </summary>
        public static string DefaultWebsiteName
        {
            get
            {
                return GetConfigValueAsString("YAF.DefaultWebsiteName") ?? "Default Web Site";
            }
        }

        /// <summary>
        /// Gets Test Forum Url.
        /// </summary>
        public static string TestForumUrl
        {
            get
            {
                return !UseExistingInstallation
                           ? InstallTestSiteURL
                           : GetConfigValueAsString("YAF.TestForumUrl") ?? "http://localhost:63645/";
            }
        }

        /// <summary>
        /// Gets the forum URL rewriting prefix.
        /// </summary>
        /// <value>
        /// The forum URL rewriting prefix.
        /// </value>
        public static string ForumUrlRewritingPrefix
        {
            get
            {
                return GetConfigValueAsString("YAF.UrlRewritingPrefix") ?? "yaf_";
            }
        }

        /// <summary>
        /// Gets a value indicating whether [use test mail server].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [use test mail server]; otherwise, <c>false</c>.
        /// </value>
        public static bool UseTestMailServer
        {
            get
            {
                return GetConfigValueAsBoolean("YAF.UseTestMailServer", true);
            }
        }

        /// <summary>
        /// Gets the test mail host.
        /// </summary>
        public static string TestMailHost
        {
            get
            {
                return GetConfigValueAsString("YAF.TestMailHost") ?? "localhost";
            }
        }

        /// <summary>
        /// Gets the test mail password.
        /// </summary>
        public static string TestMailPassword
        {
            get
            {
                return GetConfigValueAsString("YAF.TestMailPassword") ?? "pass";
            }
        }

        /// <summary>
        /// Gets the name of the test mail user.
        /// </summary>
        /// <value>
        /// The name of the test mail user.
        /// </value>
        public static string TestMailUserName
        {
            get
            {
                return GetConfigValueAsString("YAF.TestMailUserName") ?? "forum@yafnettest.com";
            }
        }

        /// <summary>
        /// Gets the test mail port.
        /// </summary>
        public static string TestMailPort
        {
            get
            {
                return GetConfigValueAsString("YAF.TestMailPort") ?? "25";
            }
        }

        /// <summary>
        /// Gets the test forum mail.
        /// </summary>
        public static string TestForumMail
        {
            get
            {
                return GetConfigValueAsString("YAF.TestForumMail") ?? "forum@yafnettest.com";
            }
        }

        /// <summary>
        /// Gets the config value as Int32.
        /// </summary>
        /// <param name="configKey">The config key.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>
        /// Returns Boolean Value
        /// </returns>
        public static int GetConfigValueAsInt(string configKey, int defaultValue)
        {
            string value = GetConfigValueAsString(configKey);

            return !string.IsNullOrEmpty(value) ? value.ToLower().ToType<int>() : defaultValue;
        }

        /// <summary>
        /// Gets the config value as Boolean.
        /// </summary>
        /// <param name="configKey">The config key.</param>
        /// <param name="defaultValue">if set to <c>true</c> [default value].</param>
        /// <returns>Returns Boolean Value</returns>
        public static bool GetConfigValueAsBoolean(string configKey, bool defaultValue)
        {
            string value = GetConfigValueAsString(configKey);

            return !string.IsNullOrEmpty(value) ? Convert.ToBoolean(value.ToLower()) : defaultValue;
        }

        /// <summary>
        /// Gets the config value as string.
        /// </summary>
        /// <param name="configKey">The config key.</param>
        /// <returns>Returns String Value</returns>
        public static string GetConfigValueAsString(string configKey)
        {
            return ConfigurationManager.AppSettings[configKey];
        }
    }
}