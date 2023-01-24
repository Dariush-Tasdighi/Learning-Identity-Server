using Serilog;
using Infrastructure;

Serilog.Log.Logger =
	new Serilog.LoggerConfiguration()
	.WriteTo.Console()
	.CreateBootstrapLogger();

Serilog.Log.Information
	(messageTemplate: "Starting up");

try
{
	var builder =
		Microsoft.AspNetCore.Builder
		.WebApplication.CreateBuilder(args: args);

	builder.Host.UseSerilog((ctx, lc) => lc
		.WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level}] {SourceContext}{NewLine}{Message:lj}{NewLine}{Exception}{NewLine}")
		.Enrich.FromLogContext()
		.ReadFrom.Configuration(configuration: ctx.Configuration));

	var app = builder
		// ConfigureServices() -> using Infrastructure;;
		.ConfigureServices()
		// ConfigurePipeline() -> using Infrastructure;;
		.ConfigurePipeline()
		;

	app.Run();
}
catch (System.Exception ex)
{
	Serilog.Log.Fatal
		(exception: ex, messageTemplate: "Unhandled exception");
}
finally
{
	Serilog.Log.Information
		(messageTemplate: "Shut down complete");

	Serilog.Log.CloseAndFlush();
}
