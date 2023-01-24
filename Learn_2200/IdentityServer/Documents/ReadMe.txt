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

Copy Folders (Pages and wwwroot) to IdentityServer

Note:

	There is also a template called 'isinmem' which
	combines the basic IdentityServer from the isempty
	template with the quickstart UI from the isui template.
**************************************************

**************************************************
Enable the UI

	Update All Files in Pages!!!

		Namespace Problems!!!
**************************************************

**************************************************
In [Properties] Folder:

	Update File: launchSettings.json

		"launchBrowser": true -> false
**************************************************

**************************************************
In Folder: [Infrastructure]

	Update File: HostingExtensions.cs

		Uncomment Some Commands!
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

			Update PORT: 5002
**************************************************

**************************************************
Install the OIDC NuGet Package in RazorPages Project:

	<ItemGroup>
		<PackageReference Include="Microsoft.AspNetCore.Authentication.OpenIdConnect" Version="7.0.2" />
	</ItemGroup>
**************************************************

Update File: Program.cs



























**************************************************
Create Folder:

	[Infrastructure]
**************************************************

**************************************************
Move Files to [Infrastructure] Folder:

	Config.cs
	HostingExtensions.cs
**************************************************

**************************************************
Fix All Files! DT Clean Code!
**************************************************

**************************************************
Rename File:

	Config.cs -> Configuration.cs

Update File in [Infrastructure] Folder:

	Configuration.cs
**************************************************

**************************************************
Note: On first startup, IdentityServer will use its automatic key management
feature to create a signing key and store it in the src/IdentityServer/keys
directory. To avoid accidentally disclosing cryptographic secrets, the entire
keys directory should be excluded from source control. It will be recreated if it is not present.
**************************************************

**************************************************
Run the application and check the below address:

	https://localhost:5001/.well-known/openid-configuration

This is discovery document:

	It is used by your clients and APIs to retrieve configuration
	data needed to request and validate tokens, login and logout, etc.

	https://docs.duendesoftware.com/identityserver/v6/reference/endpoints/discovery/
**************************************************

**************************************************
Get Access Token from Postman:

POST Verb:

	https://localhost:5001/connect/token

In Auth (Authorization) tab:

	Type:

		Basic Auth

			Username: Client1
			Password: ClientSecret1

Body:

	Select: x-www-form-urlencoded

		Key: grant_type		Value: client_credentials
		Key: scope			Value: MyApiScope1

We get:

{
	"access_token": "eyJhbGciOiJSUzI1NiIsImtpZCI6IkVBMjhBMjNENkM3MDVDNUEzMEJEODk0OUFGMzZFMTZBIiwidHlwIjoiYXQrand0In0.eyJpc3MiOiJodHRwczovL2xvY2FsaG9zdDo1MDAxIiwibmJmIjoxNjc0NDE4NTc0LCJpYXQiOjE2NzQ0MTg1NzQsImV4cCI6MTY3NDQyMjE3NCwiYXVkIjoiaHR0cHM6Ly9sb2NhbGhvc3Q6NTAwMS9yZXNvdXJjZXMiLCJzY29wZSI6WyJNeUFwaVNjb3BlMSJdLCJjbGllbnRfaWQiOiJDbGllbnQxIiwianRpIjoiRjgwQjM4QzZCREVBNDk2ODFEQTRBNzVDQTE2MTNDODgifQ.dt-zmJoYy2edptYFHiFjIqDPZE7OkM9UnXVu_mb96SofI5Px7Y7f53B6x1WXxszlj0N-yAO5rV0ZjqLTvOkDVt-VyRyiyZ_T3kT7KrTP2_y3z_3FHBDiz0v2x93jN8dKY1HvL3JIm42bDr7DfUQzWztEJ3YfUMcRUUyFN2JVwe3viHNM9ymY6niCUArjFdnUt9g6LMdxkI1b0A69MBAPK0PNyV3PXYiw0qSfq9OeRtX7Xpl2x9Afltuwsi8RO_ryloPCiUdQDpKIgFO0BZutjG1EQyVKT0SY5nzSuZ85HcvCCEoFhVMy0Ot8wbhPV1lg5dpkQveB1JBwYIYiKyveSA",
	"expires_in": 3600,
	"token_type": "Bearer",
	"scope": "MyApiScope1"
}
Note:

	"expires_in": 3600 -> 3600 Seconds = 60 Minutes = 1 Hour
**************************************************

**************************************************
JWT = جات یا جوت

Check JWT Access Token in Site:

	https://jwt.io
	https://JsonWebToken.io

	https://jwt.ms
	https://token.dev/
	http://calebb.net/
	https://www.jstoolset.com/jwt
**************************************************








**************************************************
Create an API Project
**************************************************

**************************************************
Add JWT Bearer Authentication

	<PackageReference Include="Microsoft.AspNetCore.Authentication.JwtBearer" Version="7.0.2" />

This middleware will

	Find and parse a JWT sent with incoming requests as an Authorization: Bearer header.
	Validate the JWT’s signature to ensure that it was issued by IdentityServer.
	Validate that the JWT is not expired.
**************************************************

