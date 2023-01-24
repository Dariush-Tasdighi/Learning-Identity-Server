using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

var builder =
	Microsoft.AspNetCore.Builder
	.WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<Models.DatabaseContext>(options =>
{
	options.UseInMemoryDatabase
		(databaseName: "MyInMemoryDatabase");
});

// **************************************************
// *** Solution (1) *********************************
// **************************************************
builder.Services
	.AddAuthentication(defaultScheme: "Bearer")

	.AddJwtBearer(authenticationScheme: "Bearer", configureOptions: options =>
	{
		options.Authority = "https://localhost:5001";

		options.TokenValidationParameters =
			new Microsoft.IdentityModel.Tokens.TokenValidationParameters
			{
				ValidateAudience = false,
			};
	})
	;
// **************************************************

// **************************************************
// *** Solution (2) *********************************
// **************************************************
//builder.Services.AddAuthorization(options =>
//{
//	options.AddPolicy(name: "MyApiPolicy", configurePolicy: policy =>
//	{
//		policy.RequireAuthenticatedUser();

//		policy.RequireClaim
//			(claimType: "scope", allowedValues: "SomeAlakiName");
//	});
//});
// **************************************************

// **************************************************
// *** Solution (3) *********************************
// **************************************************
builder.Services.AddAuthorization(options =>
{
	options.AddPolicy(name: "MyApiPolicy", configurePolicy: policy =>
	{
		policy.RequireAuthenticatedUser();

		policy.RequireClaim
			(claimType: "scope", allowedValues: "MyApiScope1");
	});
});
// **************************************************












//builder.Services
//	.AddAuthentication(options =>
//	{
//		options.DefaultScheme =
//			Microsoft.AspNetCore.Authentication
//			.JwtBearer.JwtBearerDefaults.AuthenticationScheme;

//		options.DefaultChallengeScheme =
//			Microsoft.AspNetCore.Authentication
//			.JwtBearer.JwtBearerDefaults.AuthenticationScheme;
//	})
//	.AddIdentityServerAuthentication
//		(authenticationScheme: Microsoft.AspNetCore
//		.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme, options =>
//		{
//			options.ApiName = "MyApi";
//			options.ApiSecret = "MyApiSecret";

//			options.Authority = "https://localhost:5001";

//			options.JwtBearerEvents =
//				new Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerEvents
//				{
//					OnAuthenticationFailed = current =>
//					{
//						return System.Threading.Tasks.Task.CompletedTask;
//					},

//					OnTokenValidated = current =>
//					{
//						return System.Threading.Tasks.Task.CompletedTask;
//					},

//					OnChallenge = current =>
//					{
//						return System.Threading.Tasks.Task.CompletedTask;
//					},

//					OnForbidden = current =>
//					{
//						return System.Threading.Tasks.Task.CompletedTask;
//					},

//					OnMessageReceived = current =>
//					{
//						return System.Threading.Tasks.Task.CompletedTask;
//					},
//				};
//		});

// فعلا با دستورات ذیل کاری نداریم
//builder.Services
//	.AddCors(options =>
//	{
//		// This defines a CORS policy called "default"
//		options.AddPolicy("default", policy =>
//		{
//			policy
//				.AllowAnyHeader()
//				.AllowAnyMethod()
//				.WithOrigins(origins: "https://localhost:5001")
//				;
//		});
//	});

var app =
	builder.Build();

if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

// **************************************************
// *** Solution (1) *********************************
// **************************************************
app.MapControllers();
// **************************************************

// **************************************************
// *** Solution (2) *********************************
// **************************************************
//app.MapControllers()
//	.RequireAuthorization(policyNames: "MyApiPolicy")
//	;
// **************************************************

app.Run();
