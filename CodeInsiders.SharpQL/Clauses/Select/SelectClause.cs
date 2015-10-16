// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SelectClause.cs" company="CODE Insiders LTD">
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

    using CodeInsiders.SharpQL.Helpers.Internal;

    public abstract class SelectClause<T> : Clause<T>
        where T : IStatement
    {
        protected SelectClause(T statement)
            : base(statement) {}

        public FromClause<T> From(Table table) {
            if (table == null) {
                throw new ArgumentNullException("table");
            }
            return this.NextClause(new FromClause<T>(this.Statement, table));
        }

        public FromClause<T> From(SelectStatement selectStatement, string alias) {
            if (selectStatement == null) {
                throw new ArgumentNullException("selectStatement");
            }
            if (StringHelper.IsNullOrWhiteSpace(alias)) {
                throw new ArgumentNullException("alias");
            }
            return this.NextClause(new FromClause<T>(this.Statement, selectStatement, alias));
        }

        public GroupByClause<T> GroupBy(Expression expression, params Expression[] expressions) {
            if (expression == null) {
                throw new ArgumentNullException("expression");
            }
            return this.NextClause(new GroupByClause<T>(this.Statement, Collection<Expression>.From(expression, expressions)));
        }

        public WhereClause<T> Where(Predicate searchCondition) {
            if (searchCondition == null) {
                throw new ArgumentNullException("searchCondition");
            }
            return this.NextClause(new WhereClause<T>(this.Statement, searchCondition));
        }
    }
}