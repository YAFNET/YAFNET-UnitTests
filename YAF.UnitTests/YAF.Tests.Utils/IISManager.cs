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
    using Microsoft.Web.Administration;

    using YAF.Types.Extensions;

    /// <summary>
    /// IISManager Class to modify IIS Apps
    /// </summary>
    public class IISManager
    {
        /// <summary>
        /// Creates the IIS application.
        /// </summary>
        /// <param name="applicationName">Name of the application.</param>
        /// <param name="physicalPath">The physical path.</param>
        public static void CreateIISApplication(string applicationName, string physicalPath)
        {
            using (ServerManager serverManager = new ServerManager())
            {
                var defaultSite = serverManager.Sites[TestConfig.DefaultWebsiteName];
                Application newApplication = defaultSite.Applications["/{0}".FormatWith(applicationName)];

                // Remove if exists?!
                if (newApplication != null)
                {
                    defaultSite.Applications.Remove(newApplication);
                    serverManager.CommitChanges();
                }

                defaultSite = serverManager.Sites[TestConfig.DefaultWebsiteName];
                newApplication = defaultSite.Applications.Add("/{0}".FormatWith(applicationName), physicalPath);

                newApplication.ApplicationPoolName = TestConfig.TestApplicationPool;

                serverManager.CommitChanges();
                serverManager.Dispose();
            }
        }

        /// <summary>
        /// Recycles the application pool.
        /// </summary>
        /// <param name="applicationName">Name of the application.</param>
        public static void RecycleApplicationPool(string applicationName)
        {
            ServerManager iisManager = new ServerManager();
            Site defaultSite = iisManager.Sites["Default Web Site"];
            var application = defaultSite.Applications["/{0}".FormatWith(applicationName)];

            if (application == null)
            {
                return;
            }

            string appPool = application.ApplicationPoolName;
            ApplicationPool pool = iisManager.ApplicationPools[appPool];
            pool.Recycle();
        }

        /// <summary>
        /// Deletes the IIS application.
        /// </summary>
        /// <param name="applicationName">Name of the application.</param>
        public static void DeleteIISApplication(string applicationName)
        {
            using (ServerManager serverManager = new ServerManager())
            {
                var defaultSite = serverManager.Sites[TestConfig.DefaultWebsiteName];
                Application newApplication = defaultSite.Applications["/{0}".FormatWith(applicationName)];

                // Remove
                if (newApplication != null)
                {
                    defaultSite.Applications.Remove(newApplication);
                    serverManager.CommitChanges();
                }

                serverManager.Dispose();
            }
        }
    }
}