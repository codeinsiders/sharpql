// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Column.cs" company="CODE Insiders LTD">
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

    using CodeInsiders.SharpQL.Assignments;
    using CodeInsiders.SharpQL.Helpers.Internal;

    public class Column : Expression, IEquatable<Column>
    {
        public string ColumnName { get; private set; }

        public readonly Table Table;

        internal Column(Table table, string name) {
            if (table == null) {
                throw new ArgumentNullException("table");
            }

            if (StringHelper.IsNullOrWhiteSpace(name)) {
                throw new ArgumentNullException("name");
            }

            this.ColumnName = name;
            this.Table = table;
        }

        public override void Build(SqlFragment parent, TSqlVisitor visitor) {
            visitor.VisitColumn(parent, this);
        }

        public Assignment Assign(Expression expression) {
            if (expression == null) {
                throw new ArgumentNullException("expression");
            }

            return new ExpressionAssign(this, expression);
        }

        public Assignment AddAssign(Expression expression) {
            if (expression == null) {
                throw new ArgumentNullException("expression");
            }

            return new AddAssign(this, expression);
        }

        public Assignment SubAssign(Expression expression) {
            if (expression == null) {
                throw new ArgumentNullException("expression");
            }

            return new SubstractAssign(this, expression);
        }

        public Assignment MulAssign(Expression expression) {
            if (expression == null) {
                throw new ArgumentNullException("expression");
            }

            return new MultiplyAssign(this, expression);
        }

        public Assignment DivAssign(Expression expression) {
            if (expression == null) {
                throw new ArgumentNullException("expression");
            }

            return new DivideAssign(this, expression);
        }

        public Assignment ModAssign(Expression expression) {
            if (expression == null) {
                throw new ArgumentNullException("expression");
            }

            return new ModuloAssign(this, expression);
        }

        public Assignment BitwAndAssign(Expression expression) {
            if (expression == null) {
                throw new ArgumentNullException("expression");
            }

            return new BitwiseAndAssign(this, expression);
        }

        public Assignment BitwXorAssign(Expression expression) {
            if (expression == null) {
                throw new ArgumentNullException("expression");
            }

            return new BitwiseXorAssign(this, expression);
        }

        public Assignment BitwOrAssign(Expression expression) {
            if (expression == null) {
                throw new ArgumentNullException("expression");
            }

            return new BitwiseOrAssign(this, expression);
        }

        public Assignment Assign(SingleExprSelectStatement selectStatement) {
            if (selectStatement == null) {
                string message =
                    string.Format(
                        "Cannot assign null select statement expression on table [{0}], column [{1}]",
                        this.Table.TableName,
                        this.ColumnName);
                throw new ArgumentNullException("selectStatement", message);
            }

            return new ExpressionAssign(this, new ScalarSelectStatementExpression(selectStatement));
        }

        public Assignment Assign(
            string stringValueExpression,
            StringAssignOption option = StringAssignOption.ThrowOnNullString) {
            switch (option) {
                case StringAssignOption.ThrowOnNullString:
                    if (stringValueExpression == null) {
                        string message =
                            string.Format(
                                "Cannot assign null string expression on table [{0}], column [{1}]",
                                this.Table.TableName,
                                this.ColumnName);
                        throw new ArgumentNullException("stringValueExpression", message);
                    }
                    break;
                case StringAssignOption.NullIfNullOrWhitespace:
                    if (string.IsNullOrWhiteSpace(stringValueExpression)) {
                        return new ExpressionAssign(this, ConstantExpression.NULL);
                    }
                    break;
                case StringAssignOption.DefaultIfNullOrWhitespace:
                    if (string.IsNullOrWhiteSpace(stringValueExpression)) {
                        return new ExpressionAssign(this, Assignment.DEFAULT);
                    }
                    break;
                default:
                    throw new ArgumentOutOfRangeException("option");
            }

            return new ExpressionAssign(this, new ConstantExpression(stringValueExpression));
        }

        public Assignment Assign(byte[] byteArrayValueExpression) {
            if (byteArrayValueExpression == null) {
                string message = string.Format(
                    "Cannot assign null byte array expression on table [{0}], column [{1}]",
                    this.Table.TableName,
                    this.ColumnName);

                throw new ArgumentNullException("byteArrayValueExpression", message);
            }

            return new ExpressionAssign(this, new ConstantExpression(byteArrayValueExpression));
        }

        public override bool Equals(object obj) {
            if (ReferenceEquals(null, obj)) {
                return false;
            }
            if (ReferenceEquals(this, obj)) {
                return true;
            }
            if (obj.GetType() != this.GetType()) {
                return false;
            }
            return this.Equals((Column)obj);
        }

        public bool Equals(Column other) {
            if (ReferenceEquals(null, other)) {
                return false;
            }
            if (ReferenceEquals(this, other)) {
                return true;
            }
            return this.Table.Equals(other.Table) && this.ColumnName.Equals(other.ColumnName);
        }

        public override int GetHashCode() {
            unchecked {
                return (this.Table.GetHashCode() * 397) ^ this.ColumnName.GetHashCode();
            }
        }

        public static bool operator ==(Column left, Column right) {
            return Equals(left, right);
        }

        public static bool operator !=(Column left, Column right) {
            return !Equals(left, right);
        }

        public override string ToString() {
            return string.Format("{0}.[{1}]", this.Table, this.ColumnName);
        }
    }
}