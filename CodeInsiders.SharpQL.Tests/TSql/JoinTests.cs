// --------------------------------------------------------------------------------------------------------------------
// <copyright file="JoinTests.cs" company="CODE Insiders LTD">
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
    public class JoinTests
    {
        [Test]
        public void InnerJoin() {
            var u = new UserTable();
            var p = new PostTable();

            var q = new XQuery();

            q.Select(p.AllColumns)
                .From(p)
                .InnerJoin(u, u.Id.IsEqualTo(p.UserId))
                .Where(u.Name.IsEqualTo("Mike"))
                .EndStatement();
            var script = q.ToString();

            TSqlAssert.ScriptsAreEqual(script, @"
SELECT
    [dbo].[Post].[Id]
  , [dbo].[Post].[UserId]
  , [dbo].[Post].[Title]
  , [dbo].[Post].[Text]FROM
[dbo].[Post]
INNER JOIN [dbo].[User] ON [dbo].[User].[Id] = [dbo].[Post].[UserId]WHERE
[dbo].[User].[Name] =  @p0 
");
        }

        [Test]
        public void RightOuterJoin() {
            var u = new UserTable();
            var p = new PostTable();

            var q = new XQuery();

            q.Select(u.Name, p.Title)
                .From(u)
                .RightOuterJoin(p, p.UserId.IsEqualTo(u.Id))
                .EndStatement();

            var script = q.ToString();

            TSqlAssert.ScriptsAreEqual(script, @"
SELECT
    [dbo].[User].[Name]
  , [dbo].[Post].[Title]FROM
[dbo].[User]RIGHT OUTER JOIN
[dbo].[Post] ON [dbo].[Post].[UserId] = [dbo].[User].[Id]
");
        }

        [Test]
        public void MultipleJoins() {
            var u = new UserTable();
            var p1 = new PostTable("p1");
            var p2 = new PostTable("p2");
            var tv = new CustomTableValuedFunction();

            var q = new XQuery();
            q.Select(u.AllColumns)
                .From(u)
                .InnerJoin(p1, p1.UserId.IsEqualTo(u.Id))
                .InnerJoin(p2, Predicate.TRUE)
                .CrossApply(tv, Predicate.TRUE)
                .LeftOuterJoin(p2, Predicate.TRUE)
                .InnerJoin(p2, Predicate.TRUE)
                .CrossApply(tv, Predicate.TRUE)
                .LeftOuterJoin(p2, Predicate.TRUE)
                .InnerJoin(p2, Predicate.TRUE)
                .InnerJoin(p2, Predicate.TRUE)
                .InnerJoin(p2, Predicate.TRUE)
                .InnerJoin(p2, Predicate.TRUE)
                .InnerJoin(p2, Predicate.TRUE)
                .InnerJoin(p2, Predicate.TRUE)
                .EndStatement();
        }
    }
}