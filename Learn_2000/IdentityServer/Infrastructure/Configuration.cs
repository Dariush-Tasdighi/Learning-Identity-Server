// **************************************************
//namespace Infrastructure;

//public static class Config : object
//{
//	static Config()
//	{
//	}

//	public static System.Collections.Generic.IEnumerable
//		<Duende.IdentityServer.Models.ApiScope> ApiScopes =>
//		new Duende.IdentityServer.Models.ApiScope[]
//		{
//		};

//	public static System.Collections.Generic.IEnumerable
//		<Duende.IdentityServer.Models.Client> Clients =>
//		new Duende.IdentityServer.Models.Client[]
//		{
//		};
//}
// **************************************************

// **************************************************
using Duende.IdentityServer.Models;

namespace Infrastructure;

public static class Configuration : object
{
	static Configuration()
	{
	}

	public static System.Collections.Generic.IEnumerable
		<Duende.IdentityServer.Models.ApiScope> GetApiScopes()
	{
		var result =
			new System.Collections.Generic.List
			<Duende.IdentityServer.Models.ApiScope>();

		// Defining an API Scope:
		// Scope is a core feature of OAuth that allows
		// you to express the extent or scope of access
		Duende.IdentityServer.Models.ApiScope apiScope;

		// **************************************************
		apiScope = new
			(name: "MyApiScope1", displayName: "My Api Scope");

		result.Add(item: apiScope);
		// **************************************************

		return result;
	}

	public static System.Collections.Generic
		.IEnumerable<Duende.IdentityServer.Models.Client> GetClients()
	{
		var result =
			new System.Collections.Generic
			.List<Duende.IdentityServer.Models.Client>();

		// Defining the client:
		// Configure a client application
		// that you will use to access the API.
		Duende.IdentityServer.Models.Client client;

		// **************************************************
		client =
			new Duende.IdentityServer.Models.Client()
			{
				// A ClientId, which identifies the application
				// to IdentityServer so that it knows which
				// client is trying to connect to it.
				ClientId = "Client1",

				ClientName = "My Client Name (1)",

				// Scopes that client has access to
				// The list of scopes that the client is allowed
				// to ask for. Notice that the allowed scope here
				// matches the name of the ApiScope above.
				AllowedScopes =
				{
					"MyApiScope1",
				},

				// Secret for authentication
				ClientSecrets =
				{
					// Sha256() -> using Duende.IdentityServer.Models;
					new Duende.IdentityServer.Models
						.Secret(value: "ClientSecret1".Sha256()),
				},

				// No interactive user
				// Use the 'clientid' and 'secret' for authentication
				// Machine to Machine (M2M) client credentials flow client
				AllowedGrantTypes =
					Duende.IdentityServer.Models.GrantTypes.ClientCredentials,
			};

		result.Add(item: client);
		// **************************************************

		return result;
	}
}
// **************************************************
