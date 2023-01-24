using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;

var builder =
	Microsoft.AspNetCore.Builder
	.WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();

// **************************************************
System.IdentityModel.Tokens.Jwt
	.JwtSecurityTokenHandler.DefaultMapInboundClaims = false;

builder.Services.AddAuthentication(options =>
{
	options.DefaultScheme = "Cookies";
	options.DefaultChallengeScheme = "oidc";
})
	.AddCookie(authenticationScheme: "Cookies")

	.AddOpenIdConnect(authenticationScheme: "oidc", options =>
	{
		options.Authority =
			"https://localhost:5001";

		options.ClientId = "web1";
		options.ClientSecret = "WebSecret1";

		options.ResponseType = "code";

		options.Scope.Clear();
		options.Scope.Add("openid");
		options.Scope.Add("profile");

		options.SaveTokens = true;
	});
// **************************************************

//// New
//builder.Services
//	.Configure
//	<Services.IdentityServerSettings>
//	(builder.Configuration.GetSection(key: "IdentityServerSettings"));

//// New
//builder.Services
//	.AddSingleton<Services.ITokenService, Services.TokenService>();

var app =
	builder.Build();

if (app.Environment.IsDevelopment() == false)
{
	app.UseExceptionHandler("/Error");

	app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

//app.UseAuthorization();

app.MapRazorPages();

app.Run();
