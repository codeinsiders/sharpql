// --------------------------------------------------------------------------------------------------------------------
// <copyright file="JoinClause.cs" company="CODE Insiders LTD">
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

    public abstract class JoinClause<T> : Clause<T>
        where T : IStatement
    {
        protected JoinClause(T statement)
            : base(statement) {
            if (statement == null) {
                throw new ArgumentNullException("statement");
            }
        }

        public JoinClause<T> CrossApply(TableValuedFunction tvFunction, Predicate onPredicate) {
            var clause = new CrossApply<T>(this.Statement, tvFunction, onPredicate);
            this.NextFragment = clause;
            return clause;
        }

        public GroupByClause<T> GroupBy(Expression expression, params Expression[] expressions) {
            var clause = new GroupByClause<T>(this.Statement, Collection<Expression>.From(expression, expressions));
            this.NextFragment = clause;
            return clause;
        }

        public JoinClause<T> InnerJoin(Table table, Predicate onPredicate) {
            var clause = new InnerJoin<T>(this.Statement, table, onPredicate);
            this.NextFragment = clause;
            return clause;
        }

        public JoinClause<T> LeftOuterJoin(Table table, Predicate onPredicate) {
            var clause = new LeftOuterJoin<T>(this.Statement, table, onPredicate);
            this.NextFragment = clause;
            return clause;
        }

        public WhereClause<T> Where(Predicate searchCondition) {
            var clause = new WhereClause<T>(this.Statement, searchCondition);
            this.NextFragment = clause;
            return clause;
        }
    }
}