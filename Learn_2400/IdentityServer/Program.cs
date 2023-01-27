using Serilog;
using Infrastructure;

Serilog.Log.Logger =
	new Serilog.LoggerConfiguration()
	// Console() -> using Serilog;
	.WriteTo.Console()
	.CreateBootstrapLogger();

Serilog.Log.Information
	(messageTemplate: "Starting up");

try
{
	var builder =
		Microsoft.AspNetCore.Builder
		.WebApplication.CreateBuilder(args: args);

	// UseSerilog() -> using Serilog;
	builder.Host.UseSerilog((ctx, lc) => lc
		.WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level}] {SourceContext}{NewLine}{Message:lj}{NewLine}{Exception}{NewLine}")
		.Enrich.FromLogContext()
		.ReadFrom.Configuration(configuration: ctx.Configuration));

	var app =
		builder

		// ConfigureServices() -> using Infrastructure;;
		.ConfigureServices()

		// ConfigurePipeline() -> using Infrastructure;;
		.ConfigurePipeline()
		;

	app.Run();
}
//catch (System.Exception ex)
//{
//	Serilog.Log.Fatal
//		(exception: ex, messageTemplate: "Unhandled exception");
//}
catch (System.Exception ex) when
	// https://github.com/dotnet/runtime/issues/60600
	(ex.GetType().Name is not "StopTheHostException"
	// HostAbortedException was added in .NET 7,
	// but since we target .NET 6 we need to do it this way until we target .NET 8
	&& ex.GetType().Name is not "HostAbortedException")
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
