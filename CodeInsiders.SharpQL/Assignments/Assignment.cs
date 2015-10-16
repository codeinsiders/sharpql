// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Assignment.cs" company="CODE Insiders LTD">
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

    public abstract class Assignment : SqlFragment
    {
        public Column Column { get; private set; }

        public Expression Expression { get; private set; }

        protected Assignment(Column column, Expression expression) {
            if (column == null) {
                throw new ArgumentNullException("column");
            }

            if (expression == null) {
                string message = string.Format(
                    "Cannot set null expression on table [{0}], column [{1}]",
                    column.Table.TableName,
                    column.ColumnName);

                throw new SharpQLNullValueAssignmentException(message);
            }

            this.Column = column;
            this.Expression = expression;
        }

        public static Expression DEFAULT
        {
            get
            {
                return new DefaultValueExpression();
            }
        }
    }
}