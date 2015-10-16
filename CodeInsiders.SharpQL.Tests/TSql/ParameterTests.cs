// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ParameterTests.cs" company="CODE Insiders LTD">
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
    public class ParameterTests
    {
        [Test]
        public void Test1() {
            var q = new XQuery();

            q.Select(1).EndStatement();
            var query = q.ToString();
            Assert.That(q.ScriptBuilder.Parameters.Count, Is.EqualTo(1));
        }
    }
}