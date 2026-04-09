using ClinicBooking.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ClinicBooking.Infrastructure.Persistence;

public static class DataSeeder
{
    public static async Task SeedAsync(
        ApplicationDbContext context,
        UserManager<IdentityUser> userManager,
        RoleManager<IdentityRole> roleManager)
    {
        // Create roles
        string[] roles = { "Admin", "Doctor", "Patient" };
        foreach (var role in roles)
        {
            if (!await roleManager.RoleExistsAsync(role))
                await roleManager.CreateAsync(new IdentityRole(role));
        }

        // Create admin user
        if (!await userManager.Users.AnyAsync())
        {
            var admin = new IdentityUser
            {
                UserName = "admin@clinic.com",
                Email = "admin@clinic.com",
                EmailConfirmed = true
            };
            await userManager.CreateAsync(admin, "Admin@123456");
            await userManager.AddToRoleAsync(admin, "Admin");
        }

        // Seed specialties
        if (!await context.Specialties.AnyAsync())
        {
            context.Specialties.AddRange(
                new Specialty { Name = "Cardiology", Description = "Heart & blood vessels" },
                new Specialty { Name = "Dermatology", Description = "Skin conditions" },
                new Specialty { Name = "General Practice", Description = "Primary care" },
                new Specialty { Name = "Pediatrics", Description = "Children's health" }
            );
            await context.SaveChangesAsync();
        }
    }
}