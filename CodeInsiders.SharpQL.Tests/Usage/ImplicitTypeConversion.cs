// --------------------------------------------------------------------------------------------------------------------
// <copyright file="OperatorUsage.cs" company="CODE Insiders LTD">
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
    using System;
    using System.Xml.Linq;

    using NUnit.Framework;

    [TestFixture]
    public class ImplicitTypeConversion
    {
        [Test]
        public void ImplicitlyConvertByteToExpression() {
            Expression e = (byte)1;
            Assert.That(e, Is.Not.Null);
        }

        [Test]
        public void ImplicitlyConvertSByteToExpression() {
            Expression e = (sbyte)-1;
            Assert.That(e, Is.Not.Null);
        }

        [Test]
        public void ImplicitlyConvertShortToExpression() {
            Expression e = (short)1;
            Assert.That(e, Is.Not.Null);
        }

        [Test]
        public void ImplicitlyConvertUShortToExpression() {
            Expression e = (ushort)1;
            Assert.That(e, Is.Not.Null);
        }

        [Test]
        public void ImplicitlyConvertIntToExpression() {
            Expression e = (int)1;
            Assert.That(e, Is.Not.Null);
        }

        [Test]
        public void ImplicitlyConvertUIntToExpression() {
            Expression e = (uint)1;
            Assert.That(e, Is.Not.Null);
        }

        [Test]
        public void ImplicitlyConvertLongToExpression() {
            Expression e = (long)1;
            Assert.That(e, Is.Not.Null);
        }

        [Test]
        public void ImplicitlyConvertULongToExpression() {
            Expression e = (ulong)1;
            Assert.That(e, Is.Not.Null);
        }

        [Test]
        public void ImplicitlyConvertFloatToExpression() {
            Expression e = (float)1.1;
            Assert.That(e, Is.Not.Null);
        }

        [Test]
        public void ImplicitlyConvertDoubleToExpression() {
            Expression e = (double)1.1;
            Assert.That(e, Is.Not.Null);
        }

        [Test]
        public void ImplicitlyConvertDecimalToExpression() {
            Expression e = (decimal)1.1;
            Assert.That(e, Is.Not.Null);
        }

        [Test]
        public void ImplicitlyConvertStringToExpression() {
            Expression e = "Test string";
            Assert.That(e, Is.Not.Null);
        }

        [Test]
        public void ImplicitlyConvertCharToExpression() {
            Expression e = 'c';
            Assert.That(e, Is.Not.Null);
        }

        [Test]
        public void ImplicitlyConvertGuidToExpression() {
            Expression e = Guid.Empty;
            Assert.That(e, Is.Not.Null);
        }

        [Test]
        public void ImplicitlyConvertByteArrayToExpression() {
            Expression e = new byte[] { 1, 2, 3, 4, 5 };
            Assert.That(e, Is.Not.Null);
        }

        [Test]
        public void ImplicitlyConvertBoolToExpression() {
            Expression e = true;
            Assert.That(e, Is.Not.Null);
        }

        [Test]
        public void ImplicitlyConvertDateTimeToExpression() {
            Expression e = DateTime.Now;
            Assert.That(e, Is.Not.Null);
        }

        [Test]
        public void ImplicitlyConvertDateTimeOffsetToExpression() {
            Expression e = new DateTimeOffset(DateTime.Now);
            Assert.That(e, Is.Not.Null);
        }

        [Test]
        public void ImplicitlyConvertXElementToExpression() {
            Expression e = new XElement("Node", "Content");
            Assert.That(e, Is.Not.Null);
        }
    }
}