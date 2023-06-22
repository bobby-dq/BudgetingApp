using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BudgetingApp.Models.RepositoryModels
{
    public class IdentitySeedData
    {
        public static void CreateAdminAccount(IServiceProvider serviceProvider, IConfiguration configuration)
        {
            CreateAdminAccountAsync(serviceProvider, configuration).Wait();
        }

        public static async Task CreateAdminAccountAsync (IServiceProvider serviceProvider, IConfiguration configuration)
        {
            serviceProvider = serviceProvider.CreateScope().ServiceProvider;

            UserManager<IdentityUser> userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();
            RoleManager<IdentityRole> roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            string username = configuration["Data:AdminUser:Name"] ?? "BobbyDQ";
            string email = configuration["Data:AdminUser:Email"] ?? "seed.com";
            string password = configuration["Data:AdminUser:Role"] ?? "Password123$";
            string role = configuration["Data:AdminUser:Role"] ?? "Admin";

            if (await userManager.FindByNameAsync(username) == null)
            {
                if (await roleManager.FindByNameAsync(role) == null)
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }

                IdentityUser user = new IdentityUser
                {
                    UserName = username,
                    Email = email
                };

                IdentityResult result = await userManager.CreateAsync(user, password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, role);
                }
            }
                
        }
    }
}