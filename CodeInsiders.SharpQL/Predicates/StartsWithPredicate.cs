// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StartsWithPredicate.cs" company="CODE Insiders LTD">
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

    public class StartsWithPredicate : BinaryPredicateOperator
    {
        public StartsWithPredicate(Expression exprLeft, Expression exprRight)
            : base(exprLeft, exprRight) {
            if (exprLeft == null) {
                throw new ArgumentNullException("exprLeft");
            }
            if (exprRight == null) {
                throw new ArgumentNullException("exprRight");
            }
        }

        public override void Build(SqlFragment parent, TSqlVisitor visitor) {
            visitor.StartsWithPredicate(parent, this);
        }
    }
}