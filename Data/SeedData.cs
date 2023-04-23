using AppDev.Helpers;
using AppDev.Models;
using Microsoft.AspNetCore.Identity;

namespace AppDev.Data
{
    public class SeedData
    {

        public async static Task SeedAsync(IServiceProvider sp)
        {
            var roleManager = sp.GetRequiredService<RoleManager<IdentityRole>>();

            if (!await roleManager.RoleExistsAsync(Roles.User))
            {
                await roleManager.CreateAsync(new(Roles.User));
            }
            if (!await roleManager.RoleExistsAsync(Roles.Admin))
            {
                await roleManager.CreateAsync(new(Roles.Admin));
            }
            if (!await roleManager.RoleExistsAsync(Roles.StoreOwner))
            {
                await roleManager.CreateAsync(new(Roles.StoreOwner));
            }

            var userManager = sp.GetRequiredService<UserManager<ApplicationUser>>();

            if (await userManager.FindByEmailAsync("admin@g.c") == null)
            {
                var admin = new ApplicationUser()
                {
                    UserName = "admin@g.c",
                    FullName = "Administrator",
                    HomeAddress = "",
                    EmailConfirmed= true,
                };
                await userManager.CreateAsync(admin, "Asd@123");

                await userManager.AddToRoleAsync(admin, "Admin");
            }
        }
    }
}
