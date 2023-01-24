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
