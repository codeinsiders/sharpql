// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SqlUdtInfo.cs" company="CODE Insiders LTD">
// 
// Copyright 2013-2015 CODE Insiders LTD
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//  
// http://www.apache.org/licenses/LICENSE-2.0
// 
//  Unless required by applicable law or agreed to in writing, software
//  distributed under the License is distributed on an "AS IS" BASIS,
//  WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//  See the License for the specific language governing permissions and
//  limitations under the License.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace CodeInsiders.SharpQL.Helpers.Internal
{
    using System;
    using System.Collections.Generic;

    using Microsoft.SqlServer.Server;

    internal class SqlUdtInfo
    {
        [ThreadStatic]
        private static Dictionary<Type, SqlUdtInfo> types2UdtInfo;

        internal readonly bool IsByteOrdered;
        internal readonly bool IsFixedLength;
        internal readonly int MaxByteSize;
        internal readonly string Name;
        internal readonly Format SerializationFormat;
        internal readonly string ValidationMethodName;

        private SqlUdtInfo(SqlUserDefinedTypeAttribute attr) {
            this.SerializationFormat = attr.Format;
            this.IsByteOrdered = attr.IsByteOrdered;
            this.IsFixedLength = attr.IsFixedLength;
            this.MaxByteSize = attr.MaxByteSize;
            this.Name = attr.Name;
            this.ValidationMethodName = attr.ValidationMethodName;
        }

        internal static SqlUdtInfo GetFromType(Type target) {
            SqlUdtInfo fromType = TryGetFromType(target);
            if (fromType == null) {
                throw new InvalidOperationException("SqlUdtReason_NoUdtAttribute");
            }
            return fromType;
        }

        internal static SqlUdtInfo TryGetFromType(Type target) {
            if (types2UdtInfo == null) {
                types2UdtInfo = new Dictionary<Type, SqlUdtInfo>();
            }

            SqlUdtInfo sqlUdtInfo;

            if (!types2UdtInfo.TryGetValue(target, out sqlUdtInfo)) {
                object[] customAttributes = target.GetCustomAttributes(typeof(SqlUserDefinedTypeAttribute), false);

                if (customAttributes.Length == 1) {
                    sqlUdtInfo = new SqlUdtInfo((SqlUserDefinedTypeAttribute)customAttributes[0]);
                }

                types2UdtInfo.Add(target, sqlUdtInfo);
            }

            return sqlUdtInfo;
        }
    }
}