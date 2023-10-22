using eSports.DAL;
using eSports.DAL.Interfaces;
using eSports.DAL.Repositories;
using eSports.Domain.Stats.Entity;
using eSports.Domain.Teams.Entity;
using eSports.Service.Stats.Implementations;
using eSports.Service.Stats.Interfaces;
using eSports.Service.Teams.Implementations;
using eSports.Service.Teams.Interfaces;
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

builder.Services.AddScoped<IBaseRepository<StatsEntity>, StatsRepository>();
builder.Services.AddScoped<IStatsService, StatsService>();
builder.Services.AddScoped<IBaseRepository<TeamEntity>, TeamRepository>();
builder.Services.AddScoped<ITeamService, TeamService>();

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

//app.MapControllerRoute(
//    name: "default",
//    pattern: "{controller=Stats}/{action=StatsHandler}");

app.Run();