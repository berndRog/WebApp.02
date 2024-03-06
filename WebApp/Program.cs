using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.HttpLogging;
using Microsoft.Extensions.DependencyInjection;
using WebApp;

// WebApplication Builder Pattern
var builder = WebApplication.CreateBuilder(args);

// add http logging 
builder.Services.AddHttpLogging(opts =>
   opts.LoggingFields = HttpLoggingFields.All);

// Build the WebApplication
var app = builder.Build();

// use http logging
app.UseHttpLogging();

// routing
Map.StaticFilesRoutes(app);
Map.BookRoutes(app);

// Run the WebApplication
app.Run();

