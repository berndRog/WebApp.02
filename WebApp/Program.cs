using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.HttpLogging;
using Microsoft.Extensions.DependencyInjection;
using WebApp;
using WebApp.Routing;

// WebApplication Builder Pattern
WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
// add http logging 
builder.Services.AddHttpLogging(opts =>
   opts.LoggingFields = HttpLoggingFields.All);


// Build the WebApplication
WebApplication app = builder.Build();
// use http logging
app.UseHttpLogging();

// routing
Map.StaticFilesRoutes(app);
Map.BookRoutes(app);


// Run the WebApplication
app.Run();

