**************************************************
https://docs.duendesoftware.com/identityserver/v6/quickstarts/2_interactive/

In Some Folder/Drive, Run:

	dotnet new install Duende.IdentityServer.Templates

	Not!!!

		dotnet new --install Duende.IdentityServer.Templates
**************************************************

**************************************************
In Some!!! Folder: 

	H:\Temp\IdentityServer

Note:

	The 'IdentityServer' name is important! Because of namespace!

Run:

	dotnet new isui

The template "Duende IdentityServer Quickstart UI (UI assets only)" was created successfully.

Copy Folders (Pages and wwwroot) to IdentityServer Project

Note:

	There is also a template called 'isinmem' which
	combines the basic IdentityServer from the isempty
	template with the quickstart UI from the isui template.
**************************************************

**************************************************
Enable the UI

	Update All Files in Pages!!!

		Using Problems!!!
**************************************************

**************************************************
In [Properties] Folder:

	Update File: launchSettings.json

		"launchBrowser": false -> true
**************************************************

**************************************************
In Folder: [Infrastructure]

	Update File: HostingExtensions.cs

		Uncomment Some Commands for activating Razor Pages
**************************************************

**************************************************
In Folder: [Infrastructure]

	Update File: Configuration.cs

		public static System.Collections.Generic.IEnumerable
			<Duende.IdentityServer.Models.IdentityResource> GetIdentityResources()
		{
			var result =
				new Duende.IdentityServer.Models.IdentityResource[]
				{
					new Duende.IdentityServer.Models.IdentityResources.OpenId(),
					new Duende.IdentityServer.Models.IdentityResources.Profile(),
				};

			return result;
		}

Note:

	All standard scopes and their corresponding claims
	can be found in the OpenID Connect specification:

	https://openid.net/specs/openid-connect-core-1_0.html#ScopeClaims
**************************************************

**************************************************
In Folder: [Infrastructure]

	Update File: HostingExtensions.cs

		.AddTestUsers
			(users: IdentityServer.TestUsers.Users)

		.AddInMemoryIdentityResources
			(identityResources: Configuration.GetIdentityResources())
**************************************************

**************************************************
Register an OIDC client:

	In Folder: [Infrastructure]

		Update File: Configuration.cs

			Add New Client (WEB)

				client =
					new Duende.IdentityServer.Models.Client()
					{
						ClientId = "web1",

						ClientSecrets =
						{
							new Duende.IdentityServer.Models
								.Secret(value: "WebSecret1".Sha256()),
						},

						ClientName =
							"My Web (Razor Pages) (1)",

						// Interactive user!
						AllowedGrantTypes =
							Duende.IdentityServer.Models.GrantTypes.Code,

						AllowedScopes =
						{
							Duende.IdentityServer.IdentityServerConstants.StandardScopes.OpenId,
							Duende.IdentityServer.IdentityServerConstants.StandardScopes.Profile
						},

						// Where to redirect to after login
						RedirectUris =
							{ "https://localhost:5002/signin-oidc" },

						// Where to redirect to after logout
						PostLogoutRedirectUris =
							{ "https://localhost:5002/signout-callback-oidc" },
					};

				result.Add(item: client);
**************************************************

**************************************************
Create the OIDC client:

	Add New Project:

		RazorPages

			In Folder: Properties

				Update File: launchSettings.json (Update PORT: 5002)

					{
						"profiles": {
							"https": {
								"launchBrowser": true,
								"commandName": "Project",
								"dotnetRunMessages": true,
								"applicationUrl": "https://localhost:5002",
								"environmentVariables": {
									"ASPNETCORE_ENVIRONMENT": "Development"
								}
							}
						}
					}
**************************************************

**************************************************
Install the OIDC NuGet Package in RazorPages Project:

	<ItemGroup>
		<PackageReference Include="Microsoft.AspNetCore.Authentication.OpenIdConnect" Version="7.0.2" />
	</ItemGroup>

