using MongoDB.Driver;
using SlugBasedApi.Database;
using SlugBasedApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton(serviceProvider =>
{
    var connectionString = "mongodb://localhost:27017";
    var databaseName = "SlugBased";
    var logger = serviceProvider.GetRequiredService<ILogger<MongoDbContext>>();

    return new MongoDbContext(connectionString, databaseName, logger);
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapPost("/item", async (MongoDbContext mongoDbContext, CreateItemRequest request) =>
{
    var item = new Item
    {
        Id = Guid.NewGuid(),
        Description = request.Description,
        Year = 2023
    };

    await mongoDbContext.Items.InsertOneAsync(item);

    return Results.Created($"/users/{item.Id}", item);
})
.WithName("CreateItem")
.WithOpenApi();

app.MapGet("/item/{idOrSlug}", async (MongoDbContext mongoDbContext, string idOrSlug) =>
{
    var item = Guid.TryParse(idOrSlug, out var id)
        ? await mongoDbContext.Items.Find(x => x.Id == id).FirstOrDefaultAsync()
        : await mongoDbContext.Items.Find(x => x.Slug == idOrSlug).FirstOrDefaultAsync();

    if (item is null)
    {
        return Results.NotFound();
    }

    return Results.Ok(item);
})
.WithName("GetItem")
.WithOpenApi();

app.Run();

public record CreateItemRequest(string Description, int Year);