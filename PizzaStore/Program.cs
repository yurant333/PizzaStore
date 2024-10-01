using Microsoft.OpenApi.Models;
using PizzaStore.Models;
using PizzaStore.Repository;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("Pizzas") ?? "Data Source=Pizzas.db";

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSqlite<PizzaDb>(connectionString);
builder.Services.AddScoped<IPizzaRepository, PizzaRepository>();

builder.Services.AddSwaggerGen(c =>
{
     c.SwaggerDoc("v1", new OpenApiInfo {
         Title = "PizzaStore API",
         Description = "Making the Pizzas you love",
         Version = "v1" });
});

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI(c =>
{
   c.SwaggerEndpoint("/swagger/v1/swagger.json", "PizzaStore API V1");
});

app.MapGet("/pizzas", async (IPizzaRepository repository) => await repository.GetPizzasAsync());
app.MapPost("/pizza", async (IPizzaRepository repository, Pizza pizza) =>
{
    await repository.AddPizzaAsync(pizza);
    return Results.Created($"/pizza/{pizza.Id}", pizza);
});
app.MapGet("/pizza/{id}", async (IPizzaRepository repository, int id) => await repository.GetPizzaByIdAsync(id));
app.MapPut("/pizza/{id}", async (IPizzaRepository repository, Pizza updatepizza, int id) =>
{
    var pizza = await repository.GetPizzaByIdAsync(id);
    if (pizza is null) return Results.NotFound();
    
    pizza.Name = updatepizza.Name;
    pizza.Description = updatepizza.Description;
    
    await repository.UpdatePizzaAsync(pizza);
    return Results.NoContent();
});
app.MapDelete("/pizza/{id}", async (IPizzaRepository repository, int id) =>
{
    await repository.DeletePizzaAsync(id);
    return Results.Ok();
});


app.Run();