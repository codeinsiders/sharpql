﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StatementBase.cs" company="CODE Insiders LTD">
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

    public abstract class StatementBase : IStatement
    {
        protected StatementBase(SharpBatch batch) {
            if (batch == null) {
                throw new ArgumentNullException("batch");
            }
            this.Batch = batch;
        }

        public SharpBatch Batch { get; private set; }

        public SqlFragment FirstFragment { get; set; }

        public SharpBatch End() {
            this.Batch.AppendStatement(this);
            return this.Batch;
        }
    }
}