using Microsoft.EntityFrameworkCore;
using R56_M8_Class_07_Work_01.Models;
using R56_M8_Class_07_Work_01.Repositories;
using R56_M8_Class_07_Work_01.Repositories.Interfaces;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<BookDbContext>(op => op.UseSqlServer(builder.Configuration.GetConnectionString("db")));

builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IBookRepo, BookRepo>();
builder.Services.AddScoped<IAuthorRepo, AuthorRepo>();
var app = builder.Build();

app.UseStaticFiles();
app.MapDefaultControllerRoute();


app.Run();
