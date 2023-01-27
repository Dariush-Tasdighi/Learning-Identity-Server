**************************************************
https://docs.duendesoftware.com/identityserver/v6/quickstarts/4_ef/
**************************************************

**************************************************
In Project: IdentityServer

	Install Nuget:

	<ItemGroup>
		<PackageReference Include="Duende.IdentityServer.EntityFramework" Version="6.2.2" />
	</ItemGroup>
**************************************************

**************************************************
Dependencies:

	<PackageReference Include="Microsoft.EntityFrameworkCore.Analyzers" Version="7.0.2" />



	<PackageReference Include="Microsoft.EntityFrameworkCore.Abstractions" Version="7.0.2" />



	<PackageReference Include="Microsoft.EntityFrameworkCore" Version="7.0.2" />

		Microsoft.Extensions.Logging (>= 7.0.0)
		Microsoft.Extensions.Caching.Memory (>= 7.0.0)
		Microsoft.EntityFrameworkCore.Analyzers (>= 7.0.2)
		Microsoft.Extensions.DependencyInjection (>= 7.0.0)
		Microsoft.EntityFrameworkCore.Abstractions (>= 7.0.2)



	Microsoft.EntityFrameworkCore.Relational

		Microsoft.EntityFrameworkCore (>= 7.0.2)
		Microsoft.Extensions.Configuration.Abstractions (>= 7.0.0)



	<PackageReference Include="Microsoft.EntityFrameworkCore.SqlServer" Version="7.0.2" />

		Microsoft.Data.SqlClient (>= 5.0.1)
		Microsoft.EntityFrameworkCore.Relational (>= 7.0.2)



	<PackageReference Include="Microsoft.EntityFrameworkCore.Design" Version="7.0.2">
		<PrivateAssets>all</PrivateAssets>
		<IncludeAssets>runtime; build; native; contentfiles; analyzers</IncludeAssets>
	</PackageReference>

		Humanizer.Core (>= 2.14.1)
		Mono.TextTemplating (>= 2.2.1)
		Microsoft.Extensions.DependencyModel (>= 7.0.0)
		Microsoft.EntityFrameworkCore.Relational (>= 7.0.2)



	<PackageReference Include="Microsoft.EntityFrameworkCore.Tools" Version="7.0.2">
		<PrivateAssets>all</PrivateAssets>
		<IncludeAssets>runtime; build; native; contentfiles; analyzers</IncludeAssets>
	</PackageReference>

		Microsoft.EntityFrameworkCore.Design (>= 7.0.2)



	<PackageReference Include="Microsoft.EntityFrameworkCore.InMemory" Version="7.0.2" />

		Microsoft.EntityFrameworkCore (>= 7.0.2)



	<PackageReference Include="Microsoft.EntityFrameworkCore.Proxies" Version="7.0.2" />

		Castle.Core (>= 5.1.0)
		Microsoft.EntityFrameworkCore (>= 7.0.2)



	<PackageReference Include="Microsoft.AspNetCore.Identity.EntityFrameworkCore" Version="7.0.2" />

		Microsoft.Extensions.Identity.Stores (>= 7.0.2)
		Microsoft.EntityFrameworkCore.Relational (>= 7.0.2)
**************************************************

**************************************************
In Project: IdentityServer

	Install Nuget:

	<ItemGroup>
		<PackageReference Include="Microsoft.EntityFrameworkCore.SqlServer" Version="7.0.2" />
	</ItemGroup>
**************************************************

**************************************************
Configuring the Stores

	Duende.IdentityServer.EntityFramework stores configuration and
	operational data in separate stores, each with their own DbContext.

	ConfigurationDbContext:

		used for configuration data such as clients, resources, and scopes
	
	PersistedGrantDbContext:
	
		used for dynamic operational data such as authorization codes and refresh tokens
**************************************************

**************************************************
To use these stores, replace the existing calls to AddInMemoryClients,
AddInMemoryIdentityResources, and AddInMemoryApiScopes in your
ConfigureServices method in src/IdentityServer/HostingExtensions.cs with
AddConfigurationStore and AddOperationalStore, like this:

	var migrationsAssembly =
		typeof(Program).Assembly.GetName().Name;

	const string connectionString =
		@"Data Source=Duende.IdentityServer.Quickstart.EntityFramework.db";

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
					sql.MigrationsAssembly(assemblyName: migrationsAssembly);
				});
			};
		})

		.AddOperationalStore(options =>
		{
			options.ConfigureDbContext = b =>
			{
				b.UseSqlServer(connectionString: connectionString, sqlServerOptionsAction: sql =>
				{
					sql.MigrationsAssembly(assemblyName: migrationsAssembly);
				});
			};
		})

		.AddTestUsers
			(users: IdentityServer.TestUsers.Users)
		;
