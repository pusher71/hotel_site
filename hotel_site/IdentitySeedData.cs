using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace hotel_site
{
    public static class IdentitySeedData
    {
        private const string adminUser = "Admin";
        private const string adminPassword = "Secret123$";
        public static async void EnsurePopulated(IApplicationBuilder арр)
        {
            using var scope = арр.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope();
            UserManager<IdentityUser> userManager = scope.ServiceProvider.GetService<UserManager<IdentityUser>>();

            IdentityUser user = await userManager.FindByIdAsync(adminUser);
            if (user == null)
            {
                user = new IdentityUser(adminUser);
                await userManager.CreateAsync(user, adminPassword);
            }
        }
    }
}
