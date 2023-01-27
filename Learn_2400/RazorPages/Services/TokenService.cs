//using Microsoft.Extensions.Logging;

//namespace Services;

//public class TokenService : object, ITokenService
//{
//	public TokenService
//		(Microsoft.Extensions.Logging.ILogger<TokenService> logger,
//		Microsoft.Extensions.Options.IOptions<IdentityServerSettings> identityServerSettings)
//	{
//		Logger = logger;
//		IdentityServerSettings = identityServerSettings;

//		using var httpClient =
//			new System.Net.Http.HttpClient();

//		// GetDiscoveryDocumentAsync() -> using IdentityModel.Client;
//		DiscoveryDocument =
//			httpClient.GetDiscoveryDocumentAsync
//			(identityServerSettings.Value.DiscoveryUrl).Result;

//		if (DiscoveryDocument.IsError)
//		{
//			logger.LogError
//				($"Unable to get discovery document. Error is: {DiscoveryDocument.Error}");

//			throw new System.Exception
//				(message: "Unable to get discovery document", DiscoveryDocument.Exception);
//		}
//	}

//	private Microsoft.Extensions.Logging.ILogger<TokenService> Logger { get; }

//	private IdentityModel.Client.DiscoveryDocumentResponse DiscoveryDocument { get; }

//	private Microsoft.Extensions.Options.IOptions<IdentityServerSettings> IdentityServerSettings { get; }

//	public async System.Threading.Tasks.Task
//		<IdentityModel.Client.TokenResponse> GetTokenAsync(string scope)
//	{
//		using var client =
//			new System.Net.Http.HttpClient();

//		var clientCredentialsTokenRequest =
//			new IdentityModel.Client.ClientCredentialsTokenRequest
//			{
//				Scope = scope,
//				Address = DiscoveryDocument.TokenEndpoint,
//				ClientId = IdentityServerSettings.Value.ClientId,
//				ClientSecret = IdentityServerSettings.Value.ClientSecret,
//			};


//		// RequestClientCredentialsTokenAsync() -> using IdentityModel.Client;
//		var tokenResponse = await client
//			.RequestClientCredentialsTokenAsync(request: clientCredentialsTokenRequest);

//		if (tokenResponse.IsError)
//		{
//			Logger.LogError
//				($"Unable to get token. Error is: {tokenResponse.Error}");

//			throw new System.Exception
//				(message: "Unable to get token", tokenResponse.Exception);
//		}

//		return tokenResponse;
//	}
//}
