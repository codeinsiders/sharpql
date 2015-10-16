# SharpQL

SharpQL is an open source library written in C#.
It's sole purpose is to help developers generate dynamic SQL scripts fast and safe.
No more ugly string concatenation and SQL injection!

Project Website: http://sharpql.com

The best thing is that if you know SQL you already know how to use SharpQL


```csharp
// create classes to describe database table
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

// than use it to generate sql scripts
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


