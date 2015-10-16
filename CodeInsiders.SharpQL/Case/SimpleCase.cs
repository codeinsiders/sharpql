// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SimpleCase.cs" company="CODE Insiders LTD">
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

    public class SimpleCase : Expression
    {
        private readonly List<SimpleCaseCondition> conditions = new List<SimpleCaseCondition>();

        public Expression Expression { get; private set; }
        public Expression ElseResultExpression { get; private set; }

        public SimpleCase(Expression expression) {
            if (expression == null) {
                throw new ArgumentNullException("expression");
            }

            this.Expression = expression;
        }

        public IReadOnlyCollection<SimpleCaseCondition> Conditions
        {
            get
            {
                return this.conditions;
            }
        }

        public SimpleCase When(Expression whenExpression, Expression resultExpression) {
            if (whenExpression == null) {
                throw new ArgumentNullException("whenExpression");
            }

            if (resultExpression == null) {
                throw new ArgumentNullException("resultExpression");
            }

            this.conditions.Add(new SimpleCaseCondition(whenExpression, resultExpression));
            return this;
        }

        public SimpleCase Else(Expression resultExpression) {
            if (resultExpression == null) {
                throw new ArgumentNullException("resultExpression");
            }

            this.ElseResultExpression = resultExpression;

            return this;
        }

        public override void Build(SqlFragment parent, TSqlVisitor visitor) {
            visitor.SimpleCase(parent, this);
        }
    }
}