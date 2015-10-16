// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AssignListBuilder.cs" company="CODE Insiders LTD">
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
    using System.Linq;
    using System.Reflection;

    public class AssignListBuilder
    {
        private readonly List<Table> tables = new List<Table>();

        private readonly List<Assignment> theList = new List<Assignment>();

        private AssignListBuilder() { }

        public AssignListBuilder(IEnumerable<Table> tables)
        {
            if (tables == null)
            {
                throw new ArgumentNullException("tables");
            }

            this.Tables.AddRange(tables);
        }

        public static AssignListBuilder From(Table table, params Table[] args)
        {
            if (table == null)
            {
                throw new ArgumentNullException("table");
            }

            var builder = new AssignListBuilder();
            builder.Tables.Add(table);
            if (args != null)
            {
                builder.Tables.AddRange(args);
            }

            return builder;
        }

        public static AssignListBuilder From(IEnumerable<Table> tables)
        {
            if (tables == null)
            {
                throw new ArgumentNullException("tables");
            }

            var builder = new AssignListBuilder();
            builder.tables.AddRange(tables);
            return builder;
        }

        public List<Table> Tables
        {
            get
            {
                return this.tables;
            }

            set
            {
                if (ReferenceEquals(value, this.tables))
                {
                    return;
                }

                this.tables.Clear();
                this.tables.AddRange(value);
            }
        }

        private PropertyInfo[] GetTableProperties(Table table)
        {
            return table.GetType().GetProperties().Where(
                p =>
                {
                    var columnType = typeof(Column);
                    return p.PropertyType == columnType;
                }).ToArray();
        }

        public AssignListBuilder BuildForObject(object obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("obj");
            }

            if (this.tables.Any() == false)
            {
                throw new InvalidOperationException("At least one table must be specified");
            }

            var typeOfObject = obj.GetType();
            var propertiesOfObject = typeOfObject.GetProperties().Where(p => p.CanRead).ToArray();
            foreach (var table in this.tables)
            {
                var tp = this.GetTableProperties(table);
                foreach (var po in propertiesOfObject)
                {
                    var tcol = tp.SingleOrDefault(p => p.Name == po.Name);
                    if (tcol != null)
                    {
                        Expression constantExpr;
                        if (Expression.TryGetConstant(po.GetValue(obj), out constantExpr))
                        {
                            var ea = new ExpressionAssign((Column)tcol.GetValue(table), constantExpr);
                            this.Append(ea);
                        }
                    }
                }
            }
            return this;
        }

        public void Append(Assignment expressionAssign)
        {
            if (expressionAssign == null)
            {
                throw new ArgumentNullException("expressionAssign");
            }

            this.theList.Add(expressionAssign);
        }

        public Assignment[] ToArray()
        {
            return this.theList.ToArray();
        }
    }
}