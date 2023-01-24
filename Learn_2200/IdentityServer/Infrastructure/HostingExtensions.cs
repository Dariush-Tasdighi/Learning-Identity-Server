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
		builder.Services.AddRazorPages();

		builder.Services.AddIdentityServer(setupAction: options =>
		{
			options.EmitStaticAudienceClaim = true;
		})

		.AddTestUsers
			(users: IdentityServer.TestUsers.Users)

		.AddInMemoryClients
			(clients: Configuration.GetClients())

		.AddInMemoryApiScopes
			(apiScopes: Configuration.GetApiScopes())

		.AddInMemoryIdentityResources
			(identityResources: Configuration.GetIdentityResources())
		;

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

		app.UseStaticFiles();
		app.UseRouting();

		app.UseIdentityServer();

		app.UseAuthorization();

		app.MapRazorPages()
			.RequireAuthorization()
			;

		return app;
	}
}
