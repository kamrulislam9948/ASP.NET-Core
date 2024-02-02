using Microsoft.EntityFrameworkCore;
using R54_M8_Class_01_Work_01.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<ApplicantDbContext>(op => op.UseSqlServer(builder.Configuration.GetConnectionString("db")));
builder.Services.AddControllersWithViews();
var app = builder.Build();
app.UseStaticFiles();
app.MapDefaultControllerRoute();
app.Run();
