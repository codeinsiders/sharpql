// --------------------------------------------------------------------------------------------------------------------
// <copyright file="InsertUsage.cs" company="CODE Insiders LTD">
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
    public class InsertUsage
    {
        [Test]
        public void InsertWithMultipleValues() {
            var u = new UserTable();

            var q = new XQuery();
            q.InsertInto(u, u.Id, u.Name, u.Email)
                .Values(1, "a", "b")
                .Values(2, "a", "b")
                .Values(3, "a", "b")
                .EndStatement();

            var str = q.ToString();

        }
    }
}