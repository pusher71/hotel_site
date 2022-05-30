using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using hotel_site.Models;

namespace hotel_site
{
    public static class IdentitySeedData
    {
        private const string adminUserName = "Admin";
        private const string adminFirstName = "Admin";
        private const string adminLastName = "Adminov";
        private const string adminPassword = "Secret123$";
        public static async void EnsurePopulated(IApplicationBuilder арр)
        {
            using var scope = арр.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope();
            UserManager<User> userManager = scope.ServiceProvider.GetService<UserManager<User>>();

            User admin = await userManager.FindByIdAsync(adminUserName);
            if (admin == null)
            {
                admin = new User(adminUserName, adminFirstName, adminLastName);
                await userManager.CreateAsync(admin, adminPassword);
            }
        }
    }
}
