// --------------------------------------------------------------------------------------------------------------------
// <copyright file="QueryTests.cs" company="CODE Insiders LTD">
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
    using CodeInsiders.SharpQL.Doc.TestMockTables;

    using NUnit.Framework;

    [TestFixture]
    public class QueryTests
    {
        [Test]
        public void EmptyQueryReturnsEmptyScript() {
            var q = new XQuery();
            TSqlAssert.ScriptsAreEqual(string.Empty, q.ToString());
        }

        [Test]
        public void NotEndedStatementsShouldNotBeAddedToScript1() {
            var q = new XQuery();
            q.Select(1);
            TSqlAssert.ScriptsAreEqual(string.Empty, q.ToString());
        }

        [Test]
        public void NotEndedStatementsShouldNotBeAddedToScript2() {
            var q = new XQuery();
            q.Select(1);
            q.Select(2).EndStatement();

            var script = q.ToString();
            TSqlAssert.ScriptsAreEqual(script, "SELECT @p0");
        }

        [Test]
        public void EndedStatementsShouldBeAddedToScript() {
            var q = new XQuery();
            q.Select(1).EndStatement();
            var script = q.ToString();
            TSqlAssert.ScriptsAreEqual(script, "SELECT @p0");
        }

        [Test]
        public void QueryAlwaysGeneratesTheSameScript() {
            var q = new XQuery();
            var u = new UserTable();
            var p = new PostTable();

            q.Select(u.Name, u.Email, p.Title).From(u).LeftOuterJoin(p, p.UserId.IsEqualTo(u.Id)).EndStatement();

            var script1 = q.ToString();
            var script2 = q.ToString();

            TSqlAssert.ScriptsAreEqual(script1, script2);
        }
    }
}