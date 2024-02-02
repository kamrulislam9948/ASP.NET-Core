using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using R54_M8_Class_09_Work_01.HostedServices;
using R54_M8_Class_09_Work_01.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<AppDbContext>(op => op.UseSqlServer(builder.Configuration.GetConnectionString("db")));
#region Hosted services
builder.Services.AddScoped<IdentityDbSeeder>();
builder.Services.AddHostedService<SeederHostedService>();
#endregion
builder.Services.AddControllersWithViews();
#region identity configuration
builder.Services.AddIdentity<AppUser, IdentityRole>(op =>
{
    op.SignIn.RequireConfirmedAccount = false;
    op.Password.RequiredLength = 6;
    op.Password.RequireNonAlphanumeric = false;
    op.Password.RequireDigit = false;
    op.Password.RequireUppercase = false;
    op.Password.RequireLowercase = false;
}).AddEntityFrameworkStores<AppDbContext>();
builder.Services.ConfigureApplicationCookie(op =>
{
    op.LoginPath = "/Account/Login";
    op.Cookie.HttpOnly = true;
});
#endregion
var app = builder.Build();
app.UseStaticFiles();
app.UseAuthentication();
app.UseAuthorization();
app.MapDefaultControllerRoute();
app.Run();
