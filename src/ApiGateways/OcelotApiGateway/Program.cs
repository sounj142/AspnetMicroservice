using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using OcelotApiGateway;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddOcelot();
builder.ConfigureDownstreamHostAndPortsPlaceholders();

var app = builder.Build();

await app.UseOcelot();

app.Run();