// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ExpressionArrayExtensions.cs" company="CODE Insiders LTD">
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
    using System.Linq;

    public static class ExpressionArrayExtensions
    {
        public static Expression[] ToExpressionArray(this IEnumerable<int> values) {
            if (values == null) {
                throw new ArgumentNullException("values");
            }
            return values.Select(v => new ConstantExpression(v)).Cast<Expression>().ToArray();
        }

        public static Expression[] ToExpressionArray(this IEnumerable<long> values) {
            if (values == null) {
                throw new ArgumentNullException("values");
            }
            return values.Select(v => new ConstantExpression(v)).Cast<Expression>().ToArray();
        }

        public static Expression[] ToExpressionArray(this IEnumerable<float> values) {
            if (values == null) {
                throw new ArgumentNullException("values");
            }
            return values.Select(v => new ConstantExpression(v)).Cast<Expression>().ToArray();
        }

        public static Expression[] ToExpressionArray(this IEnumerable<double> values) {
            if (values == null) {
                throw new ArgumentNullException("values");
            }
            return values.Select(v => new ConstantExpression(v)).Cast<Expression>().ToArray();
        }

        public static Expression[] ToExpressionArray(this IEnumerable<decimal> values) {
            if (values == null) {
                throw new ArgumentNullException("values");
            }
            return values.Select(v => new ConstantExpression(v)).Cast<Expression>().ToArray();
        }

        public static Expression[] ToExpressionArray(this IEnumerable<bool> values) {
            if (values == null) {
                throw new ArgumentNullException("values");
            }
            return values.Select(v => new ConstantExpression(v)).Cast<Expression>().ToArray();
        }

        public static Expression[] ToExpressionArray(this IEnumerable<char> values) {
            if (values == null) {
                throw new ArgumentNullException("values");
            }
            return values.Select(v => new ConstantExpression(v)).Cast<Expression>().ToArray();
        }

        public static Expression[] ToExpressionArray(this IEnumerable<string> values) {
            if (values == null) {
                throw new ArgumentNullException("values");
            }
            return values.Select(v => new ConstantExpression(v)).Cast<Expression>().ToArray();
        }

        public static Expression[] ToExpressionArray(this IEnumerable<DateTime> values) {
            if (values == null) {
                throw new ArgumentNullException("values");
            }
            return values.Select(v => new ConstantExpression(v)).Cast<Expression>().ToArray();
        }

        public static Expression[] ToExpressionArray(this IEnumerable<short> values) {
            if (values == null) {
                throw new ArgumentNullException("values");
            }
            return values.Select(v => new ConstantExpression(v)).Cast<Expression>().ToArray();
        }

        public static Expression[] ToExpressionArray(this IEnumerable<byte> values) {
            if (values == null) {
                throw new ArgumentNullException("values");
            }
            return values.Select(v => new ConstantExpression(v)).Cast<Expression>().ToArray();
        }

        public static Expression[] ToExpressionArray(this IEnumerable<Guid> values) {
            if (values == null) {
                throw new ArgumentNullException("values");
            }
            return values.Select(v => new ConstantExpression(v)).Cast<Expression>().ToArray();
        }

        public static Expression[] ToExpressionArray(this IEnumerable<byte[]> values) {
            if (values == null) {
                throw new ArgumentNullException("values");
            }
            return values.Select(v => new ConstantExpression(v)).Cast<Expression>().ToArray();
        }
    }
}