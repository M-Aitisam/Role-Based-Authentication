using Microsoft.AspNetCore.Identity;

namespace Role_Based_Authentication.Data
{

    public static class RoleGiven
    {
        public static async Task SeedRoles(RoleManager<IdentityRole> roleManager)
        {
            if (!await roleManager.RoleExistsAsync("Supervisor"))
            {
                await roleManager.CreateAsync(new IdentityRole("Supervisor"));
            }

            if (!await roleManager.RoleExistsAsync("Student"))
            {
                await roleManager.CreateAsync(new IdentityRole("Student"));
            }
        }
    }
}

