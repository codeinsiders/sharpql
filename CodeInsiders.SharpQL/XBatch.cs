// --------------------------------------------------------------------------------------------------------------------
// <copyright file="XBatch.cs" company="CODE Insiders LTD">
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
    using System.Data.SqlClient;

    using CodeInsiders.SharpQL.Helpers.Internal;

    public abstract class XBatch
    {
        private readonly List<IStatement> statementList = new List<IStatement>(5);
        private ScriptBuilder scriptBuilder;

        public ScriptBuilder ScriptBuilder
        {
            get
            {
                return this.scriptBuilder ?? (this.scriptBuilder = new ScriptBuilder(this));
            }
        }

        public T AppendStatement<T>(T statement) where T : IStatement {
            this.statementList.Add(statement);
            return statement;
        }

        public IReadOnlyCollection<SqlParameter> Parameters
        {
            get {
                return this.ScriptBuilder.Parameters;
            }
        }

        protected void CreateInputParam(SqlParameter parameter) {
            if (parameter == null) {
                throw new ArgumentNullException("parameter");
            }
        }

        public DeleteFromTable DeleteFrom(Table table) {
            var stm = new DeleteStatement(this);
            var clause = new DeleteFromTable(stm, table);
            stm.FirstFragment = clause;
            return clause;
        }

        public DeleteClause DeleteTopFrom(Expression top, Table table) {
            var stm = new DeleteStatement(this);
            var clause = new DeleteTopFromTable(stm, top, table);
            stm.FirstFragment = clause;
            return clause;
        }

        public DeleteClause DeleteTopPercentFrom(Expression percent, Table table) {
            var stm = new DeleteStatement(this);
            var clause = new DeleteTopPercentFromTable(stm, percent, table);
            stm.FirstFragment = clause;
            return clause;
        }

        public InsertClause InsertInto(Table table, params Column[] columns) {
            var stm = new InsertStatement(this);
            var clause = new InsertIntoTable(stm, table, columns);
            stm.FirstFragment = clause;
            return clause;
        }

        public InsertIntoTableAdvanced InsertInto(Table table) {
            var stm = new InsertStatement(this);
            var clause = new InsertIntoTableAdvanced(stm, table);
            stm.FirstFragment = clause;
            return clause;
        }

        public InsertClause InsertTopInto(Expression top, Table table, params Column[] columns) {
            var stm = new InsertStatement(this);
            var clause = new InsertTopIntoTable(stm, top, table, columns);
            stm.FirstFragment = clause;
            return clause;
        }

        public InsertClause InsertTopPercentInto(Expression percent, Table table, params Column[] columns) {
            var statement = new InsertStatement(this);
            var clause = new InsertTopPercentIntoTable(statement, percent, table, columns);
            statement.FirstFragment = clause;
            return clause;
        }

        public OrderByAsc OrderByAsc(Expression expression, params Expression[] expressions) {
            var statement = new OrderByStatement(this);
            var clause = new OrderByAsc(statement, Collection<Expression>.From(expression, expressions));
            statement.FirstFragment = clause;
            return clause;
        }

        public OrderByDesc OrderByDesc(Expression expression, params Expression[] expressions) {
            var statement = new OrderByStatement(this);
            var clause = new OrderByDesc(statement, Collection<Expression>.From(expression, expressions));
            statement.FirstFragment = clause;
            return clause;
        }

        public Select<SelectStatement> Select(IEnumerable<Expression> expressions) {
            var stm = new SelectStatement(this);
            var clause = new Select<SelectStatement>(stm, expressions);
            stm.FirstFragment = clause;
            return clause;
        }

        public Select<SelectStatement> Select(Expression expression, params Expression[] expressions) {
            if (expression == null) {
                throw new ArgumentNullException("expression");
            }

            var statement = new SelectStatement(this);
            var clause = new Select<SelectStatement>(
                statement,
                Collection<Expression>.From(expression, expressions));
            statement.FirstFragment = clause;
            return clause;
        }

        public Select<SingleExprSelectStatement> Select(Expression expression) {
            var statement = new SingleExprSelectStatement(this);
            var clause = new Select<SingleExprSelectStatement>(statement, new[] { expression });
            statement.FirstFragment = clause;
            return clause;
        }

        public SelectDistinct<SelectStatement> SelectDistinct(Expression expression, params Expression[] expressions) {
            var statement = new SelectStatement(this);
            var clause = new SelectDistinct<SelectStatement>(
                statement,
                Collection<Expression>.From(expression, expressions));
            statement.FirstFragment = clause;

            return clause;
        }

        public SelectDistinct<SingleExprSelectStatement> SelectDistinct(Expression expression) {
            var statement = new SingleExprSelectStatement(this);
            var clause = new SelectDistinct<SingleExprSelectStatement>(statement, new[] { expression });
            statement.FirstFragment = clause;
            return clause;
        }

        public SelectDistinctTop<SelectStatement> SelectDistinctOne(
            Expression expression,
            params Expression[] expressions) {
            return this.SelectDistinctTop(1, expression, expressions);
        }

        public SelectDistinctTop<SelectStatement> SelectDistinctTop(
            Expression top,
            Expression expression,
            params Expression[] expressions) {
            var statement = new SelectStatement(this);

            var clause = new SelectDistinctTop<SelectStatement>(
                statement,
                top,
                Collection<Expression>.From(expression, expressions));

            statement.FirstFragment = clause;
            return clause;
        }

        public SelectDistinctTopPercent<SelectStatement> SelectDistinctTopPercent(
            Expression percent,
            Expression expression,
            params Expression[] expressions) {
            var statement = new SelectStatement(this);
            var clause = new SelectDistinctTopPercent<SelectStatement>(
                statement,
                percent,
                Collection<Expression>.From(expression, expressions));

            statement.FirstFragment = clause;
            return clause;
        }

        public SelectTop<SelectStatement> SelectOne(Expression expression, params Expression[] expressions) {
            if (expression == null) {
                throw new ArgumentNullException("expression");
            }
            return this.SelectTop(1, expression, expressions);
        }

        public SelectTop<SingleExprSingleRowSelectStatement> SelectOne(Expression expression) {
            if (expression == null) {
                throw new ArgumentNullException("expression");
            }
            var statement = new SingleExprSingleRowSelectStatement(this);
            var clause = new SelectTop<SingleExprSingleRowSelectStatement>(statement, 1, new[] { expression });
            statement.FirstFragment = clause;
            return clause;
        }

        public SelectTop<SelectStatement> SelectOne(IEnumerable<Expression> expressions) {
            if (expressions == null) {
                throw new ArgumentNullException("expressions");
            }
            var statement = new SelectStatement(this);
            var clause = new SelectTop<SelectStatement>(statement, 1, expressions);
            statement.FirstFragment = clause;
            return clause;
        }

        public SelectTop<SelectStatement> SelectTop(
            Expression top,
            Expression expression,
            params Expression[] expressions) {
            var statement = new SelectStatement(this);
            var clause = new SelectTop<SelectStatement>(
                statement,
                top,
                Collection<Expression>.From(expression, expressions));

            statement.FirstFragment = clause;
            return clause;
        }

        public SelectTop<SelectStatement> SelectTop(Expression top, IEnumerable<Expression> expressions) {
            var statement = new SelectStatement(this);
            var clause = new SelectTop<SelectStatement>(statement, top, expressions);
            statement.FirstFragment = clause;
            return clause;
        }

        public SelectTop<SingleExprSelectStatement> SelectTop(Expression top, Expression expression) {
            var statement = new SingleExprSelectStatement(this);
            var clause = new SelectTop<SingleExprSelectStatement>(statement, top, new[] { expression });
            statement.FirstFragment = clause;
            return clause;
        }

        public SelectTopPercent<SelectStatement> SelectTopPercent(
            Expression percent,
            Expression expression,
            params Expression[] expressions) {
            var statement = new SelectStatement(this);
            var clause = new SelectTopPercent<SelectStatement>(
                statement,
                percent,
                Collection<Expression>.From(expression, expressions));

            statement.FirstFragment = clause;
            return clause;
        }

        public SelectTopPercent<SingleExprSelectStatement> SelectTopPercent(Expression percent, Expression expression) {
            var statement = new SingleExprSelectStatement(this);
            var clause = new SelectTopPercent<SingleExprSelectStatement>(statement, percent, new[] { expression });
            statement.FirstFragment = clause;
            return clause;
        }

        public override string ToString() {
            return this.ToString(false);
        }

        public string ToString(bool outputParameterDeclarations) {
            this.ScriptBuilder.Clear();
            TSqlVisitor visitor = new TSqlVisitor(this.ScriptBuilder);

            foreach (var statement in this.statementList) {
                this.ScriptBuilder.AppendFragment(statement.FirstFragment, null, visitor);
                this.ScriptBuilder.AppendLine();
            }
            return this.ScriptBuilder.ToSqlString(outputParameterDeclarations);
        }

        public UpdateClause Update(Table table) {
            var statement = new UpdateStatement(this);
            var clause = new UpdateTable(statement, table);
            statement.FirstFragment = clause;
            return clause;
        }

        public UpdateClause UpdateTop(Expression top, Table table) {
            var stetment = new UpdateStatement(this);
            var clause = new UpdateTopTable(stetment, top, table);
            stetment.FirstFragment = clause;
            return clause;
        }

        public UpdateClause UpdateTopPercent(Expression percent, Table table) {
            var stetment = new UpdateStatement(this);
            var clause = new UpdateTopPercentTable(stetment, percent, table);
            stetment.FirstFragment = clause;
            return clause;
        }

        public TruncateClause TruncateTable(Table table) {
            if (table == null) {
                throw new ArgumentNullException("table");
            }
            var statement = new TruncateTableStatement(this);
            var clause = new TruncateClause(statement, table);
            statement.FirstFragment = clause;
            return clause;
        }

        public SimpleCase Case(Expression expression) {
            if (expression == null) {
                throw new ArgumentNullException("expression");
            }
            return new SimpleCase(expression);
        }

        public SearchedCase CaseWhen(Predicate whenPredicate, Expression thenValue) {
            if (whenPredicate == null) {
                throw new ArgumentNullException("whenPredicate");
            }
            if (thenValue == null) {
                throw new ArgumentNullException("thenValue");
            }

            return new SearchedCase().CaseWhen(whenPredicate, thenValue);
        }
    }
}