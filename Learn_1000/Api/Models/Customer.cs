namespace Models;

public class Customer : object
{
	//public Customer() : base()
	//{
	//}

	public Customer(string firstName, string lastName) : base()
	{
		LastName = lastName;
		FirstName = firstName;
	}

	public long Id { get; set; }

	[System.ComponentModel.DataAnnotations.Required
		(AllowEmptyStrings = false)]
	public string LastName { get; set; }

	[System.ComponentModel.DataAnnotations.Required
		(AllowEmptyStrings = false)]
	public string FirstName { get; set; }
}
