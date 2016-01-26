// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DapperExtensions.cs" company="CODE Insiders LTD">
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

using System.Threading.Tasks;

namespace CodeInsiders.SharpQL.DapperExtensions
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;

    using Dapper;

    public static class DapperExtensions
    {
        public static IEnumerable<T> Query<T>(this IDbConnection connection, SharpQuery query, IDbTransaction tran = null) {
            if (connection == null) {
                throw new ArgumentNullException("connection");
            }
            if (query == null) {
                throw new ArgumentNullException("query");
            }

            var commandText = query.ToString();
            var args = query.Parameters;

            if (args.Any() == false) {
                return connection.Query<T>(commandText);
            }
            var parameters = args.ToDictionary(k => k.ParameterName, v => v.Value);
            var commandDefinition = new CommandDefinition(commandText, parameters, tran);
            return connection.Query<T>(commandDefinition);
        }

        public static Task<IEnumerable<T>> QueryAsync<T>(this IDbConnection connection, SharpQuery query) {
            if (connection == null) {
                throw new ArgumentNullException("connection");
            }
            if (query == null) {
                throw new ArgumentNullException("query");
            }

            var commandText = query.ToString();
            var args = query.Parameters;

            if (args.Any() == false) {
                return connection.QueryAsync<T>(commandText);
            }
            var parameters = args.ToDictionary(k => k.ParameterName, v => v.Value);
            var commandDefinition = new CommandDefinition(commandText, parameters);
            return connection.QueryAsync<T>(commandDefinition);
        }

        public static IEnumerable<T> Query<T>(this IDbConnection connection, Func<SharpQuery> queryFactory) {
            if (connection == null) {
                throw new ArgumentNullException("connection");
            }
            if (queryFactory == null) {
                throw new ArgumentNullException("queryFactory");
            }
            var xQuery = queryFactory();
            return Query<T>(connection, xQuery);
        }

        public static Task<IEnumerable<T>> QueryAsync<T>(this IDbConnection connection, Func<SharpQuery> queryFactory) {
            if (connection == null) {
                throw new ArgumentNullException("connection");
            }
            if (queryFactory == null) {
                throw new ArgumentNullException("queryFactory");
            }
            var xQuery = queryFactory();
            return QueryAsync<T>(connection, xQuery);
        }

        public static IEnumerable<T> Query<T>(this IDbConnection connection, Action<SharpQuery> queryFactory) {
            if (connection == null) {
                throw new ArgumentNullException("connection");
            }
            if (queryFactory == null) {
                throw new ArgumentNullException("queryFactory");
            }
            var xQuery = new SharpQuery();
            queryFactory(xQuery);
            return Query<T>(connection, xQuery);
        }

        public static Task<IEnumerable<T>> QueryAsync<T>(this IDbConnection connection, Action<SharpQuery> queryFactory) {
            if (connection == null) {
                throw new ArgumentNullException("connection");
            }
            if (queryFactory == null) {
                throw new ArgumentNullException("queryFactory");
            }
            var xQuery = new SharpQuery();
            queryFactory(xQuery);
            return QueryAsync<T>(connection, xQuery);
        }

        public static void Execute(this IDbConnection connection, SharpQuery query) {
            if (connection == null) {
                throw new ArgumentNullException("connection");
            }
            if (query == null) {
                throw new ArgumentNullException("query");
            }

            var commandText = query.ToString();
            var args = query.Parameters;

            if (args.Any() == false) {
                connection.Execute(commandText);
            }
            var dict = args.ToDictionary(k => k.ParameterName, v => v.Value);
            var commandDefinition = new CommandDefinition(commandText, dict);
            connection.Execute(commandDefinition);
        }

        public static Task ExecuteAsync(this IDbConnection connection, SharpQuery query) {
            if (connection == null) {
                throw new ArgumentNullException("connection");
            }
            if (query == null) {
                throw new ArgumentNullException("query");
            }

            var commandText = query.ToString();
            var args = query.Parameters;

            if (args.Any() == false) {
                connection.Execute(commandText);
            }
            var dict = args.ToDictionary(k => k.ParameterName, v => v.Value);
            var commandDefinition = new CommandDefinition(commandText, dict);
            return connection.ExecuteAsync(commandDefinition);
        }

        public static void Execute(this IDbConnection connection, Func<SharpQuery> queryFactory) {
            if (connection == null) {
                throw new ArgumentNullException("connection");
            }
            if (queryFactory == null) {
                throw new ArgumentNullException("queryFactory");
            }

            var xQuery = queryFactory();
            Execute(connection, xQuery);
        }

        public static Task ExecuteAsync(this IDbConnection connection, Func<SharpQuery> queryFactory) {
            if (connection == null) {
                throw new ArgumentNullException("connection");
            }
            if (queryFactory == null) {
                throw new ArgumentNullException("queryFactory");
            }

            var xQuery = queryFactory();
            return ExecuteAsync(connection, xQuery);
        }

        public static void Execute(this IDbConnection connection, Action<SharpQuery> queryFactory) {
            if (connection == null) {
                throw new ArgumentNullException("connection");
            }
            if (queryFactory == null) {
                throw new ArgumentNullException("queryFactory");
            }

            var xQuery = new SharpQuery();
            queryFactory(xQuery);
            Execute(connection, xQuery);
        }

        public static Task ExecuteAsync(this IDbConnection connection, Action<SharpQuery> queryFactory) {
            if (connection == null) {
                throw new ArgumentNullException("connection");
            }
            if (queryFactory == null) {
                throw new ArgumentNullException("queryFactory");
            }

            var xQuery = new SharpQuery();
            queryFactory(xQuery);
            return ExecuteAsync(connection, xQuery);
        }

        public static T ExecuteScalar<T>(this IDbConnection connection, SharpQuery query) {
            if (connection == null) {
                throw new ArgumentNullException("connection");
            }
            if (query == null) {
                throw new ArgumentNullException("query");
            }

            var commandText = query.ToString();
            var args = query.Parameters;
            if (args.Any() == false) {
                return connection.ExecuteScalar<T>(commandText);
            }
            var dict = args.ToDictionary(k => k.ParameterName, v => v.Value);
            var commandDefinition = new CommandDefinition(commandText, dict);
            return connection.ExecuteScalar<T>(commandDefinition);
        }

        public static Task<T> ExecuteScalarAsync<T>(this IDbConnection connection, SharpQuery query) {
            if (connection == null) {
                throw new ArgumentNullException("connection");
            }
            if (query == null) {
                throw new ArgumentNullException("query");
            }

            var commandText = query.ToString();
            var args = query.Parameters;
            if (args.Any() == false) {
                return connection.ExecuteScalarAsync<T>(commandText);
            }
            var dict = args.ToDictionary(k => k.ParameterName, v => v.Value);
            var commandDefinition = new CommandDefinition(commandText, dict);
            return connection.ExecuteScalarAsync<T>(commandDefinition);
        }

        public static T ExecuteScalar<T>(this IDbConnection connection, Func<SharpQuery> queryFactory) {
            if (connection == null) {
                throw new ArgumentNullException("connection");
            }
            if (queryFactory == null) {
                throw new ArgumentNullException("queryFactory");
            }

            var xQuery = queryFactory();
            return ExecuteScalar<T>(connection, xQuery);
        }

        public static Task<T> ExecuteScalarAsync<T>(this IDbConnection connection, Func<SharpQuery> queryFactory) {
            if (connection == null) {
                throw new ArgumentNullException("connection");
            }
            if (queryFactory == null) {
                throw new ArgumentNullException("queryFactory");
            }

            var xQuery = queryFactory();
            return ExecuteScalarAsync<T>(connection, xQuery);
        }

        public static Task<T> ExecuteScalarAsync<T>(this IDbConnection connection, Action<SharpQuery> queryFactory) {
            if (connection == null) {
                throw new ArgumentNullException("connection");
            }
            if (queryFactory == null) {
                throw new ArgumentNullException("queryFactory");
            }
            var xQuery = new SharpQuery();
            queryFactory(xQuery);
            return ExecuteScalarAsync<T>(connection, xQuery);
        }
    }
}