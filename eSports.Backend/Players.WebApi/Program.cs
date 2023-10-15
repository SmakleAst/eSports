using eSports.Backend.DAL;
using eSports.Backend.DAL.Interfaces;
using eSports.Backend.DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using Player.Service.Implementations;
using Player.Service.Interfaces;
using Players.Domain.Entity;

var builder = WebApplication.CreateBuilder(args);

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

app.Run();
