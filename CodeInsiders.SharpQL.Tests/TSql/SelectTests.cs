// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SelectTests.cs" company="CODE Insiders LTD">
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
    using System;

    using CodeInsiders.SharpQL.Doc.TestMockTables;

    using NUnit.Framework;

    [TestFixture]
    public class SelectTests
    {
        [Test]
        [Url("http://codeinsiders.net/sharpql/doc/tsql/select/select-single-column-from-table")]
        public void SelectSingleColumnFromTable() {
            /*? <span class='title'>Example</span>
             * **Description: Returns whether event.stopImmediatePropagation() was ever called on this event object.
             * <span>This method does not accept any arguments.</span>
             * Checks whether event.stopImmediatePropagation() was called.
             * 
             */
            var u = new UserTable();
            var q = new XQuery();

            q.Select(u.Name).From(u).EndStatement();

            var script = q.ToString();

            TSqlAssert.ScriptsAreEqual(script, @"
SELECT [dbo].[User].[Name] 
FROM [dbo].[User]
");
        }

        [Test]
        public void SelectAllColumnsFromTable() {
            var q = new XQuery();
            var u = new UserTable();

            q.Select(u.AllColumns).From(u).EndStatement();

            TSqlAssert.ScriptsAreEqual(q.ToString(), @"
SELECT
    [dbo].[User].[Id]
  , [dbo].[User].[Name]
  , [dbo].[User].[Email]
 FROM [dbo].[User]
");
        }

        [Test]
        public void SelectSingleColumnFromAliasedTable() {
            var u = new UserTable("u");
            var q = new XQuery();

            q.Select(u.Name).From(u).EndStatement();

            var script = q.ToString();

            TSqlAssert.ScriptsAreEqual(script, @"
SELECT [u].[Name] 
FROM [dbo].[User] AS [u]
");
        }

        [Test]
        public void SelectAliasedColumnFromTable() {
            var u = new UserTable();
            var q = new XQuery();

            q.Select(u.Name.As("Username")).From(u).EndStatement();

            var script = q.ToString();
            TSqlAssert.ScriptsAreEqual(script, @"
SELECT [dbo].[User].[Name] AS [Username] 
FROM [dbo].[User]
");
        }

        [Test]
        public void SelectWithWhere() {
            var u = new UserTable();
            var q = new XQuery();

            q.Select(u.Name).From(u).Where(u.Id.IsEqualTo(1)).EndStatement();

            var script = q.ToString();
            Console.WriteLine(script);

            TSqlAssert.ScriptsAreEqual(script, @"
SELECT
    [dbo].[User].[Name]
FROM [dbo].[User]
WHERE
    [dbo].[User].[Id] =  @p0 
");
        }

        [Test]
        public void Having() {
            var q = new XQuery();
            var u = new UserTable();
            var p = new PostTable();

            int userId = 1;
            q.Select(u.Id, Sql.Count(1))
                .From(u)
                .InnerJoin(p, u.Id.IsEqualTo(p.UserId))
                .Where(u.Id.IsEqualTo(userId)
                       & p.PostDate.IsGreaterThanOrEqualTo(DateTime.Now.AddDays(-7)))
                .GroupBy(u.Id)
                .Having(Sql.Count(1).IsGreaterThan(10))
                .EndStatement();

            TSqlAssert.ScriptsAreEqual(q.ToString(), @"
SELECT   [dbo].[User].[Id],
         COUNT(@p0)
FROM     [dbo].[User]
         INNER JOIN
         [dbo].[Post]
         ON [dbo].[User].[Id] = [dbo].[Post].[UserId]
WHERE    ([dbo].[User].[Id] = @p0
          AND [dbo].[Post].[PostDate] >= @p1)
GROUP BY [dbo].[User].[Id]
HAVING   COUNT(@p0) > @p2;
");
        }

        [Test]
        public void OrderBy() {
            var q = new XQuery();
            var u = new UserTable();

            q.Select(u.AllColumns).From(u).EndStatement();
            q.OrderByAsc(u.Id, u.Name)
                .OrderByDesc(u.Email).EndStatement();

            TSqlAssert.ScriptsAreEqual(q.ToString(), @"
SELECT
    [dbo].[User].[Id]
  , [dbo].[User].[Name]
  , [dbo].[User].[Email]
 FROM [dbo].[User]
ORDER BY
    [dbo].[User].[Id] ASC 
  , [dbo].[User].[Name] ASC 
  ,     [dbo].[User].[Email] DESC 
");
        }

        [Test]
        public void OrderBy2() {
            var q = new XQuery();
            var u = new UserTable();

            q.Select(u.AllColumns).From(u).EndStatement();
            q.OrderByAsc(u.Id)
                .OrderByDesc(u.Name)
                .OrderByAsc(u.Email)
                .EndStatement();

            TSqlAssert.ScriptsAreEqual(q.ToString(), @"
SELECT
    [dbo].[User].[Id]
  , [dbo].[User].[Name]
  , [dbo].[User].[Email]
 FROM [dbo].[User]
ORDER BY
    [dbo].[User].[Id] ASC 
  ,     [dbo].[User].[Name] DESC 
  ,     [dbo].[User].[Email] ASC 
");
        }
    }
}