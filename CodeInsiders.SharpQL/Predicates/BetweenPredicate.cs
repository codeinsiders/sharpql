// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BetweenPredicate.cs" company="CODE Insiders LTD">
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
namespace CodeInsiders.SharpQL.Predicates
{
    using System;

    using CodeInsiders.SharpQL.Helpers.Internal;

    public class BetweenPredicate : Predicate
    {
        public Expression Maximum { get; private set; }
        public Expression Minimum { get; private set; }
        public Expression Expression { get; private set; }

        public BetweenPredicate(Expression expression, Expression exprMin, Expression expMax) {
            if (expression == null) {
                throw new ArgumentNullException("expression");
            }
            if (exprMin == null) {
                throw new ArgumentNullException("exprMin");
            }
            if (expMax == null) {
                throw new ArgumentNullException("expMax");
            }

            this.Expression = expression;
            this.Minimum = exprMin;
            this.Maximum = expMax;
        }

        public override void Build(SqlFragment parent, TSqlVisitor visitor) {
            visitor.BetweenPredicate(parent, this);
      
        }
    }
}