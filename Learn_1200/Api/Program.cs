using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

var builder =
	Microsoft.AspNetCore.Builder
	.WebApplication.CreateBuilder(args);

builder.Services
	.AddControllers();

builder.Services.AddDbContext<Models.DatabaseContext>(options =>
{
	options.UseInMemoryDatabase
		(databaseName: "MyInMemoryDatabase");
});

// **************************************************
// *** Step(4) * ************************************
// **************************************************
var accessTokenSecurityKey =
	builder.Configuration.GetSection
	(key: "AccessTokenSecurityKey").Value;

if (accessTokenSecurityKey == null)
{
	return;
}

if (accessTokenSecurityKey.Length < 16)
{
	return;
}

var accessTokenSecurityKeyBytes =
	System.Text.Encoding.UTF8.GetBytes(s: accessTokenSecurityKey);

var symmetricSecurityKey =
	new Microsoft.IdentityModel.Tokens
	.SymmetricSecurityKey(key: accessTokenSecurityKeyBytes);

var tokenValidationParameters =
	new Microsoft.IdentityModel.Tokens.TokenValidationParameters
	{
		ValidateIssuer = false,
		ValidateAudience = false,

		ValidateIssuerSigningKey = true,
		IssuerSigningKey = symmetricSecurityKey,
	};

builder.Services
	.AddAuthentication(defaultScheme: Microsoft.AspNetCore
		.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)

	.AddJwtBearer(options =>
	{
		options.TokenValidationParameters = tokenValidationParameters;
	})
	;
// **************************************************

builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

// **************************************************
// *** Step(8) * ************************************
// **************************************************
// هر کلمه‌ای می‌تواند باشد
var schemaName = "Bearer";

var openApiSecurityScheme =
	new Microsoft.OpenApi.Models.OpenApiSecurityScheme
	{
		Name = "Authorization",
		Description = "Please enter a valid token!",
		In = Microsoft.OpenApi.Models.ParameterLocation.Header,
		Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey,
	};

var openApiReference =
	new Microsoft.OpenApi.Models.OpenApiReference
	{
		Id =
			schemaName,

		Type =
			Microsoft.OpenApi.Models
			.ReferenceType.SecurityScheme,
	};

var currentOpenApiSecurityScheme =
	new Microsoft.OpenApi.Models.OpenApiSecurityScheme
	{
		Reference =
			openApiReference,
	};

var openApiSecurityRequirement =
	new Microsoft.OpenApi.Models.OpenApiSecurityRequirement();

openApiSecurityRequirement.Add
	(key: currentOpenApiSecurityScheme, value: new string[] { });

builder.Services.AddSwaggerGen(options =>
{
	options.AddSecurityRequirement
		(securityRequirement: openApiSecurityRequirement);

	options.AddSecurityDefinition
		(name: schemaName, securityScheme: openApiSecurityScheme);
});
// **************************************************

var app =
	builder.Build();

if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// **************************************************
// این برنامه بدون دستورات ذیل نیز کار می‌کند
// **************************************************
//app.UseAuthentication();
//app.UseAuthorization();
// **************************************************

app.MapControllers();

app.Run();
