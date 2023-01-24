**************************************************
https://docs.duendesoftware.com/identityserver/v6/quickstarts/0_overview/

In Some Folder/Drive, Run:

	dotnet new install Duende.IdentityServer.Templates

	Not!!!

		dotnet new --install Duende.IdentityServer.Templates
**************************************************

**************************************************
https://docs.duendesoftware.com/identityserver/v6/quickstarts/1_client_credentials/

	An Identity Server
	An API that requires authentication
	A client that accesses that API

In Folder: 

	H:\.NET 7.x\Learning Identity Server\Learn_2000

Run:

	dotnet new isempty -n IdentityServer

The template "Duende IdentityServer Empty" was created successfully.
**************************************************

**************************************************
In [Properties] Folder:

	Update File: launchSettings.json

		"launchBrowser": true -> false
**************************************************

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
