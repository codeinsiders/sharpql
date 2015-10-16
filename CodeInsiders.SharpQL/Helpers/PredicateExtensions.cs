// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PredicateExtensions.cs" company="CODE Insiders LTD">
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
namespace CodeInsiders.SharpQL
{
    using System;

    using CodeInsiders.SharpQL.Helpers.Internal;

    public static class PredicateExtensions
    {
        public static Predicate AndIf(this Predicate predicate, Func<bool> condition, Predicate rightPart) {
            if (condition()) {
                return predicate & rightPart;
            }

            return predicate;
        }

        public static Predicate AndIfNotNull<T>(this Predicate predicate, T value, Func<T, Predicate> handler) {
            if (value != null) {
                return predicate & handler(value);
            }

            return predicate;
        }

        public static Predicate AndIfNotNullOrWhitespace<T>(
            this Predicate predicate,
            T value,
            Func<T, Predicate> handler) {
            if (value != null) {
                if (!StringHelper.IsNullOrWhiteSpace(value.ToString())) {
                    return predicate & handler(value);
                }
            }

            return predicate;
        }

        public static Predicate OrIfNotNull<T>(this Predicate predicate, T value, Func<T, Predicate> handler) {
            if (value != null) {
                return predicate & handler(value);
            }

            return predicate;
        }

        public static Predicate OrIfNotNullOrWhitespace<T>(
            this Predicate predicate,
            T value,
            Func<T, Predicate> handler) {
            if (value != null) {
                if (!StringHelper.IsNullOrWhiteSpace(value.ToString())) {
                    return predicate & handler(value);
                }
            }

            return predicate;
        }
    }
}