﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="InsertTests.cs" company="CODE Insiders LTD">
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
    public class InsertTests
    {
        [Test]
        public void InsertIntoTable() {
            var u = new UserTable();
            var q = new XQuery();

            q.InsertInto(u, u.Email, u.Name).Values("test@test.com", "username").EndStatement();

            TSqlAssert.ScriptsAreEqual(q.ToString(), @"
INSERT INTO [dbo].[User](    [dbo].[User].[Email]
  , [dbo].[User].[Name])
VALUES(
     @p0 
  ,  @p1  ) 
");
        }
    }
}