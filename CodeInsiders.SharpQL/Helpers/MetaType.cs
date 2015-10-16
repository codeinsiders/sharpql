// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MetaType.cs" company="CODE Insiders LTD">
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
    using System.Data;
    using System.Data.Common;
    using System.Data.OleDb;
    using System.Data.SqlTypes;
    using System.IO;
    using System.Xml;

    using CodeInsiders.SharpQL.Helpers;
    using CodeInsiders.SharpQL.Helpers.Internal;

    using Microsoft.SqlServer.Server;

    internal sealed class MetaType
    {
        internal static readonly MetaType MetaDateTimeOffset = new MetaType(
            byte.MaxValue,
            7,
            -1,
            false,
            false,
            false,
            43,
            43,
            "datetimeoffset",
            typeof(DateTimeOffset),
            typeof(DateTimeOffset),
            SqlDbType.DateTimeOffset,
            DbType.DateTimeOffset,
            1);

        /// <summary>
        /// The meta decimal.
        /// </summary>
        internal static readonly MetaType MetaDecimal = new MetaType(
            38,
            4,
            17,
            true,
            false,
            false,
            108,
            108,
            "decimal",
            typeof(Decimal),
            typeof(SqlDecimal),
            SqlDbType.Decimal,
            DbType.Decimal,
            2);

        /// <summary>
        /// The meta image.
        /// </summary>
        internal static readonly MetaType MetaImage = new MetaType(
            byte.MaxValue,
            byte.MaxValue,
            -1,
            false,
            true,
            false,
            34,
            34,
            "image",
            typeof(byte[]),
            typeof(SqlBinary),
            SqlDbType.Image,
            DbType.Binary,
            0);

        /// <summary>
        /// The meta max n var char.
        /// </summary>
        internal static readonly MetaType MetaMaxNVarChar = new MetaType(
            byte.MaxValue,
            byte.MaxValue,
            -1,
            false,
            true,
            true,
            231,
            231,
            "nvarchar",
            typeof(string),
            typeof(SqlString),
            SqlDbType.NVarChar,
            DbType.String,
            7);

        /// <summary>
        /// The meta max var binary.
        /// </summary>
        internal static readonly MetaType MetaMaxVarBinary = new MetaType(
            byte.MaxValue,
            byte.MaxValue,
            -1,
            false,
            true,
            true,
            165,
            165,
            "varbinary",
            typeof(byte[]),
            typeof(SqlBinary),
            SqlDbType.VarBinary,
            DbType.Binary,
            2);

        /// <summary>
        /// The meta max var char.
        /// </summary>
        internal static readonly MetaType MetaMaxVarChar = new MetaType(
            byte.MaxValue,
            byte.MaxValue,
            -1,
            false,
            true,
            true,
            167,
            167,
            "varchar",
            typeof(string),
            typeof(SqlString),
            SqlDbType.VarChar,
            DbType.AnsiString,
            7);

        /// <summary>
        /// The meta n text.
        /// </summary>
        internal static readonly MetaType MetaNText = new MetaType(
            byte.MaxValue,
            byte.MaxValue,
            -1,
            false,
            true,
            false,
            99,
            99,
            "ntext",
            typeof(string),
            typeof(SqlString),
            SqlDbType.NText,
            DbType.String,
            7);

        /// <summary>
        /// The meta n var char.
        /// </summary>
        internal static readonly MetaType MetaNVarChar = new MetaType(
            byte.MaxValue,
            byte.MaxValue,
            -1,
            false,
            false,
            false,
            231,
            231,
            "nvarchar",
            typeof(string),
            typeof(SqlString),
            SqlDbType.NVarChar,
            DbType.String,
            7);

        /// <summary>
        /// The meta text.
        /// </summary>
        internal static readonly MetaType MetaText = new MetaType(
            byte.MaxValue,
            byte.MaxValue,
            -1,
            false,
            true,
            false,
            35,
            35,
            "text",
            typeof(string),
            typeof(SqlString),
            SqlDbType.Text,
            DbType.AnsiString,
            0);

        /// <summary>
        /// The meta time.
        /// </summary>
        internal static readonly MetaType MetaTime = new MetaType(
            byte.MaxValue,
            7,
            -1,
            false,
            false,
            false,
            41,
            41,
            "time",
            typeof(TimeSpan),
            typeof(TimeSpan),
            SqlDbType.Time,
            DbType.Time,
            1);

        /// <summary>
        /// The meta udt.
        /// </summary>
        internal static readonly MetaType MetaUdt = new MetaType(
            byte.MaxValue,
            byte.MaxValue,
            -1,
            false,
            false,
            true,
            240,
            240,
            "udt",
            typeof(object),
            typeof(object),
            SqlDbType.Udt,
            DbType.Object,
            0);

        /// <summary>
        /// The meta var binary.
        /// </summary>
        internal static readonly MetaType MetaVarBinary = new MetaType(
            byte.MaxValue,
            byte.MaxValue,
            -1,
            false,
            false,
            false,
            165,
            165,
            "varbinary",
            typeof(byte[]),
            typeof(SqlBinary),
            SqlDbType.VarBinary,
            DbType.Binary,
            2);

        /// <summary>
        /// The meta xml.
        /// </summary>
        internal static readonly MetaType MetaXml = new MetaType(
            byte.MaxValue,
            byte.MaxValue,
            -1,
            false,
            true,
            true,
            241,
            241,
            "xml",
            typeof(string),
            typeof(SqlXml),
            SqlDbType.Xml,
            DbType.Xml,
            0);

        /// <summary>
        /// The meta big int.
        /// </summary>
        private static readonly MetaType MetaBigInt = new MetaType(
            19,
            byte.MaxValue,
            8,
            true,
            false,
            false,
            127,
            38,
            "bigint",
            typeof(long),
            typeof(SqlInt64),
            SqlDbType.BigInt,
            DbType.Int64,
            0);

        /// <summary>
        /// The meta binary.
        /// </summary>
        private static readonly MetaType MetaBinary = new MetaType(
            byte.MaxValue,
            byte.MaxValue,
            -1,
            false,
            false,
            false,
            173,
            173,
            "binary",
            typeof(byte[]),
            typeof(SqlBinary),
            SqlDbType.Binary,
            DbType.Binary,
            2);

        /// <summary>
        /// The meta bit.
        /// </summary>
        private static readonly MetaType MetaBit = new MetaType(
            byte.MaxValue,
            byte.MaxValue,
            1,
            true,
            false,
            false,
            50,
            104,
            "bit",
            typeof(bool),
            typeof(SqlBoolean),
            SqlDbType.Bit,
            DbType.Boolean,
            0);

        /// <summary>
        /// The meta char.
        /// </summary>
        private static readonly MetaType MetaChar = new MetaType(
            byte.MaxValue,
            byte.MaxValue,
            -1,
            false,
            false,
            false,
            175,
            175,
            "char",
            typeof(string),
            typeof(SqlString),
            SqlDbType.Char,
            DbType.AnsiStringFixedLength,
            7);

        /// <summary>
        /// The meta date.
        /// </summary>
        private static readonly MetaType MetaDate = new MetaType(
            byte.MaxValue,
            byte.MaxValue,
            3,
            true,
            false,
            false,
            40,
            40,
            "date",
            typeof(DateTime),
            typeof(DateTime),
            SqlDbType.Date,
            DbType.Date,
            0);

        /// <summary>
        /// The meta date time.
        /// </summary>
        private static readonly MetaType MetaDateTime = new MetaType(
            23,
            3,
            8,
            true,
            false,
            false,
            61,
            111,
            "datetime",
            typeof(DateTime),
            typeof(SqlDateTime),
            SqlDbType.DateTime,
            DbType.DateTime,
            0);

        /// <summary>
        /// The meta date time 2.
        /// </summary>
        private static readonly MetaType MetaDateTime2 = new MetaType(
            byte.MaxValue,
            7,
            -1,
            false,
            false,
            false,
            42,
            42,
            "datetime2",
            typeof(DateTime),
            typeof(DateTime),
            SqlDbType.DateTime2,
            DbType.DateTime2,
            1);

        /// <summary>
        /// The meta float.
        /// </summary>
        private static readonly MetaType MetaFloat = new MetaType(
            15,
            byte.MaxValue,
            8,
            true,
            false,
            false,
            62,
            109,
            "float",
            typeof(double),
            typeof(SqlDouble),
            SqlDbType.Float,
            DbType.Double,
            0);

        /// <summary>
        /// The meta int.
        /// </summary>
        private static readonly MetaType MetaInt = new MetaType(
            10,
            byte.MaxValue,
            4,
            true,
            false,
            false,
            56,
            38,
            "int",
            typeof(int),
            typeof(SqlInt32),
            SqlDbType.Int,
            DbType.Int32,
            0);

        /// <summary>
        /// The meta max udt.
        /// </summary>
        private static readonly MetaType MetaMaxUdt = new MetaType(
            byte.MaxValue,
            byte.MaxValue,
            -1,
            false,
            true,
            true,
            240,
            240,
            "udt",
            typeof(object),
            typeof(object),
            SqlDbType.Udt,
            DbType.Object,
            0);

        /// <summary>
        /// The meta money.
        /// </summary>
        private static readonly MetaType MetaMoney = new MetaType(
            19,
            byte.MaxValue,
            8,
            true,
            false,
            false,
            60,
            110,
            "money",
            typeof(Decimal),
            typeof(SqlMoney),
            SqlDbType.Money,
            DbType.Currency,
            0);

        /// <summary>
        /// The meta n char.
        /// </summary>
        private static readonly MetaType MetaNChar = new MetaType(
            byte.MaxValue,
            byte.MaxValue,
            -1,
            false,
            false,
            false,
            239,
            239,
            "nchar",
            typeof(string),
            typeof(SqlString),
            SqlDbType.NChar,
            DbType.StringFixedLength,
            7);

        /// <summary>
        /// The meta real.
        /// </summary>
        private static readonly MetaType MetaReal = new MetaType(
            7,
            byte.MaxValue,
            4,
            true,
            false,
            false,
            59,
            109,
            "real",
            typeof(float),
            typeof(SqlSingle),
            SqlDbType.Real,
            DbType.Single,
            0);

        /// <summary>
        /// The meta sudt.
        /// </summary>
        private static readonly MetaType MetaSUDT = new MetaType(
            byte.MaxValue,
            byte.MaxValue,
            -1,
            false,
            false,
            false,
            31,
            31,
            string.Empty,
            typeof(SqlDataRecord),
            typeof(SqlDataRecord),
            SqlDbType.Structured,
            DbType.Object,
            0);

        /// <summary>
        /// The meta small date time.
        /// </summary>
        private static readonly MetaType MetaSmallDateTime = new MetaType(
            16,
            0,
            4,
            true,
            false,
            false,
            58,
            111,
            "smalldatetime",
            typeof(DateTime),
            typeof(SqlDateTime),
            SqlDbType.SmallDateTime,
            DbType.DateTime,
            0);

        /// <summary>
        /// The meta small int.
        /// </summary>
        private static readonly MetaType MetaSmallInt = new MetaType(
            5,
            byte.MaxValue,
            2,
            true,
            false,
            false,
            52,
            38,
            "smallint",
            typeof(short),
            typeof(SqlInt16),
            SqlDbType.SmallInt,
            DbType.Int16,
            0);

        /// <summary>
        /// The meta small money.
        /// </summary>
        private static readonly MetaType MetaSmallMoney = new MetaType(
            10,
            byte.MaxValue,
            4,
            true,
            false,
            false,
            122,
            110,
            "smallmoney",
            typeof(Decimal),
            typeof(SqlMoney),
            SqlDbType.SmallMoney,
            DbType.Currency,
            0);

        /// <summary>
        /// The meta small var binary.
        /// </summary>
        private static readonly MetaType MetaSmallVarBinary = new MetaType(
            byte.MaxValue,
            byte.MaxValue,
            -1,
            false,
            false,
            false,
            37,
            173,
            string.Empty,
            typeof(byte[]),
            typeof(SqlBinary),
            (SqlDbType)24,
            DbType.Binary,
            2);

        /// <summary>
        /// The meta table.
        /// </summary>
        private static readonly MetaType MetaTable = new MetaType(
            byte.MaxValue,
            byte.MaxValue,
            -1,
            false,
            false,
            false,
            243,
            243,
            "table",
            typeof(IEnumerable<DbDataRecord>),
            typeof(IEnumerable<DbDataRecord>),
            SqlDbType.Structured,
            DbType.Object,
            0);

        /// <summary>
        /// The meta timestamp.
        /// </summary>
        private static readonly MetaType MetaTimestamp = new MetaType(
            byte.MaxValue,
            byte.MaxValue,
            -1,
            false,
            false,
            false,
            173,
            173,
            "timestamp",
            typeof(byte[]),
            typeof(SqlBinary),
            SqlDbType.Timestamp,
            DbType.Binary,
            2);

        /// <summary>
        /// The meta tiny int.
        /// </summary>
        private static readonly MetaType MetaTinyInt = new MetaType(
            3,
            byte.MaxValue,
            1,
            true,
            false,
            false,
            48,
            38,
            "tinyint",
            typeof(byte),
            typeof(SqlByte),
            SqlDbType.TinyInt,
            DbType.Byte,
            0);

        /// <summary>
        /// The meta unique id.
        /// </summary>
        private static readonly MetaType MetaUniqueId = new MetaType(
            byte.MaxValue,
            byte.MaxValue,
            16,
            true,
            false,
            false,
            36,
            36,
            "uniqueidentifier",
            typeof(Guid),
            typeof(SqlGuid),
            SqlDbType.UniqueIdentifier,
            DbType.Guid,
            0);

        /// <summary>
        /// The meta var char.
        /// </summary>
        private static readonly MetaType MetaVarChar = new MetaType(
            byte.MaxValue,
            byte.MaxValue,
            -1,
            false,
            false,
            false,
            167,
            167,
            "varchar",
            typeof(string),
            typeof(SqlString),
            SqlDbType.VarChar,
            DbType.AnsiString,
            7);

        /// <summary>
        /// The meta variant.
        /// </summary>
        private static readonly MetaType MetaVariant = new MetaType(
            byte.MaxValue,
            byte.MaxValue,
            -1,
            true,
            false,
            false,
            98,
            98,
            "sql_variant",
            typeof(object),
            typeof(object),
            SqlDbType.Variant,
            DbType.Object,
            0);

        internal readonly Type ClassType;
        internal readonly DbType DbType;
        internal readonly int FixedLength;
        internal readonly bool Is100Supported;
        internal readonly bool Is70Supported;
        internal readonly bool Is80Supported;
        internal readonly bool Is90Supported;
        internal readonly bool IsAnsiType;
        internal readonly bool IsBinType;
        internal readonly bool IsCharType;
        internal readonly bool IsFixed;
        internal readonly bool IsLong;
        internal readonly bool IsNCharType;
        internal readonly bool IsNewKatmaiType;
        internal readonly bool IsPlp;
        internal readonly bool IsSizeInCharacters;
        internal readonly bool IsVarTime;
        internal readonly byte NullableType;
        internal readonly byte Precision;
        internal readonly byte PropBytes;
        internal readonly byte Scale;
        internal readonly SqlDbType SqlDbType;
        internal readonly Type SqlType;
        internal readonly byte TDSType;
        internal readonly string TypeName;

        static MetaType() {}

        public MetaType(
            byte precision,
            byte scale,
            int fixedLength,
            bool isFixed,
            bool isLong,
            bool isPlp,
            byte tdsType,
            byte nullableTdsType,
            string typeName,
            Type classType,
            Type sqlType,
            SqlDbType sqldbType,
            DbType dbType,
            byte propBytes) {
            this.Precision = precision;
            this.Scale = scale;
            this.FixedLength = fixedLength;
            this.IsFixed = isFixed;
            this.IsLong = isLong;
            this.IsPlp = isPlp;
            this.TDSType = tdsType;
            this.NullableType = nullableTdsType;
            this.TypeName = typeName;
            this.SqlDbType = sqldbType;
            this.DbType = dbType;
            this.ClassType = classType;
            this.SqlType = sqlType;
            this.PropBytes = propBytes;
            this.IsAnsiType = _IsAnsiType(sqldbType);
            this.IsBinType = _IsBinType(sqldbType);
            this.IsCharType = _IsCharType(sqldbType);
            this.IsNCharType = _IsNCharType(sqldbType);
            this.IsSizeInCharacters = _IsSizeInCharacters(sqldbType);
            this.IsNewKatmaiType = _IsNewKatmaiType(sqldbType);
            this.IsVarTime = _IsVarTime(sqldbType);
            this.Is70Supported = _Is70Supported(this.SqlDbType);
            this.Is80Supported = _Is80Supported(this.SqlDbType);
            this.Is90Supported = _Is90Supported(this.SqlDbType);
            this.Is100Supported = _Is100Supported(this.SqlDbType);
        }

        public int TypeId {
            get {
                return 0;
            }
        }

        public static TdsDateTime FromDateTime(DateTime dateTime, byte cb) {
            var tdsDateTime = new TdsDateTime();
            SqlDateTime sqlDateTime;
            if (cb == 8) {
                sqlDateTime = new SqlDateTime(dateTime);
                tdsDateTime.time = sqlDateTime.TimeTicks;
            }
            else {
                sqlDateTime = new SqlDateTime(dateTime.AddSeconds(30.0));
                tdsDateTime.time = sqlDateTime.TimeTicks / SqlDateTime.SQLTicksPerMinute;
            }

            tdsDateTime.days = sqlDateTime.DayTicks;
            return tdsDateTime;
        }

        public static DateTime ToDateTime(int sqlDays, int sqlTime, int length) {
            if (length == 4) {
                return new SqlDateTime(sqlDays, sqlTime * SqlDateTime.SQLTicksPerMinute).Value;
            }

            return new SqlDateTime(sqlDays, sqlTime).Value;
        }

        internal static object GetComValueFromSqlVariant(object sqlVal) {
            object obj = null;
            if (ADP.IsNull(sqlVal)) {
                return obj;
            }

            if (sqlVal is SqlSingle) {
                obj = ((SqlSingle)sqlVal).Value;
            }
            else if (sqlVal is SqlString) {
                obj = ((SqlString)sqlVal).Value;
            }
            else if (sqlVal is SqlDouble) {
                obj = ((SqlDouble)sqlVal).Value;
            }
            else if (sqlVal is SqlBinary) {
                obj = ((SqlBinary)sqlVal).Value;
            }
            else if (sqlVal is SqlGuid) {
                obj = ((SqlGuid)sqlVal).Value;
            }
            else if (sqlVal is SqlBoolean) {
                obj = ((SqlBoolean)sqlVal).Value;
            }
            else if (sqlVal is SqlByte) {
                obj = ((SqlByte)sqlVal).Value;
            }
            else if (sqlVal is SqlInt16) {
                obj = ((SqlInt16)sqlVal).Value;
            }
            else if (sqlVal is SqlInt32) {
                obj = ((SqlInt32)sqlVal).Value;
            }
            else if (sqlVal is SqlInt64) {
                obj = ((SqlInt64)sqlVal).Value;
            }
            else if (sqlVal is SqlDecimal) {
                obj = ((SqlDecimal)sqlVal).Value;
            }
            else if (sqlVal is SqlDateTime) {
                obj = ((SqlDateTime)sqlVal).Value;
            }
            else if (sqlVal is SqlMoney) {
                obj = ((SqlMoney)sqlVal).Value;
            }
            else if (sqlVal is SqlXml) {
                obj = ((SqlXml)sqlVal).Value;
            }

            return obj;
        }

        internal static MetaType GetDefaultMetaType() {
            return MetaNVarChar;
        }

        internal static MetaType GetMaxMetaTypeFromMetaType(MetaType mt) {
            switch (mt.SqlDbType) {
                case SqlDbType.VarBinary:
                case SqlDbType.Binary:
                    return MetaMaxVarBinary;
                case SqlDbType.VarChar:
                case SqlDbType.Char:
                    return MetaMaxVarChar;
                case SqlDbType.Udt:
                    return MetaMaxUdt;
                case SqlDbType.NChar:
                case SqlDbType.NVarChar:
                    return MetaMaxNVarChar;
                default:
                    return mt;
            }
        }

        internal static MetaType GetMetaTypeFromDbType(DbType target) {
            switch (target) {
                case DbType.AnsiString:
                    return MetaVarChar;
                case DbType.Binary:
                    return MetaVarBinary;
                case DbType.Byte:
                    return MetaTinyInt;
                case DbType.Boolean:
                    return MetaBit;
                case DbType.Currency:
                    return MetaMoney;
                case DbType.Date:
                case DbType.DateTime:
                    return MetaDateTime;
                case DbType.Decimal:
                    return MetaDecimal;
                case DbType.Double:
                    return MetaFloat;
                case DbType.Guid:
                    return MetaUniqueId;
                case DbType.Int16:
                    return MetaSmallInt;
                case DbType.Int32:
                    return MetaInt;
                case DbType.Int64:
                    return MetaBigInt;
                case DbType.Object:
                    return MetaVariant;
                case DbType.Single:
                    return MetaReal;
                case DbType.String:
                    return MetaNVarChar;
                case DbType.Time:
                    return MetaDateTime;
                case DbType.AnsiStringFixedLength:
                    return MetaChar;
                case DbType.StringFixedLength:
                    return MetaNChar;
                case DbType.Xml:
                    return MetaXml;
                case DbType.DateTime2:
                    return MetaDateTime2;
                case DbType.DateTimeOffset:
                    return MetaDateTimeOffset;
                default:
                    throw new ArgumentException(string.Format("DbTypeNotSupported {0}", typeof(SqlDbType)));

                // throw ADP.DbTypeNotSupported(target, typeof(SqlDbType));
            }
        }

        internal static MetaType GetMetaTypeFromSqlDbType(SqlDbType target, bool isMultiValued) {
            switch (target) {
                case SqlDbType.BigInt:
                    return MetaBigInt;
                case SqlDbType.Binary:
                    return MetaBinary;
                case SqlDbType.Bit:
                    return MetaBit;
                case SqlDbType.Char:
                    return MetaChar;
                case SqlDbType.DateTime:
                    return MetaDateTime;
                case SqlDbType.Decimal:
                    return MetaDecimal;
                case SqlDbType.Float:
                    return MetaFloat;
                case SqlDbType.Image:
                    return MetaImage;
                case SqlDbType.Int:
                    return MetaInt;
                case SqlDbType.Money:
                    return MetaMoney;
                case SqlDbType.NChar:
                    return MetaNChar;
                case SqlDbType.NText:
                    return MetaNText;
                case SqlDbType.NVarChar:
                    return MetaNVarChar;
                case SqlDbType.Real:
                    return MetaReal;
                case SqlDbType.UniqueIdentifier:
                    return MetaUniqueId;
                case SqlDbType.SmallDateTime:
                    return MetaSmallDateTime;
                case SqlDbType.SmallInt:
                    return MetaSmallInt;
                case SqlDbType.SmallMoney:
                    return MetaSmallMoney;
                case SqlDbType.Text:
                    return MetaText;
                case SqlDbType.Timestamp:
                    return MetaTimestamp;
                case SqlDbType.TinyInt:
                    return MetaTinyInt;
                case SqlDbType.VarBinary:
                    return MetaVarBinary;
                case SqlDbType.VarChar:
                    return MetaVarChar;
                case SqlDbType.Variant:
                    return MetaVariant;
                case (SqlDbType)24:
                    return MetaSmallVarBinary;
                case SqlDbType.Xml:
                    return MetaXml;
                case SqlDbType.Udt:
                    return MetaUdt;
                case SqlDbType.Structured:
                    if (isMultiValued) {
                        return MetaTable;
                    }

                    return MetaSUDT;
                case SqlDbType.Date:
                    return MetaDate;
                case SqlDbType.Time:
                    return MetaTime;
                case SqlDbType.DateTime2:
                    return MetaDateTime2;
                case SqlDbType.DateTimeOffset:
                    return MetaDateTimeOffset;
                default:
                    throw new ArgumentOutOfRangeException("target", target, "Invalid SqlDbType");
            }
        }

        internal static MetaType GetMetaTypeFromType(Type dataType) {
            return GetMetaTypeFromValue(dataType, null, false, true);
        }

        internal static MetaType GetMetaTypeFromValue(object value, bool streamAllowed = true) {
            if (value == null) {
                throw new ArgumentNullException("value");
            }
            return GetMetaTypeFromValue(value.GetType(), value, true, streamAllowed);
        }

        internal static object GetNullSqlValue(Type sqlType) {
            if (sqlType == typeof(SqlSingle)) {
                return SqlSingle.Null;
            }

            if (sqlType == typeof(SqlString)) {
                return SqlString.Null;
            }

            if (sqlType == typeof(SqlDouble)) {
                return SqlDouble.Null;
            }

            if (sqlType == typeof(SqlBinary)) {
                return SqlBinary.Null;
            }

            if (sqlType == typeof(SqlGuid)) {
                return SqlGuid.Null;
            }

            if (sqlType == typeof(SqlBoolean)) {
                return SqlBoolean.Null;
            }

            if (sqlType == typeof(SqlByte)) {
                return SqlByte.Null;
            }

            if (sqlType == typeof(SqlInt16)) {
                return SqlInt16.Null;
            }

            if (sqlType == typeof(SqlInt32)) {
                return SqlInt32.Null;
            }

            if (sqlType == typeof(SqlInt64)) {
                return SqlInt64.Null;
            }

            if (sqlType == typeof(SqlDecimal)) {
                return SqlDecimal.Null;
            }

            if (sqlType == typeof(SqlDateTime)) {
                return SqlDateTime.Null;
            }

            if (sqlType == typeof(SqlMoney)) {
                return SqlMoney.Null;
            }

            if (sqlType == typeof(SqlXml)) {
                return SqlXml.Null;
            }

            if (sqlType == typeof(object)) {
                return DBNull.Value;
            }

            if (sqlType == typeof(IEnumerable<DbDataRecord>)) {
                return DBNull.Value;
            }

            if (sqlType == typeof(DataTable)) {
                return DBNull.Value;
            }

            if (sqlType == typeof(DateTime)) {
                return DBNull.Value;
            }

            if (sqlType == typeof(TimeSpan)) {
                return DBNull.Value;
            }

            if (sqlType == typeof(DateTimeOffset)) {
                return DBNull.Value;
            }

            return DBNull.Value;
        }

        internal static MetaType GetSqlDataType(int tdsType, uint userType, int length) {
            switch (tdsType) {
                case 231:
                    return MetaNVarChar;
                case 239:
                    return MetaNChar;
                case 240:
                    return MetaUdt;
                case 241:
                    return MetaXml;
                case 243:
                    return MetaTable;
                case 165:
                    return MetaVarBinary;
                case 167:
                case 39:
                    return MetaVarChar;
                case 173:
                case 45:
                    if (80 != (int)userType) {
                        return MetaBinary;
                    }

                    return MetaTimestamp;
                case 175:
                case 47:
                    return MetaChar;
                case 122:
                    return MetaSmallMoney;
                case sbyte.MaxValue:
                    return MetaBigInt;
                case 34:
                    return MetaImage;
                case 35:
                    return MetaText;
                case 36:
                    return MetaUniqueId;
                case 37:
                    return MetaSmallVarBinary;
                case 38:
                    if (4 > length) {
                        if (2 != length) {
                            return MetaTinyInt;
                        }

                        return MetaSmallInt;
                    }

                    if (4 != length) {
                        return MetaBigInt;
                    }

                    return MetaInt;
                case 40:
                    return MetaDate;
                case 41:
                    return MetaTime;
                case 42:
                    return MetaDateTime2;
                case 43:
                    return MetaDateTimeOffset;
                case 48:
                    return MetaTinyInt;
                case 50:
                case 104:
                    return MetaBit;
                case 52:
                    return MetaSmallInt;
                case 56:
                    return MetaInt;
                case 58:
                    return MetaSmallDateTime;
                case 59:
                    return MetaReal;
                case 60:
                    return MetaMoney;
                case 61:
                    return MetaDateTime;
                case 62:
                    return MetaFloat;
                case 98:
                    return MetaVariant;
                case 99:
                    return MetaNText;
                case 106:
                case 108:
                    return MetaDecimal;
                case 109:
                    if (4 != length) {
                        return MetaFloat;
                    }

                    return MetaReal;
                case 110:
                    if (4 != length) {
                        return MetaMoney;
                    }

                    return MetaSmallMoney;
                case 111:
                    if (4 != length) {
                        return MetaDateTime;
                    }

                    return MetaSmallDateTime;
                default:
                    throw new ArgumentOutOfRangeException("tdsType");
            }
        }

        internal static SqlDbType GetSqlDbTypeFromOleDbType(short dbType, string typeName) {
            var sqlDbType = SqlDbType.Variant;
            switch ((OleDbType)dbType) {
                case OleDbType.Guid:
                    sqlDbType = SqlDbType.UniqueIdentifier;
                    break;
                case OleDbType.Binary:
                case OleDbType.VarBinary:
                    sqlDbType = typeName == "binary" ? SqlDbType.Binary : SqlDbType.VarBinary;
                    break;
                case OleDbType.Char:
                case OleDbType.VarChar:
                    sqlDbType = typeName == "char" ? SqlDbType.Char : SqlDbType.VarChar;
                    break;
                case OleDbType.WChar:
                case OleDbType.VarWChar:
                case OleDbType.BSTR:
                    sqlDbType = typeName == "nchar" ? SqlDbType.NChar : SqlDbType.NVarChar;
                    break;
                case OleDbType.Numeric:
                case OleDbType.Decimal:
                    sqlDbType = SqlDbType.Decimal;
                    break;
                case (OleDbType)132:
                    sqlDbType = SqlDbType.Udt;
                    break;
                case OleDbType.DBDate:
                    sqlDbType = SqlDbType.Date;
                    break;
                case OleDbType.DBTimeStamp:
                case OleDbType.Date:
                case OleDbType.Filetime:
                    switch (typeName) {
                        case "smalldatetime":
                            sqlDbType = SqlDbType.SmallDateTime;
                            break;
                        case "datetime2":
                            sqlDbType = SqlDbType.DateTime2;
                            break;
                        default:
                            sqlDbType = SqlDbType.DateTime;
                            break;
                    }

                    break;
                case (OleDbType)141:
                    sqlDbType = SqlDbType.Xml;
                    break;
                case (OleDbType)145:
                    sqlDbType = SqlDbType.Time;
                    break;
                case (OleDbType)146:
                    sqlDbType = SqlDbType.DateTimeOffset;
                    break;
                case OleDbType.LongVarChar:
                    sqlDbType = SqlDbType.Text;
                    break;
                case OleDbType.LongVarWChar:
                    sqlDbType = SqlDbType.NText;
                    break;
                case OleDbType.LongVarBinary:
                    sqlDbType = SqlDbType.Image;
                    break;
                case OleDbType.SmallInt:
                case OleDbType.UnsignedSmallInt:
                    sqlDbType = SqlDbType.SmallInt;
                    break;
                case OleDbType.Integer:
                    sqlDbType = SqlDbType.Int;
                    break;
                case OleDbType.Single:
                    sqlDbType = SqlDbType.Real;
                    break;
                case OleDbType.Double:
                    sqlDbType = SqlDbType.Float;
                    break;
                case OleDbType.Currency:
                    sqlDbType = typeName == "smallmoney" ? SqlDbType.SmallMoney : SqlDbType.Money;
                    break;
                case OleDbType.Boolean:
                    sqlDbType = SqlDbType.Bit;
                    break;
                case OleDbType.Variant:
                    sqlDbType = SqlDbType.Variant;
                    break;
                case OleDbType.TinyInt:
                case OleDbType.UnsignedTinyInt:
                    sqlDbType = SqlDbType.TinyInt;
                    break;
                case OleDbType.BigInt:
                    sqlDbType = SqlDbType.BigInt;
                    break;
            }

            return sqlDbType;
        }

        internal static object GetSqlValueFromComVariant(object comVal) {
            object obj = null;
            if (comVal != null && DBNull.Value != comVal) {
                if (comVal is float) {
                    obj = new SqlSingle((float)comVal);
                }
                else if (comVal is string) {
                    obj = new SqlString((string)comVal);
                }
                else if (comVal is double) {
                    obj = new SqlDouble((double)comVal);
                }
                else if (comVal is byte[]) {
                    obj = new SqlBinary((byte[])comVal);
                }
                else if (comVal is char) {
                    obj = new SqlString(((char)comVal).ToString());
                }
                else if (comVal is char[]) {
                    obj = new SqlChars((char[])comVal);
                }
                else if (comVal is Guid) {
                    obj = new SqlGuid((Guid)comVal);
                }
                else if (comVal is bool) {
                    obj = new SqlBoolean((bool)comVal);
                }
                else if (comVal is byte) {
                    obj = new SqlByte((byte)comVal);
                }
                else if (comVal is short) {
                    obj = new SqlInt16((short)comVal);
                }
                else if (comVal is int) {
                    obj = new SqlInt32((int)comVal);
                }
                else if (comVal is long) {
                    obj = new SqlInt64((long)comVal);
                }
                else if (comVal is Decimal) {
                    obj = new SqlDecimal((Decimal)comVal);
                }
                else if (comVal is DateTime) {
                    obj = new SqlDateTime((DateTime)comVal);
                }
                else if (comVal is XmlReader) {
                    obj = new SqlXml((XmlReader)comVal);
                }
                else if (comVal is TimeSpan || comVal is DateTimeOffset) {
                    obj = comVal;
                }
            }

            return obj;
        }

        internal static string GetStringFromXml(XmlReader xmlreader) {
            return new SqlXml(xmlreader).Value;
        }

        internal static int GetTimeSizeFromScale(byte scale) {
            if (scale <= 2) {
                return 3;
            }

            return (int)scale <= 4 ? 4 : 5;
        }

        internal static MetaType PromoteStringType(string s) {
            if (s.Length << 1 > 8000) {
                return MetaVarChar;
            }

            return MetaNVarChar;
        }

        internal static bool _IsVarTime(SqlDbType type) {
            if (type != SqlDbType.Time && type != SqlDbType.DateTime2) {
                return type == SqlDbType.DateTimeOffset;
            }

            return true;
        }

        private static MetaType GetMetaTypeFromValue(Type dataType, object value, bool inferLen, bool streamAllowed) {
            switch (Type.GetTypeCode(dataType)) {
                case TypeCode.Empty:
                    throw new ArgumentException("Invalid DataType TypeCode.Empty");
                case TypeCode.Object:
                    if (dataType == typeof(byte[])) {
                        if (!inferLen || ((byte[])value).Length <= 8000) {
                            return MetaVarBinary;
                        }

                        return MetaImage;
                    }

                    if (dataType == typeof(Guid)) {
                        return MetaUniqueId;
                    }

                    if (dataType == typeof(object)) {
                        return MetaVariant;
                    }

                    if (dataType == typeof(SqlBinary)) {
                        return MetaVarBinary;
                    }

                    if (dataType == typeof(SqlBoolean)) {
                        return MetaBit;
                    }

                    if (dataType == typeof(SqlByte)) {
                        return MetaTinyInt;
                    }

                    if (dataType == typeof(SqlBytes)) {
                        return MetaVarBinary;
                    }

                    if (dataType == typeof(SqlChars)) {
                        return MetaNVarChar;
                    }

                    if (dataType == typeof(SqlDateTime)) {
                        return MetaDateTime;
                    }

                    if (dataType == typeof(SqlDouble)) {
                        return MetaFloat;
                    }

                    if (dataType == typeof(SqlGuid)) {
                        return MetaUniqueId;
                    }

                    if (dataType == typeof(SqlInt16)) {
                        return MetaSmallInt;
                    }

                    if (dataType == typeof(SqlInt32)) {
                        return MetaInt;
                    }

                    if (dataType == typeof(SqlInt64)) {
                        return MetaBigInt;
                    }

                    if (dataType == typeof(SqlMoney)) {
                        return MetaMoney;
                    }

                    if (dataType == typeof(SqlDecimal)) {
                        return MetaDecimal;
                    }

                    if (dataType == typeof(SqlSingle)) {
                        return MetaReal;
                    }

                    if (dataType == typeof(SqlXml)) {
                        return MetaXml;
                    }

                    if (dataType == typeof(SqlString)) {
                        if (!inferLen || ((SqlString)value).IsNull) {
                            return MetaNVarChar;
                        }

                        return PromoteStringType(((SqlString)value).Value);
                    }

                    if (dataType == typeof(IEnumerable<DbDataRecord>) || dataType == typeof(DataTable)) {
                        return MetaTable;
                    }

                    if (dataType == typeof(TimeSpan)) {
                        return MetaTime;
                    }

                    if (dataType == typeof(DateTimeOffset)) {
                        return MetaDateTimeOffset;
                    }

                    if (SqlUdtInfo.TryGetFromType(dataType) != null) {
                        return MetaUdt;
                    }

                    if (streamAllowed) {
                        if (typeof(Stream).IsAssignableFrom(dataType)) {
                            return MetaVarBinary;
                        }

                        if (typeof(TextReader).IsAssignableFrom(dataType)) {
                            return MetaNVarChar;
                        }

                        if (typeof(XmlReader).IsAssignableFrom(dataType)) {
                            return MetaXml;
                        }
                    }

                    throw new ArgumentException(string.Format("Unknown data type {0}", dataType));
                // ADP.UnknownDataType(dataType);
                case TypeCode.DBNull:
                    throw new ArgumentException("Invalid data type TypeCode.DBNull");
                // ADP.InvalidDataType(TypeCode.DBNull);
                case TypeCode.Boolean:
                    return MetaBit;
                case TypeCode.Char:
                    return MetaNChar;
                    throw new ArgumentException("Invalid data type TypeCode.Char");
                // ADP.InvalidDataType(TypeCode.Char);
                case TypeCode.SByte:
                    throw new ArgumentException("Invalid data type TypeCode.SByte");
                // ADP.InvalidDataType(TypeCode.SByte);
                case TypeCode.Byte:
                    return MetaTinyInt;
                case TypeCode.Int16:
                    return MetaSmallInt;
                case TypeCode.UInt16:
                    return MetaSmallInt;
                // throw new ArgumentException("Invalid data type TypeCode.UInt16)");
                case TypeCode.Int32:
                    return MetaInt;
                case TypeCode.UInt32:
                    return MetaInt;
                // throw new ArgumentException("Invalid data type TypeCode.UInt32");
                case TypeCode.Int64:
                    return MetaBigInt;
                case TypeCode.UInt64:
                    return MetaBigInt;
                // throw new ArgumentException("Invalid data type TypeCode.UInt64");
                case TypeCode.Single:
                    return MetaReal;
                case TypeCode.Double:
                    return MetaFloat;
                case TypeCode.Decimal:
                    return MetaDecimal;
                case TypeCode.DateTime:
                    return MetaDateTime;
                case TypeCode.String:
                    if (!inferLen) {
                        return MetaNVarChar;
                    }

                    return PromoteStringType((string)value);
                default:
                    throw new ArgumentException(
                        string.Format("Unknown DataTypeCode {0}, {1}", dataType, Type.GetTypeCode(dataType)));
            }
        }

        private static bool _Is100Supported(SqlDbType type) {
            if (!_Is90Supported(type) && SqlDbType.Date != type
                && (SqlDbType.Time != type && SqlDbType.DateTime2 != type)) {
                return SqlDbType.DateTimeOffset == type;
            }

            return true;
        }

        private static bool _Is70Supported(SqlDbType type) {
            if (type != SqlDbType.BigInt && type > SqlDbType.BigInt) {
                return type <= SqlDbType.VarChar;
            }

            return false;
        }

        private static bool _Is80Supported(SqlDbType type) {
            if (type >= SqlDbType.BigInt) {
                return type <= SqlDbType.Variant;
            }

            return false;
        }

        private static bool _Is90Supported(SqlDbType type) {
            if (!_Is80Supported(type) && SqlDbType.Xml != type) {
                return SqlDbType.Udt == type;
            }

            return true;
        }

        private static bool _IsAnsiType(SqlDbType type) {
            if (type != SqlDbType.Char && type != SqlDbType.VarChar) {
                return type == SqlDbType.Text;
            }

            return true;
        }

        private static bool _IsBinType(SqlDbType type) {
            if (type != SqlDbType.Image && type != SqlDbType.Binary
                && (type != SqlDbType.VarBinary && type != SqlDbType.Timestamp) && type != SqlDbType.Udt) {
                return type == (SqlDbType)24;
            }

            return true;
        }

        private static bool _IsCharType(SqlDbType type) {
            if (type != SqlDbType.NChar && type != SqlDbType.NVarChar
                && (type != SqlDbType.NText && type != SqlDbType.Char)
                && (type != SqlDbType.VarChar && type != SqlDbType.Text)) {
                return type == SqlDbType.Xml;
            }

            return true;
        }

        private static bool _IsNCharType(SqlDbType type) {
            if (type != SqlDbType.NChar && type != SqlDbType.NVarChar && type != SqlDbType.NText) {
                return type == SqlDbType.Xml;
            }

            return true;
        }

        private static bool _IsNewKatmaiType(SqlDbType type) {
            return SqlDbType.Structured == type;
        }

        private static bool _IsSizeInCharacters(SqlDbType type) {
            if (type != SqlDbType.NChar && type != SqlDbType.NVarChar && type != SqlDbType.Xml) {
                return type == SqlDbType.NText;
            }

            return true;
        }

        private static class ADP
        {
            internal static bool IsNull(object value) {
                if (value == null || DBNull.Value == value) {
                    return true;
                }

                var nullable = value as INullable;
                if (nullable != null) {
                    return nullable.IsNull;
                }

                return false;
            }
        }
    }

    internal struct TdsDateTime
    {
        public int days;
        public int time;
    }
}