namespace RazorPages.Pages.Account;

public class SignoutModel :
	Microsoft.AspNetCore.Mvc.RazorPages.PageModel
{
	public SignoutModel() : base()
	{
	}

	//public void OnGet()
	//{
	//}

	public Microsoft.AspNetCore.Mvc.IActionResult OnGet()
	{
		//return SignOut("Cookies", "oidc");

		return SignOut(authenticationSchemes:
			new string[] { "Cookies", "oidc" });
	}
}
