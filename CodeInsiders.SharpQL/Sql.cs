// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Sql.cs" company="CODE Insiders LTD">
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

    using CodeInsiders.SharpQL.DataType;
    using CodeInsiders.SharpQL.ScalarFunctions;

    public static class Sql
    {
        public static Function Cast(Expression exp, SqlDataType type) {
            if (exp == null) {
                throw new ArgumentNullException("exp");
            }
            if (type == null) {
                throw new ArgumentNullException("type");
            }

            return new CastFunction(exp, type);
        }

        public static Function Avg(Expression exp) {
            if (exp == null) {
                throw new ArgumentNullException("exp");
            }

            return new SimpleFunction("AVG", exp);
        }

        public static Function Convert(SqlDataType type, Expression exp, uint? style = null) {
            if (type == null) {
                throw new ArgumentNullException("type");
            }

            if (exp == null) {
                throw new ArgumentNullException("exp");
            }

            return new ConvertFunction(type, exp, style);
        }

        public static Function Count(Expression exp)
        {
            if (exp == null) {
                throw new ArgumentNullException("exp");
            }

            return new SimpleFunction("COUNT", exp);
        }

        public static Function DateAdd(DatePartType datePart, Expression number, Expression date) {
            if (number == null) {
                throw new ArgumentNullException("number");
            }

            if (date == null) {
                throw new ArgumentNullException("date");
            }

            return new DateAddFunction(datePart, number, date);
        }
        

        public static Function GetDate() {
            return new GetDateFunction();
        }

        public static Function IsNull(Expression arg1, Expression arg2) {
            if (arg1 == null) {
                throw new ArgumentNullException("arg1");
            }

            if (arg2 == null) {
                throw new ArgumentNullException("arg2");
            }

            return new SimpleFunction("ISNULL", arg1, arg2);
        }

        public static Function Left(Expression exp, uint lenght) {
            if (exp == null) {
                throw new ArgumentNullException("exp");
            }

            return new SimpleFunction("LEFT", exp, lenght);
        }

        public static Function Max(Expression exp) {
            if (exp == null) {
                throw new ArgumentNullException("exp");
            }

            return new SimpleFunction("MAX", exp);
        }

        public static Function Min(Expression exp) {
            if (exp == null) {
                throw new ArgumentNullException("exp");
            }

            return new SimpleFunction("MIN", exp);
        }

        public static Function Right(Expression exp, uint lenght) {
            if (exp == null) {
                throw new ArgumentNullException("exp");
            }

            return new SimpleFunction("RIGHT", exp, lenght);
        }

        public static Expression RowNumberOverOrderBy(Column column) {
            return new RowNumberOverOrderByExpression(column);
        }

        public static Function Scope_Identity() {
            return new SimpleFunction("SCOPE_IDENTITY");
        }

        public static Function Substring(Expression exp, Expression start, Expression lenght) {
            if (exp == null) {
                throw new ArgumentNullException("exp");
            }

            if (start == null) {
                throw new ArgumentNullException("start");
            }

            if (lenght == null) {
                throw new ArgumentNullException("lenght");
            }

            return new SimpleFunction("SUBSTRING", exp, start, lenght);
        }

        public static Function Sum(Expression exp) {
            if (exp == null) {
                throw new ArgumentNullException("exp");
            }

            return new SimpleFunction("SUM", exp);
        }
    }
}