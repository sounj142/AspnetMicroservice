using Catalog.Api;
using Catalog.Api.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
ConfigureServices.Config(builder);

var app = builder.Build();

CatalogDbMigration.SeedData(app.Services);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();