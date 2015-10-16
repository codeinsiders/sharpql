// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TSqlAssert.cs" company="CODE Insiders LTD">
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
namespace CodeInsiders.SharpQL.Doc
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;

    using Microsoft.SqlServer.TransactSql.ScriptDom;

    using NUnit.Framework;

    // ReSharper disable once InconsistentNaming
    public class TSqlAssert
    {
        public static void ScriptsAreEqual(string actual, string expected) {
            if (actual == null) {
                throw new ArgumentNullException("actual");
            }
            if (expected == null) {
                throw new ArgumentNullException("expected");
            }

            Console.WriteLine("------------  Actual: -------------");
            Console.WriteLine(actual);
            Console.WriteLine("------------ Expected: ------------");
            Console.WriteLine(expected);
            IList<ParseError> errors;
            actual = TSqlHelpers.ParseSqlString(actual, out errors);
            if (errors.Any()) {
                PrintScriptAndErrors(actual, errors);
            }

            CollectionAssert.IsEmpty(errors);

            expected = TSqlHelpers.ParseSqlString(expected, out errors);
            if (errors.Any()) {
                PrintScriptAndErrors(expected, errors);
            }

            CollectionAssert.IsEmpty(errors);
            StringAssert.AreEqualIgnoringCase(expected, actual);
        }

        private static void PrintScriptAndErrors(string actual, IList<ParseError> errors) {
            Console.WriteLine("====================================");
            Console.WriteLine(actual);
            foreach (var e in errors) {
                Console.WriteLine(e.Message);
            }
        }

        // ReSharper disable once InconsistentNaming
        private static class TSqlHelpers
        {
            private static readonly TSqlParser Parser = new TSql110Parser(true);
            private static readonly Sql110ScriptGenerator ScriptGen = new Sql110ScriptGenerator();

            public static string ParseSqlString(string sqlString, out IList<ParseError> errors) {
                using (var sr = new StringReader(sqlString)) {
                    var fragment = Parser.Parse(sr, out errors);

                    if (errors.Any()) {
                        return string.Empty;
                    }

                    var sb = new StringBuilder();
                    using (var strStream = new StringWriter(sb)) {
                        ScriptGen.GenerateScript(fragment, strStream);
                    }

                    return sb.ToString();
                }
            }
        }
    }
}