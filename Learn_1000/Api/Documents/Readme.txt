**************************************************
Delete all about Weather Forcast elements!

	Model:		WeatherForecast.cs
	Controller:	WeatherForecastController.cs
**************************************************

**************************************************
In Api.csproj:

	<PropertyGroup>
		<IncludeOpenAPIAnalyzers>true</IncludeOpenAPIAnalyzers>
	</PropertyGroup>

	<ItemGroup>
		<PackageReference Include="Microsoft.EntityFrameworkCore" Version="7.0.1" />
		<PackageReference Include="Microsoft.EntityFrameworkCore.InMemory" Version="7.0.1" />
		<PackageReference Include="Microsoft.EntityFrameworkCore.SqlServer" Version="7.0.1" />

		<PackageReference Include="Microsoft.EntityFrameworkCore.Tools" Version="7.0.1">
			<PrivateAssets>all</PrivateAssets>
			<IncludeAssets>runtime; build; native; contentfiles; analyzers; buildtransitive</IncludeAssets>
		</PackageReference>

		<PackageReference Include="Microsoft.VisualStudio.Web.CodeGeneration.Design" Version="7.0.1" />
	</ItemGroup>
**************************************************

**************************************************
Create Folder: Models
**************************************************

**************************************************
Create Class: Customer.cs in Models Folder
**************************************************

**************************************************
Create Class: DatabaseContext.cs in Models Folder
**************************************************

**************************************************
In Program.cs:

using Microsoft.EntityFrameworkCore;

builder.Services.AddDbContext<Models.DatabaseContext>(options =>
{
    options.UseInMemoryDatabase
        (databaseName: "MyInMemoryDatabase");
});
**************************************************

**************************************************
In Contollers Folder:

	Create CustomersController.cs

	[API Controllers with actions, using Entity Framework]
**************************************************

**************************************************
In Postman:

	Create Collection

		Name: Learning Identity Server 4 (Learn 100)

		In Description:

			Endpoints:

			Get All Customers		: api/customers		GET
			Get Customer By Id		: api/customers/1	GET
			Create Customer			: api/customers		POST	Customer Object
			Update Customer	By Id	: api/customers/1	PUT		Customer Object
			Delete Customer By Id	: api/customers/1	DELETE
**************************************************

**************************************************
In Postman:

	Export Collection

	Delete Collection

	Import Collection
**************************************************
