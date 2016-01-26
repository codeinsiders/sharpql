#region ns meta
//@/DocName:Overview
//@/DocDescription:Overview of SharpQL library
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#region ns md

// # SharpQL Documentation #
//
// ## What is SharpQL? ##
// SharpQL is a SQL script builder. It has fluent API syntax that makes generating dynamic SQL scripts in code dramatically easier. 
// It's open source .NET library written entirely in C#.
// SharpQL API syntax closely mimics SQL syntax making writing queries a joy.
// Currently it supports T-SQL

#endregion

namespace CodeInsiders.SharpQL.Docs
{
    using System.Data.SqlClient;

    using CodeInsiders.SharpQL.Doc.TestMockTables;

    public class Index
    {
        public string Email;
        public DateTime? Date;

        #region ns md

        // ## Why use it? ##
        // There are cases when ORM just can't handle the complexity of the stuff that needs to be done. Or may be you are a true DBA
        // that preffers to write his/her own queries and you do not want to rely on some custom tool to come up with the SQL that
        // is hitting the database.
        // If you find yourself using strings to construct your SQL queries in code SharpQL will become your best friend.

        // It's sole purpose is to avoid writing code like this:

        #endregion

        #region ns code

        public void CodeUsingStrings() {
            using (var connection = new SqlConnection("connectionString")) {
                var cmd = connection.CreateCommand();
                var query = @"
SELECT  [dbo].[Post].[Id] ,
        [dbo].[Post].[Title] ,
        [dbo].[Post].[Text] ,
        [dbo].[Post].[PostDate] ,
        [dbo].[User].[Email] AS UserMail
FROM    dbo.Post
        INNER JOIN dbo.[User] ON [User].Id = Post.Id
WHERE   1 = 1
";
                if (string.IsNullOrWhiteSpace(this.Email)) {
                    query += " AND [dbo].[User].[Email] = @email";
                    cmd.Parameters.AddWithValue("@mail", this.Email);
                }

                if (this.Date.HasValue) {
                    query += " AND [dbo].[Post].[PostDate] >= @date";
                    cmd.Parameters.AddWithValue("@date", this.Date.Value);
                }
                query += " ORDER BY [dbo].[Post].[PostDate] DESC";
                cmd.CommandText = query;
                using (var reader = cmd.ExecuteReader()) {
                    // code to read data goes here
                }
            }
        }

        #endregion

        #region ns md

        // You know for sure that the previous code sample is very error prone. 
        // It is hard to understand and hard to extend. If you need to change it you'll most likely break something and you'll find out in runtime instead of compile time.
        //
        // Here is the equivalent code written in SharpQL

        #endregion

        #region ns code

        public void CodeUsingSharpQL() {
            var u = new UserTable();
            var p = new PostTable();

            var q = new SharpQuery();
            var condition = Predicate.TRUE;
            if (string.IsNullOrWhiteSpace(this.Email)) {
                condition &= u.Email.IsEqualTo(this.Email);
            }

            if (this.Date.HasValue) {
                condition &= p.PostDate.IsGreaterThanOrEqualTo(this.Date);
            }

            q.Select(p.Id, p.Title, p.Text, p.PostDate, u.Email.As("UserMail"))
                .From(p)
                .InnerJoin(u, p.UserId.IsEqualTo(u.Id))
                .Where(condition)
                .EndStatement()
                .OrderByDesc(p.PostDate)
                .EndStatement();

            using (var connection = new SqlConnection("connectionString")) {
                var cmd = q.CreateCommand(connection);
                using (var reader = cmd.ExecuteReader()) {
                    // code to read data goes here
                }
            }
        }

        #endregion

        #region ns md

        // Here is an incomplete list of the *advantages* that SharpQL has
        //
        // * It is predictable. You know exactly what SQL script will be generated.
        // * It benefits from building the SQL in a strongly typed manner.
        // * It protects you from making SQL syntax errors.
        // * It protects you from introducing sql injection weak spots.
        // * If you know how to program in SQL you already know how to use SharpQL.
        // * You can greatly benefit from 3rd party data access libraries like Dapper, PetaPoco, Massive and many others.
        //

        #endregion

    }
}


#region ns md
// ## Who uses SharpQL? ##
// SharpQL is currently known to be in production at:
//
// * [codeinsiders.net](https://codeinsiders.net "CODE Insiders")
// * [physicsfreax.com](https://physicsfreax.com/ "Physics Freax")
// * [Rapid Tests Systems](http://www.rapidtestsystems.com.au "Rapid Test Systems")
// * [Cashflow7](https://cashflow7.com "Cashflow7")

#endregion