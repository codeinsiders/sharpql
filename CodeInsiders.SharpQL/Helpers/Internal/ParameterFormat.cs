﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ParameterFormat.cs" company="CODE Insiders LTD">
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
namespace CodeInsiders.SharpQL.Helpers.Internal
{
    internal class ParameterFormat
    {
        private readonly string printableParameterValue;
        private readonly string sqlServerDbEngineTypeString;

        internal ParameterFormat(string sqlServerDbEngineTypeString, string printableParameterValue) {
            this.sqlServerDbEngineTypeString = sqlServerDbEngineTypeString;
            this.printableParameterValue = printableParameterValue;
        }

        public string PrintableParameterValue {
            get {
                return this.printableParameterValue;
            }
        }

        public string SqlServerDbEngineTypeString {
            get {
                return this.sqlServerDbEngineTypeString;
            }
        }
    }
}