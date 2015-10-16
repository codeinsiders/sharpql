// --------------------------------------------------------------------------------------------------------------------
// <copyright file="OrderByDesc.cs" company="CODE Insiders LTD">
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

    public class OrderByDesc : OrderBy
    {
        public OrderByDesc(OrderByStatement statement, IEnumerable<Expression> expressions)
            : base(statement, TSqlKeyword.DESC, expressions) {}

        public OrderByAsc OrderByAsc(Expression expression, params Expression[] exprs) {
            if (expression == null) {
                throw new ArgumentNullException("expression");
            }
            return this.OrderByAsc(Collection<Expression>.From(expression, exprs));
        }

        public OrderByAsc OrderByAsc(IEnumerable<Expression> exprs) {
            if (exprs == null) {
                throw new ArgumentNullException("exprs");
            }
            return this.NextClause(new OrderByAsc(this.Statement, exprs));
        }
    }
}