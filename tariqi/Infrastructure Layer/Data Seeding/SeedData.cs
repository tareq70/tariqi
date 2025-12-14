using Microsoft.AspNetCore.Identity;
using tariqi.Domain_Layer.Entities;
using tariqi.Domain_Layer.Enums;

namespace tariqi.Infrastructure_Layer.Data_Seeding
{
    public class SeedData
    {
        public static async Task SeedRolesAndAdminAsync(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            var roles = new[] { "Admin", "RegionManager", "AreaManager", "Driver", "Passenger" };

            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }

            var adminEmail = "admin@tariqi.com";
            var adminUser = await userManager.FindByEmailAsync(adminEmail);
            if (adminUser == null)
            {
                adminUser = new ApplicationUser
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    FullName = "Super Admin",
                    Role = UserRole.Admin,
                    IsActive = true
                };

                var result = await userManager.CreateAsync(adminUser, "Admin@123"); 
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(adminUser, "Admin");
                }
            }
        }
    }
}
