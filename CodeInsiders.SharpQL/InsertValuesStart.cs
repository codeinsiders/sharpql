// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FirstValues.cs" company="CODE Insiders LTD">
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

    public class InsertValuesStart : ValuesClause
    {
        public IEnumerable<Expression> ExpressionList { get; private set; }

        public InsertValuesStart(InsertStatement statement, IEnumerable<Expression> expressionList)
            : base(statement) {
            if (expressionList == null) {
                throw new ArgumentNullException("expressionList");
            }
            this.ExpressionList = expressionList;
        }

        public override void Build(SqlFragment parent, TSqlVisitor visitor) {
            visitor.InsertValuesStart(parent, this);
        }

        public NextValues Values(params Expression[] expressions) {
            return this.NextClause(new NextValues(this.Statement, expressions));
        }
    }
}