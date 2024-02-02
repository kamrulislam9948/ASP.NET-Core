using Microsoft.AspNetCore.Identity;
using R54_M8_Class_09_Work_01.Models;

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
        }
        public async Task SeedAsync()
        {
            await CreateRoleAsync(new IdentityRole { Name = "JobSeeker", NormalizedName = "JOOBSEEKER" });
            await CreateRoleAsync(new IdentityRole { Name = "JobProvider", NormalizedName = "JOBPROVIDER" });
        }
        private async Task CreateRoleAsync(IdentityRole role)
        {
            var exits = await roleManager.RoleExistsAsync(role.Name ?? "");
            if (!exits)
                await roleManager.CreateAsync(role);
        }

    }
}
