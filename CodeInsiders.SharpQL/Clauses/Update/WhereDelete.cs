// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WhereDelete.cs" company="CODE Insiders LTD">
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

    using CodeInsiders.SharpQL.Helpers.Internal;

    public class WhereDelete : Clause<DeleteStatement>
    {
        public Predicate SearhCondition { get; private set; }

        public WhereDelete(DeleteStatement statement, Predicate searhCondition)
            : base(statement) {
            if (searhCondition == null) {
                throw new ArgumentNullException("searhCondition");
            }
            this.SearhCondition = searhCondition;
        }

        public override void Build(SqlFragment parent, TSqlVisitor visitor) {
            visitor.WhereDelete(parent, this);
        }
    }
}