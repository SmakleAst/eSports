using eSports.DAL.Interfaces;
using eSports.DAL.Repositories;
using eSports.Domain.Teams.Entity;
using eSports.Service.Teams.Interfaces;
using eSports.Service.Teams.Implementations;
using eSports.DAL;
using Microsoft.EntityFrameworkCore;
using eSports.Service.Players.Interfaces;
using eSports.Service.Players.Implementations;
using eSports.Domain.Players.Entity;
using eSports.Domain.Tournament.Entity;
using eSports.Service.Tournaments.Implementations;
using eSports.Service.Tournaments.Interfaces;
using eSports.Domain.Stats.Entity;
using eSports.Service.Stats.Implementations;
using eSports.Service.Stats.Interfaces;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCors(options =>
    options.AddPolicy("AllowAllOrigins",
        policy => policy.AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod()
    )
);

builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();

builder.Services.AddScoped<IBaseRepository<PlayerEntity>, PlayerRepository>();
builder.Services.AddScoped<IPlayerService, PlayerService>();
builder.Services.AddScoped<IBaseRepository<TeamEntity>, TeamRepository>();
builder.Services.AddScoped<ITeamService, TeamService>();
builder.Services.AddScoped<IBaseRepository<TournamentEntity>, TournamentRepository>();
builder.Services.AddScoped<ITournamentService, TournamentService>();
builder.Services.AddScoped<IBaseRepository<StatsEntity>, StatsRepository>();
builder.Services.AddScoped<IStatsService, StatsService>();

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
    pattern: "{controller=Team}/{action=TeamHandler}");

app.Run();