**************************************************

**************************************************
Managing the Database Schema

	The Duende.IdentityServer.EntityFramework.Storage NuGet package
	(installed as a dependency of Duende.IdentityServer.EntityFramework)
	contains entity classes that map onto IdentityServer’s models. These
	entities are maintained in sync with IdentityServer’s models - when
	the models are changed in a new release, corresponding changes are
	made to the entities. As you use IdentityServer and upgrade over
	time, you are responsible for your database schema and changes
	necessary to that schema.

	One approach for managing those changes is to use EF migrations,
	which is what this quickstart will use. If migrations are not your
	preference, then you can manage the schema changes in any way you see fit.
**************************************************

**************************************************
Adding Migrations

	To create migrations, you will need to install the Entity
	Framework Core CLI tool on your machine and the
	Microsoft.EntityFrameworkCore.Design NuGet package in
	IdentityServer. Run the following commands from the src/IdentityServer directory:

		dotnet tool install --global dotnet-ef

		dotnet add package Microsoft.EntityFrameworkCore.Design
		or



**************************************************

**************************************************
Handle Expected Exception

	The Entity Framework CLI internally starts up IdentityServer
	for a short time in order to read your database configuration.
	After it has read the configuration, it shuts IdentityServer
	down by throwing a StopTheHostException (in Entity Framework 6)
	or HostAbortedException (in Entity Framework 7) exception.
	We expect this exception to be unhandled and therefore stop
	IdentityServer. Since it is expected, you do not need to log
	it as a fatal error. Update the error logging code in
	src/IdentityServer/Program.cs as follows:

		catch (System.Exception ex) when
			(ex.GetType().Name is not "StopTheHostException"
			&&
			ex.GetType().Name is not "HostAbortedException")
		{
			Serilog.Log.Fatal
				(exception: ex, messageTemplate: "Unhandled exception");
		}
**************************************************

**************************************************
Now run the following two commands from the src/IdentityServer
directory to create the migrations:

	dotnet ef migrations add InitialIdentityServerPersistedGrantDbMigration -c PersistedGrantDbContext -o Data/Migrations/IdentityServer/PersistedGrantDb
	dotnet ef migrations add InitialIdentityServerConfigurationDbMigration -c ConfigurationDbContext -o Data/Migrations/IdentityServer/ConfigurationDb

	OR

	add-migration InitialIdentityServerPersistedGrantDbMigration -c PersistedGrantDbContext -o Data/Migrations/IdentityServer/PersistedGrantDb
	add-migration InitialIdentityServerConfigurationDbMigration -c ConfigurationDbContext -o Data/Migrations/IdentityServer/ConfigurationDb

You should now see a src/IdentityServer/Data/Migrations/IdentityServer
directory in your project containing the code for your newly created migrations.
**************************************************

**************************************************
Initializing the Database

	Now that you have the migrations, you can write code to create the
	database from them and seed the database with the same configuration
	data used in the previous quickstarts.

	Note:

		The approach used in this quickstart is used to make it easy to
		get IdentityServer up and running. You should devise your own database
		creation and maintenance strategy that is appropriate for your architecture.

	In src/IdentityServer/HostingExtensions.cs, add this method to
	initialize the database:

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

	Call InitializeDatabase from the ConfigurePipeline method:

		public static WebApplication ConfigurePipeline(this WebApplication app)
		{ 
			app.UseSerilogRequestLogging();

			if (app.Environment.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			InitializeDatabase(app);

			//...
		}

	Note:

		The InitializeDatabase method is convenient way to seed the database,
		but this approach is not ideal to leave in to execute each time the
		application runs. Once your database is populated, consider removing
		the call to the API.

	Now if you run the IdentityServer project, the database should be created
	and seeded with the quickstart configuration data. You should be able to
	use a tool like SQL Lite Studio to connect and inspect the data.
**************************************************

**************************************************
Run the client applications

You should now be able to run any of the existing client applications and
sign-in, get tokens, and call the API – all based upon the database configuration.
**************************************************
