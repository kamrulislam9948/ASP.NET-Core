using Microsoft.EntityFrameworkCore;
using R54_M8_Class_06_Work_01.Models;
using R54_M8_Class_06_Work_01.Repositories;
using R54_M8_Class_06_Work_01.Repositories.Interfaces;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<BookDbContext>(op => op.UseSqlServer(builder.Configuration.GetConnectionString("db")));

builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
var app = builder.Build();
app.UseStaticFiles();
app.MapDefaultControllerRoute();

app.Run();
