using eSports.DAL;
using eSports.DAL.Interfaces;
using eSports.DAL.Repositories;
using eSports.Domain.Tournament.Entity;
using eSports.Service.Tournaments.Implementations;
using eSports.Service.Tournaments.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCors(options =>
    options.AddPolicy("AllowAllOrigins",
        policy => policy.AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod()
    )
);

builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();

builder.Services.AddScoped<IBaseRepository<TournamentEntity>, TournamentRepository>();
builder.Services.AddScoped<ITournamentService, TournamentService>();

var connectionString = builder.Configuration.GetConnectionString("MSSQL");
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(connectionString);
    options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
});

var app = builder.Build();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseCors("AllowAllOrigins");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Tournaments}/{action=TournamentsHandler}");

app.Run();
