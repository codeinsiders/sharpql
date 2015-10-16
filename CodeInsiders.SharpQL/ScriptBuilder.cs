// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ScriptBuilder.cs" company="CODE Insiders LTD">
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
    using System.Data;
    using System.Data.SqlClient;
    using System.Linq;
    using System.Text;

    using CodeInsiders.SharpQL.Helpers;
    using CodeInsiders.SharpQL.Helpers.Internal;

    public class ScriptBuilder
    {
        private readonly XBatch batch;
        private readonly StringBuilder sb = new StringBuilder(512);

        internal ScriptBuilder(XBatch batch) {
            if (batch == null) {
                throw new ArgumentNullException("batch");
            }

            this.batch = batch;
        }

        public StringBuilder Builder
        {
            get
            {
                return this.sb;
            }
        }

        internal ScriptBuilder Append(string value) {
            this.sb.Append(' ');
            this.sb.Append(value);
            this.sb.Append(' ');
            return this;
        }

        internal ScriptBuilder OpenParen() {
            this.sb.Append("(");
            return this;
        }

        internal ScriptBuilder CloseParen() {
            this.sb.Append(")");
            return this;
        }

        public ScriptBuilder AppendFormat(string format, params object[] args) {
            this.sb.AppendFormat(format, args);
            return this;
        }

        public ScriptBuilder Write(string script, params ConstantExpression[] args) {
            var sqlParameters = new object[args.Length];

            for (int i = 0; i < args.Length; i++) {
                var a = args[i];
                var p = this.CreateInputParameter(a.DbType, a.Value, a.Size);
                sqlParameters[i] = p.ParameterName;
            }

            this.AppendLine();
            this.AppendFormat(script, sqlParameters);
            this.Append(" ");
            return this;
        }

        internal ScriptBuilder AppendFragment(SqlFragment fragment, SqlFragment parent, TSqlVisitor visitor) {
            SqlFragment v = parent;
            // TODO check here if this is a correct behaviour
            while (fragment != null) {
                fragment.Build(v, visitor);
                v = fragment;
                fragment = fragment.NextFragment;
            }

            return this;
        }

        public ScriptBuilder AppendLine(string keyword) {
            this.sb.AppendLine(keyword);
            return this;
        }

        public ScriptBuilder LineAppendLine(string script) {
            if (this.sb.Length > 0 && this.sb.EndsWith(Environment.NewLine) == false) {
                this.sb.AppendLine();
            }

            this.sb.AppendLine(script);
            return this;
        }

        public ScriptBuilder LineAppend(string script) {
            if (this.sb.Length > 0) {
                this.sb.AppendLine();
            }

            this.sb.Append(script);
            return this;
        }

        public ScriptBuilder AppendLine() {
            this.sb.AppendLine();
            return this;
        }

        public void Clear() {
            // backward compatible with .NET 3.5 we can use sb.Clear in later versions
            this.sb.Remove(0, this.sb.Length);
            this.parameters.Clear();
        }

        private readonly Dictionary<object, SqlParameter> parameters = new Dictionary<object, SqlParameter>();

        public bool HasParameters
        {
            get
            {
                return this.parameters.Count > 0;
            }
        }

        public IReadOnlyCollection<SqlParameter> Parameters
        {
            get
            {
                return this.parameters.Values.ToArray();
            }
        }

        public SqlParameter CreateInputParameter(DbType dbType, object value, int? size) {
            SqlParameter p;
            if (this.parameters.TryGetValue(value, out p) == false) {
                string paramName = string.Format("@p{0}", this.Parameters.Count());

                p = new SqlParameter(paramName, dbType)
                    {
                        Direction = ParameterDirection.Input, Value = value
                    };

                if (size.HasValue) {
                    p.Size = size.Value;
                }

                this.parameters.Add(value, p);
            }
            return p;
        }

        public void AddSqlParameter(SqlParameter parameter) {
            if (parameter == null) {
                throw new ArgumentNullException("parameter");
            }

            this.parameters.Add(parameter.ParameterName, parameter);
        }

        /// <summary>
        /// Outputs the SQL script as a string
        /// </summary>
        /// <param name="outputParameterDeclarations">
        /// If set to true will place the parameter DECLARE statements on top of the script
        /// and try to escape values where needed.
        /// </param>
        /// <returns>
        /// SQL script as a string
        /// </returns>
        public string ToSqlString(bool outputParameterDeclarations) {
            if (outputParameterDeclarations && this.HasParameters) {
                var tsb = new StringBuilder();

                foreach (SqlParameter sqlParameter in this.Parameters) {
                    ParameterFormat paramInfo = ParameterFormatter.Format(sqlParameter);
                    tsb.AppendFormat(
                        "DECLARE {0} AS {1} = {2};",
                        sqlParameter.ParameterName,
                        paramInfo.SqlServerDbEngineTypeString,
                        paramInfo.PrintableParameterValue);
                    tsb.AppendLine();
                }

                tsb.AppendLine(this.sb.ToString());
                return tsb.ToString();
            }

            return this.sb.ToString();
        }
    }
}