Note:

	AddAuthentication registers the authentication services.
	Notice that in its options, the DefaultChallengeScheme is
	set to “oidc”, and the DefaultScheme is set to “Cookies”.
	The DefaultChallengeScheme is used when an unauthenticated
	user must log in. This begins the OpenID Connect protocol,
	redirecting the user to IdentityServer. After the user has
	logged in and been redirected back to the client, the client
	creates its own local cookie. Subsequent requests to the client
	will include this cookie and be authenticated with the default Cookie scheme.
**************************************************

**************************************************
Update File: Program.cs

	System.IdentityModel.Tokens.Jwt
		.JwtSecurityTokenHandler.DefaultMapInboundClaims = false;

	builder.Services.AddAuthentication(configureOptions: options =>
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
		});

Note:

	Finally, AddOpenIdConnect is used to configure the handler
	that performs the OpenID Connect protocol. The Authority
	indicates where the trusted token service is located.
	The ClientId and the ClientSecret identify this client.
	The Scope is the collection of scopes that the client
	will request. By default it includes the openid and
	profile scopes, but clear the collection and add them
	back for explicit clarity. SaveTokens is used to persist
	the tokens in the cookie (as they will be needed later).

Note:

	This uses the authorization 'code' 'flow' with 'PKCE' to connect
	to the OpenID Connect provider. See the below link for more information on protocol flows:

	https://docs.duendesoftware.com/identityserver/v6/fundamentals/clients/
**************************************************

**************************************************
Update File: Program.cs

	app.UseAuthentication();
	app.UseAuthorization();

	app.MapRazorPages()
		.RequireAuthorization()
		;

Note:

	https://learn.microsoft.com/en-us/aspnet/core/security/authorization/razor-pages-authorization?view=aspnetcore-6.0	
**************************************************

**************************************************
In Project: RazorPages

	In Folder: [Pages]

		Create Page: Index
**************************************************

**************************************************
In Project: RazorPages

	Adding sign-out

		To sign out, you need to

			Clear local application cookies
			Make a roundtrip to IdentityServer using the OIDC protocol to clear its session
**************************************************

**************************************************
In Project: RazorPages

	In Folder: Pages

		Create Folder: Account

		In Folder: Account

			Create Page: Signout
**************************************************

**************************************************
In Project: RazorPages

	In Folder: Pages

		In Folder: Shared

			Update File: _Layout.cshtml

				Add a 'Signout' link to layout 
**************************************************

**************************************************
Getting claims from the UserInfo endpoint

	In Project: RazorPages

		Update File: Program.cs

			.AddOpenIdConnect(authenticationScheme: "oidc", configureOptions: options =>
			{
				...

				// New
				options.GetClaimsFromUserInfoEndpoint = true;
			});

			Note:

				You might have noticed that even though you’ve configured
				the client to be allowed to retrieve the profile identity
				scope, the claims associated with that scope (such as name,
				family_name, website etc.) don’t appear in the returned token.
				You need to tell the client to retrieve those claims from the
				userinfo endpoint by specifying scopes that the client application
				needs to access and setting the GetClaimsFromUserInfoEndpoint option
**************************************************

**************************************************
Further Experiments

	This quickstart created a client with interactive login using OIDC. To experiment further you can

		Add additional claims to the identity
		Add support for external authentication
**************************************************

**************************************************
Add More Claims

	To add more claims to the identity:

		Add a new identity resource to the list in
		src/IdentityServer/Config.cs. Name it and specify
		which claims should be returned when it is requested.
		The Name property of the resource is the scope value
		that clients can request to get the associated UserClaims.
		For example, you could add an IdentityResource named
		“verification” which would include the email and email_verified claims.

		In Project: IdentityServer

			In Folder: [Infrastructure]

				Update File: Configuration.cs

		In Project: RazorPages

			Update File: Program.cs

