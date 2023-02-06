**************************************************
*** Step (1) *************************************
**************************************************
In Folder: Controllers

	Update File: CustomersController.cs

		Attributes:

			For Controller or Actions:

				[Microsoft.AspNetCore.Authorization.Authorize]
				[Microsoft.AspNetCore.Authorization.AllowAnonymous]

				[Microsoft.AspNetCore.Mvc.ProducesResponseType
					(statusCode: Microsoft.AspNetCore.Http.StatusCodes.Status401Unauthorized)]
**************************************************

**************************************************
*** Step (2) *************************************
**************************************************
حال اقدام به ایجاد یک مشتری می‌کنیم و با خطای ذیل مواجه می‌شویم

	No authenticationScheme was specified,
	and there was no DefaultChallengeScheme found.
**************************************************

**************************************************
*** Step (3) *************************************
**************************************************
Update File: Api.csproj

	<ItemGroup>
		<PackageReference Include="Microsoft.AspNetCore.Authentication.JwtBearer" Version="7.0.0" />
	</ItemGroup>
**************************************************

**************************************************
*** Step (4) *************************************
**************************************************
Update File:

	Program.cs

		builder.Services
			.AddAuthentication(defaultScheme: Microsoft.AspNetCore
				.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)

			.AddJwtBearer(options =>
			{
				options.TokenValidationParameters = tokenValidationParameters;
			})
			;
**************************************************

**************************************************
*** Step (5) *************************************
**************************************************
حال اقدام به ایجاد یک مشتری می‌کنیم و با خطای ذیل مواجه می‌شویم

	Error: response status is 401
	www-authenticate: Bearer
**************************************************

**************************************************
*** Step (6) *************************************
**************************************************
Test Application with Postman

	Register (POST)

	Login (POST)

	Get Access Token (JWT)

	Create Customer without Access Token (JWT)

		Error: 401

	Create Customer with Access Token (JWT)

		Authorization Tab

			Type

				Bearer Token
**************************************************

**************************************************
*** Step (7) *************************************
**************************************************
Activate Swagger Authentication

	Update File:

		Program.cs

			builder.Services.AddSwaggerGen(options =>
			{
				options.AddSecurityRequirement
					(securityRequirement: openApiSecurityRequirement);

				options.AddSecurityDefinition
					(name: schemaName, securityScheme: openApiSecurityScheme);
			});

Note: In TextBox we should write!!!

	brearer [Access Token]
**************************************************

**************************************************
*** Step (8) *************************************
**************************************************
In Folder: Controllers

	Update File: CustomersController.cs

		Attributes:

			For DeleteAsync() Action:

				[Microsoft.AspNetCore.Authorization.Authorize(Roles = "Admin")]

				[Microsoft.AspNetCore.Mvc.ProducesResponseType
					(statusCode: Microsoft.AspNetCore.Http.StatusCodes.Status403Forbidden)]
**************************************************

**************************************************
*** Step (9) *************************************
**************************************************
حال اقدام به حذف یک مشتری می‌کنیم و با خطای ذیل مواجه می‌شویم

	Error: response status is 403 (Forbidden) (Access Denied)
**************************************************

**************************************************
*** Step (10) ************************************
**************************************************
In Folder: Infrastructure

	Update File: Utility.cs

		Update Method: GetAccessToken()

			Add Role to User: 'Admin'
**************************************************

**************************************************
*** Step (11) ************************************
**************************************************
حال اقدام به حذف یک مشتری می‌کنیم
**************************************************
