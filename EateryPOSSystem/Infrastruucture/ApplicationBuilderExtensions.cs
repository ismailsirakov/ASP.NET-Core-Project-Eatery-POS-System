namespace EateryPOSSystem.Infrastructure
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using EateryPOSSystem.Data;
    using EateryPOSSystem.Data.Models;
    using static Areas.Admin.AdminConstants;

    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder PrepareDatabase(this IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices.CreateScope();

            var services = serviceScope.ServiceProvider;

            MigrateDatabase(services);

            SeedAdministrator(services);

            return app;
        }

        private static void MigrateDatabase(IServiceProvider services)
        {
            var data = services.GetRequiredService<EateryPOSDbContext>();

            data.Database.Migrate();
        }

        private static void SeedAdministrator(IServiceProvider services)
        {
            var userManager = services.GetService<UserManager<User>>();

            var roleManager = services.GetService<RoleManager<IdentityRole>>();

            Task.Run(async () =>
                {
                    if (await roleManager.RoleExistsAsync(AdministratorRoleName))
                    {
                        return;
                    }

                    var role = new IdentityRole { Name = AdministratorRoleName };

                    await roleManager.CreateAsync(role);

                    const string adminEmail = "admin@eaterypos.com";

                    const string adminPassword = "admin12";

                    var user = new User
                    {
                        Email = adminEmail,
                        UserName = adminEmail,
                        FirstName = "Admin",
                        LastName = "admin"
                    };

                    await userManager.CreateAsync(user, adminPassword);

                    await userManager.AddToRoleAsync(user, role.Name);
                })
                .GetAwaiter()
                .GetResult();


        }
    }
}
