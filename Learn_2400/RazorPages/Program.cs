using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.DependencyInjection;

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
//System.IdentityModel.Tokens.Jwt
//	.JwtSecurityTokenHandler.DefaultMapInboundClaims = false;

//builder.Services
//	.AddAuthentication(configureOptions: options =>
//	{
//		options.DefaultScheme = "Cookies";
//		options.DefaultChallengeScheme = "oidc";
//	})

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
//		options.Scope.Add(item: "verification");

//		// New
//		options.ClaimActions.MapJsonKey
//			(claimType: "email_verified", jsonKey: "email_verified");

//		options.GetClaimsFromUserInfoEndpoint = true;
//	})
//	;
// **************************************************

// **************************************************
// *** Solution (4) *********************************
// **************************************************
System.IdentityModel.Tokens.Jwt
	.JwtSecurityTokenHandler.DefaultMapInboundClaims = false;

// این دستور را صرفا در زمان برنامه‌نویسی فعال می‌کنیم که خطای کاملی را به
// ما نشان دهد ولی در محیط عملیاتی خطرناک است
//Microsoft.IdentityModel.Logging.IdentityModelEventSource.ShowPII = true;

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

		//options.Scope.Add(item: "verification");

		// MapJsonKey() -> using Microsoft.AspNetCore.Authentication;
		options.ClaimActions.MapJsonKey
			(claimType: "email_verified", jsonKey: "email_verified");

		options.GetClaimsFromUserInfoEndpoint = true;

		options.Events =
			new Microsoft.AspNetCore.Authentication.OpenIdConnect.OpenIdConnectEvents
			{
				OnAccessDenied = current =>
				{
					return System.Threading.Tasks.Task.CompletedTask;
				},

				OnAuthorizationCodeReceived = current =>
				{
					return System.Threading.Tasks.Task.CompletedTask;
				},

				OnAuthenticationFailed = current =>
				{
					return System.Threading.Tasks.Task.CompletedTask;
				},

				OnMessageReceived = current =>
				{
					return System.Threading.Tasks.Task.CompletedTask;
				},

				OnRedirectToIdentityProvider = current =>
				{
					return System.Threading.Tasks.Task.CompletedTask;
				},

				OnRedirectToIdentityProviderForSignOut = current =>
				{
					return System.Threading.Tasks.Task.CompletedTask;
				},

				OnRemoteFailure = current =>
				{
					return System.Threading.Tasks.Task.CompletedTask;
				},

				OnRemoteSignOut = current =>
				{
					return System.Threading.Tasks.Task.CompletedTask;
				},

				OnSignedOutCallbackRedirect = current =>
				{
					return System.Threading.Tasks.Task.CompletedTask;
				},

				OnTicketReceived = current =>
				{
					return System.Threading.Tasks.Task.CompletedTask;
				},

				OnTokenResponseReceived = current =>
				{
					return System.Threading.Tasks.Task.CompletedTask;
				},

				OnTokenValidated = current =>
				{
					return System.Threading.Tasks.Task.CompletedTask;
				},

				OnUserInformationReceived = current =>
				{
					return System.Threading.Tasks.Task.CompletedTask;
				},
			};
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
