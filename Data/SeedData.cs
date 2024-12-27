using Microsoft.AspNetCore.Identity;
using System.Linq;

namespace Role_Based_Authentication.Data
{
    public class SeedData
    {
        public static async Task Initialize(IServiceProvider serviceProvider, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            // Seed roles
            await CreateRoleIfNotExists(roleManager, "Supervisor");
            await CreateRoleIfNotExists(roleManager, "Student");

            // Seed Supervisor user
            var supervisorEmail = "supervisor@example.com";  // Changed email
            var supervisorUser = await userManager.FindByEmailAsync(supervisorEmail);
            if (supervisorUser == null)
            {
                supervisorUser = new IdentityUser { UserName = supervisorEmail, Email = supervisorEmail };
                var result = await userManager.CreateAsync(supervisorUser, "Supervisor@1234");  // Changed password
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(supervisorUser, "Supervisor");
                    Console.WriteLine("Supervior user created successfully.");
                }
                else
                {
                    // Handle if user creation failed (e.g., email already taken)
                    Console.WriteLine($"Error creating supervisor user: {string.Join(", ", result.Errors.Select(e => e.Description))}");
                }
            }
            else
            {
                // User already exists, so skip creation
                Console.WriteLine($"Supervisor user already exists with email: {supervisorEmail}");
            }

            // Seed Student user
            var studentEmail = "student@example.com";  // Changed email
            var studentUser = await userManager.FindByEmailAsync(studentEmail);
            if (studentUser == null)
            {
                studentUser = new IdentityUser { UserName = studentEmail, Email = studentEmail };
                var result = await userManager.CreateAsync(studentUser, "Student@1234");  // Changed password
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(studentUser, "Student");
                    Console.WriteLine("Student user created successfully.");
                }
                else
                {
                    // Handle if user creation failed (e.g., email already taken)
                    Console.WriteLine($"Error creating student user: {string.Join(", ", result.Errors.Select(e => e.Description))}");
                }
            }
            else
            {
                // User already exists, so skip creation
                Console.WriteLine($"Student user already exists with email: {studentEmail}");
            }
        }

        private static async Task CreateRoleIfNotExists(RoleManager<IdentityRole> roleManager, string roleName)
        {
            var role = await roleManager.FindByNameAsync(roleName);
            if (role == null)
            {
                await roleManager.CreateAsync(new IdentityRole(roleName));
                Console.WriteLine($"Role {roleName} created successfully.");
            }
            else
            {
                Console.WriteLine($"Role {roleName} already exists.");
            }
        }
    }
}



