using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

var builder =
	Microsoft.AspNetCore.Builder
	.WebApplication.CreateBuilder(args);

builder.Services
	.AddControllers();

builder.Services
	.AddEndpointsApiExplorer();

builder.Services
	.AddSwaggerGen();

builder.Services
	.AddDbContext<Models.DatabaseContext>
	(optionsAction: options =>
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
