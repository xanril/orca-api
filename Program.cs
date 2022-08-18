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

#region Movies API

app.MapGet("/movies", async (OrcaDb db) => await db.Movies.ToListAsync());

app.MapPost("movie", async (OrcaDb db, Movie movie) =>
{
    await db.Movies.AddAsync(movie);
    await db.SaveChangesAsync();
    return Results.Created($"/movie/{movie.Id}", movie);
});

app.MapGet("/movie/{id}", async (OrcaDb db, int id) => await db.Movies.FindAsync(id));

app.MapPut("/movie/{id}", async (OrcaDb db, Movie updatedMovie, int id) =>
{
      var movie = await db.Movies.FindAsync(id);
      if (movie is null) return Results.NotFound();
      movie.TmdbId = updatedMovie.TmdbId;
      movie.Title = updatedMovie.Title;
      movie.Tagline = updatedMovie.Tagline;
      movie.Overview = updatedMovie.Overview;
      movie.Runtime = updatedMovie.Runtime;
      movie.PosterUrl = updatedMovie.PosterUrl;
      movie.BackdropUrl = updatedMovie.BackdropUrl;
      movie.ReleaseDate = updatedMovie.ReleaseDate;
      await db.SaveChangesAsync();
      return Results.NoContent();
});

app.MapDelete("/movie/{id}", async (OrcaDb db, int id) =>
{
   var movie = await db.Movies.FindAsync(id);
   if (movie is null)
   {
      return Results.NotFound();
   }
   db.Movies.Remove(movie);
   await db.SaveChangesAsync();
   return Results.Ok();
});

#endregion

#region Rooms API

app.MapGet("/rooms", async (OrcaDb db) => await db.Rooms.ToListAsync());

app.MapPost("room", async (OrcaDb db, Room room) =>
{
    await db.Rooms.AddAsync(room);
    await db.SaveChangesAsync();
    return Results.Created($"/room/{room.Id}", room);
});

app.MapGet("/room/{id}", async (OrcaDb db, int id) => await db.Rooms.FindAsync(id));

app.MapPut("/room/{id}", async (OrcaDb db, Room updatedRoom, int id) =>
{
      var room = await db.Rooms.FindAsync(id);
      if (room is null) return Results.NotFound();
      room.Name = updatedRoom.Name;
      room.CinemaId = updatedRoom.CinemaId;
      await db.SaveChangesAsync();
      return Results.NoContent();
});

app.MapDelete("/room/{id}", async (OrcaDb db, int id) =>
{
   var room = await db.Rooms.FindAsync(id);
   if (room is null)
   {
      return Results.NotFound();
   }
   db.Rooms.Remove(room);
   await db.SaveChangesAsync();
   return Results.Ok();
});

#endregion

#region Schedules API

app.MapGet("/schedules", async (OrcaDb db) => await db.Schedules.ToListAsync());

app.MapPost("schedule", async (OrcaDb db, Schedule schedule) =>
{
    await db.Schedules.AddAsync(schedule);
    await db.SaveChangesAsync();
    return Results.Created($"/schedule/{schedule.Id}", schedule);
});

app.MapGet("/schedule/{id}", async (OrcaDb db, int id) => await db.Schedules.FindAsync(id));

app.MapPut("/schedule/{id}", async (OrcaDb db, Schedule updatedSchedule, int id) =>
{
      var schedule = await db.Schedules.FindAsync(id);
      if (schedule is null) return Results.NotFound();
      schedule.CinemaId = updatedSchedule.CinemaId;
      schedule.RoomId = updatedSchedule.RoomId;
      schedule.MovieId = updatedSchedule.MovieId;
      schedule.DayOfWeek = updatedSchedule.DayOfWeek;
      schedule.StartTime = updatedSchedule.StartTime;
      schedule.EndTime = updatedSchedule.EndTime;
      schedule.TicketPrice = updatedSchedule.TicketPrice;
      await db.SaveChangesAsync();
      return Results.NoContent();
});

app.MapDelete("/schedule/{id}", async (OrcaDb db, int id) =>
{
   var schedule = await db.Schedules.FindAsync(id);
   if (schedule is null)
   {
      return Results.NotFound();
   }
   db.Schedules.Remove(schedule);
   await db.SaveChangesAsync();
   return Results.Ok();
});

#endregion


app.Run();
