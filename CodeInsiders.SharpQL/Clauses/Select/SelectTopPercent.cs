// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SelectTopPercent.cs" company="CODE Insiders LTD">
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

    public class SelectTopPercent<T> : SelectClause<T>
        where T : IStatement
    {
        public IEnumerable<Expression> ColumnList { get; private set; }
        public Expression Percent { get; private set; }

        public SelectTopPercent(T statement, Expression percent, IEnumerable<Expression> columnList)
            : base(statement) {
            if (percent == null) {
                throw new ArgumentNullException("percent");
            }
            if (columnList == null) {
                throw new ArgumentNullException("columnList");
            }
            this.Percent = percent;
            this.ColumnList = columnList;
        }

        public override void Build(SqlFragment parent, TSqlVisitor visitor) {
            visitor.SelectTopPercent<T>(parent, this);
        }
    }
}