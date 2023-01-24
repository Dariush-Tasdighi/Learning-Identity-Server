using System.Linq;
using NuGet.Protocol;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

var builder =
	Microsoft.AspNetCore.Builder
	.WebApplication.CreateBuilder(args);

builder.Services
	.AddControllers();

//builder.Services
//	.AddControllers(options =>
//{
//	options.Filters.Add(new Microsoft.AspNetCore.Mvc
//		.ProducesAttribute(contentType: "application/json"));
//});

//builder.Services
//	.AddControllers(options =>
//	{
//	})
//	.ConfigureApiBehaviorOptions(options =>
//	{
//		options.InvalidModelStateResponseFactory = context =>
//		{
//			var loggerFactory =
//				Microsoft.Extensions.Logging.LoggerFactory
//				.Create(builder => builder.AddConsole());

//			var logger =
//				loggerFactory.CreateLogger(categoryName: "BadRequest");

//			var result =
//				from ms in context.ModelState
//				where ms.Value.Errors.Any()
//				let fieldKey = ms.Key
//				let errors = ms.Value.Errors
//				from error in errors
//				select error.ErrorMessage;

//			logger.LogError(message: result.ToJson());

//			return new Microsoft.AspNetCore.Mvc
//				.BadRequestObjectResult(modelState: context.ModelState);
//		};
//	});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<Models.DatabaseContext>(options =>
{
	options.UseInMemoryDatabase
		(databaseName: "MyInMemoryDatabase");
});

var app =
	builder.Build();

if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