Note:

	IdentityServer uses the IProfileService to retrieve claims
	for tokens and the userinfo endpoint. You can provide your
	own implementation of IProfileService to customize this process
	with custom logic, data access, etc. Since you are using AddTestUsers,
	the TestUserProfileService is used automatically. It will automatically
	include requested claims from the test users added in src/IdentityServer/TestUsers.cs.
**************************************************

**************************************************
Add Support for External Authentication

	Adding support for external authentication to your
	IdentityServer can be done with very little code;
	all that is needed is an authentication handler.

	ASP.NET Core ships with handlers for Google, Facebook,
	Twitter, Microsoft Account and OpenID Connect. In addition,
	you can find handlers for many other authentication providers here:

	https://github.com/aspnet-contrib/AspNet.Security.OAuth.Providers
**************************************************

**************************************************
Add Google support

	To use Google for authentication, you need to:

		Add the 'Microsoft.AspNetCore.Authentication.Google' nuget package
		to the IdentityServer project:

			<PackageReference Include="Microsoft.AspNetCore.Authentication.Google" Version="7.0.2" />

		Register with Google and set up a client.

		Store the client id and secret securely with dotnet user-secrets.

		Add the Google authentication handler to the middleware pipeline and configure it.

	See Microsoft’s guide for details on how to register with Google,
	create the client, and store the secrets in user-secrets.
	Stop before adding the authentication middleware and Google
	authentication handler to the pipeline.
	You will need an IdentityServer specific option.

	Add the following to ConfigureServices in src/IdentityServer/HostingExtensions.cs:

		builder.Services.AddAuthentication()
		.AddGoogle("Google", options =>
		{
			options.SignInScheme =
				IdentityServerConstants.ExternalCookieAuthenticationScheme;

			options.ClientId =
				builder.Configuration["Authentication:Google:ClientId"];

			options.ClientSecret =
				builder.Configuration["Authentication:Google:ClientSecret"];
		});

	When authenticating with Google, there are again two authentication schemes.
	AddGoogle adds the Google scheme, which handles the protocol flow back and
	forth with Google. After successful login, the application needs to sign in
	to an additional scheme that can authenticate future requests without needing
	a round-trip to Google - typically by issuing a local cookie. The SignInScheme
	tells the Google handler to use the scheme named
	IdentityServerConstants.ExternalCookieAuthenticationScheme, which is a cookie
	authentication handler automatically created by IdentityServer that is intended
	for external logins.

	Now run IdentityServer and WebClient and try to authenticate (you may need to
	log out and log back in). You will see a Google button on the login page.

	Click on Google and authenticate with a Google account. You should land back
	on the WebClient home page, showing that the user is now coming from Google
	with claims sourced from Google’s data.

	Note:

	The Google button is rendered by the login page automatically when there are
	external providers registered as authentication schemes. See the BuildModelAsync
	method in src/IdentityServer/Pages/Login/Index.cshtml.cs and the corresponding
	Razor template for more details.
**************************************************

**************************************************
Adding an additional OpenID Connect-based external provider

	A cloud-hosted demo version of Duende IdentityServer can be added as an
	additional external provider:

	https://demo.duendesoftware.com/

	Register and configure the services for the OpenId Connect handler
	in src/IdentityServer/HostingExtensions.cs:


**************************************************

**************************************************
	Now if you try to authenticate, you should see an additional button to log in
	to the cloud-hosted Demo IdentityServer. If you click that button, you will be
	redirected to https://demo.duendesoftware.com/. Note that the demo site is using
	the same UI as your site, so there will not be very much that changes visually when
	you’re redirected. Check that the page’s location has changed and then log in using
	the alice or bob users (their passwords are their usernames, just as they are for the
	local test users). You should land back at WebClient, authenticated with a demo user.

	The demo users are logically distinct entities from the local test users, even though
	they happen to have identical usernames. Inspect their claims in WebClient and note the
	differences between them, such as the distinct sub claims.
**************************************************
