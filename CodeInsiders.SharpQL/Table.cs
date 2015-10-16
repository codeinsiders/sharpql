// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Table.cs" company="CODE Insiders LTD">
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

    using CodeInsiders.SharpQL.Helpers.Internal;

    public class Table : TableSource, IEquatable<Table>
    {
        public string Alias { get; private set; }
        public string Scheme { get; private set; }
        public string TableName { get; private set; }
        private readonly Dictionary<string, Column> columns = new Dictionary<string, Column>();

        public Table(string scheme, string tableName, string alias = null) {
            if (StringHelper.IsNullOrWhiteSpace(scheme)) {
                throw new ArgumentException("scheme");
            }

            if (StringHelper.IsNullOrWhiteSpace(tableName)) {
                throw new ArgumentException("tableName");
            }

            if (alias != null && StringHelper.IsNullOrWhiteSpace(alias)) {
                throw new ArgumentException("alias");
            }

            this.Scheme = scheme;
            this.TableName = tableName;
            this.Alias = alias;
        }

        public IEnumerable<Column> AllColumns
        {
            get
            {
                return this.columns.Values;
            }
        }

        public override void Build(SqlFragment parent, TSqlVisitor visitor) {
            visitor.Table(parent, this);
        }

        public Expression[] GetMatchingColumnsFor<T>() {
            return this.GetMatchingColumnsFor(typeof(T));
        }

        public Column this[string columnName]
        {
            get
            {
                if (columnName == null) {
                    throw new ArgumentNullException("columnName");
                }
                Column c;
                if (this.columns.TryGetValue(columnName, out c)) {
                    return c;
                }
                throw new ColumnNotFoundException(this, columnName);
            }
        }

        public Expression[] GetMatchingColumnsFor(Type type) {
            if (type == null) {
                throw new ArgumentNullException("type");
            }

            // TODO introduce plugin for attributes provided by other frameworks that describe colums

            var targetProps = type.GetProperties().Where(p => p.CanWrite).ToArray();
            var selected = new List<Expression>(targetProps.Length);

            foreach (var key in this.columns.Keys) {
                var columnName = key;
                if (targetProps.Any(p => p.Name.Equals(columnName, StringComparison.OrdinalIgnoreCase))) {
                    selected.Add(this.columns[columnName]);
                }
            }

            return selected.ToArray();
        }

        public Column RegisterColumn(string columnName) {
            if (StringHelper.IsNullOrWhiteSpace(columnName)) {
                throw new ArgumentNullException("columnName");
            }

            if (this.columns.ContainsKey(columnName)) {
                var message = string.Format("Column with name {0} already exists for table {1}", columnName, this.TableName);
                throw new InvalidOperationException(message);
            }

            var c = new Column(this, columnName);
            this.columns.Add(c.ColumnName, c);
            return c;
        }

        public void RegisterColumnsDefault() {
            var bindingFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.GetProperty;
            var columnProperties = this.GetType()
                .GetProperties(bindingFlags)
                .Where(p => p.PropertyType == typeof(Column)
                            && p.MemberType == MemberTypes.Property
                            && p.GetIndexParameters().Length == 0
                ).ToArray();
            foreach (var cp in columnProperties) {
                if (cp.GetValue(this) == null) {
                    cp.SetValue(this, this.RegisterColumn(cp.Name));
                }
            }
        }

        public static bool operator ==(Table left, Table right) {
            return Equals(left, right);
        }

        public static bool operator !=(Table left, Table right) {
            return !Equals(left, right);
        }

        public override bool Equals(object obj) {
            if (ReferenceEquals(null, obj)) {
                return false;
            }
            if (ReferenceEquals(this, obj)) {
                return true;
            }
            if (obj.GetType() != this.GetType()) {
                return false;
            }
            return this.Equals((Table)obj);
        }

        public override int GetHashCode() {
            unchecked {
                return ((this.Scheme != null ? this.Scheme.GetHashCode() : 0) * 397) ^ (this.TableName != null ? this.TableName.GetHashCode() : 0);
            }
        }

        public bool Equals(Table other) {
            if (ReferenceEquals(null, other)) {
                return false;
            }
            if (ReferenceEquals(this, other)) {
                return true;
            }
            return string.Equals(this.Scheme, other.Scheme) && string.Equals(this.TableName, other.TableName);
        }

        public override string ToString() {
            return string.Format("{0}.[{1}]", this.Scheme, this.TableName);
        }
    }
}