// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FromClause.cs" company="CODE Insiders LTD">
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

    using CodeInsiders.SharpQL.Clauses;
    using CodeInsiders.SharpQL.Helpers.Internal;

    public class FromClause<T> : Clause<T>
        where T : IStatement
    {
        public string Alias { get; private set; }
        public SelectStatement SelectStatement { get; private set; }
        public Table Table { get; private set; }

        internal FromClause(T statement, Table tableInternal)
            : base(statement) {
            if (tableInternal == null) {
                throw new ArgumentNullException("tableInternal");
            }
            this.Table = tableInternal;
        }

        internal FromClause(T statement, SelectStatement selectStatement, string alias)
            : base(statement) {
            if (selectStatement == null) {
                throw new ArgumentNullException("selectStatement");
            }
            this.SelectStatement = selectStatement;
            this.Alias = alias;
        }

        public override void Build(SqlFragment parent, TSqlVisitor visitor) {
            visitor.FromClause(parent, this);
        }

        public JoinClause<T> CrossApply(TableValuedFunction tvFunction, Predicate onSearchCondition) {
            if (tvFunction == null) {
                throw new ArgumentNullException("tvFunction");
            }
            if (onSearchCondition == null) {
                throw new ArgumentNullException("onSearchCondition");
            }
            return this.NextClause(new CrossApply<T>(this.Statement, tvFunction, onSearchCondition));
        }

        public GroupByClause<T> GroupBy(Expression expression, params Expression[] expressions) {
            if (expression == null) {
                throw new ArgumentNullException("expression");
            }
            return this.NextClause(new GroupByClause<T>(this.Statement, Collection<Expression>.From(expression, expressions)));
        }

        public JoinClause<T> InnerJoin(Table table, Predicate onSearchCondition) {
            if (table == null) {
                throw new ArgumentNullException("table");
            }
            return this.NextClause(new InnerJoin<T>(this.Statement, table, onSearchCondition));
        }

        public JoinClause<T> LeftOuterJoin(Table table, Predicate onSearchCondition) {
            if (table == null) {
                throw new ArgumentNullException("table");
            }
            if (onSearchCondition == null) {
                throw new ArgumentNullException("onSearchCondition");
            }
            return this.NextClause(new LeftOuterJoin<T>(this.Statement, table, onSearchCondition));
        }

        public JoinClause<T> RightOuterJoin(Table table, Predicate onSearchCondition) {
            if (table == null) {
                throw new ArgumentNullException("table");
            }
            if (onSearchCondition == null) {
                throw new ArgumentNullException("onSearchCondition");
            }
            return this.NextClause(new RightOuterJoin<T>(this.Statement, table, onSearchCondition));
        }

        public JoinClause<T> FullOuterJoin(Table table, Predicate onSearchCondition) {
            if (table == null) {
                throw new ArgumentNullException("table");
            }
            if (onSearchCondition == null) {
                throw new ArgumentNullException("onSearchCondition");
            }
            return this.NextClause(new FullOuterJoin<T>(this.Statement, table, onSearchCondition));
        }

        public WhereClause<T> Where(Predicate searchCondition) {
            if (searchCondition == null) {
                throw new ArgumentNullException("searchCondition");
            }
            return this.NextClause(new WhereClause<T>(this.Statement, searchCondition));
        }
    }
}