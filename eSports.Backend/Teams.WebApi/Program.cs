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

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCors(options =>
    options.AddPolicy("AllowAllOrigins",
        policy => policy.AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod()
    )
);

builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();

builder.Services.AddScoped<IBaseRepository<TeamEntity>, TeamRepository>();
builder.Services.AddScoped<ITeamService, TeamService>();
builder.Services.AddScoped<IBaseRepository<PlayerEntity>, PlayerRepository>();
builder.Services.AddScoped<IPlayerService, PlayerService>();

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
