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
    using System.Collections.Specialized;
    using System.Threading;

    using Microsoft.SqlServer.Management.Smo;

    /// <summary>
    /// Database Manager
    /// </summary>
    public class DBManager
    {
        /// <summary>
        /// Attaches the database.
        /// </summary>
        /// <param name="databaseName">Name of the database.</param>
        /// <param name="databaseFile">The database file.</param>
        public static void AttachDatabase(string databaseName, string databaseFile)
        {
            var server = new Server(TestConfig.DatabaseServer);

            // Drop the database
            DropDatabase(server, databaseName);

            server.AttachDatabase(databaseName, new StringCollection { databaseFile });

            while (server.Databases[databaseName].State != SqlSmoState.Existing)
            {
                Thread.Sleep(100);
            }
        }

        /// <summary>
        /// Drops the database.
        /// </summary>
        /// <param name="databaseName">Name of the database.</param>
        public static void DropDatabase(string databaseName)
        {
            var server = new Server(TestConfig.DatabaseServer);

            DropDatabase(server, databaseName);
        }

        /// <summary>
        /// Drops the database.
        /// </summary>
        /// <param name="server">The <paramref name="server"/>.</param>
        /// <param name="databaseName">Name of the database.</param>
        public static void DropDatabase(Server server, string databaseName)
        {
            var database = server.Databases[databaseName];

            if (database == null)
            {
                return;
            }

            server.KillDatabase(databaseName);

            while (server.Databases[databaseName] != null)
            {
                Thread.Sleep(100);
            }
        }
    }
}