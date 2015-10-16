// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Predicate.cs" company="CODE Insiders LTD">
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

    using CodeInsiders.SharpQL.Predicates;

    public abstract class Predicate : SqlFragment
    {
        // ReSharper disable once InconsistentNaming
        public static Predicate FALSE {
            get {
                return new FalsePredicate();
            }
        }

        // ReSharper disable once InconsistentNaming
        public static Predicate TRUE {
            get {
                return new TruePredicate();
            }
        }

        public static Predicate operator &(Predicate leftOperand, Predicate rightOperand) {
            bool leftIsTrue = leftOperand is TruePredicate;
            bool rightIsTrue = rightOperand is TruePredicate;

            if (leftIsTrue && rightIsTrue) {
                return new TruePredicate();
            }

            if (leftIsTrue || rightIsTrue) {
                return leftIsTrue ? rightOperand : leftOperand;
            }

            return new AndConditionalOperatorAndPredicate(leftOperand, rightOperand);
        }

        public static Predicate operator |(Predicate leftOperand, Predicate rightOperand) {
            bool leftIsTrue = leftOperand is TruePredicate;
            bool rightIsTrue = rightOperand is TruePredicate;

            if (leftIsTrue && rightIsTrue) {
                return new TruePredicate();
            }

            return new OrConditionalOperator(leftOperand, rightOperand);
        }

        public static Predicate NotExists(SelectStatement subQuery) {
            if (subQuery == null) {
                throw new ArgumentNullException("subQuery");
            }

            return new NotExistsPredicate(subQuery);
        }

        public static Predicate Exists(SelectStatement subQuery) {
            if (subQuery == null) {
                throw new ArgumentNullException("subQuery");
            }

            return new ExistsPredicate(subQuery);
        }
    }
}