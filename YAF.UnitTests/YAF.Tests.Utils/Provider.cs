/* Yet Another Forum.NET
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

namespace YAF.Tests.Utils
{
    using System.Collections;
    using System.Configuration.Provider;
    using System.Reflection;
    using System.Web.Security;

    /// <summary>
    /// Provider Helper
    /// </summary>
    public static class Provider
    {
        /// <summary>
        /// Adds the membership provider.
        /// </summary>
        /// <param name="providerName">Name of the provider.</param>
        /// <param name="provider">The provider.</param>
        public static void AddMembershipProvider(
            string providerName,
            MembershipProvider provider)
        {
            GetMembershipHashtable().Add(providerName, provider);
        }

        /// <summary>
        /// Adds the role provider.
        /// </summary>
        /// <param name="providerName">Name of the provider.</param>
        /// <param name="provider">The provider.</param>
        public static void AddRoleProvider(
            string providerName,
            RoleProvider provider)
        {
            GetRolesHashtable().Add(providerName, provider);
        }

        /// <summary>
        /// Removes the membership provider.
        /// </summary>
        /// <param name="providerName">Name of the provider.</param>
        public static void RemoveMembershipProvider(string providerName)
        {
            GetMembershipHashtable().Remove(providerName);
        }

        /// <summary>
        /// Removes the role provider.
        /// </summary>
        /// <param name="providerName">Name of the provider.</param>
        public static void RemoveRoleProvider(string providerName)
        {
            GetRolesHashtable().Remove(providerName);
        }

        /// <summary>
        /// Gets the membership hash table.
        /// </summary>
        /// <returns>Returns the Membership Hash Table</returns>
        private static Hashtable GetMembershipHashtable()
        {
            var hashtableField = typeof(ProviderCollection).GetField(
                "_Hashtable",
                BindingFlags.Instance | BindingFlags.NonPublic);
            return hashtableField.GetValue(Membership.Providers) as Hashtable;
        }

        /// <summary>
        /// Gets the roles hash table.
        /// </summary>
        /// <returns>Returns the Roles Hash Table</returns>
        private static Hashtable GetRolesHashtable()
        {
            var hashtableField = typeof(ProviderCollection).GetField(
                "_Hashtable",
                BindingFlags.Instance | BindingFlags.NonPublic);
            return hashtableField.GetValue(Roles.Providers) as Hashtable;
        }
    }
}