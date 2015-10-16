// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ExpressionExtensions.cs" company="CODE Insiders LTD">
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

    using CodeInsiders.SharpQL.Helpers.Internal;
    using CodeInsiders.SharpQL.Predicates;

    public static class ExpressionExtensions
    {
        public static Expression As(this Expression expression, string alias) {
            if (expression == null) {
                throw new ArgumentNullException("expression");
            }
            if (StringHelper.IsNullOrWhiteSpace(alias)) {
                throw new ArgumentNullException("alias");
            }
            return new AsExpression(expression, alias);
        }

        public static Predicate Contains(this Expression expression, Expression value) {
            if (expression == null) {
                throw new ArgumentNullException("expression");
            }
            if (value == null) {
                throw new ArgumentNullException("value");
            }
            return new ContainsPredicate(expression, value);
        }

        public static Predicate EndsWith(this Expression expression, Expression value) {
            if (expression == null) {
                throw new ArgumentNullException("expression");
            }
            if (value == null) {
                throw new ArgumentNullException("value");
            }
            return new EndsWithPredicate(expression, value);
        }

        public static Predicate IsBetween(this Expression expression, Expression minValue, Expression maxValue) {
            if (expression == null) {
                throw new ArgumentNullException("expression");
            }
            if (minValue == null) {
                throw new ArgumentNullException("minValue");
            }
            if (maxValue == null) {
                throw new ArgumentNullException("maxValue");
            }
            return new BetweenPredicate(expression, minValue, maxValue);
        }

        public static Predicate IsEqualTo(this Expression expression, Expression value) {
            if (value == null) {
                throw new ArgumentNullException("value");
            }
            return new EqualToPredicate(expression, value);
        }

        public static Predicate IsGreaterThan(this Expression expression, Expression value) {
            if (value == null) {
                throw new ArgumentNullException("value");
            }
            return new IsGreaterThanPredicate(expression, value);
        }

        public static Predicate IsGreaterThanOrEqualTo(this Expression expression, Expression value) {
            if (expression == null) {
                throw new ArgumentNullException("expression");
            }
            if (value == null) {
                throw new ArgumentNullException("value");
            }
            return new IsGreaterThanOrEqualToPredicate(expression, value);
        }

        public static Predicate IsIn(this Expression expression, params Expression[] values) {
            return new InValuesPredicate(expression, values);
        }

        public static Predicate IsIn(
            this Expression expression,
            SingleExprSelectStatement singleExpressionSelectStatement) {
            return new InSingleExprSelectStatementPredicate(expression, singleExpressionSelectStatement);
        }

        public static Predicate IsLessThan(this Expression expression, Expression value) {
            if (expression == null) {
                throw new ArgumentNullException("expression");
            }
            if (value == null) {
                throw new ArgumentNullException("value");
            }
            return new IsLessThanPredicate(expression, value);
        }

        public static Predicate IsLessThanOrEqualTo(this Expression expression, Expression value) {
            if (expression == null) {
                throw new ArgumentNullException("expression");
            }
            if (value == null) {
                throw new ArgumentNullException("value");
            }
            return new IsLessThanOrEqualToPredicate(expression, value);
        }

        public static Predicate IsLike(this Expression expression, Expression value) {
            if (expression == null) {
                throw new ArgumentNullException("expression");
            }
            if (value == null) {
                throw new ArgumentNullException("value");
            }
            return new IsLikePredicate(expression, value);
        }

        public static Predicate IsNotEqualTo(this Expression expression, Expression value) {
            if (expression == null) {
                throw new ArgumentNullException("expression");
            }
            if (value == null) {
                throw new ArgumentNullException("value");
            }
            return new IsNotEqualToPredicate(expression, value);
        }

        public static Predicate IsNotIn(this Expression expression, params Expression[] values) {
            return new NotInValuesPredicate(expression, values);
        }

        public static Predicate IsNotIn(this Expression expression, SingleExprSelectStatement selectStatement) {
            if (expression == null) {
                throw new ArgumentNullException("expression");
            }
            if (selectStatement == null) {
                throw new ArgumentNullException("selectStatement");
            }
            return new NotInSingleExprSelectStatementPredicate(expression, selectStatement);
        }

        public static Predicate IsNotNull(this Expression expression) {
            if (expression == null) {
                throw new ArgumentNullException("expression");
            }
            return new IsNotNullPredicate(expression);
        }

        public static Predicate IsNull(this Expression expression) {
            if (expression == null) {
                throw new ArgumentNullException("expression");
            }
            return new IsNullPredicate(expression);
        }

        //        public static Expression Minus(this Expression expression, Expression value) {
        //            if (expression == null) {
        //                throw new ArgumentNullException("expression");
        //            }
        //            if (value == null) {
        //                throw new ArgumentNullException("value");
        //            }
        //            return new MinusArithmeticExpressionOperator(expression, value);
        //        }
        //
        //        public static Expression Plus(this Expression epxression, Expression value) {
        //            if (epxression == null) {
        //                throw new ArgumentNullException("epxression");
        //            }
        //            if (value == null) {
        //                throw new ArgumentNullException("value");
        //            }
        //            return new PlusArithmeticExpressionOperator(epxression, value);
        //        }

        public static Predicate StartsWith(this Expression expression, Expression value) {
            if (expression == null) {
                throw new ArgumentNullException("expression");
            }
            if (value == null) {
                throw new ArgumentNullException("value");
            }
            return new StartsWithPredicate(expression, value);
        }
    }
}