namespace SchoolApp.Common
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.DependencyInjection;

    public class RoleHelper
    {
        public static async Task SetRoles(IServiceProvider serviceProvider)
        {
            string[] roleNames = { "Administrator", "Teacher", "Parent" };

            foreach (var roleName in roleNames)
            {
                await AddRole(serviceProvider, roleName);
            }

            await SetAdmin(serviceProvider);
        }

        private static async Task AddRole(IServiceProvider serviceProvider, string roleName)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            if (!await roleManager.RoleExistsAsync(roleName))
            {
                await roleManager.CreateAsync(new IdentityRole(roleName));
            }
        }

        private static async Task SetAdmin(
            IServiceProvider serviceProvider, IdentityUser user = null, string adminRoleName = "Administrator")
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();

            if (user == null)
            {
                if (await userManager.FindByEmailAsync("admin@admin.pl") == null)
                {
                    var newUser = new IdentityUser
                    {
                        Email = "admin@admin.pl",
                        UserName = "admin@admin.pl"
                    };

                    var createResult = await userManager.CreateAsync(newUser, "Admin123.");

                    if (createResult.Succeeded)
                    {
                        user = newUser;
                    }
                    else
                    {
                        throw new Exception("Something went wrong with Admin creation :(");
                    }

                    await userManager.AddToRoleAsync(user, adminRoleName);
                }
                else
                {
                    return;
                }
            }

            await userManager.AddToRoleAsync(user, adminRoleName);
        }
    }
}