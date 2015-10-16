﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SetUpdate.cs" company="CODE Insiders LTD">
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

    public class SetUpdate : Clause<UpdateStatement>
    {
        public Assignment[] AssingList { get; private set; }

        public SetUpdate(UpdateStatement statement, Assignment[] assingList)
            : base(statement) {
            if (statement == null) {
                throw new ArgumentNullException("statement");
            }
            if (assingList == null) {
                throw new ArgumentNullException("assingList");
            }
            this.AssingList = assingList;
        }

        public override void Build(SqlFragment parent, TSqlVisitor visitor) {
            visitor.SetUpdate(parent, this);
        }

        public FromClause<UpdateStatement> From(Table table) {
            return this.NextClause(new FromClause<UpdateStatement>(this.Statement, table));
        }

        public WhereClause<UpdateStatement> Where(Predicate searchCondition) {
            return this.NextClause(new WhereClause<UpdateStatement>(this.Statement, searchCondition));
        }
    }
}