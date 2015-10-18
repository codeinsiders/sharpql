# SharpQL
[logo]: https://codeinsiders.net/content/img/sharpql-logo.svg "SharpQL logo"
SharpQL is an open source library written in C#.
It's sole purpose is to help you generate dynamic SQL scripts faster and safer.
When used in IDE it's like writing pure SQL with the help of intellisense.
It puts you in complete control of what SQL script will be generated and guards you from syntax errors.
It also manages input parameters for you.

Project Website: http://sharpql.com

The best thing is that if you know SQL you already know how to use SharpQL.

## Simple Demo
```csharp
// First start with classes that describe your database tables
public class UserTable : Table
{
	public UserTable(string alias = null)
		: base("dbo", "User", alias) {
		this.Id = this.RegisterColumn("Id");
		this.Name = this.RegisterColumn("Name");
		this.Email = this.RegisterColumn("Email");
	}

	public Column Id { get; private set; }
	public Column Email { get; private set; }
	public Column Name { get; private set; }
}

public class PostTable : Table
{
	public PostTable(string alias = null)
		: base("dbo", "Post", alias) {
		this.Id = this.RegisterColumn("Id");
		this.UserId = this.RegisterColumn("UserId");
		this.Title = this.RegisterColumn("Title");
		this.Text = this.RegisterColumn("Text");
		this.PostDate = this.RegisterColumn("PostDate");
	}

	public Column Id { get; private set; }
	public Column UserId { get; private set; }
	public Column Title { get; private set; }
	public Column Text { get; private set; }
	public Column PostDate { get; private set; }
}
````

SharpQL's fluent API syntax tries to mimic the exact SQL grammar where possible.
````csharp
// than use the tables to generate the sql script
	var q = new XQuery();
	var u = new UserTable();
	var p = new PostTable();

	int userId = 1;
	q.Select(u.Id, Sql.Count(1))
		.From(u)
		.InnerJoin(p, u.Id.IsEqualTo(p.UserId))
		.Where(u.Id.IsEqualTo(userId)
			   & p.PostDate.IsGreaterThanOrEqualTo(DateTime.Now.AddDays(-7)))
		.GroupBy(u.Id)
		.Having(Sql.Count(1).IsGreaterThan(10))
		.EndStatement();

	var script = q.ToString();
	var sqlParameters = q.Parameters;
````
The previous code will result in the following SQL script

````sql
SELECT   [dbo].[User].[Id],
         COUNT(@p0)
FROM     [dbo].[User]
INNER JOIN
         [dbo].[Post]
ON       [dbo].[User].[Id] = [dbo].[Post].[UserId]
WHERE    ([dbo].[User].[Id] = @p0
          AND [dbo].[Post].[PostDate] >= @p1)
GROUP BY [dbo].[User].[Id]
HAVING   COUNT(@p0) > @p2;
````


