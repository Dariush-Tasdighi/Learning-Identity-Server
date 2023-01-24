**************************************************
Create A New Project:

	ASP.NET Core Web App:

		RazorPages
**************************************************

**************************************************
Install Nugets:

	<ItemGroup>
		<PackageReference Include="IdentityModel" Version="6.0.0" />
		<PackageReference Include="Microsoft.AspNetCore.Authentication.OpenIdConnect" Version="7.0.1" />
	</ItemGroup>

**************************************************

**************************************************
Create Folder:

	Models
**************************************************

**************************************************
Create File:

	Customer.cs
**************************************************

**************************************************
Create Folder:

	Services
**************************************************

**************************************************
Create Files in Services Folder:

	TokenService
	ITokenService
	IdentityServerSettings
**************************************************

**************************************************
Update Files:

	appsettings.json
	appsettings.Development.json
**************************************************

**************************************************
In Pages Folder -> Create Folder:

	Employees
**************************************************

**************************************************
In Folder Pages\Employees, Add Razor Page:

	Index (For Displaying All Employees)
**************************************************

**************************************************
Update File:

	Program.cs
**************************************************

**************************************************
In Pages\Shared Folder:

	Update File:

		_Layout.cshtml (Add New Menu Item for Displaying Customers)
**************************************************
