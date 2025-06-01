using CarParts.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CarParts.Data
{
    public class UserModel
    {
        public string Username { get; set; }
        public string Role { get; set; }
        public string Email { get; set; }

    }
    public static class SeedData
    {
        private static readonly List<UserModel> SeedUser = new List<UserModel>()
        {
            new UserModel { Username = "Admin", Role = "Admin", Email = "admin@carparts.com" },
            new UserModel { Username = "Staff", Role = "Staff", Email = "staff@carparts.com"}
        };
        public static void EnsurePopulated(IApplicationBuilder app)
        {
            AppDbContext context = app.ApplicationServices
                .CreateScope().ServiceProvider.GetRequiredService<AppDbContext>();

            if (context.Database.GetPendingMigrations().Any())
            {
                context.Database.Migrate();
            }

            if (!context.Categories.Any())
            {
                context.Categories.AddRange
                    (
                        new Category { CategoryName = "Engine" },
                        new Category { CategoryName = "Accesories" },
                        new Category { CategoryName = "Wheels" }
                    );
            }

            context.SaveChanges();

            if (!context.CarParts.Any())
            {
                context.CarParts.AddRange
                    (
                        new CarPart { CategoryID = 1, Code = "TG", Name = "Top Gasket - VW Polo", Price = (decimal)1699.00 },
                        new CarPart { CategoryID = 1, Code = "PS", Name = "Piston - Toyota Corrola", Price = (decimal)1099.00 },
                        new CarPart { CategoryID = 1, Code = "P1", Name = "VW Polo - Water Pipe", Price = (decimal)2517.00 },
                        new CarPart { CategoryID = 1, Code = "P2", Name = "Toyota Corrola - Water Pipe", Price = (decimal)489.99 },
                        new CarPart { CategoryID = 1, Code = "P3", Name = "Kia Sportage - Water Pipe", Price = (decimal)2299.00 },
                        new CarPart { CategoryID = 1, Code = "WB", Name = "VW Polo - Washer Bottle", Price = (decimal)415.00 },
                        new CarPart { CategoryID = 2, Code = "T1", Name = "Window Tint", Price = (decimal)799.99 },
                        new CarPart { CategoryID = 2, Code = "M2", Name = "Rear View Mirror - Generic", Price = (decimal)499.99 },
                        new CarPart { CategoryID = 3, Code = "Tyre1", Name = "VW Polo Tyre", Price = (decimal)1699.99 },
                        new CarPart { CategoryID = 3, Code = "Tyre2", Name = "Kie Sportage Tyre", Price = (decimal)2799.99 }
                    );
            }

            context.SaveChanges();

            if (!context.Orders.Any())
            {
                context.Orders.AddRange
                    (
                        new Order { CustomerName="Thabo", CarPartName = "Top Gasket - VW Polo", OrderDate = DateTime.Parse("2025-03-26"), Status = "Available", CategoryID = 1},
                        new Order { CustomerName = "Gert", CarPartName = "Piston - Toyota Corrola", OrderDate = DateTime.Parse("2025-02-01"), Status = "Pending", CategoryID = 1 },
                        new Order { CustomerName = "Marriam", CarPartName = "VW Polo Tyre", OrderDate = DateTime.Parse("2025-04-03"), Status = "Pending", CategoryID = 3 },
                        new Order { CustomerName = "John", CarPartName = "Window Tint", OrderDate = DateTime.Parse("2025-03-29"), Status = "Pending", CategoryID = 2 },
                        new Order { CustomerName = "Ben", CarPartName = " VW Polo - Washer Bottle", OrderDate = DateTime.Parse("2025-04-05"), Status = "Pending", CategoryID = 1 },
                        new Order { CustomerName = "Daniel", CarPartName = "Toyota Radiator", OrderDate = DateTime.Parse("2025-03-15"), Status = "Pending", CategoryID = 2 }
                    );
            }
            context.SaveChanges();
        }

        public static async void EnsureIdentityPopulated(IApplicationBuilder app)
        {
            AppDbContext context = app.ApplicationServices
                .CreateScope().ServiceProvider
                .GetRequiredService<AppDbContext>();

            if (context.Database.GetPendingMigrations().Any())
            {
                context.Database.Migrate();
            }

            UserManager<IdentityUser> userManager = app.ApplicationServices
                .CreateScope().ServiceProvider
                .GetRequiredService<UserManager<IdentityUser>>();

            RoleManager<IdentityRole> roleManager = app.ApplicationServices
                .CreateScope().ServiceProvider
                .GetRequiredService<RoleManager<IdentityRole>>();

            foreach (var seedUser in SeedUser)
            {
               if(await userManager.FindByNameAsync(seedUser.Username) == null)
                {
                    if(await roleManager.FindByNameAsync(seedUser.Role) == null)
                    {
                        await roleManager.CreateAsync(new IdentityRole(seedUser.Role));
                    }
                    var user = new IdentityUser
                    {
                        UserName = seedUser.Username,
                        Email = seedUser.Email
                    };
                    IdentityResult result = await userManager.CreateAsync(user,"Part@123");
                    if (result.Succeeded)
                    {
                        await userManager.AddToRoleAsync(user,seedUser.Role);
                    }
                }
            }

        }
    }
}
