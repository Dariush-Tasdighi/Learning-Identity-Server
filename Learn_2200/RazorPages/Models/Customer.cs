namespace Models;

public class Customer : object
{
	public Customer() : base()
	{
	}

	public long Id { get; set; }

	public string? LastName { get; set; }

	public string? FirstName { get; set; }
}
