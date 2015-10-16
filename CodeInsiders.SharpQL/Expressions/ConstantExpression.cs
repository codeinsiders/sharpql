// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ConstantExpression.cs" company="CODE Insiders LTD">
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
    using System.Data;
    using System.Data.SqlTypes;
    using System.Xml.Linq;

    public class ConstantExpression : Expression
    {
        public ConstantExpression(byte value)
            : this(value, DbType.Byte) {}

        public ConstantExpression(sbyte value)
            : this(value, DbType.SByte) {}

        public ConstantExpression(short value)
            : this(value, DbType.Int16) {}

        public ConstantExpression(ushort value)
            : this(value, DbType.UInt16) {}

        public ConstantExpression(int value)
            : this(value, DbType.Int32) {}

        public ConstantExpression(uint value)
            : this(value, DbType.UInt32) {}

        public ConstantExpression(long value)
            : this(value, DbType.Int64) {}

        public ConstantExpression(ulong value)
            : this(value, DbType.UInt64) {}

        public ConstantExpression(float value)
            : this(value, DbType.Single) {}

        public ConstantExpression(double value)
            : this(value, DbType.Double) {}

        public ConstantExpression(decimal value)
            : this(value, DbType.Decimal) {}

        public ConstantExpression(bool value)
            : this(value, DbType.Boolean) {}

        public ConstantExpression(string value)
            : this(value, DbType.StringFixedLength) {
            if (value == null) {
                throw new ArgumentNullException(
                    "value",
                    "The value passed to a constant string expression must not be null.");
            }
            this.Size = value.Length < 4000 ? value.Length : -1;
        }

        public ConstantExpression(char value)
            : this(value, DbType.StringFixedLength) {
            this.Size = sizeof(char);
        }

        public ConstantExpression(Guid value)
            : this(value, DbType.Guid) {}

        public ConstantExpression(DateTime value)
            : this(value, DbType.DateTime) {}

        public ConstantExpression(DateTimeOffset value)
            : this(value, DbType.DateTimeOffset) {}

        public ConstantExpression(byte[] value)
            : this(value, DbType.Binary) {
            if (value == null) {
                throw new ArgumentNullException(
                    "value",
                    "The value passed to a constant byte array expression must not be null.");
            }

            this.Value = value;
            this.DbType = DbType.Binary;
            this.Size = value.Length;
        }

        public ConstantExpression(XNode value)
            : this(value, DbType.AnsiStringFixedLength) {
            if (value == null) {
                throw new ArgumentNullException(
                    "value",
                    "The value passed to a constant XNode expression must not be null");
            }

            // TODO reader is IDisposable remove that from here
            this.Value = new SqlXml(value.CreateReader());
            this.DbType = DbType.Xml;
        }

        protected ConstantExpression(object value, DbType dbType) {
            if (value == null) {
                throw new ArgumentNullException("value");
            }

            this.Value = value;
            this.DbType = dbType;
        }

        public DbType DbType { get; protected set; }
        public int? Size { get; protected set; }
        public object Value { get; protected set; }

   
        public override void Build(SqlFragment parent, TSqlVisitor visitor) {
            visitor.ConstantExpression(parent, this);
        }

        public override string ToString() {
            return this.Value.ToString();
        }

       

        #region implicit operators

        //        public static implicit operator ConstantExpression(byte value) {
        //            return new ConstantExpression(value);
        //        }
        //
        //        public static implicit operator ConstantExpression(sbyte value) {
        //            return new ConstantExpression(value);
        //        }
        //
        //        public static implicit operator ConstantExpression(short value) {
        //            return new ConstantExpression(value);
        //        }
        //
        //        public static implicit operator ConstantExpression(ushort value) {
        //            return new ConstantExpression(value);
        //        }
        //
        //        public static implicit operator ConstantExpression(int value) {
        //            return new ConstantExpression(value);
        //        }
        //
        //        public static implicit operator ConstantExpression(uint value) {
        //            return new ConstantExpression(value);
        //        }
        //
        //        public static implicit operator ConstantExpression(long value) {
        //            return new ConstantExpression(value);
        //        }
        //
        //        public static implicit operator ConstantExpression(ulong value) {
        //            return new ConstantExpression(value);
        //        }
        //
        //        public static implicit operator ConstantExpression(float value) {
        //            return new ConstantExpression(value);
        //        }
        //
        //        public static implicit operator ConstantExpression(double value) {
        //            return new ConstantExpression(value);
        //        }
        //
        //        public static implicit operator ConstantExpression(decimal value) {
        //            return new ConstantExpression(value);
        //        }
        //
        //        public static implicit operator ConstantExpression(bool value) {
        //            return new ConstantExpression(value);
        //        }
        //
        //        public static implicit operator ConstantExpression(string value) {
        //            return new ConstantExpression(value);
        //        }
        //
        //        public static implicit operator ConstantExpression(char value) {
        //            return new ConstantExpression(value);
        //        }
        //
        //        public static implicit operator ConstantExpression(Guid value) {
        //            return new ConstantExpression(value);
        //        }
        //
        //        public static implicit operator ConstantExpression(DateTime value) {
        //            return new ConstantExpression(value);
        //        }
        //
        //        public static implicit operator ConstantExpression(DateTimeOffset value) {
        //            return new ConstantExpression(value);
        //        }
        //
        //        public static implicit operator ConstantExpression(byte[] value) {
        //            return new ConstantExpression(value);
        //        }
        //
        //        public static implicit operator ConstantExpression(XNode xml) {
        //            return new ConstantExpression(xml);
        //        }

        #endregion
    }
}