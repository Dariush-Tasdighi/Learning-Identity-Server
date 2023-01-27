using Serilog;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Duende.IdentityServer.EntityFramework.Mappers;

namespace Infrastructure;

internal static class HostingExtensions : object
{
	static HostingExtensions()
	{
	}

	public static Microsoft.AspNetCore.Builder.WebApplication
		ConfigureServices(this Microsoft.AspNetCore.Builder.WebApplicationBuilder builder)
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
					options.SignInScheme = Duende.IdentityServer
						.IdentityServerConstants.ExternalCookieAuthenticationScheme;

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

	public static Microsoft.AspNetCore.Builder.WebApplication
		ConfigurePipeline(this Microsoft.AspNetCore.Builder.WebApplication app)
	{
		app.UseSerilogRequestLogging();

		if (app.Environment.IsDevelopment())
		{
			app.UseDeveloperExceptionPage();
		}

		InitializeDatabase(app: app);

		app.UseStaticFiles();

		app.UseRouting();

		app.UseIdentityServer();

		app.UseAuthorization();

		app.MapRazorPages()
			.RequireAuthorization()
			;

		return app;
	}

	private static void InitializeDatabase
		(Microsoft.AspNetCore.Builder.IApplicationBuilder app)
	{
		var service =
			app.ApplicationServices.GetService
			<Microsoft.Extensions.DependencyInjection.IServiceScopeFactory>();

		if (service == null)
		{
			return;
		}

		using var serviceScope = service.CreateScope();

		serviceScope.ServiceProvider.GetRequiredService
			<Duende.IdentityServer.EntityFramework.DbContexts.PersistedGrantDbContext>()
			.Database
			.Migrate();

		var context =
			serviceScope.ServiceProvider.GetRequiredService
			<Duende.IdentityServer.EntityFramework.DbContexts.ConfigurationDbContext>();

		context.Database.Migrate();

		if (context.Clients.Any() == false)
		{
			foreach (var client in Configuration.GetClients())
			{
				context.Clients
					.Add(client.ToEntity());
			}

			context.SaveChanges();
		}

		if (context.IdentityResources.Any() == false)
		{
			foreach (var resource in Configuration.GetIdentityResources())
			{
				context.IdentityResources
					.Add(resource.ToEntity());
			}

			context.SaveChanges();
		}

		if (context.ApiScopes.Any() == false)
		{
			foreach (var resource in Configuration.GetApiScopes())
			{
				context.ApiScopes
					.Add(resource.ToEntity());
			}

			context.SaveChanges();
		}
	}
}
