// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Collection.cs" company="CODE Insiders LTD">
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
namespace CodeInsiders.SharpQL.Helpers.Internal
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    internal class Collection<T> : IEnumerable<T>
    {
        private readonly T val1;
        private readonly IEnumerable<T> values;

        private Collection(T val1, IEnumerable<T> values) {
            if (val1 == null) {
                throw new ArgumentException("expr1");
            }

            this.val1 = val1;
            this.values = values;
        }

        private Collection(T val1, params T[] exprs)
            : this(val1, (IEnumerable<T>)exprs) {}

        public IEnumerator<T> GetEnumerator() {
            yield return this.val1;

            if (this.values == null) {
                yield break;
            }

            foreach (var expr in this.values) {
                yield return expr;
            }
        }

        IEnumerator IEnumerable.GetEnumerator() {
            return this.GetEnumerator();
        }

        public static IEnumerable<T> From(T val1, params T[] values) {
            return new Collection<T>(val1, values);
        }

        public static IEnumerable<T> From(T val1, IEnumerable<T> values) {
            return new Collection<T>(val1, values);
        }

        public static IEnumerable<T> From(T val1, T val2) {
            return new Collection<T>(val1, val2);
        }

        public static IEnumerable<T> From(T val1, T val2, T val3) {
            return new Collection<T>(val1, val2, val3);
        }
    }
}