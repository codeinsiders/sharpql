// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GroupByTests.cs" company="CODE Insiders LTD">
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
namespace CodeInsiders.SharpQL.Doc._TSql
{
    using CodeInsiders.SharpQL.Doc.TestMockTables;

    using NUnit.Framework;

    [TestFixture]
    public class GroupByTests
    {
        [Test]
        public void Test1() {
            var q = new XQuery();
            var u = new UserTable();
            var p = new PostTable();

            q.Select(u.Id, Sql.Count(1))
                .From(u)
                .InnerJoin(p, p.UserId.IsEqualTo(u.Id))
                .GroupBy(u.Id)
                .EndStatement();
            var query = q.ToString();

            TSqlAssert.ScriptsAreEqual(query, @"
SELECT
    [dbo].[User].[Id]
  , COUNT( @p0  ) 
FROM [dbo].[User]
INNER JOIN [dbo].[Post] ON [dbo].[Post].[UserId] = [dbo].[User].[Id]
GROUP BY
    [dbo].[User].[Id]");
        }
    }
}