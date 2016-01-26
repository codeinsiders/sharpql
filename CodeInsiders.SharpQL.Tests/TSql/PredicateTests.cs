// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PredicateTests.cs" company="CODE Insiders LTD">
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
    public class PredicateTests
    {
        [Test]
        public void FalsePredicate() {
            var q = new SharpQuery();
            var u = new UserTable();
            q.Select(1).From(u).Where(Predicate.FALSE).EndStatement();

            TSqlAssert.ScriptsAreEqual(q.ToString(), @"
SELECT @p0
FROM   [dbo].[User]
WHERE  1 <> 1;
");
        }

        [Test]
        public void Between() {
            var q = new SharpQuery();
            q.Select(1).Where(ConstantExpression.GetConstant(4).IsBetween(3, 5)).EndStatement();
            TSqlAssert.ScriptsAreEqual(q.ToString(), @"
SELECT @p0
WHERE  (@p1 BETWEEN @p2 AND @p3)
");
        }

        [Test]
        public void BetweenMustThrowOnNullArgument1() {
            var q = new SharpQuery();
            Assert.Throws<ArgumentNullException>(() => { q.Select(1).Where(Expression.GetConstant(2).IsBetween(null, 1)); });
        }

        [Test]
        public void BetweenMustThrowOnNullArgument2() {
            var q = new SharpQuery();
            Assert.Throws<ArgumentNullException>(() => q.Select(1).Where(Expression.GetConstant(2).IsBetween(1, null)));
        }

        [Test]
        public void BetweenMustThrowOnNullArgument3() {
            var q = new SharpQuery();
            var expr = (Expression)null;
            Assert.Throws<ArgumentNullException>(() => q.Select(1).Where(expr.IsBetween(1, 2)));
        }

        [Test]
        public void NotIn() {
            var q = new SharpQuery();
            q.Select(1).Where(ConstantExpression.GetConstant(1).IsNotIn(2, 3, 4, 5, 6, 7)).EndStatement();
            TSqlAssert.ScriptsAreEqual(q.ToString(), @"
SELECT @p0
WHERE  @p0 NOT IN (@p1, @p2, @p3, @p4, @p5,@p6)
");
        }

        [Test]
        public void NotInSelect() {
            var q = new SharpQuery();
            var singleExprSelectStatement = q.Select(1).Statement;
            q.Select(1).Where(ConstantExpression.GetConstant(2).IsNotIn(singleExprSelectStatement)).EndStatement();

            TSqlAssert.ScriptsAreEqual(q.ToString(), @"
SELECT @p0
WHERE  @p1 NOT IN (SELECT @p0)
");
        }

        [Test]
        public void IsNotNull() {
            var q = new SharpQuery();

            q.Select(1).Where(ConstantExpression.GetConstant(1).IsNotNull()).EndStatement();
            TSqlAssert.ScriptsAreEqual(q.ToString(), @"
SELECT @p0
WHERE  @p0 IS NOT NULL
");
        }

        [Test]
        public void In() {
            var q = new SharpQuery();
            q.Select(1).Where(ConstantExpression.GetConstant(1).IsIn(1, 2, 3, 4, 5)).EndStatement();
            TSqlAssert.ScriptsAreEqual(q.ToString(), @"
SELECT @p0
WHERE  @p0 IN (@p0, @p1, @p2, @p3, @p4)
");
        }

        [Test]
        public void InWithSelect() {
            var q = new SharpQuery();
            var singleExprSelectStatement = q.Select(1).Statement;
            q.Select(1).Where(ConstantExpression.GetConstant(2).IsIn(singleExprSelectStatement)).EndStatement();

            TSqlAssert.ScriptsAreEqual(q.ToString(), @"
SELECT @p0
WHERE  @p1 IN (SELECT @p0)
");
        }

        [Test]
        public void IsNull() {
            var q = new SharpQuery();
            q.Select(1).Where(ConstantExpression.NULL.IsNull()).EndStatement();
            TSqlAssert.ScriptsAreEqual(q.ToString(), @"
SELECT @p0
WHERE  NULL IS NULL
");
        }

        [Test]
        public void GreaterThanOrEqualTo() {
            var q = new SharpQuery();
            q.Select(1).Where(ConstantExpression.GetConstant(1).IsGreaterThanOrEqualTo(1)).EndStatement();
            TSqlAssert.ScriptsAreEqual(q.ToString(), @"
SELECT @p0
WHERE  @p0 >= @p0
");
        }

        [Test]
        public void GreaterThan() {
            var q = new SharpQuery();
            q.Select(1).Where(ConstantExpression.GetConstant(1).IsGreaterThan(1)).EndStatement();
            TSqlAssert.ScriptsAreEqual(q.ToString(), @"
SELECT @p0
WHERE  @p0 > @p0
");
        }

        [Test]
        public void NotEqualTo() {
            var q = new SharpQuery();
            q.Select(1).Where(ConstantExpression.GetConstant(1).IsNotEqualTo(2)).EndStatement();
            TSqlAssert.ScriptsAreEqual(q.ToString(), @"
SELECT @p0
WHERE  @p0 != @p1
");
        }

        [Test]
        public void Like() {
            var q = new SharpQuery();
            q.Select(1).Where(ConstantExpression.GetConstant(1).IsLike(1)).EndStatement();
            TSqlAssert.ScriptsAreEqual(q.ToString(), @"
SELECT @p0
WHERE  @p0 LIKE @p0
");
        }

        [Test]
        public void LessThan() {
            var q = new SharpQuery();
            q.Select(1).Where(ConstantExpression.GetConstant(1).IsLessThan(2)).EndStatement();
            TSqlAssert.ScriptsAreEqual(q.ToString(), @"
SELECT @p0
WHERE  @p0 < @p1
");
        }

        [Test]
        public void LessThanOrEqualTo() {
            var q = new SharpQuery();
            q.Select(1).Where(ConstantExpression.GetConstant(1).IsLessThanOrEqualTo(1)).EndStatement();
            TSqlAssert.ScriptsAreEqual(q.ToString(), @"
SELECT
     @p0 WHERE
 @p0  <=  @p0 
");
        }

        [Test]
        public void Or() {
            var q = new SharpQuery();
            var a = ConstantExpression.GetConstant(1);
            var b = ConstantExpression.GetConstant(2);

            q.Select(1).Where(a.IsEqualTo(1) | b.IsEqualTo(3)).EndStatement();

            var actual = q.ToString();
            TSqlAssert.ScriptsAreEqual(actual, @"
SELECT @p0
WHERE  (@p0 = @p0
        OR @p1 = @p2)
");
        }

        [Test]
        public void And() {
            var q = new SharpQuery();
            var a = ConstantExpression.GetConstant(1);
            var b = ConstantExpression.GetConstant(2);

            q.Select(1).Where(a.IsEqualTo(1) & b.IsEqualTo(3)).EndStatement();

            var actual = q.ToString();
            TSqlAssert.ScriptsAreEqual(actual, @"
SELECT @p0
WHERE  (@p0 = @p0
        AND @p1 = @p2)
");
        }

        [Test]
        public void Contains() {
            var q = new SharpQuery();
            var a = ConstantExpression.GetConstant("code insiders");
            q.Select(1).Where(a.Contains("code")).EndStatement();
            var actual = q.ToString();
            TSqlAssert.ScriptsAreEqual(actual, @"
SELECT @p0
WHERE  @p1 LIKE ((@p2 + @p3) + @p2)
");
        }

        [Test]
        public void EndsWith() {
            var q = new SharpQuery();
            var a = ConstantExpression.GetConstant("code insiders");
            q.Select(1).Where(a.EndsWith("insiders")).EndStatement();
            var actual = q.ToString();
            TSqlAssert.ScriptsAreEqual(actual, @"
SELECT @p0
WHERE  @p1 LIKE (@p2 + @p3)
");
        }

        [Test]
        public void NotExists() {
            var q = new SharpQuery();

            var subQuery = q.Select(1).Statement;
            q.Select(1).Where(Predicate.NotExists(subQuery)).EndStatement();
            var actual = q.ToString();
            TSqlAssert.ScriptsAreEqual(actual, @"
SELECT
     @p0 
WHERE NOT EXISTS( SELECT @p0 ) 
");
        }

        [Test]
        public void NotExistShouldThrowOnNull() {
            var q = new SharpQuery();
            Assert.Throws<ArgumentNullException>(() => q.Select(1).Where(Predicate.NotExists(null)).EndStatement());
        }

        [Test]
        public void Exists() {
            var q = new SharpQuery();
            var subQuery = q.Select(1).Statement;
            q.Select(1).Where(Predicate.Exists(subQuery)).EndStatement();
            var actual = q.ToString();
            TSqlAssert.ScriptsAreEqual(actual, @"
SELECT
     @p0 
WHERE EXISTS( SELECT @p0 ) 
");
        }

        [Test]
        public void ExistsShouldThrowOnNullArgument() {
            var q = new SharpQuery();
            Assert.Throws<ArgumentNullException>(() => q.Select(1).Where(Predicate.Exists(null)).EndStatement());
        }

        [Test]
        public void SelectMustThrowOnNullExpression() {
            var q = new SharpQuery();
            Assert.Throws<ArgumentNullException>(() => q.Select(null, 1, 2, 3));
        }
    }
}