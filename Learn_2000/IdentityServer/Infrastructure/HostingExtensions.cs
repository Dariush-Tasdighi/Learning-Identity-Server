using Serilog;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

internal static class HostingExtensions : object
{
	static HostingExtensions()
	{
	}

	public static WebApplication
		ConfigureServices(this WebApplicationBuilder builder)
	{
		// uncomment if you want to add a UI
		//builder.Services.AddRazorPages();

		// **************************************************
		//builder.Services.AddIdentityServer(setupAction: options =>
		//{
		//	// https://docs.duendesoftware.com/identityserver/v6/fundamentals/resources/api_scopes#authorization-based-on-scopes
		//	options.EmitStaticAudienceClaim = true;
		//})
		//.AddInMemoryClients(clients: Configuration.Clients)
		//.AddInMemoryApiScopes(apiScopes: Configuration.ApiScopes)
		//;
		// **************************************************

		// **************************************************
		builder.Services.AddIdentityServer(setupAction: options =>
		{
			options.EmitStaticAudienceClaim = true;
		})

		.AddInMemoryClients
			(clients: Configuration.GetClients())

		.AddInMemoryApiScopes
			(apiScopes: Configuration.GetApiScopes())
		;
		// **************************************************

		return builder.Build();
	}

	public static WebApplication
		ConfigurePipeline(this WebApplication app)
	{
		app.UseSerilogRequestLogging();

		if (app.Environment.IsDevelopment())
		{
			app.UseDeveloperExceptionPage();
		}

		// uncomment if you want to add a UI
		//app.UseStaticFiles();
		//app.UseRouting();

		app.UseIdentityServer();

		// uncomment if you want to add a UI
		//app.UseAuthorization();
		//app.MapRazorPages().RequireAuthorization();

		return app;
	}
}
