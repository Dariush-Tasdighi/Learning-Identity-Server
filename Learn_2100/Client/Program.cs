using IdentityModel.Client;

var client =
	new System.Net.Http.HttpClient();


// **************************************************
System.Console.WriteLine
	(value: "Discovering endpoints from metadata...");

var discoveryDocument =
	await
	// GetDiscoveryDocumentAsync() -> using IdentityModel.Client;
	client.GetDiscoveryDocumentAsync
		(address: "https://localhost:5001");

if (discoveryDocument.IsError)
{
	System.Console.WriteLine
		(value: discoveryDocument.Error);

	PressAnyKeyToExit();

	return;
}
// **************************************************



// **************************************************
System.Console.WriteLine
	(value: "Requesting Access Token...");

var clientCredentialsTokenRequest =
	new IdentityModel.Client.ClientCredentialsTokenRequest
	{
		Address =
			discoveryDocument.TokenEndpoint,

		// Required!
		Scope = "MyApiScope1",

		ClientId = "Client1",
		ClientSecret = "ClientSecret1",
	};

var tokenResponse =
	await
	// RequestClientCredentialsTokenAsync() -> using IdentityModel.Client;
	client.RequestClientCredentialsTokenAsync
	(request: clientCredentialsTokenRequest);

if (tokenResponse.IsError)
{
	System.Console.WriteLine
		(value: tokenResponse.Error);

	PressAnyKeyToExit();

	return;
}

System.Console.WriteLine
	(value: tokenResponse.AccessToken);
// **************************************************



// **************************************************
System.Console.WriteLine
	(value: "Calling the API...");

var apiClient =
	new System.Net.Http.HttpClient();

// SetBearerToken() -> using IdentityModel.Client;
apiClient.SetBearerToken
	(tokenResponse.AccessToken);

var response =
	await
	apiClient.GetAsync(requestUri: "https://localhost:6001/identity");

if (response.IsSuccessStatusCode == false)
{
	System.Console.WriteLine
		(value: response.StatusCode);

	PressAnyKeyToExit();

	return;
}

var responseString =
	await
	response.Content.ReadAsStringAsync();

var responseJson =
	System.Text.Json
	.JsonDocument.Parse(json: responseString);

var rootElement =
	responseJson.RootElement;

var serializeOptions =
	new System.Text.Json.JsonSerializerOptions
	{
		WriteIndented = true,
	};

var result =
	System.Text.Json.JsonSerializer.Serialize
	(value: rootElement, options: serializeOptions);

System.Console.WriteLine(value: result);
// **************************************************

PressAnyKeyToExit();

void PressAnyKeyToExit()
{
	System.Console.Write
		(value: "Press any key to exit...");

	System.Console.ReadKey();
}
