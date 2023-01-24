namespace RazorPages.Pages.Customers;

public class IndexModel :
	Microsoft.AspNetCore.Mvc.RazorPages.PageModel
{
	//public IndexModel
	//	(Microsoft.Extensions.Logging.ILogger<IndexModel> logger,
	//	Services.ITokenService tokenService) : base()
	//{
	//	Logger = logger;
	//	TokenService = tokenService;

	//	Customers =
	//		new System.Collections.Generic.List<Models.Customer>();
	//}

	//private Services.ITokenService TokenService { get; }

	//private Microsoft.Extensions.Logging.ILogger<IndexModel> Logger { get; }

	//protected System.Collections.Generic.IList<Models.Customer>? Customers { get; set; }

	//public async System.Threading.Tasks.Task OnGet()
	//{
	//	using var client =
	//		new System.Net.Http.HttpClient();

	//	var tokenResponse =
	//		await
	//		TokenService.GetTokenAsync
	//		(scope: "Scope.Manage.Customers");

	//	// SetBearerToken() -> using IdentityModel.Client;
	//	client.SetBearerToken
	//		(token: tokenResponse.AccessToken);

	//	var requestUri =
	//		"https://localhost:7283/Customers";

	//	var result =
	//		await client.GetAsync(requestUri: requestUri);

	//	if (result.IsSuccessStatusCode)
	//	{
	//		var jsonData =
	//			await
	//			result.Content.ReadAsStringAsync();

	//		Customers =
	//			System.Text.Json.JsonSerializer.Deserialize
	//			<System.Collections.Generic.IList<Models.Customer>>(json: jsonData);
	//	}
	//}
}
