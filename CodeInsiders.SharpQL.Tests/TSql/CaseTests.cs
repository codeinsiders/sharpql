// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CaseTests.cs" company="CODE Insiders LTD">
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
    using NUnit.Framework;

    [TestFixture]
    public class CaseTests
    {
        [Test]
        public void SimpleCase() {
            var q = new SharpQuery();
            SimpleCase c = new SimpleCase(1);

            c.When(1, 2).When(3, 4).Else(5);
            q.Select(c).EndStatement();

            var script = q.ToString();

            TSqlAssert.ScriptsAreEqual(script, @"
SELECT
    ( CASE  @p0 
 WHEN  @p0  THEN  @p1 
 WHEN  @p2  THEN  @p3 
 ELSE  @p4 
 END )
");
        }

        [Test]
        public void SimpleCase2() {
            var q = new SharpQuery();
            q.Select(q.Case(1).When(1, 2).When(3, 4).When(5, 6).Else(7)).EndStatement();
            TSqlAssert.ScriptsAreEqual(q.ToString(), @"
SELECT
    ( CASE  @p0 
 WHEN  @p0  THEN  @p1 
 WHEN  @p2  THEN  @p3 
 WHEN  @p4  THEN  @p5 
 ELSE  @p6 
 END )
");
        }

        [Test]
        public void SearchedCase() {
            var q = new SharpQuery();

            var sc = new SearchedCase();
            sc.CaseWhen(((Expression)1).IsEqualTo(1), 1)
                .CaseWhen(((Expression)2).IsEqualTo(3), 4)
                .Else(5);

            q.Select(sc).EndStatement();

            var script = q.ToString();

            TSqlAssert.ScriptsAreEqual(script, @"
SELECT
     (CASE  WHEN  @p0  =  @p0  THEN  @p0 
 WHEN  @p1  =  @p2  THEN  @p3 
 ELSE  @p4 
 END) 

");
        }

        [Test]
        public void SearchedCase2() {
            var q = new SharpQuery();
            var v = ConstantExpression.GetConstant(1);
            var p = ConstantExpression.GetConstant(2);
            q.Select(q
                .CaseWhen(v.IsBetween(2, 3), 3)
                .CaseWhen(v.IsEqualTo(5), 4)
                .CaseWhen(p.IsEqualTo(v), 10)
                .Else(p))
                .EndStatement();

            TSqlAssert.ScriptsAreEqual(q.ToString(), @"
SELECT
     (CASE  WHEN ( @p0  BETWEEN  @p1  AND  @p2 ) THEN  @p2 
 WHEN  @p0  =  @p3  THEN  @p4 
 WHEN  @p1  =  @p0  THEN  @p5 
 ELSE  @p1 
 END) 
");
        }
    }
}