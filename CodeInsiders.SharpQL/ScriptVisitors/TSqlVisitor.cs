// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TSqlVisitor.cs" company="CODE Insiders LTD">
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
    using System.Globalization;
    using System.Linq;

    using CodeInsiders.SharpQL.AggregateFunctions;
    using CodeInsiders.SharpQL.Assignments;
    using CodeInsiders.SharpQL.Clauses;
    using CodeInsiders.SharpQL.DataType;
    using CodeInsiders.SharpQL.Expressions;
    using CodeInsiders.SharpQL.Functions.AggregateFunctions;
    using CodeInsiders.SharpQL.Helpers.Internal;
    using CodeInsiders.SharpQL.Operators;
    using CodeInsiders.SharpQL.Predicates;
    using CodeInsiders.SharpQL.ScalarFunctions;

    // ReSharper disable once InconsistentNaming
    public class TSqlVisitor : ScriptVisitor
    {
        public TSqlVisitor(ScriptBuilder scriptBuilder)
            : base(scriptBuilder) {}

        public void VisitFalsePredicate(SqlFragment parent, FalsePredicate falsePredicate) {
            this.Script.Append("1<>1");
        }

        public void VisitTruePredicate(SqlFragment parent, TruePredicate truePredicate) {
            this.Script.Append("1=1");
        }

        public void VisitNullConstantExpression(SqlFragment parent, NullConstantExpression falsePredicate) {
            this.Script.Append(TSqlKeyword.NULL);
        }

        public void VisitDefaultValueExpression(SqlFragment parent, DefaultValueExpression falsePredicate) {
            this.Script.Append(TSqlKeyword.DEFAULT);
        }

        public void VisitExpressionAssign(SqlFragment parent, ExpressionAssign expressionAssign) {
            expressionAssign.Column.Build(expressionAssign, this);
            this.Script.Append("=");
            expressionAssign.Expression.Build(expressionAssign, this);
        }

        public void VisitSubstractAssign(SqlFragment parent, SubstractAssign substractAssign) {
            substractAssign.Column.Build(parent, this);
            this.Script.Append("-=");
            substractAssign.Expression.Build(parent, this);
        }

        public void VisitMultiplyAssign(SqlFragment parent, MultiplyAssign multiplyAssign) {
            multiplyAssign.Column.Build(parent, this);
            this.Script.Append("*=");
            multiplyAssign.Expression.Build(parent, this);
        }

        public void VisitModuloAssign(SqlFragment parent, ModuloAssign moduloAssign) {
            moduloAssign.Column.Build(parent, this);
            this.Script.Append("%=");
            moduloAssign.Expression.Build(parent, this);
        }

        public void VisitAsExpression(SqlFragment parent, AsExpression asExpression) {
            asExpression.Expression1.Build(parent, this);

            if (parent is OrderBy == false) {
                this.Script.AppendFormat(" AS [{0}]", asExpression.Alias);
            }
        }

        public void VisitColumn(SqlFragment parent, Column column) {
            column.Table.Build(column, this);
            this.Script.AppendFormat(".[{0}]", column.ColumnName);
        }

        public void VisitBitwiseXorAssign(SqlFragment parent, BitwiseXorAssign bitwiseXorAssign) {
            bitwiseXorAssign.Column.Build(parent, this);
            this.Script.Append("^=");
            bitwiseXorAssign.Expression.Build(parent, this);
        }

        public void VisitDivideAssign(SqlFragment parent, DivideAssign divideAssign) {
            divideAssign.Column.Build(parent, this);
            this.Script.Append("-=");
            divideAssign.Expression.Build(parent, this);
        }

        public void VisitBitwiseOrAssign(SqlFragment parent, BitwiseOrAssign bitwiseOrAssign) {
            bitwiseOrAssign.Column.Build(parent, this);
            this.Script.Append("|=");
            bitwiseOrAssign.Expression.Build(parent, this);
        }

        public void VisitBitwiseAndAssign(SqlFragment parent, BitwiseAndAssign bitwiseAndAssign) {
            bitwiseAndAssign.Column.Build(parent, this);
            this.Script.Append("&=");
            bitwiseAndAssign.Expression.Build(parent, this);
        }

        public void VisitAddAssign(SqlFragment parent, AddAssign addAssign) {
            addAssign.Column.Build(parent, this);
            this.Script.Append("+=");
            addAssign.Expression.Build(parent, this);
        }

        public void VisitScalarSelectStatementExpression(SqlFragment parent, ScalarSelectStatementExpression scalarSelectStatementExpression) {
            this.Script.Append("(");
            this.Script.AppendFragment(scalarSelectStatementExpression.Statement.FirstFragment, scalarSelectStatementExpression, this);
            this.Script.Append(")");
        }

        public void VisitRowNumberOverOrderByExpression(SqlFragment parent, RowNumberOverOrderByExpression rowNumberOverOrderByExpression) {
            this.Script.Append("ROW_NUMBER() OVER (ORDER BY (");
            this.Script.AppendFragment(rowNumberOverOrderByExpression.Column, parent, this);
            this.Script.Append("))");
        }

        public void VisitIsNullPredicate(SqlFragment parent, IsNullPredicate isNullPredicate) {
            isNullPredicate.Operand.Build(parent, this);
            this.Script.Append("IS NULL");
        }

        public void VisitIsNotNullPredicate(SqlFragment parent, IsNotNullPredicate isNotNullPredicate) {
            isNotNullPredicate.Operand.Build(parent, this);
            this.Script.Append("IS NOT NULL");
        }

        public void VisitUpdateTable(SqlFragment parent, UpdateTable updateTable) {
            this.Script.Append("UPDATE ");
            this.Script.AppendFragment(updateTable.Table, parent, this);
        }

        public void VisitIsLessThanOrEqualToPredicate(SqlFragment parent, IsLessThanOrEqualToPredicate isLessThanOrEqualToPredicate) {
            isLessThanOrEqualToPredicate.LeftOperand.Build(parent, this);
            this.Script.Append("<=");
            isLessThanOrEqualToPredicate.RightOperand.Build(parent, this);
        }

        public void VisitIsNotEqualToPredicate(SqlFragment parent, IsNotEqualToPredicate isNotEqualToPredicate) {
            isNotEqualToPredicate.LeftOperand.Build(parent, this);
            this.Script.Append("!=");
            isNotEqualToPredicate.RightOperand.Build(parent, this);
        }

        public void IsLessThanPredicate(SqlFragment parent, IsLessThanPredicate isLessThanPredicate) {
            isLessThanPredicate.LeftOperand.Build(parent, this);
            this.Script.Append("<");
            isLessThanPredicate.RightOperand.Build(parent, this);
        }

        public void NotExistsPredicate(SqlFragment parent, NotExistsPredicate notExistsPredicate) {
            this.Script.Append("NOT EXISTS(");
            this.Script.AppendFragment(notExistsPredicate.SubQuery.FirstFragment, parent, this);
            this.Script.Append(")");
        }

        public void VisitVarbinaryDataType(SqlFragment parent, VarbinaryDataType varbinaryDataType) {
            // TODO what is that? this.Build(parent, this);

            this.Script.OpenParen();

            this.Script.Append(
                varbinaryDataType.Lenght == uint.MaxValue ?
                    TSqlKeyword.MAX :
                    varbinaryDataType.Lenght.ToString(CultureInfo.InvariantCulture));

            this.Script.CloseParen();
        }

        public void IsLikePredicate(SqlFragment parent, IsLikePredicate isLikePredicate) {
            isLikePredicate.LeftOperand.Build(parent, this);
            this.Script.Append(TSqlKeyword.LIKE);
            isLikePredicate.RightOperand.Build(parent, this);
        }

        public void AndConditionalOperator(SqlFragment parent, AndConditionalOperatorAndPredicate andConditionalOperatorAndPredicate) {
            this.Script.OpenParen();
            andConditionalOperatorAndPredicate.LeftOperand.Build(parent, this);
            this.Script.Append(TSqlKeyword.AND);
            andConditionalOperatorAndPredicate.RightOperand.Build(parent, this);
            this.Script.CloseParen();
        }

        public void EqualToPredicate(SqlFragment parent, EqualToPredicate equalToPredicate) {
            equalToPredicate.LeftOperand.Build(parent, this);
            this.Script.Append("=");
            equalToPredicate.RightOperand.Build(parent, this);
        }

        public void IsGreaterThanOrEqualToPredicate(SqlFragment parent, IsGreaterThanOrEqualToPredicate isGreaterThanOrEqualToPredicate) {
            isGreaterThanOrEqualToPredicate.LeftOperand.Build(parent, this);
            this.Script.Append(">=");
            isGreaterThanOrEqualToPredicate.RightOperand.Build(parent, this);
        }

        public void OrConditionalOperator(SqlFragment parent, OrConditionalOperator orConditionalOperator) {
            this.Script.OpenParen();
            orConditionalOperator.LeftOperand.Build(parent, this);
            this.Script.Append(TSqlKeyword.OR);
            orConditionalOperator.RightOperand.Build(parent, this);
            this.Script.CloseParen();
        }

        public void IsGreaterThanPredicate(SqlFragment parent, IsGreaterThanPredicate isGreaterThanPredicate) {
            isGreaterThanPredicate.LeftOperand.Build(parent, this);
            this.Script.Append(">");
            isGreaterThanPredicate.RightOperand.Build(parent, this);
        }

        public void EndsWithPredicate(SqlFragment parent, EndsWithPredicate endsWithPredicate) {
            endsWithPredicate.LeftOperand.Build(parent, this);
            this.Script.Append("LIKE");
            Expression expression = "%" + endsWithPredicate.RightOperand;
            expression.Build(parent, this);
        }

        public void DeleteTopPercentFromTable(SqlFragment parent, DeleteTopPercentFromTable deleteTopPercentFromTable) {
            this.Script.Append("DELETE TOP (");
            this.Script.AppendFragment(deleteTopPercentFromTable.TopPercent, deleteTopPercentFromTable, this);
            this.Script.Append(") PERCENT FROM");
            this.Script.AppendFragment(deleteTopPercentFromTable.Table, deleteTopPercentFromTable, this);
        }

        public void DeleteTopFromTable(SqlFragment parent, DeleteTopFromTable deleteTopFromTable) {
            this.Script.Append("DELETE TOP (");
            this.Script.AppendFragment(deleteTopFromTable.Top, deleteTopFromTable, this);
            this.Script.Append(") FROM");
            this.Script.AppendFragment(deleteTopFromTable.Table, deleteTopFromTable, this);
        }

        public void ContainsPredicate(SqlFragment parent, ContainsPredicate containsPredicate) {
            // TODO do we keep contains here?
            containsPredicate.LeftOperand.Build(parent, this);
            this.Script.Append("LIKE");

            Expression expression = "%" + containsPredicate.RightOperand + "%";
            expression.Build(parent, this);
        }

        public void DivideArithmeticExpressionOperator(SqlFragment parent, DivideArithmeticExpressionOperator divideArithmeticExpressionOperator) {
            this.Script.OpenParen();
            divideArithmeticExpressionOperator.LeftOperand.Build(divideArithmeticExpressionOperator, this);
            this.Script.Append("/");
            divideArithmeticExpressionOperator.RightOperand.Build(divideArithmeticExpressionOperator, this);
            this.Script.CloseParen();
        }

        public void MinusArithmeticExpressionOperator(SqlFragment parent, MinusArithmeticExpressionOperator minusArithmeticExpressionOperator) {
            this.Script.OpenParen();
            minusArithmeticExpressionOperator.LeftOperand.Build(minusArithmeticExpressionOperator, this);
            this.Script.Append("-");
            minusArithmeticExpressionOperator.RightOperand.Build(minusArithmeticExpressionOperator, this);
            this.Script.CloseParen();
        }

        public void ModuloArithmeticExpressionOperator(SqlFragment parent, ModuloArithmeticExpressionOperator moduloArithmeticExpressionOperator) {
            this.Script.OpenParen();
            moduloArithmeticExpressionOperator.LeftOperand.Build(moduloArithmeticExpressionOperator, this);
            this.Script.Append("%");
            moduloArithmeticExpressionOperator.RightOperand.Build(moduloArithmeticExpressionOperator, this);
            this.Script.CloseParen();
        }

        public void MultiplyArithmeticExpressionOperator(SqlFragment parent, MultiplyArithmeticExpressionOperator multiplyArithmeticExpressionOperator) {
            this.Script.OpenParen();
            multiplyArithmeticExpressionOperator.LeftOperand.Build(multiplyArithmeticExpressionOperator, this);
            this.Script.Append("*");
            multiplyArithmeticExpressionOperator.RightOperand.Build(multiplyArithmeticExpressionOperator, this);
            this.Script.CloseParen();
        }

        public void PlusArithmeticExpressionOperator(SqlFragment parent, PlusArithmeticExpressionOperator plusArithmeticExpressionOperator) {
            this.Script.OpenParen();
            plusArithmeticExpressionOperator.LeftOperand.Build(plusArithmeticExpressionOperator, this);
            this.Script.Append("+");
            plusArithmeticExpressionOperator.RightOperand.Build(plusArithmeticExpressionOperator, this);
            this.Script.CloseParen();
        }

        public void TruePredicate(SqlFragment parent, TruePredicate truePredicate) {
            this.Script.Append("1==1");
        }

        public void FalsePredicate(SqlFragment parent, FalsePredicate falsePredicate) {
            this.Script.Append("1<>1");
        }

        public void NullConstantExpression(SqlFragment parent, NullConstantExpression nullConstantExpression) {
            this.Script.Append("NULL");
        }

        public void DefaultValueExpression(SqlFragment parent, DefaultValueExpression defaultValueExpression) {
            this.Script.Append("DEFAULT");
        }

        public void ExpressionAssign(SqlFragment parent, ExpressionAssign expressionAssign) {
            expressionAssign.Column.Build(parent, this);
            this.Script.Append("=");
            expressionAssign.Expression.Build(parent, this);
        }

        public void OrderBy(SqlFragment parent, OrderBy orderBy) {
            if ((parent is OrderBy) == false) {
                this.Script.LineAppendLine(TSqlKeyword.ORDER_BY);
            }
            else {
                this.Script.LineAppend("  , ");
            }

            ExpressionFormatter.FormatExpressionList(orderBy.Expressions, orderBy, this.Script, this, orderBy.Direction);
        }

        public void ConstantExpression(SqlFragment parent, ConstantExpression constantExpression) {
            var parameter = this.Script.CreateInputParameter(constantExpression.DbType, constantExpression.Value, constantExpression.Size);
            this.Script.Append(parameter.ParameterName);
        }

        public void UpdateTopTable(SqlFragment parent, UpdateTopTable updateTopTable) {
            this.Script.Append("UPDATE TOP(");
            this.Script.AppendFragment(updateTopTable.Top, parent, this);
            this.Script.Append(")");
            this.Script.AppendFragment(updateTopTable.Table, parent, this);
        }

        public void SetUpdate(SqlFragment parent, SetUpdate setUpdate) {
            this.Script.Append("SET");
            ExpressionFormatter.FormatAssignList(setUpdate.AssingList, setUpdate, this.Script, this);
        }

        public void StartsWithPredicate(SqlFragment parent, StartsWithPredicate startsWithPredicate) {
            startsWithPredicate.LeftOperand.Build(startsWithPredicate, this);
            this.Script.Append(TSqlKeyword.LIKE);
            Expression expression = startsWithPredicate.RightOperand + "%";
            expression.Build(startsWithPredicate, this);
        }

        public void NextValues(SqlFragment parent, NextValues nextValues) {
            this.Script.Append("(");
            ExpressionFormatter.FormatExpressionList(nextValues.ExpressionList, parent, this.Script, this);
            this.Script.Append(")");
        }

        public void ExistsPredicate(SqlFragment parent, ExistsPredicate existsPredicate) {
            this.Script.Append("EXISTS(");
            this.Script.AppendFragment(existsPredicate.SubQuery.FirstFragment, parent, this);
            this.Script.Append(")");
        }

        public void UpdateTopPercentTable(SqlFragment parent, UpdateTopPercentTable updateTopPercentTable) {
            this.Script.Append(TSqlKeyword.UPDATE_TOP).OpenParen();
            this.Script.AppendFragment(updateTopPercentTable.TopPercent, parent, this);
            this.Script.CloseParen().Append(TSqlKeyword.PERCENT);
            this.Script.AppendFragment(updateTopPercentTable.Table, parent, this);
        }

        public void LeftOuterJoin<T>(SqlFragment parent, LeftOuterJoin<T> leftOuterJoin) where T : IStatement {
            this.Script.Append(TSqlKeyword.LEFT_OUTER_JOIN);
            this.Script.AppendFragment(leftOuterJoin.Table, parent, this);
            this.Script.Append(TSqlKeyword.ON);
            this.Script.AppendFragment(leftOuterJoin.OnPredicate, parent, this);
        }

        public void InnerJoin<T>(SqlFragment parent, InnerJoin<T> innerJoin) where T : IStatement {
            this.Script.AppendLine();
            this.Script.Append(TSqlKeyword.INNER_JOIN);
            this.Script.AppendFragment(innerJoin.Table, parent, this);
            this.Script.Append(TSqlKeyword.ON);
            this.Script.AppendFragment(innerJoin.OnPredicate, parent, this);
        }

        public void CrossApply<T>(SqlFragment parent, CrossApply<T> crossApply) where T : IStatement {
            this.Script.Append(TSqlKeyword.CROSS_APPLY);
            this.Script.AppendFragment(crossApply.TableValuedFunction, parent, this);
            this.Script.Append(TSqlKeyword.ON);
            this.Script.AppendFragment(crossApply.OnPredicate, parent, this);
        }

        public void InsertValuesStart(SqlFragment parent, InsertValuesStart insertValuesStart) {
            this.Script.AppendLine();
            this.Script.AppendLine("VALUES(");
            ExpressionFormatter.FormatExpressionList(insertValuesStart.ExpressionList, insertValuesStart, this.Script, this);
            this.Script.Append(")");
        }

        public void FullOuterJoin<T>(SqlFragment parent, FullOuterJoin<T> fullOuterJoin) where T : IStatement {
            this.Script.LineAppend(TSqlKeyword.FULL_OUTER_JOIN);
            this.Script.AppendFragment(fullOuterJoin.Table, parent, this);
            this.Script.Append(TSqlKeyword.ON);
            this.Script.AppendFragment(fullOuterJoin.OnPredicate, parent, this);
        }

        public void SelectTopPercent<T>(SqlFragment parent, SelectTopPercent<T> selectTopPercent) where T : IStatement {
            this.Script.Append("SELECT TOP (");
            this.Script.AppendFragment(selectTopPercent.Percent, parent, this);
            this.Script.Append(") PERCENT");
            ExpressionFormatter.FormatExpressionList(selectTopPercent.ColumnList, parent, this.Script, this);
        }

        public void SelectTop<T>(SqlFragment parent, SelectTop<T> selectTop) where T : IStatement {
            this.Script.Append("SELECT TOP (");
            this.Script.AppendFragment(selectTop.Count, parent, this);
            this.Script.Append(")");
            ExpressionFormatter.FormatExpressionList(selectTop.ColumnList, parent, this.Script, this);
        }

        public void SelectDistinctTop<T>(SqlFragment parent, SelectDistinctTop<T> selectDistinctTop) where T : IStatement {
            this.Script.Append("SELECT DISTINCT TOP (");
            this.Script.AppendFragment(selectDistinctTop.Count, parent, this);
            this.Script.Append(")");
            ExpressionFormatter.FormatExpressionList(selectDistinctTop.Columns, parent, this.Script, this);
        }

        public void GetDateFunction(SqlFragment parent, GetDateFunction getDateFunction) {
            this.Script.Append("GETDATE()");
        }

        public void InsertIntoTable(SqlFragment parent, InsertIntoTable insertIntoTable) {
            this.Script.Append(TSqlKeyword.INSERT_INTO);
            this.Script.AppendFragment(insertIntoTable.Table, insertIntoTable, this);

            if (!insertIntoTable.ColumnList.Any()) {
                return;
            }

            this.Script.OpenParen();
            ExpressionFormatter.FormatColumnList(insertIntoTable.ColumnList, insertIntoTable, this.Script, this);
            this.Script.CloseParen();
        }

        public void Parameter(SqlFragment parent, Parameter parameter) {
            this.Script.Append(parameter.ParameterName);
        }

        public void RightOuterJoin<T>(SqlFragment parent, RightOuterJoin<T> rightOuterJoin) where T : IStatement {
            this.Script.AppendLine(TSqlKeyword.RIGHT_OUTER_JOIN);
            this.Script.AppendFragment(rightOuterJoin.Table, rightOuterJoin, this);
            this.Script.Append(TSqlKeyword.ON);
            this.Script.AppendFragment(rightOuterJoin.OnPredicate, rightOuterJoin, this);
        }

        public void TruncateClause(SqlFragment parent, TruncateClause truncateClause) {
            this.Script.Append("TRUNCATE TABLE ");
            this.Script.AppendFragment(truncateClause.Table, truncateClause, this);
        }

        public void CastFunction(SqlFragment parent, CastFunction castFunction) {
            this.Script.Append("CAST(");
            castFunction.Expression.Build(castFunction, this);
            this.Script.Append(TSqlKeyword.AS);
            castFunction.Type.Build(castFunction, this);
            this.Script.Append(")");
        }

        public void InsertTopPercentIntoTable(SqlFragment insertTop, InsertTopPercentIntoTable insertTopPercentIntoTable) {
            this.Script.Append("INSERT TOP (");
            this.Script.AppendFragment(insertTopPercentIntoTable.Percent, insertTopPercentIntoTable, this);
            this.Script.Append(") PERCENT INTO");
            this.Script.AppendFragment(insertTopPercentIntoTable.Table, insertTopPercentIntoTable, this);

            if (insertTopPercentIntoTable.ColumnList.Any()) {
                this.Script.Append("(");
                ExpressionFormatter.FormatColumnList(insertTopPercentIntoTable.ColumnList, insertTopPercentIntoTable, this.Script, this);
                this.Script.Append(")");
            }
        }

        public void InsertTopIntoTable(SqlFragment parent, InsertTopIntoTable insertTopIntoTable) {
            this.Script.Append("INSERT TOP (");
            this.Script.AppendFragment(insertTopIntoTable.Top, insertTopIntoTable, this);
            this.Script.Append(") INTO");
            this.Script.AppendFragment(insertTopIntoTable.Table, insertTopIntoTable, this);

            if (insertTopIntoTable.ColumnList.Any()) {
                this.Script.Append("(");
                ExpressionFormatter.FormatColumnList(insertTopIntoTable.ColumnList, insertTopIntoTable, this.Script, this);
                this.Script.Append(")");
            }
        }

        public void SqlDataType(SqlFragment parent, SqlDataType sqlDataType) {
            this.Script.Append(sqlDataType.DataTypeSqlScript);
            if (sqlDataType.Length != null) {
                var v = sqlDataType.Length.Value;
                if (v <= 8000) {
                    this.Script.AppendFormat("({0})", v);
                }
                else {
                    this.Script.Append("(MAX)");
                }
            }
        }

        public void SystemExpression(SqlFragment parent, SystemConstant systemConstant) {
            this.Script.Append(systemConstant.SqlScript);
        }

        public void InValuesPredicate(SqlFragment parent, InValuesPredicate inValuesPredicate) {
            inValuesPredicate.Expression.Build(parent, this);
            this.Script.Append("IN(");
            ExpressionFormatter.FormatExpressionList(inValuesPredicate.ValueList, inValuesPredicate, this.Script, this);
            this.Script.Append(")");
        }

        public void InSingleExprSelectStatementPredicate(SqlFragment parent, InSingleExprSelectStatementPredicate element) {
            element.Expression.Build(parent, this);
            this.Script.Append("IN(");
            this.Script.AppendFragment(element.SingleExpressionSelectStatement.FirstFragment, element, this);
            this.Script.Append(")");
        }

        public void BetweenPredicate(SqlFragment parent, BetweenPredicate element) {
            this.Script.OpenParen();
            element.Expression.Build(parent, this);
            this.Script.Append(TSqlKeyword.BETWEEN);
            element.Minimum.Build(parent, this);
            this.Script.Append(TSqlKeyword.AND);
            element.Maximum.Build(parent, this);
            this.Script.CloseParen();
        }

        public void FromClause<T>(SqlFragment parent, FromClause<T> fromClause) where T : IStatement {
            // TODO: make it smarter here, impelemnt base interface as table source or something like that see BNF
            // TODO: decide how to handle that may be  introduce subquery class that will act as cte or select but provide columns etc
            if (fromClause.Table != null) {
                this.Script.AppendLine();
                this.Script.Append(TSqlKeyword.FROM);
                this.Script.AppendFragment(fromClause.Table, fromClause, this);
            }
            else {
                this.Script.AppendLine();
                this.Script.Append(TSqlKeyword.FROM);
                this.Script.Append(" (");
                this.Script.AppendFragment(fromClause.SelectStatement.FirstFragment, fromClause, this);
                this.Script.AppendFormat(") AS {0}", fromClause.Alias);
            }
        }

        public void SearchedCase(SqlFragment parent, SearchedCase searchedCase) {
            this.Script.Append("(CASE");

            foreach (var condition in searchedCase.Conditions) {
                this.Script.Append("WHEN");
                this.Script.AppendFragment(condition.WhenPredicate, searchedCase, this);
                this.Script.Append("THEN");
                this.Script.AppendFragment(condition.ThenValue, searchedCase, this);
                this.Script.AppendLine();
            }

            if (searchedCase.ElseResultExpression != null) {
                this.Script.Append("ELSE");
                this.Script.AppendFragment(searchedCase.ElseResultExpression, searchedCase, this);
                this.Script.AppendLine();
            }

            this.Script.Append("END)");
        }

        public void SimpleCase(SqlFragment parent, SimpleCase simpleCase) {
            this.Script.OpenParen().Append(TSqlKeyword.CASE);
            this.Script.AppendFragment(simpleCase.Expression, simpleCase, this);
            this.Script.AppendLine();

            foreach (var condition in simpleCase.Conditions) {
                this.Script.Append(TSqlKeyword.WHEN);
                this.Script.AppendFragment(condition.WhenValue, simpleCase, this);
                this.Script.Append(TSqlKeyword.THEN);
                this.Script.AppendFragment(condition.ThenValue, simpleCase, this);
                this.Script.AppendLine();
            }

            if (simpleCase.ElseResultExpression != null) {
                this.Script.Append(TSqlKeyword.ELSE);
                this.Script.AppendFragment(simpleCase.ElseResultExpression, simpleCase, this);
                this.Script.AppendLine();
            }
            this.Script.Append(TSqlKeyword.END).CloseParen();
        }

        public void NotInValuesPredicate(SqlFragment parent, NotInValuesPredicate notInValuesPredicate) {
            notInValuesPredicate.Expression.Build(parent, this);
            this.Script.Append("NOT IN(");
            ExpressionFormatter.FormatExpressionList(notInValuesPredicate.Values, notInValuesPredicate, this.Script, this);
            this.Script.Append(")");
        }

        public void NotInSingleExprSelectStatementPredicate(SqlFragment parent, NotInSingleExprSelectStatementPredicate element) {
            element.Expression.Build(parent, this);
            this.Script.Append("NOT IN(");
            this.Script.AppendFragment(element.SingleExpressionSelectStatement.FirstFragment, element, this);
            this.Script.Append(")");
        }

        public void SimpleFunction(SqlFragment parent, SimpleFunction function) {
            this.Script.AppendFormat("{0}(", function.FunctionName);
            ExpressionFormatter.FormatExpressionList(function.Arguments, function, this.Script, this);
            this.Script.Append(")");
        }

        public void ConvertFunction(SqlFragment parent, ConvertFunction convertFunction) {
            this.Script.Append("CONVERT(");
            convertFunction.Type.Build(parent, this);
            this.Script.Append(",");
            convertFunction.Value.Build(parent, this);

            if (convertFunction.Style.HasValue) {
                this.Script.AppendFormat(", {0}", convertFunction.Style.Value.ToString(CultureInfo.InvariantCulture));
            }

            this.Script.Append(")");
        }

        public void UserDefinedFunction(SqlFragment parent, UserDefinedFunction userDefinedFunction) {
            throw new NotImplementedException();
        }

        public void InsertIntoTableAdvanced(SqlFragment parent, InsertIntoTableAdvanced insertIntoTableAdvanced) {
            this.Script.LineAppendLine(TSqlKeyword.INSERT_INTO);
            this.Script.AppendFragment(insertIntoTableAdvanced.Table, insertIntoTableAdvanced, this);
        }

        public void DateAddFunction(SqlFragment parent, DateAddFunction dateAddFunction) {
            this.Script.Append("DATEADD(");
            this.Script.Append(dateAddFunction.DatePart.ToString());
            this.Script.Append(", ");
            this.Script.AppendFragment(dateAddFunction.Number, dateAddFunction, this);
            this.Script.Append(", ");
            this.Script.AppendFragment(dateAddFunction.Date, dateAddFunction, this);
            this.Script.Append(")");
        }

        public void SelectDistinctTopPercent<T>(SqlFragment parent, SelectDistinctTopPercent<T> selectDistinctTopPercent) where T : IStatement {
            this.Script.Append("SELECT DISTINCT TOP (");
            this.Script.AppendFragment(selectDistinctTopPercent.Percent, parent, this);
            this.Script.Append(") PERCENT");
            ExpressionFormatter.FormatExpressionList(selectDistinctTopPercent.ColumnList, parent, this.Script, this);
        }

        public void Table(SqlFragment parent, Table table) {
            if (parent is DeleteClause) {
                this.Script.AppendFormat("[{0}].[{1}]", table.Scheme, table.TableName);
                return;
            }

            if (parent is UpdateClause && table.Alias != null) {
                this.Script.AppendFormat("[{0}]", table.Alias);
                return;
            }

            if (table.Alias != null) {
                if (parent is Column) {
                    this.Script.AppendFormat("[{0}]", table.Alias);
                }
                else {
                    this.Script.AppendFormat("[{0}].[{1}] AS [{2}]", table.Scheme, table.TableName, table.Alias);
                }
            }
            else {
                this.Script.AppendFormat("[{0}].[{1}]", table.Scheme, table.TableName);
            }
        }

        public void WhereDelete(SqlFragment parent, WhereDelete whereDelete) {
            this.Script.LineAppendLine(TSqlKeyword.WHERE);
            this.Script.AppendFragment(whereDelete.SearhCondition, whereDelete, this);
        }

        public void WhereClause<T>(SqlFragment parent, WhereClause<T> whereClause) where T : IStatement {
            this.Script.LineAppendLine(TSqlKeyword.WHERE);
            this.Script.AppendFragment(whereClause.SearchCondition, whereClause, this);
        }

        public void DeleteFromTable(SqlFragment parent, DeleteFromTable deleteFromTable) {
            this.Script.LineAppendLine(TSqlKeyword.DELETE_FROM);
            this.Script.AppendFragment(deleteFromTable.Table, deleteFromTable, this);
        }

        public void SingleExpSingleRowSelectStatementExpression(SqlFragment parent, SingleExpSingleRowSelectStatementExpression element) {
            this.Script.OpenParen();
            this.Script.AppendFragment(element.SelectStatement.FirstFragment, element, this);
            this.Script.CloseParen();
        }

        public void Having<T>(SqlFragment parent, Having<T> having) where T : IStatement {
            this.Script.LineAppendLine(TSqlKeyword.HAVING);
            this.Script.AppendFragment(having.SearchCondition, having, this);
        }

        public void ExpressionAssignValues(SqlFragment parent, ExpressionAssignValues expressionAssignValues) {
            IEnumerable<Column> columns = expressionAssignValues.ValueAssignList.Select(v => v.Column);
            IEnumerable<Expression> expressions = expressionAssignValues.ValueAssignList.Select(v => v.Expression);

            this.Script.OpenParen();
            ExpressionFormatter.FormatColumnList(columns, expressionAssignValues, this.Script, this);
            this.Script.CloseParen();
            this.Script.AppendLine("VALUES (");
            ExpressionFormatter.FormatExpressionList(expressions, expressionAssignValues, this.Script, this);
            this.Script.CloseParen();
        }

        public void Select<T>(SqlFragment parent, Select<T> element) where T : IStatement {
            this.Script.LineAppendLine(TSqlKeyword.SELECT);
            ExpressionFormatter.FormatExpressionList(element.ExpressionList, parent, this.Script, this);
        }

        public void GroupByClause<T>(SqlFragment parent, GroupByClause<T> groupByClause) where T : IStatement {
            this.Script.LineAppendLine(TSqlKeyword.GROUP_BY);
            ExpressionFormatter.FormatExpressionList(groupByClause.ExpressionList, groupByClause, this.Script, this);
        }

        public void SelectDistinct<T>(SqlFragment parent, SelectDistinct<T> selectDistinct) where T : IStatement {
            this.Script.LineAppendLine(TSqlKeyword.SELECT_DISTINCT);
            ExpressionFormatter.FormatExpressionList(selectDistinct.ColumnList, parent, this.Script, this);
        }

        public void InsertAdvancedClause(SqlFragment parent, InsertAdvancedClause insertAdvancedClause) {
            this.Script.OpenParen();
            Column[] cols = insertAdvancedClause.Args.Select(v => v.Column).ToArray();
            ExpressionFormatter.FormatColumnList(cols, insertAdvancedClause, this.Script, this);
            this.Script.CloseParen();

            this.Script.Append(TSqlKeyword.VALUES).OpenParen();
            Expression[] values = insertAdvancedClause.Args.Select(v => v.Expression).ToArray();
            ExpressionFormatter.FormatExpressionList(values, insertAdvancedClause, this.Script, this);
            this.Script.CloseParen();
        }

        public void TableValuedFunction(SqlFragment parent, TableValuedFunction tableValuedFunction) {}

        public void AvgAggregateFunction(SqlFragment parent, AvgAggregateFunction function) {
            this.Script.Append("AVG(");
            this.Script.AppendFragment(function.Expression, function, this);
            this.Script.Append(")");
        }

        public void BinaryChecksumAggregateFunction(SqlFragment parent, BinaryChecksumFunction function) {
            this.Script.Append("BINARY_CHECKSUM(");
            ExpressionFormatter.FormatExpressionList(function.Arguments, function, this.Script, this);
            this.Script.Append(")");
        }

        public void Checksum_AggAllAggregateFunction(SqlFragment parent, Checksum_AggAggregateFunction function) {
            
        }
    }
}