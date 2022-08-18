using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using OrcaApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();

// database setup
var connectionString = builder.Configuration["AzureSQLConnectionString"];
builder.Services.AddDbContext<OrcaDb>(options => options.UseSqlServer(connectionString));

// swagger
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Orca API",
        Version = "v1"
    });
});

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Orca API V1");
});

app.MapGet("/", () => "Hello World!");

#region  Cinemas API

app.MapGet("/cinemas", async (OrcaDb db) => await db.Cinemas.ToListAsync());

app.MapPost("cinema", async (OrcaDb db, Cinema cinema) =>
{
    await db.Cinemas.AddAsync(cinema);
    await db.SaveChangesAsync();
    return Results.Created($"/cinema/{cinema.Id}", cinema);
});

app.MapGet("/cinema/{id}", async (OrcaDb db, int id) => await db.Cinemas.FindAsync(id));

app.MapPut("/cinema/{id}", async (OrcaDb db, Cinema updatedCinema, int id) =>
{
      var cinema = await db.Cinemas.FindAsync(id);
      if (cinema is null) return Results.NotFound();
      cinema.Name = updatedCinema.Name;
      cinema.Location = updatedCinema.Location;
      await db.SaveChangesAsync();
      return Results.NoContent();
});

app.MapDelete("/cinema/{id}", async (OrcaDb db, int id) =>
{
   var cinema = await db.Cinemas.FindAsync(id);
   if (cinema is null)
   {
      return Results.NotFound();
   }
   db.Cinemas.Remove(cinema);
   await db.SaveChangesAsync();
   return Results.Ok();
});

#endregion

app.Run();
