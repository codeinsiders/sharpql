// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ColumnList.cs" company="CODE Insiders LTD">
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
namespace CodeInsiders.SharpQL
{
    using System;
    using System.Collections.Generic;

    using CodeInsiders.SharpQL.Helpers.Internal;

    public class ColumnList
    {
        public static List<Expression> For<T>(Table table1, params Table[] tables) {
            if (table1 == null) {
                throw new InvalidOperationException("At least one table must be specified");
            }
            var list = new List<Expression>();
            foreach (var table in Collection<Table>.From(table1, tables))
            {
                if (table == null) {
                    continue;
                }
                var matchingColumns = table.GetMatchingColumnsFor<T>();
                list.AddRange(matchingColumns);
            }
            return list;
        }
    }
}