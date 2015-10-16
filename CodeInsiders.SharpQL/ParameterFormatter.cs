// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ParameterFormatter.cs" company="CODE Insiders LTD">
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
    using System.Data;
    using System.Data.SqlClient;
    using System.Linq;
    using System.Text;

    using CodeInsiders.SharpQL.Helpers.Internal;

    internal class ParameterFormatter
    {
        internal static ParameterFormat Format(SqlParameter parameter) {
            if (parameter == null) {
                throw new ArgumentNullException("parameter");
            }

            MetaType metaType = MetaType.GetMetaTypeFromValue(parameter.Value);
            object value = parameter.Value;

            var sqlServerDbEngineTypeString = new StringBuilder();
            var printableParameterValue = new StringBuilder();

            sqlServerDbEngineTypeString.Append(metaType.TypeName.ToUpper());

            if (metaType.IsNCharType) {
                string valueAsString = value.ToString();
                sqlServerDbEngineTypeString.AppendFormat("({0})", valueAsString.Length);
            }

            var sqlDbTypesAsStrings = new[]
                                      {
                                          SqlDbType.UniqueIdentifier,
                                          SqlDbType.NChar,
                                          SqlDbType.NText,
                                          SqlDbType.NVarChar,
                                          SqlDbType.Text,
                                          SqlDbType.VarChar,
                                          SqlDbType.Char
                                      };
            var sqlDbTypesWithPoint = new[] { SqlDbType.Money, SqlDbType.Float, SqlDbType.Real, SqlDbType.Decimal };
            var sqlDbTypesDates = new[]
                                  {
                                      SqlDbType.Date,
                                      SqlDbType.DateTime,
                                      SqlDbType.DateTime2,
                                      SqlDbType.SmallDateTime
                                  };

            if (sqlDbTypesAsStrings.Contains(metaType.SqlDbType)) {
                if (metaType.IsNCharType) {
                    printableParameterValue.Append("N");
                }

                printableParameterValue.AppendFormat("'{0}'", SqlEscapeString(value.ToString()));
            }
            else if (sqlDbTypesWithPoint.Contains(metaType.SqlDbType)) {
                printableParameterValue.Append(value);
                printableParameterValue.Replace(',', '.');
            }
            else if (sqlDbTypesDates.Contains(metaType.SqlDbType)) {
                printableParameterValue.AppendFormat(
                    "'{0}'",
                    ((DateTime)value).ToString("yyyy-MM-dd hh:mm:ss"));
            }
            else {
                printableParameterValue.Append(value);
            }

            return new ParameterFormat(sqlServerDbEngineTypeString.ToString(), printableParameterValue.ToString());
        }

        internal static string SqlEscapeString(string value) {
            var sb = new StringBuilder(value.Length);
            foreach (char c in value) {
                if (c == '\'') {
                    sb.Append("''");
                }
                else {
                    sb.Append(c);
                }
            }

            return sb.ToString();
        }
    }
}