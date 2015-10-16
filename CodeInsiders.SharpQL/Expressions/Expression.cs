// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Expression.cs" company="CODE Insiders LTD">
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
    using System.Xml.Linq;

    using CodeInsiders.SharpQL.Operators;

    public abstract class Expression : SqlFragment
    {
        #region arithmetic operators

        public static Expression operator +(Expression left, Expression right) {
            if (left == null) {
                throw new ArgumentNullException("left");
            }
            if (right == null) {
                throw new ArgumentNullException("right");
            }
            return new PlusArithmeticExpressionOperator(left, right);
        }

        public static Expression operator /(Expression divident, Expression devisor) {
            if (divident == null) {
                throw new ArgumentNullException("divident");
            }
            if (devisor == null) {
                throw new ArgumentNullException("devisor");
            }
            return new DivideArithmeticExpressionOperator(divident, devisor);
        }

        public static Expression operator %(Expression divident, Expression devisor) {
            if (divident == null) {
                throw new ArgumentNullException("divident");
            }
            if (devisor == null) {
                throw new ArgumentNullException("devisor");
            }
            return new ModuloArithmeticExpressionOperator(divident, devisor);
        }

        public static Expression operator *(Expression left, Expression right) {
            if (left == null) {
                throw new ArgumentNullException("left");
            }
            if (right == null) {
                throw new ArgumentNullException("right");
            }
            return new MultiplyArithmeticExpressionOperator(left, right);
        }

        public static Expression operator -(Expression left, Expression right) {
            if (left == null) {
                throw new ArgumentNullException("left");
            }
            if (right == null) {
                throw new ArgumentNullException("right");
            }
            return new MinusArithmeticExpressionOperator(left, right);
        }

        #endregion

        #region implicit conversion operators

        public static implicit operator Expression(byte value) {
            return new ConstantExpression(value);
        }

        public static implicit operator Expression(sbyte value) {
            return new ConstantExpression(value);
        }

        public static implicit operator Expression(short value) {
            return new ConstantExpression(value);
        }

        public static implicit operator Expression(ushort value) {
            return new ConstantExpression(value);
        }

        public static implicit operator Expression(int value) {
            return new ConstantExpression(value);
        }

        public static implicit operator Expression(uint value) {
            return new ConstantExpression(value);
        }

        public static implicit operator Expression(long value) {
            return new ConstantExpression(value);
        }

        public static implicit operator Expression(ulong value) {
            return new ConstantExpression(value);
        }

        public static implicit operator Expression(float value) {
            return new ConstantExpression(value);
        }

        public static implicit operator Expression(double value) {
            return new ConstantExpression(value);
        }

        public static implicit operator Expression(decimal value) {
            return new ConstantExpression(value);
        }

        public static implicit operator Expression(bool value) {
            return new ConstantExpression(value);
        }

        public static implicit operator Expression(string value) {
            return new ConstantExpression(value);
        }

        public static implicit operator Expression(char value) {
            return new ConstantExpression(value);
        }

        public static implicit operator Expression(Guid value) {
            return new ConstantExpression(value);
        }

        public static implicit operator Expression(DateTime value) {
            return new ConstantExpression(value);
        }

        public static implicit operator Expression(DateTimeOffset value) {
            return new ConstantExpression(value);
        }

        public static implicit operator Expression(byte[] value) {
            return new ConstantExpression(value);
        }

        public static implicit operator Expression(XNode xml) {
            return new ConstantExpression(xml);
        }

        //TODO SingleExprSelectStatement or SingleExprSingleRowSelectStatement?
        public static implicit operator Expression(SingleExprSelectStatement selectStatement) {
            return new ScalarSelectStatementExpression(selectStatement);
        }



        #endregion

        // ReSharper disable once InconsistentNaming
        public static Expression NULL
        {
            get
            {
                return new NullConstantExpression();
            }
        }

        public static Expression GetConstant(object value) {
            if (value == null) {
                throw new ArgumentNullException("value");
            }

            Expression expr;
            if (TryGetConstant(value, out expr)) {
                return expr;
            }

            var message = String.Format(
                "Cannot convert value of type {0} to {1}",
                value.GetType(),
                typeof(ConstantExpression));
            throw new NotSupportedException(message);
        }

        public static bool TryGetConstant(object value, out Expression expression) {
            if (value == null) {
                throw new ArgumentNullException("value");
            }

            if (value is int) {
                expression = (int)value;
                return true;
            }
            if (value is uint) {
                expression = (uint)value;
                return true;
            }
            if (value is string) {
                expression = (string)value;
                return true;
            }
            if (value is DateTime) {
                expression = (DateTime)value;
                return true;
            }
            if (value is Guid) {
                expression = (Guid)value;
                return true;
            }
            if (value is long) {
                expression = (long)value;
                return true;
            }
            if (value is ulong) {
                expression = (ulong)value;
                return true;
            }
            if (value is short) {
                expression = (short)value;
                return true;
            }
            if (value is ushort) {
                expression = (ushort)value;
                return true;
            }
            if (value is byte) {
                expression = (byte)value;
                return true;
            }
            if (value is sbyte) {
                expression = (sbyte)value;
                return true;
            }
            if (value is float) {
                expression = (float)value;
                return true;
            }
            if (value is double) {
                expression = (double)value;
                return true;
            }
            if (value is decimal) {
                expression = (decimal)value;
                return true;
            }
            if (value is bool) {
                expression = (bool)value;
                return true;
            }
            if (value is char) {
                expression = (char)value;
                return true;
            }
            if (value is DateTimeOffset) {
                expression = (DateTimeOffset)value;
                return true;
            }
            if (value is byte[]) {
                expression = (byte[])value;
                return true;
            }

            if (value is XNode) {
                expression = (XNode)value;
                return true;
            }

            expression = null;
            return false;
        }
    }
}