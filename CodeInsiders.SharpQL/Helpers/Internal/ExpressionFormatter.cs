// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ExpressionFormatter.cs" company="CODE Insiders LTD">
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
    using System.Collections.Generic;

    internal static class ExpressionFormatter
    {
        public static void FormatColumnList(
            IEnumerable<Column> columns,
            SqlFragment parent,
            ScriptBuilder scriptBuilder, TSqlVisitor visitor) {
            int i = 0;
            foreach (Column column in columns) {
                if (column == null) {
                    continue;
                }

                AppendDelimiter(scriptBuilder, ref i);
                scriptBuilder.AppendFragment(column, parent, visitor);
            }
        }

        public static void FormatExpressionList(
            IEnumerable<Expression> expressions,
            SqlFragment parent,
            ScriptBuilder scriptBuilder,
            TSqlVisitor visitor,
            string appendOptional = null) {
            int i = 0;
            foreach (Expression expression in expressions) {
                if (expression == null) {
                    continue;
                }

                AppendDelimiter(scriptBuilder, ref i);
                scriptBuilder.AppendFragment(expression, parent, visitor);

                if (appendOptional != null) {
                    scriptBuilder.Append(appendOptional);
                }
            }
        }

        public static void FormatAssignList(Assignment[] assignArray, SqlFragment parent, ScriptBuilder scriptBuilder, TSqlVisitor visitor) {
            int i = 0;
            foreach (Assignment assign in assignArray) {
                if (assign == null) {
                    continue;
                }

                AppendDelimiter(scriptBuilder, ref i);
                scriptBuilder.AppendFragment(assign, parent, visitor);
            }
        }

        private static void AppendDelimiter(ScriptBuilder scriptBuilder, ref int i) {
            if (i++ == 0) {
                scriptBuilder.Append("  ");
            }
            else {
                scriptBuilder.AppendLine();
                scriptBuilder.Append(" ,");
            }
        }
    }
}