// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Class1.cs" company="CODE Insiders LTD">
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

namespace CodeInsiders.SharpQL.Doc.Usage
{
    using CodeInsiders.SharpQL.Doc.TestMockTables;

    using NUnit.Framework;

    [TestFixture]
    public class Class1
    {
        [Test]
        public void UseCustomFunction() {
            //            var f = new CanDeclareOwnFunction();
            //            var q = new XQuery();
            //
            //            q.Select(f).EndStatement();
            //            TSqlAssert.ScriptsAreEqual(q.ToSqlString(), @"
            //SELECT dbo.fnThisIsMyOwnCustomFunction()
            //");
        }

        [Test]
        public void GetMatchingColumnsFor() {
            var table = new UserTable();

            var cols = table.GetMatchingColumnsFor(typeof(User));

            var expected = new[]
                           {
                               table.Id,
                               table.Email,
                               table.Name
                           };

            CollectionAssert.AreEquivalent(expected, cols);
        }
    }

    internal class User
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
    }
}