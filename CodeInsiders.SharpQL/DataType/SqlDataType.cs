// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SqlDataType.cs" company="CODE Insiders LTD">
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
namespace CodeInsiders.SharpQL.DataType
{
    public abstract class SqlDataType : SqlFragment
    {
        public static readonly SqlDataType Bigint = new BigintDataType();
        public static readonly SqlDataType Int = new IntDataType();
        public static readonly SqlDataType Bit = new BitDataType();
        public static readonly SqlDataType Date = new DateDataType();
        public static readonly SqlDataType DateTime = new DateTimeDataType();
        public static readonly SqlDataType DateTimeOffset = new DateTimeOffset();

        public string DataTypeSqlScript { get; private set; }
        public int? Length { get; set; }

        protected SqlDataType(string dataTypeSqlScript, int? length = null) {
            this.DataTypeSqlScript = dataTypeSqlScript;
            this.Length = length;
        }

        public static SqlDataType NVarchar(uint n) {
            return new NVarcharDataType(n);
        }

        public static SqlDataType Varbinary(uint n) {
            return new VarbinaryDataType(n);
        }

        public override void Build(SqlFragment parent, TSqlVisitor visitor) {
            visitor.SqlDataType(parent, this);
        }
    }
}