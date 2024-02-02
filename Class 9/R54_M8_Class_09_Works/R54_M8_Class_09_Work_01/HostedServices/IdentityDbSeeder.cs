using Humanizer;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json.Linq;
using R54_M8_Class_09_Work_01.Models;
using System.Data;
using System.Reflection.Metadata;

namespace R54_M8_Class_09_Work_01.HostedServices
{
    public class IdentityDbSeeder
    {
        private readonly UserManager<AppUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly AppDbContext appDbContext;
        public IdentityDbSeeder(AppDbContext appDbContext,UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.appDbContext = appDbContext;

            if (!appDbContext.Database.CanConnect())
            {
                this.appDbContext.Database.EnsureCreated();
            }
            /*Within the constructor, there is a check to see if the database can be connected to(appDbContext.Database.CanConnect()).If the database can't be connected,
            it attempts to ensure that the database is created using appDbContext.Database.EnsureCreated(). This is typically used for database initialization,
            ensuring that the database schema is created if it doesn't exist.*/
        }
        public async Task SeedAsync()
        {
            await CreateRoleAsync(new IdentityRole { Name = "JobSeeker", NormalizedName = "JOOBSEEKER" });
            await CreateRoleAsync(new IdentityRole { Name = "JobProvider", NormalizedName = "JOBPROVIDER" });
            
        }
        //The SeedAsync method is responsible for seeding roles in the database.It creates two roles: "JobSeeker" and "JobProvider."
        private async Task CreateRoleAsync(IdentityRole role)
        {
            var exits = await roleManager.RoleExistsAsync(role.Name ?? "");
            if (!exits)
                await roleManager.CreateAsync(role);
        }
        /*This private method is used to create a role if it does not already exist.It takes an IdentityRole object as a parameter.
        It checks if the role already exists using roleManager.RoleExistsAsync(role.Name ?? ""). If the role doesn't exist (!exists),
        it creates the role using roleManager.CreateAsync(role).*/

    }
}
