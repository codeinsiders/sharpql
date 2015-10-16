// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EndsWithPredicate.cs" company="CODE Insiders LTD">
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

    // TODO do we support EndsWith?
    public class EndsWithPredicate : BinaryPredicateOperator
    {
        public EndsWithPredicate(Expression leftOperand, Expression rightOperand)
            : base(leftOperand, rightOperand) {
            if (leftOperand == null) {
                throw new ArgumentNullException("leftOperand");
            }
            if (rightOperand == null) {
                throw new ArgumentNullException("rightOperand");
            }
        }

        public override void Build(SqlFragment parent, TSqlVisitor visitor) {
            visitor.EndsWithPredicate(parent, this);
            
        }
    }
}