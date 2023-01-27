using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Authentication;
using Microsoft.IdentityModel.Tokens;

var builder =
	Microsoft.AspNetCore.Builder
	.WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();

// **************************************************
// *** Solution (1) *********************************
// **************************************************
//System.IdentityModel.Tokens.Jwt
//	.JwtSecurityTokenHandler.DefaultMapInboundClaims = false;

//builder.Services.AddAuthentication(configureOptions: options =>
//{
//	options.DefaultScheme = "Cookies";
//	options.DefaultChallengeScheme = "oidc";
//})
//	.AddCookie(authenticationScheme: "Cookies")

//	.AddOpenIdConnect(authenticationScheme: "oidc", configureOptions: options =>
//	{
//		options.Authority =
//			"https://localhost:5001";

//		options.SaveTokens = true;
//		options.ResponseType = "code";

//		options.ClientId = "web1";
//		options.ClientSecret = "WebSecret1";

//		options.Scope.Clear();
//		options.Scope.Add(item: "openid");
//		options.Scope.Add(item: "profile");
//	});
// **************************************************

// **************************************************
// *** Solution (2) *********************************
// **************************************************
//System.IdentityModel.Tokens.Jwt
//	.JwtSecurityTokenHandler.DefaultMapInboundClaims = false;

//builder.Services.AddAuthentication(configureOptions: options =>
//{
//	options.DefaultScheme = "Cookies";
//	options.DefaultChallengeScheme = "oidc";
//})
//	.AddCookie(authenticationScheme: "Cookies")

//	.AddOpenIdConnect(authenticationScheme: "oidc", configureOptions: options =>
//	{
//		options.Authority =
//			"https://localhost:5001";

//		options.SaveTokens = true;
//		options.ResponseType = "code";

//		options.ClientId = "web1";
//		options.ClientSecret = "WebSecret1";

//		options.Scope.Clear();
//		options.Scope.Add(item: "openid");
//		options.Scope.Add(item: "profile");

//		// New
//		options.GetClaimsFromUserInfoEndpoint = true;
//	});
// **************************************************

// **************************************************
// *** Solution (3) *********************************
// **************************************************
System.IdentityModel.Tokens.Jwt
	.JwtSecurityTokenHandler.DefaultMapInboundClaims = false;

builder.Services
	.AddAuthentication(configureOptions: options =>
	{
		options.DefaultScheme = "Cookies";
		options.DefaultChallengeScheme = "oidc";
	})

	.AddCookie(authenticationScheme: "Cookies")

	.AddOpenIdConnect(authenticationScheme: "oidc", configureOptions: options =>
	{
		options.Authority =
			"https://localhost:5001";

		options.SaveTokens = true;
		options.ResponseType = "code";

		options.ClientId = "web1";
		options.ClientSecret = "WebSecret1";

		options.Scope.Clear();
		options.Scope.Add(item: "openid");
		options.Scope.Add(item: "profile");

		// New
		options.Scope.Add(item: "verification");

		// New
		options.ClaimActions.MapJsonKey
			(claimType: "email_verified", jsonKey: "email_verified");

		options.GetClaimsFromUserInfoEndpoint = true;
	})
	;
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

// **************************************************
//app.UseAuthorization();

//app.MapRazorPages();
// **************************************************

// **************************************************
app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages()
	.RequireAuthorization()
	;
// **************************************************

app.Run();
