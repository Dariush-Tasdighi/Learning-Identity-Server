namespace Models;

public class User : object
{
	public User(string username, string password)
	{
		Username = username;
		Password = password;
	}

	public long Id { get; set; }

	public bool IsAdmin { get; set; }

	public bool IsActive { get; set; }

	[System.ComponentModel.DataAnnotations.Required
		(AllowEmptyStrings = false)]
	public string Username { get; set; }

	[System.ComponentModel.DataAnnotations.Required
		(AllowEmptyStrings = false)]
	public string Password { get; set; }

	public string? LastName { get; set; }

	public string? FirstName { get; set; }
}
