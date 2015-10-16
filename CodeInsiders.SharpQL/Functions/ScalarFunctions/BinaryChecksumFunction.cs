// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BinaryChecksumAggregateFunction.cs" company="CODE Insiders LTD">
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

namespace CodeInsiders.SharpQL.Functions.AggregateFunctions
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Returns the binary checksum value computed over a row of a table or over a list of expressions. BINARY_CHECKSUM can be used to detect changes to a row of a table.
    ///  </summary>
    public class BinaryChecksumFunction : Function
    {
        public IEnumerable<Expression> Arguments { get; private set; }

        public BinaryChecksumFunction(IEnumerable<Expression> arguments) {
            if (arguments == null) {
                throw new ArgumentNullException("arguments");
            }
            this.Arguments = arguments;
        }

        public override void Build(SqlFragment parent, TSqlVisitor visitor) {
            visitor.BinaryChecksumAggregateFunction(parent, this);
        }
    }
}