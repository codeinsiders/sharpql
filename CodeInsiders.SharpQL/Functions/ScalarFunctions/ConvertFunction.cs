// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ConvertFunction.cs" company="CODE Insiders LTD">
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
namespace CodeInsiders.SharpQL.ScalarFunctions
{
    using System;
    using System.Globalization;

    using CodeInsiders.SharpQL.DataType;

    public class ConvertFunction : Function
    {
        public Expression Value { get; private set; }
        public uint? Style { get; private set; }
        public SqlDataType Type { get; private set; }

        public ConvertFunction(SqlDataType type, Expression exp, uint? style) {
            if (type == null) {
                throw new ArgumentNullException("type");
            }
            if (exp == null) {
                throw new ArgumentNullException("exp");
            }
            this.Type = type;
            this.Value = exp;
            this.Style = style;
        }

        public override void Build(SqlFragment parent, TSqlVisitor visitor) {
            visitor.ConvertFunction(parent, this);

        }
    }
}