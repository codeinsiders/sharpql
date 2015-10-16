// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SimpleFunction.cs" company="CODE Insiders LTD">
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

    public class SimpleFunction : Function
    {
        public IEnumerable<Expression> Arguments { get; private set; }
        public string FunctionName { get; private set; }

        public SimpleFunction(string functionName, IEnumerable<Expression> arguments) {
            if (functionName == null) {
                throw new ArgumentNullException("functionName");
            }
            if (arguments == null) {
                throw new ArgumentNullException("arguments");
            }
            this.FunctionName = functionName;
            this.Arguments = arguments;
        }

        public SimpleFunction(string functionName)
            : this(functionName, new Expression[0]) {
            if (functionName == null) {
                throw new ArgumentNullException("functionName");
            }
        }

        public SimpleFunction(string functionName, Expression argument)
            : this(functionName, new[] { argument }) {
            if (functionName == null) {
                throw new ArgumentNullException("functionName");
            }
            if (argument == null) {
                throw new ArgumentNullException("argument");
            }
        }

        public SimpleFunction(string functionName, params Expression[] arguments)
            : this(functionName, (IEnumerable<Expression>)arguments) {
            if (functionName == null) {
                throw new ArgumentNullException("functionName");
            }
        }

        public override void Build(SqlFragment parent, TSqlVisitor visitor) {
            visitor.SimpleFunction(parent, this);
        }
    }
}