**************************************************
Update File: Program.cs

	builder.Services
		.AddAuthentication(defaultScheme: "Bearer")

		.AddJwtBearer(authenticationScheme: "Bearer", configureOptions: options =>
		{
			options.Authority = "https://localhost:5001";

			options.TokenValidationParameters = new TokenValidationParameters
			{
				ValidateAudience = false,
			};
		});

Audience validation is disabled here because access to the api
is modeled with [ApiScopes] only. By default, no audience will be
emitted unless the api is modeled with [ApiResources] instead.
**************************************************

**************************************************
Update File: Program.cs

	app.UseAuthentication();
	app.UseAuthorization();

	ترتیب نوشتن دستورات فوق اهمیت دارد

[UseAuthentication] adds the authentication middleware to the pipeline
so authentication will be performed automatically on every call into the host.

[UseAuthorization] adds the authorization middleware to make sure your API
endpoint cannot be accessed by anonymous clients.
**************************************************

**************************************************
In Folder: [Controllers]

	Create File: IdentityController.cs
**************************************************

**************************************************
Test the controller

Run the API project and then navigate to the identity controller
at https://localhost:6001/identity in a browser. This should
return a 401 status code, which means your API requires a credential
and is now protected by IdentityServer.
**************************************************

**************************************************
Test below action in PostMan:

	https://localhost:6001/identity

	401 Unauthorized
**************************************************

**************************************************
Run both Projects:

	Get "access_token" from Identity Server

		POST Verb: https://localhost:5001/connect/token

	Use "access_token" for runing IdentityController Action (Get):

		GET Verb: https://localhost:6001/identity

		In Auth (authorization) Tab:

			Type

				Bearer Token: [Access Token]
**************************************************

**************************************************
Create the 'Client' project (Console Application)

	The last step is to create a client that requests
	an access token and then uses that token to access the API.
**************************************************

**************************************************
Add the IdentityModel nuget package to 'Client' project

	<ItemGroup>
		<PackageReference Include="IdentityModel" Version="6.0.0" />
	</ItemGroup>

IdentityModel includes a client library to use with the
discovery endpoint. This way you only need to know the
base address of IdentityServer - the actual endpoint
addresses can be read from the metadata.
**************************************************

**************************************************
In 'Client' project:

	- Update Program.cs:

	var discoveryDocument =
		await
		client.GetDiscoveryDocumentAsync("https://localhost:5001");

	- Run three projects:

* Note: If you get an error connecting it may be that you are
running https and the development certificate for localhost is not trusted.
You can run:

Run below command in PowerShell:

	dotnet dev-certs https --trust

* in order to trust the development certificate.
This only needs to be done once.

* Result:
Trusting the HTTPS development certificate was requested.
A confirmation prompt will be displayed if the certificate
was not previously trusted. Click yes on the prompt to trust the certificate.
Successfully trusted the existing HTTPS certificate.
**************************************************

**************************************************
Request a token from IdentityServer

Copy and paste the access token from the console
to one of below sites to inspect the raw token:

https://jwt.io
https://jwt.ms
**************************************************

**************************************************
Calling the API

To send the access token to the API you typically
use the HTTP Authorization header. This is done
using the SetBearerToken extension method:
**************************************************

**************************************************
Authorization at the API

Right now, the API accepts any access token issued
by your IdentityServer. In this section, you will add
an Authorization Policy to the API that will check for
the presence of the “MyApiScope1” scope in the access token.
The protocol ensures that this scope will only be in
the token if the client requests it and IdentityServer
allows the client to have that scope.
**************************************************

**************************************************
Solution (2):

	In Project: Api

		Update File: Program.cs

			builder.Services.AddAuthorization(options =>
			{
				options.AddPolicy(name: "MyApiPolicy", configurePolicy: policy =>
				{
					policy.RequireAuthenticatedUser();

					policy.RequireClaim
						(claimType: "scope", allowedValues: "SomeAlakiName");
				});
			});

			!!!AND!!!

			app.MapControllers()
				.RequireAuthorization(policyNames: "MyApiPolicy")
				;

Note:

	You can now enforce this policy at various levels, e.g.:

		globally
		for all API endpoints
		for specific controllers/actions

Error:

	InternalServerError
**************************************************

**************************************************
Solution (3):

	In Project: Api

		Update File: Program.cs

			!!!Change "SomeAlakiName"" to "MyApiScope1"

			builder.Services.AddAuthorization(options =>
			{
				options.AddPolicy(name: "MyApiPolicy", configurePolicy: policy =>
				{
					policy.RequireAuthenticatedUser();

					policy.RequireClaim
						(claimType: "scope", allowedValues: "MyApiScope1");
				});
			});

			!!!AND!!!

			app.MapControllers()
				.RequireAuthorization(policyNames: "MyApiPolicy")
				;

Error:

	OK
**************************************************

**************************************************
Further experiments

	This quickstart focused on the success path:

		The client was able to request a token.
		The client could use the token to access the API.

	You can now try to provoke errors to learn how the system behaves, e.g.:

		Try to connect to IdentityServer when it is not running (unavailable).
		Try to use an invalid client id or secret to request the token.
		Try to ask for an invalid or null scope during the token request.
		Try to call the API when it is not running (unavailable).
		Don’t send the token to the API.
		Configure the API to require a different scope than the one in the token.
**************************************************
