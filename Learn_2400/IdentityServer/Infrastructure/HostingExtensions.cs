using Serilog;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Duende.IdentityServer;
using Microsoft.EntityFrameworkCore;

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

		// **************************************************
		//builder.Services
		//	.AddIdentityServer(setupAction: options =>
		//	{
		//		options.EmitStaticAudienceClaim = true;
		//	})

		//.AddTestUsers
		//	(users: IdentityServer.TestUsers.Users)

		//.AddInMemoryClients
		//	(clients: Configuration.GetClients())

		//.AddInMemoryApiScopes
		//	(apiScopes: Configuration.GetApiScopes())

		//.AddInMemoryIdentityResources
		//	(identityResources: Configuration.GetIdentityResources())
		//;
		// **************************************************

		// **************************************************
		var migrationsAssembly =
			typeof(Program).Assembly.GetName().Name;

		const string connectionString =
			@"Server=.;User ID=sa;Password=1234512345;Database=LEARNING_IDENTITY_SERVER;MultipleActiveResultSets=true;Trusted_Connection=True;TrustServerCertificate=True;";

		builder.Services
			.AddIdentityServer(setupAction: options =>
			{
				options.EmitStaticAudienceClaim = true;
			})

			.AddConfigurationStore(options =>
			{
				options.ConfigureDbContext = b =>
				{
					b.UseSqlServer(connectionString: connectionString, sqlServerOptionsAction: sql =>
					{
						sql.MigrationsAssembly
							(assemblyName: migrationsAssembly);
					});
				};
			})

			.AddOperationalStore(options =>
			{
				options.ConfigureDbContext = b =>
				{
					b.UseSqlServer(connectionString: connectionString, sqlServerOptionsAction: sql =>
					{
						sql.MigrationsAssembly
							(assemblyName: migrationsAssembly);
					});
				};
			})

			.AddTestUsers
				(users: IdentityServer.TestUsers.Users)
			;
		// **************************************************

		builder.Services
			.AddAuthentication()

			.AddGoogle
				(authenticationScheme: "Google", configureOptions: options =>
				{
					options.SignInScheme =
						IdentityServerConstants.ExternalCookieAuthenticationScheme;

					options.ClientId =
						builder.Configuration["Authentication:Google:ClientId"]
						?? "Undefined";

					options.ClientSecret =
						builder.Configuration["Authentication:Google:ClientSecret"]
						?? "Undefined";
				})

				.AddOpenIdConnect
					(authenticationScheme: "oidc",
					displayName: "Demo IdentityServer", configureOptions: options =>
					{
						options.SignOutScheme =
							Duende.IdentityServer
							.IdentityServerConstants.SignoutScheme;

						options.SignInScheme =
							Duende.IdentityServer
							.IdentityServerConstants.ExternalCookieAuthenticationScheme;

						options.SaveTokens = true;
						options.ResponseType = "code";

						options.ClientId =
							"interactive.confidential";

						options.ClientSecret = "secret";

						options.Authority = "https://demo.duendesoftware.com";

						options.TokenValidationParameters =
							new Microsoft.IdentityModel.Tokens.TokenValidationParameters
							{
								NameClaimType = "name",
								RoleClaimType = "role",
							};
					});

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
