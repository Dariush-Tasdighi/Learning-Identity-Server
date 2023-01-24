namespace Dtos.Account;

public class RegisterDto : object
{
	public RegisterDto(string username, string password) : base()
	{
		Username = username;
		Password = password;
	}

	[System.ComponentModel.DataAnnotations.Required
		(AllowEmptyStrings = false)]
	public string Username { get; set; }

	[System.ComponentModel.DataAnnotations.Required
		(AllowEmptyStrings = false)]
	public string Password { get; set; }
}
