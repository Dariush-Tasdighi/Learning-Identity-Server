namespace Models;

public class DatabaseContext : Microsoft.EntityFrameworkCore.DbContext
{
	public DatabaseContext(Microsoft.EntityFrameworkCore
		.DbContextOptions<DatabaseContext> options) : base(options: options)
	{
	}

	public Microsoft.EntityFrameworkCore.DbSet<Customer> Customers { get; set; }
}
