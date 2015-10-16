// --------------------------------------------------------------------------------------------------------------------
// <copyright file="InsertClause.cs" company="CODE Insiders LTD">
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

    public abstract class InsertClause : Clause<InsertStatement>
    {
        protected InsertClause(InsertStatement statement)
            : base(statement) {}

        public Select<InsertStatement> Select(params Expression[] expressions) {
            if (expressions == null) {
                throw new ArgumentNullException("expressions");
            }

            if (expressions.Length == 0) {
                throw new ArgumentOutOfRangeException(
                    "expressions",
                    "At least on expression must be supplied in the select list");
            }

            return this.NextClause(new Select<InsertStatement>(this.Statement, expressions));
        }

        public Select<InsertStatement> Select(Expression expression) {
            return this.NextClause(new Select<InsertStatement>(this.Statement, new[] { expression }));
        }

        public InsertValuesStart Values(params Expression[] expressionList) {
            return this.NextClause(new InsertValuesStart(this.Statement, expressionList));
        }

        public ExpressionAssignValues Values(params Assignment[] expressionAssignList) {
            return this.NextClause(new ExpressionAssignValues(this.Statement, expressionAssignList));
        }
    }
}